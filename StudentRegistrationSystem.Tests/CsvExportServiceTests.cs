using System;
using System.Data;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentRegistrationSystem.Services;

namespace StudentRegistrationSystem.Tests
{
    [TestClass]
    public class CsvExportServiceTests
    {
        [TestMethod]
        public void ExportDataTableWritesHeadersAndRowsWithSemicolonDelimiter()
        {
            var table = new DataTable();
            table.Columns.Add("Группа", typeof(string));
            table.Columns.Add("ФИО", typeof(string));
            table.Columns.Add("Средний балл", typeof(double));
            table.Rows.Add("ИС-21", "Иванов Иван", 85.5);

            string filePath = Path.Combine(Path.GetTempPath(), $"students-export-{Guid.NewGuid():N}.csv");

            try
            {
                CsvExportService.ExportDataTable(table, filePath);

                string content = File.ReadAllText(filePath);
                StringAssert.Contains(content, "\"Группа\";\"ФИО\";\"Средний балл\"");
                StringAssert.Contains(content, "\"ИС-21\";\"Иванов Иван\";\"85,5\"");
            }
            finally
            {
                if (File.Exists(filePath))
                    File.Delete(filePath);
            }
        }

        [TestMethod]
        public void ExportDataTableThrowsWhenFilePathIsEmpty()
        {
            var table = new DataTable();
            table.Columns.Add("A", typeof(string));

            Assert.ThrowsException<ArgumentException>(() => CsvExportService.ExportDataTable(table, " "));
        }
    }
}
