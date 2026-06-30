using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Xml;

namespace StudentRegistrationSystem
{
    public static class DatabaseHelper
    {
        private static string _connectionString;

        /// <summary>Из App.config / *.exe.config: connectionStrings / add[@name="StudentDb"].</summary>
        private static string ReadConnectionStringFromConfigFile()
        {
            try
            {
                string path = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
                if (string.IsNullOrEmpty(path) || !File.Exists(path))
                    return null;

                var doc = new XmlDocument();
                doc.Load(path);
                var node = doc.SelectSingleNode("/configuration/connectionStrings/add[@name='StudentDb']");
                var attr = node?.Attributes?["connectionString"];
                if (attr != null && !string.IsNullOrWhiteSpace(attr.Value))
                    return attr.Value.Trim();
            }
            catch
            {
            }

            return null;
        }

        public static string ConnectionString
        {
            get
            {
                if (_connectionString != null)
                    return _connectionString;

                _connectionString = ReadConnectionStringFromConfigFile();
                if (string.IsNullOrWhiteSpace(_connectionString))
                {
                    throw new InvalidOperationException(
                        "Строка подключения StudentDb не найдена в App.config. " +
                        "Добавьте элемент connectionStrings / add[@name=\"StudentDb\"].");
                }

                return _connectionString;
            }
        }

        public static void EnsureConnectionAvailable()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
                connection.Open();
        }

        public static string FormatUserMessage(Exception ex)
        {
            if (ex == null)
                return string.Empty;

            if (ex is SqlException sqlEx)
                return "Ошибка базы данных: " + sqlEx.Message;

            if (ex is InvalidOperationException && ex.Message.IndexOf("StudentDb", StringComparison.OrdinalIgnoreCase) >= 0)
                return ex.Message;

            return ex.Message;
        }

        public static DataTable ExecuteQuery(string query)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        public static DataTable ExecuteQuery(string query, params SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = CreateCommand(connection, null, query, parameters))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        public static object ExecuteScalar(string query)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    return command.ExecuteScalar();
                }
            }
        }

        public static object ExecuteScalar(string query, params SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = CreateCommand(connection, null, query, parameters))
                {
                    return command.ExecuteScalar();
                }
            }
        }

        public static void ExecuteNonQuery(string query)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
            }
        }

        public static int ExecuteNonQuery(string query, params SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = CreateCommand(connection, null, query, parameters))
                {
                    return command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>Выполняет INSERT и возвращает SCOPE_IDENTITY() в той же сессии (для INT IDENTITY).</summary>
        public static int ExecuteInsertAndGetIntIdentity(string insertSql)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var cmd = new SqlCommand(insertSql, connection))
                    cmd.ExecuteNonQuery();
                using (var cmd = new SqlCommand("SELECT CAST(SCOPE_IDENTITY() AS INT)", connection))
                    return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public static int ExecuteInsertAndGetIntIdentity(string insertSql, params SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var cmd = CreateCommand(connection, null, insertSql + Environment.NewLine + "SELECT CAST(SCOPE_IDENTITY() AS INT);", parameters))
                    return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public static void ExecuteInTransaction(Action<SqlConnection, SqlTransaction> action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        action(connection, transaction);
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public static DataTable ExecuteQuery(SqlConnection connection, SqlTransaction transaction, string query, params SqlParameter[] parameters)
        {
            using (SqlCommand command = CreateCommand(connection, transaction, query, parameters))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        public static object ExecuteScalar(SqlConnection connection, SqlTransaction transaction, string query, params SqlParameter[] parameters)
        {
            using (SqlCommand command = CreateCommand(connection, transaction, query, parameters))
            {
                return command.ExecuteScalar();
            }
        }

        public static int ExecuteNonQuery(SqlConnection connection, SqlTransaction transaction, string query, params SqlParameter[] parameters)
        {
            using (SqlCommand command = CreateCommand(connection, transaction, query, parameters))
            {
                return command.ExecuteNonQuery();
            }
        }

        public static int ExecuteInsertAndGetIntIdentity(SqlConnection connection, SqlTransaction transaction, string insertSql, params SqlParameter[] parameters)
        {
            using (SqlCommand command = CreateCommand(connection, transaction, insertSql + Environment.NewLine + "SELECT CAST(SCOPE_IDENTITY() AS INT);", parameters))
            {
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        private static SqlCommand CreateCommand(SqlConnection connection, SqlTransaction transaction, string query, params SqlParameter[] parameters)
        {
            SqlCommand command = transaction == null
                ? new SqlCommand(query, connection)
                : new SqlCommand(query, connection, transaction);

            if (parameters != null && parameters.Length > 0)
                command.Parameters.AddRange(parameters);

            return command;
        }
    }
}
