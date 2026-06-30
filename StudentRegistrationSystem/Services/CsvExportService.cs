using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;

namespace StudentRegistrationSystem.Services
{
    internal static class CsvExportService
    {
        private static readonly CultureInfo RussianCulture = CultureInfo.GetCultureInfo("ru-RU");

        public static void ExportDataTable(DataTable table, string filePath)
        {
            if (table == null)
                throw new ArgumentNullException(nameof(table));

            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("Не указан путь к файлу.", nameof(filePath));

            using (var writer = new StreamWriter(filePath, false, new UTF8Encoding(true)))
            {
                WriteRow(writer, GetHeaderValues(table));

                foreach (DataRow row in table.Rows)
                    WriteRow(writer, GetRowValues(table, row));
            }
        }

        private static string[] GetHeaderValues(DataTable table)
        {
            string[] values = new string[table.Columns.Count];
            for (int i = 0; i < table.Columns.Count; i++)
                values[i] = table.Columns[i].ColumnName;

            return values;
        }

        private static string[] GetRowValues(DataTable table, DataRow row)
        {
            string[] values = new string[table.Columns.Count];
            for (int i = 0; i < table.Columns.Count; i++)
                values[i] = FormatValue(row[i]);

            return values;
        }

        private static void WriteRow(StreamWriter writer, string[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                if (i > 0)
                    writer.Write(';');

                writer.Write(Escape(values[i] ?? string.Empty));
            }

            writer.WriteLine();
        }

        private static string FormatValue(object value)
        {
            if (value == null || value == DBNull.Value)
                return string.Empty;

            if (value is DateTime dateTime)
                return dateTime.ToString("dd.MM.yyyy", RussianCulture);

            if (value is IFormattable formattable)
                return formattable.ToString(null, RussianCulture);

            return Convert.ToString(value, RussianCulture) ?? string.Empty;
        }

        private static string Escape(string value)
        {
            string escaped = value.Replace("\"", "\"\"");
            return "\"" + escaped + "\"";
        }
    }
}
