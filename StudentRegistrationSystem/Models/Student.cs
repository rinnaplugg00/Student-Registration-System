using System;
using System.Collections.Generic;
using System.Linq;
using StudentRegistrationSystem.Interfaces;

namespace StudentRegistrationSystem.Models
{
    public sealed class SubjectGradeEntry
    {
        public string Subject { get; set; }
        public int Grade { get; set; }
        public DateTime? GradeDate { get; set; }
        public int? SemesterId { get; set; }
    }

    public class Student : IValidatable
    {
        private const int MinimumStudentAge = 15;

        public int Id { get; set; }
        public string FullName { get; set; }
        public int GroupId { get; set; }
        public DateTime? BirthDate { get; set; }
        public List<int> Grades { get; set; }
        public List<SubjectGradeEntry> SubjectGradeEntries { get; set; }

        public Student()
        {
            Grades = new List<int>();
            SubjectGradeEntries = new List<SubjectGradeEntry>();
        }

        public bool Validate(out string errorMessage)
        {
            if (GroupId <= 0)
            {
                errorMessage = "Выберите учебную группу";
                return false;
            }

            if (string.IsNullOrWhiteSpace(FullName))
            {
                errorMessage = "ФИО не может быть пустым";
                return false;
            }

            foreach (char symbol in FullName)
            {
                if (!char.IsLetter(symbol) && !char.IsWhiteSpace(symbol) && symbol != '-')
                {
                    errorMessage = "В ФИО можно использовать только буквы, пробелы и дефис";
                    return false;
                }
            }

            if (BirthDate.HasValue && BirthDate.Value.Date > DateTime.Today.AddYears(-MinimumStudentAge))
            {
                errorMessage = $"Студент должен быть не младше {MinimumStudentAge} лет";
                return false;
            }

            bool hasLegacy = Grades != null && Grades.Count > 0;
            bool hasSubjectEntries = SubjectGradeEntries != null && SubjectGradeEntries.Count > 0;
            if (!hasLegacy && !hasSubjectEntries)
            {
                errorMessage = "Введите хотя бы одну запись по предмету";
                return false;
            }

            if (hasLegacy && Grades.Any(g => g < 0 || g > 100))
            {
                errorMessage = "Оценки должны быть от 0 до 100";
                return false;
            }

            if (hasSubjectEntries)
            {
                var undatedSubjects = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                var datedSubjects = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                foreach (SubjectGradeEntry entry in SubjectGradeEntries)
                {
                    string subject = entry?.Subject?.Trim() ?? string.Empty;
                    if (string.IsNullOrWhiteSpace(subject))
                    {
                        errorMessage = "Укажите предмет для каждой строки с оценкой";
                        return false;
                    }

                    if (entry.Grade < 0 || entry.Grade > 100)
                    {
                        errorMessage = "Оценки по предметам должны быть от 0 до 100";
                        return false;
                    }

                    if (entry.GradeDate.HasValue)
                    {
                        datedSubjects.Add(subject);
                    }
                    else
                    {
                        if (!undatedSubjects.Add(subject))
                        {
                            errorMessage = $"Предмет «{subject}» без даты указан несколько раз.";
                            return false;
                        }
                    }
                }

                foreach (string subject in undatedSubjects)
                {
                    if (datedSubjects.Contains(subject))
                    {
                        errorMessage = $"Для предмета «{subject}» нельзя одновременно хранить записи с датой и без даты.";
                        return false;
                    }
                }
            }

            errorMessage = string.Empty;
            return true;
        }

        public double AverageGrade()
        {
            var values = new List<double>();

            if (Grades != null)
                values.AddRange(Grades.Select(g => (double)g));

            if (SubjectGradeEntries != null && SubjectGradeEntries.Count > 0)
            {
                foreach (var group in SubjectGradeEntries
                             .Where(e => e != null && !string.IsNullOrWhiteSpace(e.Subject))
                             .GroupBy(e => e.Subject.Trim(), StringComparer.OrdinalIgnoreCase))
                {
                    var dated = group.Where(e => e.GradeDate.HasValue).ToList();
                    if (dated.Count > 0)
                        values.Add(dated.Average(e => e.Grade));
                    else
                        values.Add(group.Average(e => e.Grade));
                }
            }

            return values.Count == 0 ? 0 : values.Average();
        }

        public string Name => FullName;
    }
}
