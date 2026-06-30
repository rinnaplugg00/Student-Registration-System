using System;
using System.Data;
using System.Data.SqlClient;

namespace StudentRegistrationSystem.Services
{
    public sealed class SubjectService
    {
        public void Add(string subjectName)
        {
            DatabaseHelper.ExecuteNonQuery(
                "INSERT INTO dbo.Subjects (SubjectName) VALUES (@SubjectName)",
                CreateSubjectParameter(subjectName));
        }

        public int CountGrades(string subjectName)
        {
            return Convert.ToInt32(DatabaseHelper.ExecuteScalar(
                "SELECT COUNT(1) FROM dbo.Grades WHERE Subject = @SubjectName",
                CreateSubjectParameter(subjectName)));
        }

        public bool HasCatalogTable()
        {
            object tableId = DatabaseHelper.ExecuteScalar("SELECT OBJECT_ID(N'dbo.Subjects', N'U')");
            return tableId != null && tableId != DBNull.Value && Convert.ToInt32(tableId) != 0;
        }

        public bool ExistsInCatalog(string subjectName)
        {
            return Convert.ToInt32(DatabaseHelper.ExecuteScalar(
                "SELECT COUNT(1) FROM dbo.Subjects WHERE SubjectName = @SubjectName",
                CreateSubjectParameter(subjectName))) > 0;
        }

        public void DeleteFromCatalog(string subjectName)
        {
            DatabaseHelper.ExecuteNonQuery(
                "DELETE FROM dbo.Subjects WHERE SubjectName = @SubjectName",
                CreateSubjectParameter(subjectName));
        }

        private static SqlParameter CreateSubjectParameter(string subjectName)
        {
            return new SqlParameter("@SubjectName", SqlDbType.NVarChar, 100)
            {
                Value = (subjectName ?? string.Empty).Trim()
            };
        }
    }
}

