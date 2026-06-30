using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;

namespace StudentRegistrationSystem.Services
{
    /// <summary>
    /// Журнал: Grades.Subject + GradeDate + SemesterId.
    /// Справочник dbo.Subjects создаётся скриптом и не обязателен для чтения списка предметов.
    /// </summary>
    public static class SubjectGradeRepository
    {
        public static bool HasExtendedGradesSchema()
        {
            try
            {
                SemesterService.EnsureSchema();
                DatabaseHelper.ExecuteScalar("SELECT TOP 1 GradeDate FROM dbo.Grades");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool TryDetectExtendedSchema(out string error)
        {
            error = null;
            try
            {
                SemesterService.EnsureSchema();
                DatabaseHelper.ExecuteScalar("SELECT TOP 1 GradeDate FROM dbo.Grades");
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public static List<string> GetAllSubjectNames()
        {
            var set = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            if (TableExists("Subjects"))
            {
                try
                {
                    foreach (DataRow r in DatabaseHelper.ExecuteQuery(
                                 "SELECT SubjectName FROM dbo.Subjects ORDER BY SubjectName").Rows)
                    {
                        string n = r["SubjectName"]?.ToString()?.Trim();
                        if (!string.IsNullOrEmpty(n))
                            set.Add(n);
                    }
                }
                catch
                {
                }
            }

            try
            {
                foreach (DataRow r in DatabaseHelper.ExecuteQuery(
                             @"SELECT DISTINCT Subject FROM dbo.Grades
                               WHERE Subject IS NOT NULL AND LTRIM(RTRIM(Subject)) <> ''").Rows)
                {
                    string n = r["Subject"]?.ToString()?.Trim();
                    if (!string.IsNullOrEmpty(n))
                        set.Add(n);
                }
            }
            catch
            {
            }

            return set.OrderBy(s => s).ToList();
        }

        public static Dictionary<int, List<int>> GetGradesForMonth(int studentId, string subjectName, int year, int month, int? semesterId)
        {
            var map = new Dictionary<int, List<int>>();
            int days = DateTime.DaysInMonth(year, month);
            for (int d = 1; d <= days; d++)
                map[d] = new List<int>();

            DateTime start = new DateTime(year, month, 1);
            DateTime end = new DateTime(year, month, days);
            string query = @"
SELECT DAY(GradeDate) AS Dd, Grade
FROM dbo.Grades
WHERE StudentId = @StudentId
  AND Subject = @Subject
  AND GradeDate IS NOT NULL
  AND GradeDate >= @StartDate
  AND GradeDate <= @EndDate
  AND (@SemesterId IS NULL OR SemesterId = @SemesterId)";

            DataTable table = DatabaseHelper.ExecuteQuery(
                query,
                new SqlParameter("@StudentId", SqlDbType.Int) { Value = studentId },
                new SqlParameter("@Subject", SqlDbType.NVarChar, 100) { Value = (subjectName ?? string.Empty).Trim() },
                new SqlParameter("@StartDate", SqlDbType.Date) { Value = start.Date },
                new SqlParameter("@EndDate", SqlDbType.Date) { Value = end.Date },
                new SqlParameter("@SemesterId", SqlDbType.Int)
                {
                    Value = semesterId.HasValue ? (object)semesterId.Value : DBNull.Value
                });

            foreach (DataRow row in table.Rows)
            {
                int day = Convert.ToInt32(row["Dd"]);
                if (day >= 1 && day <= days)
                    map[day].Add(Convert.ToInt32(row["Grade"]));
            }

            return map;
        }

        public static void SetGradesForDay(int studentId, string subjectName, int year, int month, int day, IEnumerable<int> grades, int? semesterId)
        {
            string subject = (subjectName ?? string.Empty).Trim();
            DateTime date = new DateTime(year, month, day).Date;

            DatabaseHelper.ExecuteInTransaction((connection, transaction) =>
            {
                DatabaseHelper.ExecuteNonQuery(
                    connection,
                    transaction,
                    @"DELETE FROM dbo.Grades
                      WHERE StudentId = @StudentId
                        AND Subject = @Subject
                        AND GradeDate = @GradeDate
                        AND (@SemesterId IS NULL OR SemesterId = @SemesterId)",
                    new SqlParameter("@StudentId", SqlDbType.Int) { Value = studentId },
                    new SqlParameter("@Subject", SqlDbType.NVarChar, 100) { Value = subject },
                    new SqlParameter("@GradeDate", SqlDbType.Date) { Value = date },
                    new SqlParameter("@SemesterId", SqlDbType.Int)
                    {
                        Value = semesterId.HasValue ? (object)semesterId.Value : DBNull.Value
                    });

                foreach (int grade in grades ?? Enumerable.Empty<int>())
                {
                    DatabaseHelper.ExecuteNonQuery(
                        connection,
                        transaction,
                        @"INSERT INTO dbo.Grades (StudentId, Subject, Grade, GradeDate, SemesterId)
                          VALUES (@StudentId, @Subject, @Grade, @GradeDate, @SemesterId)",
                        new SqlParameter("@StudentId", SqlDbType.Int) { Value = studentId },
                        new SqlParameter("@Subject", SqlDbType.NVarChar, 100) { Value = subject },
                        new SqlParameter("@Grade", SqlDbType.Int) { Value = grade },
                        new SqlParameter("@GradeDate", SqlDbType.Date) { Value = date },
                        new SqlParameter("@SemesterId", SqlDbType.Int)
                        {
                            Value = semesterId.HasValue ? (object)semesterId.Value : DBNull.Value
                        });
                }
            });

            SyncUndatedSubjectGrade(studentId, subject, semesterId);
        }

        public static void SyncUndatedSubjectGrade(int studentId, string subjectName, int? semesterId)
        {
            string subject = (subjectName ?? string.Empty).Trim();
            if (string.IsNullOrEmpty(subject))
                return;

            DatabaseHelper.ExecuteNonQuery(
                @"DELETE FROM dbo.Grades
                  WHERE StudentId = @StudentId
                    AND Subject = @Subject
                    AND GradeDate IS NULL
                    AND (@SemesterId IS NULL OR SemesterId = @SemesterId)",
                new SqlParameter("@StudentId", SqlDbType.Int) { Value = studentId },
                new SqlParameter("@Subject", SqlDbType.NVarChar, 100) { Value = subject },
                new SqlParameter("@SemesterId", SqlDbType.Int)
                {
                    Value = semesterId.HasValue ? (object)semesterId.Value : DBNull.Value
                });

            object avgObj = DatabaseHelper.ExecuteScalar(
                @"SELECT AVG(CAST(Grade AS FLOAT))
                  FROM dbo.Grades
                  WHERE StudentId = @StudentId
                    AND Subject = @Subject
                    AND GradeDate IS NOT NULL
                    AND (@SemesterId IS NULL OR SemesterId = @SemesterId)",
                new SqlParameter("@StudentId", SqlDbType.Int) { Value = studentId },
                new SqlParameter("@Subject", SqlDbType.NVarChar, 100) { Value = subject },
                new SqlParameter("@SemesterId", SqlDbType.Int)
                {
                    Value = semesterId.HasValue ? (object)semesterId.Value : DBNull.Value
                });

            if (avgObj == null || avgObj == DBNull.Value)
                return;

            int roundedGrade = (int)Math.Round(
                Convert.ToDouble(avgObj, CultureInfo.InvariantCulture),
                MidpointRounding.AwayFromZero);

            DatabaseHelper.ExecuteNonQuery(
                @"INSERT INTO dbo.Grades (StudentId, Subject, Grade, SemesterId)
                  VALUES (@StudentId, @Subject, @Grade, @SemesterId)",
                new SqlParameter("@StudentId", SqlDbType.Int) { Value = studentId },
                new SqlParameter("@Subject", SqlDbType.NVarChar, 100) { Value = subject },
                new SqlParameter("@Grade", SqlDbType.Int) { Value = roundedGrade },
                new SqlParameter("@SemesterId", SqlDbType.Int)
                {
                    Value = semesterId.HasValue ? (object)semesterId.Value : DBNull.Value
                });
        }

        private static bool TableExists(string tableName)
        {
            try
            {
                object o = DatabaseHelper.ExecuteScalar(
                    $"SELECT OBJECT_ID(N'dbo.{tableName}', N'U')");
                return o != null && o != DBNull.Value && Convert.ToInt32(o) != 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
