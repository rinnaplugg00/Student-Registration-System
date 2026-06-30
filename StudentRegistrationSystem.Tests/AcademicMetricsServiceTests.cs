using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentRegistrationSystem.Services;

namespace StudentRegistrationSystem.Tests
{
    [TestClass]
    public class AcademicMetricsServiceTests
    {
        [DataTestMethod]
        [DataRow(95.0, "A (4.00)")]
        [DataRow(90.0, "A- (3.67)")]
        [DataRow(85.0, "B+ (3.33)")]
        [DataRow(80.0, "B (3.00)")]
        [DataRow(75.0, "B- (2.67)")]
        [DataRow(70.0, "C+ (2.33)")]
        [DataRow(65.0, "C (2.00)")]
        [DataRow(60.0, "C- (1.67)")]
        [DataRow(55.0, "D+ (1.33)")]
        [DataRow(50.0, "D (1.00)")]
        [DataRow(49.9, "F (0.00)")]
        public void FormatGpaReturnsExpectedLetter(double averageGrade, string expected)
        {
            Assert.AreEqual(expected, AcademicMetricsService.FormatGpa(averageGrade));
        }

        [TestMethod]
        public void GetRiskStatusTextReturnsNoGradesWhenGradeCountIsZero()
        {
            string status = AcademicMetricsService.GetRiskStatusText(100, 0);

            Assert.AreEqual(AcademicMetricsService.NoGradesStatus, status);
        }

        [TestMethod]
        public void GetRiskStatusTextReturnsRiskWhenAverageIsBelowThreshold()
        {
            string status = AcademicMetricsService.GetRiskStatusText(59.99, 1);

            Assert.AreEqual(AcademicMetricsService.RiskStatus, status);
        }

        [TestMethod]
        public void GetRiskStatusTextReturnsNormalWhenAverageMeetsThreshold()
        {
            string status = AcademicMetricsService.GetRiskStatusText(60.0, 1);

            Assert.AreEqual(AcademicMetricsService.NormalStatus, status);
        }
    }
}

