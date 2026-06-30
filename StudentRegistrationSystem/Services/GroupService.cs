using System;
using System.Data;
using System.Data.SqlClient;

namespace StudentRegistrationSystem.Services
{
    public sealed class GroupService
    {
        public void EnsureSchema()
        {
            const string sql = @"
IF COL_LENGTH('Groups', 'Specialty') IS NULL
BEGIN
    ALTER TABLE Groups ADD Specialty NVARCHAR(200) NULL
END";

            DatabaseHelper.ExecuteNonQuery(sql);
        }

        public DataTable GetAll()
        {
            return DatabaseHelper.ExecuteQuery(@"
SELECT Id,
       GroupName,
       ISNULL(Specialty, '') AS Specialty
FROM Groups
ORDER BY
    CASE
        WHEN CHARINDEX('-', GroupName) > 0
             AND ISNUMERIC(SUBSTRING(GroupName, CHARINDEX('-', GroupName) + 1, 50)) = 1
            THEN CONVERT(INT, SUBSTRING(GroupName, CHARINDEX('-', GroupName) + 1, 50))
        WHEN ISNUMERIC(GroupName) = 1
            THEN CONVERT(INT, GroupName)
        ELSE 2147483647
    END,
    GroupName");
        }

        public DataTable GetDetailsById(int groupId)
        {
            const string sql = @"
SELECT g.GroupName,
       ISNULL(g.Specialty, '') AS Specialty,
       COUNT(s.Id) AS StudentCount
FROM Groups g
LEFT JOIN Students s ON s.GroupId = g.Id
WHERE g.Id = @GroupId
GROUP BY g.GroupName, g.Specialty";

            return DatabaseHelper.ExecuteQuery(
                sql,
                new SqlParameter("@GroupId", SqlDbType.Int) { Value = groupId });
        }

        public DataTable GetDetailsByName(string groupName)
        {
            const string sql = @"
SELECT g.Id,
       g.GroupName,
       ISNULL(g.Specialty, '') AS Specialty,
       COUNT(s.Id) AS StudentCount
FROM Groups g
LEFT JOIN Students s ON s.GroupId = g.Id
WHERE g.GroupName = @GroupName
GROUP BY g.Id, g.GroupName, g.Specialty";

            return DatabaseHelper.ExecuteQuery(
                sql,
                new SqlParameter("@GroupName", SqlDbType.NVarChar, 50) { Value = groupName ?? string.Empty });
        }

        public DataTable GetStudents(int groupId, bool includeId)
        {
            string selectColumns = includeId ? "Id, FullName" : "FullName";
            string sql = $@"
SELECT {selectColumns}
FROM Students
WHERE GroupId = @GroupId
ORDER BY FullName";

            return DatabaseHelper.ExecuteQuery(
                sql,
                new SqlParameter("@GroupId", SqlDbType.Int) { Value = groupId });
        }

        public bool Exists(string groupName)
        {
            const string sql = "SELECT COUNT(1) FROM Groups WHERE GroupName = @GroupName";
            return Convert.ToInt32(DatabaseHelper.ExecuteScalar(
                sql,
                new SqlParameter("@GroupName", SqlDbType.NVarChar, 50) { Value = groupName ?? string.Empty })) > 0;
        }

        public void Add(string groupName, string specialty)
        {
            const string sql = @"
INSERT INTO Groups (GroupName, Specialty)
VALUES (@GroupName, @Specialty)";

            DatabaseHelper.ExecuteNonQuery(
                sql,
                new SqlParameter("@GroupName", SqlDbType.NVarChar, 50) { Value = groupName ?? string.Empty },
                CreateSpecialtyParameter(specialty));
        }

        public void UpdateSpecialty(int groupId, string specialty)
        {
            const string sql = @"
UPDATE Groups
SET Specialty = @Specialty
WHERE Id = @GroupId";

            DatabaseHelper.ExecuteNonQuery(
                sql,
                CreateSpecialtyParameter(specialty),
                new SqlParameter("@GroupId", SqlDbType.Int) { Value = groupId });
        }

        public int CountStudents(int groupId)
        {
            const string sql = "SELECT COUNT(1) FROM Students WHERE GroupId = @GroupId";
            return Convert.ToInt32(DatabaseHelper.ExecuteScalar(
                sql,
                new SqlParameter("@GroupId", SqlDbType.Int) { Value = groupId }));
        }

        public void Delete(int groupId)
        {
            DatabaseHelper.ExecuteNonQuery(
                "DELETE FROM Groups WHERE Id = @GroupId",
                new SqlParameter("@GroupId", SqlDbType.Int) { Value = groupId });
        }

        private static SqlParameter CreateSpecialtyParameter(string specialty)
        {
            return new SqlParameter("@Specialty", SqlDbType.NVarChar, 200)
            {
                Value = string.IsNullOrWhiteSpace(specialty) ? (object)DBNull.Value : specialty.Trim()
            };
        }
    }
}

