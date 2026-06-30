using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace StudentRegistrationSystem.Services
{
    public sealed class SemesterInfo
    {
        public const int AllPeriodsId = 0;

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsCurrent { get; set; }

        public bool IsAllPeriods => Id == AllPeriodsId;

        public static SemesterInfo CreateAllPeriods(string displayName)
        {
            return new SemesterInfo
            {
                Id = AllPeriodsId,
                Name = displayName ?? string.Empty
            };
        }

        public override string ToString()
        {
            return Name ?? string.Empty;
        }
    }

    public static class SemesterService
    {
        public static void EnsureSchema()
        {
            const string sql = @"
IF OBJECT_ID(N'dbo.Semesters', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Semesters (
        Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        Name NVARCHAR(120) NOT NULL,
        StartDate DATE NOT NULL,
        EndDate DATE NOT NULL
    );
END;

IF COL_LENGTH(N'dbo.Grades', N'SemesterId') IS NULL
BEGIN
    ALTER TABLE dbo.Grades ADD SemesterId INT NULL;
END;

IF NOT EXISTS (
    SELECT 1
    FROM sys.foreign_keys
    WHERE name = N'FK_Grades_Semesters'
)
BEGIN
    ALTER TABLE dbo.Grades
    ADD CONSTRAINT FK_Grades_Semesters
        FOREIGN KEY (SemesterId) REFERENCES dbo.Semesters (Id);
END;";

            DatabaseHelper.ExecuteNonQuery(sql);
        }

        public static List<SemesterInfo> GetAllSemesters()
        {
            EnsureSchema();
            GetDefaultSemesterId();

            DateTime today = DateTime.Today;
            DataTable table = DatabaseHelper.ExecuteQuery(@"
SELECT Id, Name, StartDate, EndDate
FROM dbo.Semesters
ORDER BY StartDate DESC, Id DESC");

            return table.Rows.Cast<DataRow>()
                .Select(row => new SemesterInfo
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Name = Convert.ToString(row["Name"]),
                    StartDate = Convert.ToDateTime(row["StartDate"]).Date,
                    EndDate = Convert.ToDateTime(row["EndDate"]).Date,
                    IsCurrent = today >= Convert.ToDateTime(row["StartDate"]).Date
                        && today <= Convert.ToDateTime(row["EndDate"]).Date
                })
                .ToList();
        }

        public static int GetDefaultSemesterId()
        {
            EnsureSchema();

            SemesterInfo current = BuildSemesterForDate(DateTime.Today);
            object existingId = DatabaseHelper.ExecuteScalar(
                @"SELECT Id
                  FROM dbo.Semesters
                  WHERE StartDate = @StartDate
                    AND EndDate = @EndDate",
                new SqlParameter("@StartDate", SqlDbType.Date) { Value = current.StartDate },
                new SqlParameter("@EndDate", SqlDbType.Date) { Value = current.EndDate });

            int semesterId;

            if (existingId != null && existingId != DBNull.Value)
            {
                semesterId = Convert.ToInt32(existingId);
            }
            else
            {
                semesterId = DatabaseHelper.ExecuteInsertAndGetIntIdentity(
                    @"INSERT INTO dbo.Semesters (Name, StartDate, EndDate)
                      VALUES (@Name, @StartDate, @EndDate)",
                    new SqlParameter("@Name", SqlDbType.NVarChar, 120) { Value = current.Name },
                    new SqlParameter("@StartDate", SqlDbType.Date) { Value = current.StartDate },
                    new SqlParameter("@EndDate", SqlDbType.Date) { Value = current.EndDate });
            }

            AssignLegacyGradesToSemester(semesterId);
            return semesterId;
        }

        public static SemesterInfo GetSemesterById(int semesterId)
        {
            EnsureSchema();

            DataTable table = DatabaseHelper.ExecuteQuery(
                @"SELECT Id, Name, StartDate, EndDate
                  FROM dbo.Semesters
                  WHERE Id = @SemesterId",
                new SqlParameter("@SemesterId", SqlDbType.Int) { Value = semesterId });

            if (table.Rows.Count == 0)
                return null;

            DataRow row = table.Rows[0];
            return new SemesterInfo
            {
                Id = Convert.ToInt32(row["Id"]),
                Name = Convert.ToString(row["Name"]),
                StartDate = Convert.ToDateTime(row["StartDate"]).Date,
                EndDate = Convert.ToDateTime(row["EndDate"]).Date,
                IsCurrent = DateTime.Today >= Convert.ToDateTime(row["StartDate"]).Date
                    && DateTime.Today <= Convert.ToDateTime(row["EndDate"]).Date
            };
        }

        private static SemesterInfo BuildSemesterForDate(DateTime date)
        {
            int year = date.Year;

            if (date.Month >= 9)
            {
                return new SemesterInfo
                {
                    Name = $"Осенний {year}/{year + 1}",
                    StartDate = new DateTime(year, 9, 1),
                    EndDate = new DateTime(year + 1, 1, 31),
                    IsCurrent = true
                };
            }

            int startYear = year - 1;
            return new SemesterInfo
            {
                Name = $"Весенний {startYear}/{year}",
                StartDate = new DateTime(year, 2, 1),
                EndDate = new DateTime(year, 6, 30),
                IsCurrent = true
            };
        }

        private static void AssignLegacyGradesToSemester(int semesterId)
        {
            DatabaseHelper.ExecuteNonQuery(
                @"UPDATE dbo.Grades
                  SET SemesterId = @SemesterId
                  WHERE SemesterId IS NULL",
                new SqlParameter("@SemesterId", SqlDbType.Int) { Value = semesterId });
        }
    }
}
