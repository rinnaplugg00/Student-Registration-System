namespace StudentRegistrationSystem.Services
{
    public static class AcademicMetricsService
    {
        public const double RiskAverageThreshold = 60.0;
        public const string NoGradesStatus = "Нет оценок";
        public const string RiskStatus = "Зона риска";
        public const string NormalStatus = "Норма";

        public static string GetRiskStatusText(double averageGrade, int gradeCount)
        {
            if (gradeCount <= 0)
                return NoGradesStatus;

            return averageGrade < RiskAverageThreshold
                ? RiskStatus
                : NormalStatus;
        }

        public static string FormatGpa(double averageGrade)
        {
            if (averageGrade >= 95)
                return "A (4.00)";
            if (averageGrade >= 90)
                return "A- (3.67)";
            if (averageGrade >= 85)
                return "B+ (3.33)";
            if (averageGrade >= 80)
                return "B (3.00)";
            if (averageGrade >= 75)
                return "B- (2.67)";
            if (averageGrade >= 70)
                return "C+ (2.33)";
            if (averageGrade >= 65)
                return "C (2.00)";
            if (averageGrade >= 60)
                return "C- (1.67)";
            if (averageGrade >= 55)
                return "D+ (1.33)";
            if (averageGrade >= 50)
                return "D (1.00)";

            return "F (0.00)";
        }
    }
}

