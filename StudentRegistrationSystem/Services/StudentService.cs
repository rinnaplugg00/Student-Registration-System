using StudentRegistrationSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace StudentRegistrationSystem.Services
{
    public class StudentService
    {
        public const string LegacyGeneralSubject = "Общее";
        private const string LegacyGeneralSubjectAlt = "Зачётная (общая)";
        private const string StudentIdSqlExpression = "s.Id";

        private enum StudentsFilterKind
        {
            All,
            Name,
            MinAverage,
            NameOrGroup
        }

        private static string BuildCalculatedAverageSql(string studentIdExpression, bool includeSemesterFilter)
        {
            string semesterFilter = includeSemesterFilter
                ? " AND (@SemesterId IS NULL OR {0}.SemesterId = @SemesterId)"
                : string.Empty;

            return $@"
COALESCE(
    (
        SELECT AVG(CAST(grSummary.Grade AS FLOAT))
        FROM Grades grSummary
        WHERE grSummary.StudentId = {studentIdExpression}
          AND grSummary.GradeDate IS NULL
          AND grSummary.Subject IS NOT NULL
          AND LTRIM(RTRIM(grSummary.Subject)) <> ''
          AND grSummary.Subject NOT IN (@LegacyGeneralSubject, @LegacyGeneralSubjectAlt)
          {string.Format(semesterFilter, "grSummary")}
    ),
    (
        SELECT AVG(CAST(subjectAverages.SubjectAvg AS FLOAT))
        FROM
        (
            SELECT AVG(CAST(grSubject.Grade AS FLOAT)) AS SubjectAvg
            FROM Grades grSubject
            WHERE grSubject.StudentId = {studentIdExpression}
              AND grSubject.GradeDate IS NOT NULL
              AND grSubject.Subject IS NOT NULL
              AND LTRIM(RTRIM(grSubject.Subject)) <> ''
              AND grSubject.Subject NOT IN (@LegacyGeneralSubject, @LegacyGeneralSubjectAlt)
              {string.Format(semesterFilter, "grSubject")}
            GROUP BY grSubject.Subject
        ) subjectAverages
    ),
    (
        SELECT AVG(CAST(grLegacy.Grade AS FLOAT))
        FROM Grades grLegacy
        WHERE grLegacy.StudentId = {studentIdExpression}
          AND (
                grLegacy.Subject IS NULL
                OR LTRIM(RTRIM(grLegacy.Subject)) = ''
                OR grLegacy.Subject IN (@LegacyGeneralSubject, @LegacyGeneralSubjectAlt)
              )
          {string.Format(semesterFilter, "grLegacy")}
    ),
    0
)";
        }

        private static string BuildGradeCountSql(string studentIdExpression, bool includeSemesterFilter)
        {
            string semesterFilter = includeSemesterFilter
                ? " AND (@SemesterId IS NULL OR grCount.SemesterId = @SemesterId)"
                : string.Empty;

            return $@"
(
    SELECT COUNT(1)
    FROM Grades grCount
    WHERE grCount.StudentId = {studentIdExpression}
      {semesterFilter}
)";
        }

        private static string BuildStudentsQuery(StudentsFilterKind filterKind, bool includeSemesterFilter)
        {
            string averageSql = BuildCalculatedAverageSql(StudentIdSqlExpression, includeSemesterFilter);
            string gradeCountSql = BuildGradeCountSql(StudentIdSqlExpression, includeSemesterFilter);
            string whereClause = BuildWhereClause(filterKind);
            string minAverageSql = filterKind == StudentsFilterKind.MinAverage || filterKind == StudentsFilterKind.NameOrGroup
                ? "@MinAverage"
                : "0";

            return $@"
SELECT
    s.Id,
    s.GroupId,
    s.FullName,
    ISNULL(g.GroupName, N'') AS GroupName,
    CAST(({averageSql}) AS DECIMAL(10,2)) AS AverageGrade,
    {gradeCountSql} AS GradeCount
FROM Students s
LEFT JOIN Groups g ON s.GroupId = g.Id
WHERE ({whereClause})
  AND ({averageSql}) >= {minAverageSql}
ORDER BY
    CASE
        WHEN CHARINDEX('-', g.GroupName) > 0
             AND ISNUMERIC(SUBSTRING(g.GroupName, CHARINDEX('-', g.GroupName) + 1, 50)) = 1
            THEN CONVERT(INT, SUBSTRING(g.GroupName, CHARINDEX('-', g.GroupName) + 1, 50))
        WHEN ISNUMERIC(g.GroupName) = 1
            THEN CONVERT(INT, g.GroupName)
        ELSE 2147483647
    END,
    g.GroupName,
    s.FullName";
        }

        private static string BuildWhereClause(StudentsFilterKind filterKind)
        {
            switch (filterKind)
            {
                case StudentsFilterKind.Name:
                    return "s.FullName LIKE @SearchPattern";
                case StudentsFilterKind.NameOrGroup:
                    return @"s.FullName LIKE @SearchPattern
                                OR ISNULL(g.GroupName, N'') LIKE @SearchPattern";
                case StudentsFilterKind.All:
                case StudentsFilterKind.MinAverage:
                    return "1 = 1";
                default:
                    throw new ArgumentOutOfRangeException(nameof(filterKind), filterKind, "Unknown students filter.");
            }
        }

        public DataTable GetAll()
        {
            return GetAll(null);
        }

        public DataTable GetAll(int? semesterId)
        {
            return DatabaseHelper.ExecuteQuery(
                BuildStudentsQuery(StudentsFilterKind.All, true),
                CreateSemesterParameter(semesterId),
                CreateLegacySubjectParameter(),
                CreateLegacySubjectAltParameter());
        }

        public DataTable SearchByName(string name)
        {
            return SearchByName(name, null);
        }

        public DataTable SearchByName(string name, int? semesterId)
        {
            return DatabaseHelper.ExecuteQuery(
                BuildStudentsQuery(StudentsFilterKind.Name, true),
                CreateSearchParameter(name),
                CreateSemesterParameter(semesterId),
                CreateLegacySubjectParameter(),
                CreateLegacySubjectAltParameter());
        }

        public DataTable FilterByAverage(double minAverage)
        {
            return FilterByAverage(minAverage, null);
        }

        public DataTable FilterByAverage(double minAverage, int? semesterId)
        {
            return DatabaseHelper.ExecuteQuery(
                BuildStudentsQuery(StudentsFilterKind.MinAverage, true),
                CreateMinAverageParameter(minAverage),
                CreateSemesterParameter(semesterId),
                CreateLegacySubjectParameter(),
                CreateLegacySubjectAltParameter());
        }

        public Student LoadForEdit(int id)
        {
            return LoadForEdit(id, SemesterService.GetDefaultSemesterId());
        }

        public Student LoadForEdit(int id, int? semesterId)
        {
            DataTable table = DatabaseHelper.ExecuteQuery(
                "SELECT Id, FullName, GroupId, BirthDate FROM Students WHERE Id = @Id",
                new SqlParameter("@Id", SqlDbType.Int) { Value = id });
            if (table.Rows.Count == 0)
                return null;

            DataRow row = table.Rows[0];
            var student = new Student
            {
                Id = Convert.ToInt32(row["Id"]),
                FullName = row["FullName"]?.ToString() ?? string.Empty,
                GroupId = row["GroupId"] == DBNull.Value ? 0 : Convert.ToInt32(row["GroupId"])
            };

            if (table.Columns.Contains("BirthDate") && row["BirthDate"] != DBNull.Value)
                student.BirthDate = Convert.ToDateTime(row["BirthDate"]);

            FillLegacyGrades(student);
            FillSubjectGradeEntries(student, semesterId);
            return student;
        }

        public void AddStudent(Student student)
        {
            DatabaseHelper.ExecuteInTransaction((connection, transaction) =>
            {
                int newId = DatabaseHelper.ExecuteInsertAndGetIntIdentity(
                    connection,
                    transaction,
                    @"INSERT INTO Students (FullName, GroupId, BirthDate)
                      VALUES (@FullName, @GroupId, @BirthDate)",
                    CreateStudentParameters(student));

                student.Id = newId;
                ReplaceStudentGrades(connection, transaction, student);
            });
        }

        public void UpdateStudent(Student student)
        {
            DatabaseHelper.ExecuteInTransaction((connection, transaction) =>
            {
                List<SqlParameter> parameters = CreateStudentParameters(student).ToList();
                parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = student.Id });

                DatabaseHelper.ExecuteNonQuery(
                    connection,
                    transaction,
                    @"UPDATE Students
                      SET FullName = @FullName,
                          GroupId = @GroupId,
                          BirthDate = @BirthDate
                      WHERE Id = @Id",
                    parameters.ToArray());

                ReplaceStudentGrades(connection, transaction, student);
            });
        }

        public DataTable GetFiltered(string search = "", double minAvg = 0)
        {
            return GetFiltered(search, minAvg, null);
        }

        public DataTable GetFiltered(string search, double minAvg, int? semesterId)
        {
            return DatabaseHelper.ExecuteQuery(
                BuildStudentsQuery(StudentsFilterKind.NameOrGroup, true),
                CreateSearchParameter(search),
                CreateMinAverageParameter(minAvg),
                CreateSemesterParameter(semesterId),
                CreateLegacySubjectParameter(),
                CreateLegacySubjectAltParameter());
        }

        public void DeleteStudent(int id)
        {
            DatabaseHelper.ExecuteInTransaction((connection, transaction) =>
            {
                DatabaseHelper.ExecuteNonQuery(
                    connection,
                    transaction,
                    "DELETE FROM Grades WHERE StudentId = @StudentId",
                    CreateStudentIdParameter(id));

                DatabaseHelper.ExecuteNonQuery(
                    connection,
                    transaction,
                    "DELETE FROM Students WHERE Id = @StudentId",
                    CreateStudentIdParameter(id));
            });
        }

        public static void FillLegacyGrades(Student student)
        {
            if (student == null || student.Id == 0)
                return;

            string sql;
            if (SubjectGradeRepository.HasExtendedGradesSchema())
            {
                sql = @"SELECT Grade FROM Grades
                        WHERE StudentId = @StudentId
                          AND GradeDate IS NULL
                          AND (
                              Subject = @LegacyGeneralSubject
                              OR Subject = @LegacyGeneralSubjectAlt
                              OR Subject IS NULL
                              OR LTRIM(RTRIM(Subject)) = ''
                          )
                        ORDER BY Id";
            }
            else
            {
                sql = @"SELECT Grade FROM Grades
                        WHERE StudentId = @StudentId
                          AND (
                              Subject = @LegacyGeneralSubject
                              OR Subject = @LegacyGeneralSubjectAlt
                              OR Subject IS NULL
                              OR LTRIM(RTRIM(Subject)) = ''
                          )
                        ORDER BY Id";
            }

            student.Grades = DatabaseHelper.ExecuteQuery(
                sql,
                CreateStudentIdParameter(student.Id),
                CreateLegacySubjectParameter(),
                CreateLegacySubjectAltParameter()).Rows
                .Cast<DataRow>()
                .Select(r => Convert.ToInt32(r["Grade"]))
                .ToList();
        }

        public static void FillSubjectGradeEntries(Student student, int? semesterId)
        {
            if (student == null || student.Id == 0)
                return;

            student.SubjectGradeEntries = new List<SubjectGradeEntry>();
            string sql;

            if (SubjectGradeRepository.HasExtendedGradesSchema())
            {
                sql = @"
SELECT Subject, Grade, GradeDate, SemesterId
FROM Grades
WHERE StudentId = @StudentId
  AND Subject IS NOT NULL
  AND LTRIM(RTRIM(Subject)) <> ''
  AND Subject NOT IN (@LegacyGeneralSubject, @LegacyGeneralSubjectAlt)
  AND (@SemesterId IS NULL OR SemesterId = @SemesterId)
ORDER BY Subject,
         CASE WHEN GradeDate IS NULL THEN 1 ELSE 0 END,
         GradeDate,
         Id";
            }
            else
            {
                sql = @"
SELECT Subject, Grade, NULL AS GradeDate, NULL AS SemesterId
FROM Grades
WHERE StudentId = @StudentId
  AND Subject IS NOT NULL
  AND LTRIM(RTRIM(Subject)) <> ''
  AND Subject NOT IN (@LegacyGeneralSubject, @LegacyGeneralSubjectAlt)
ORDER BY Subject, Id";
            }

            DataTable table = DatabaseHelper.ExecuteQuery(
                sql,
                CreateStudentIdParameter(student.Id),
                new SqlParameter("@SemesterId", SqlDbType.Int)
                {
                    Value = semesterId.HasValue ? (object)semesterId.Value : DBNull.Value
                },
                CreateLegacySubjectParameter(),
                CreateLegacySubjectAltParameter());

            foreach (IGrouping<string, DataRow> group in table.Rows.Cast<DataRow>().GroupBy(r => r["Subject"].ToString().Trim(), StringComparer.OrdinalIgnoreCase))
            {
                List<DataRow> datedRows = group.Where(r => table.Columns.Contains("GradeDate") && r["GradeDate"] != DBNull.Value).ToList();
                IEnumerable<DataRow> rowsToUse = datedRows.Count > 0 ? (IEnumerable<DataRow>)datedRows : group;

                foreach (DataRow row in rowsToUse)
                {
                    student.SubjectGradeEntries.Add(new SubjectGradeEntry
                    {
                        Subject = row["Subject"].ToString().Trim(),
                        Grade = Convert.ToInt32(row["Grade"]),
                        SemesterId = table.Columns.Contains("SemesterId") && row["SemesterId"] != DBNull.Value
                            ? (int?)Convert.ToInt32(row["SemesterId"])
                            : semesterId,
                        GradeDate = table.Columns.Contains("GradeDate") && row["GradeDate"] != DBNull.Value
                            ? Convert.ToDateTime(row["GradeDate"]).Date
                            : (DateTime?)null
                    });
                }
            }
        }

        private static void ReplaceStudentGrades(SqlConnection connection, SqlTransaction transaction, Student student)
        {
            List<int?> semesterIds = (student.SubjectGradeEntries ?? new List<SubjectGradeEntry>())
                .Where(e => e != null)
                .Select(e => e.SemesterId)
                .Distinct()
                .ToList();

            if (semesterIds.Count == 0)
            {
                DatabaseHelper.ExecuteNonQuery(
                    connection,
                    transaction,
                    "DELETE FROM Grades WHERE StudentId = @StudentId",
                    CreateStudentIdParameter(student.Id));
            }
            else
            {
                foreach (int? semesterId in semesterIds)
                {
                    DatabaseHelper.ExecuteNonQuery(
                        connection,
                        transaction,
                        @"DELETE FROM Grades
                          WHERE StudentId = @StudentId
                            AND ((@SemesterId IS NULL AND SemesterId IS NULL) OR SemesterId = @SemesterId)",
                        CreateStudentIdParameter(student.Id),
                        new SqlParameter("@SemesterId", SqlDbType.Int)
                        {
                            Value = semesterId.HasValue ? (object)semesterId.Value : DBNull.Value
                        });
                }
            }

            foreach (int grade in student.Grades ?? Enumerable.Empty<int>())
            {
                DatabaseHelper.ExecuteNonQuery(
                    connection,
                    transaction,
                    "INSERT INTO Grades (StudentId, Subject, Grade) VALUES (@StudentId, @Subject, @Grade)",
                    CreateStudentIdParameter(student.Id),
                    new SqlParameter("@Subject", SqlDbType.NVarChar, 50) { Value = LegacyGeneralSubject },
                    new SqlParameter("@Grade", SqlDbType.Int) { Value = grade });
            }

            InsertSubjectGradeEntries(connection, transaction, student.Id, student.SubjectGradeEntries);
        }

        private static void InsertSubjectGradeEntries(SqlConnection connection, SqlTransaction transaction, int studentId, List<SubjectGradeEntry> entries)
        {
            if (entries == null || entries.Count == 0)
                return;

            var semesterIdsBySubject = new Dictionary<string, int?>(StringComparer.OrdinalIgnoreCase);

            foreach (SubjectGradeEntry entry in entries)
            {
                if (entry == null || string.IsNullOrWhiteSpace(entry.Subject))
                    continue;

                string subject = entry.Subject.Trim();
                semesterIdsBySubject[subject] = entry.SemesterId;

                if (entry.GradeDate.HasValue)
                {
                    DatabaseHelper.ExecuteNonQuery(
                        connection,
                        transaction,
                        @"INSERT INTO Grades (StudentId, Subject, Grade, GradeDate, SemesterId)
                          VALUES (@StudentId, @Subject, @Grade, @GradeDate, @SemesterId)",
                        CreateStudentIdParameter(studentId),
                        new SqlParameter("@Subject", SqlDbType.NVarChar, 50) { Value = subject },
                        new SqlParameter("@Grade", SqlDbType.Int) { Value = entry.Grade },
                        new SqlParameter("@GradeDate", SqlDbType.Date) { Value = entry.GradeDate.Value.Date },
                        new SqlParameter("@SemesterId", SqlDbType.Int)
                        {
                            Value = entry.SemesterId.HasValue ? (object)entry.SemesterId.Value : DBNull.Value
                        });
                }
                else
                {
                    DatabaseHelper.ExecuteNonQuery(
                        connection,
                        transaction,
                        @"INSERT INTO Grades (StudentId, Subject, Grade, SemesterId)
                          VALUES (@StudentId, @Subject, @Grade, @SemesterId)",
                        CreateStudentIdParameter(studentId),
                        new SqlParameter("@Subject", SqlDbType.NVarChar, 50) { Value = subject },
                        new SqlParameter("@Grade", SqlDbType.Int) { Value = entry.Grade },
                        new SqlParameter("@SemesterId", SqlDbType.Int)
                        {
                            Value = entry.SemesterId.HasValue ? (object)entry.SemesterId.Value : DBNull.Value
                        });
                }
            }

            if (!SubjectGradeRepository.HasExtendedGradesSchema())
                return;

            foreach (KeyValuePair<string, int?> pair in semesterIdsBySubject)
                SubjectGradeRepository.SyncUndatedSubjectGrade(studentId, pair.Key, pair.Value);
        }

        private static SqlParameter[] CreateStudentParameters(Student student)
        {
            return new[]
            {
                new SqlParameter("@FullName", SqlDbType.NVarChar, 100) { Value = student.FullName ?? string.Empty },
                new SqlParameter("@GroupId", SqlDbType.Int) { Value = student.GroupId },
                new SqlParameter("@BirthDate", SqlDbType.Date)
                {
                    Value = student.BirthDate.HasValue ? (object)student.BirthDate.Value.Date : DBNull.Value
                }
            };
        }

        private static SqlParameter CreateStudentIdParameter(int studentId)
        {
            return new SqlParameter("@StudentId", SqlDbType.Int) { Value = studentId };
        }

        private static SqlParameter CreateSearchParameter(string search)
        {
            return new SqlParameter("@SearchPattern", SqlDbType.NVarChar, 200)
            {
                Value = "%" + (search ?? string.Empty).Trim() + "%"
            };
        }

        private static SqlParameter CreateMinAverageParameter(double minAverage)
        {
            return new SqlParameter("@MinAverage", SqlDbType.Float) { Value = minAverage };
        }

        private static SqlParameter CreateSemesterParameter(int? semesterId)
        {
            return new SqlParameter("@SemesterId", SqlDbType.Int)
            {
                Value = semesterId.HasValue ? (object)semesterId.Value : DBNull.Value
            };
        }

        private static SqlParameter CreateLegacySubjectParameter()
        {
            return new SqlParameter("@LegacyGeneralSubject", SqlDbType.NVarChar, 50) { Value = LegacyGeneralSubject };
        }

        private static SqlParameter CreateLegacySubjectAltParameter()
        {
            return new SqlParameter("@LegacyGeneralSubjectAlt", SqlDbType.NVarChar, 50) { Value = LegacyGeneralSubjectAlt };
        }
    }
}

