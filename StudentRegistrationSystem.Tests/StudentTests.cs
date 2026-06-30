using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentRegistrationSystem.Models;

namespace StudentRegistrationSystem.Tests
{
    [TestClass]
    public class StudentTests
    {
        [TestMethod]
        public void ValidateRejectsEmptyFullName()
        {
            var student = new Student
            {
                GroupId = 1,
                SubjectGradeEntries = new List<SubjectGradeEntry>
                {
                    new SubjectGradeEntry { Subject = "Math", Grade = 90 }
                }
            };

            bool isValid = student.Validate(out string errorMessage);

            Assert.IsFalse(isValid);
            Assert.IsFalse(string.IsNullOrWhiteSpace(errorMessage));
        }

        [TestMethod]
        public void ValidateRejectsInvalidGrade()
        {
            var student = new Student
            {
                FullName = "Ivan Ivanov",
                GroupId = 1,
                SubjectGradeEntries = new List<SubjectGradeEntry>
                {
                    new SubjectGradeEntry { Subject = "Math", Grade = 101 }
                }
            };

            bool isValid = student.Validate(out string errorMessage);

            Assert.IsFalse(isValid);
            Assert.IsFalse(string.IsNullOrWhiteSpace(errorMessage));
        }

        [TestMethod]
        public void ValidateRejectsStudentYoungerThanMinimumAge()
        {
            var student = new Student
            {
                FullName = "Ivan Ivanov",
                GroupId = 1,
                BirthDate = DateTime.Today.AddYears(-14),
                SubjectGradeEntries = new List<SubjectGradeEntry>
                {
                    new SubjectGradeEntry { Subject = "Math", Grade = 90 }
                }
            };

            bool isValid = student.Validate(out string errorMessage);

            Assert.IsFalse(isValid);
            Assert.IsFalse(string.IsNullOrWhiteSpace(errorMessage));
        }

        [TestMethod]
        public void AverageGradeAveragesLegacyAndSubjectGrades()
        {
            var student = new Student
            {
                FullName = "Ivan Ivanov",
                GroupId = 1,
                Grades = new List<int> { 80 },
                SubjectGradeEntries = new List<SubjectGradeEntry>
                {
                    new SubjectGradeEntry { Subject = "Math", Grade = 90 },
                    new SubjectGradeEntry { Subject = "Math", Grade = 100 },
                    new SubjectGradeEntry { Subject = "History", Grade = 70 }
                }
            };

            double average = student.AverageGrade();

            Assert.AreEqual(81.66666666666667, average, 0.0001);
        }

        [TestMethod]
        public void ValidateRejectsMissingGroup()
        {
            var student = new Student
            {
                FullName = "Ivan Ivanov",
                GroupId = 0,
                SubjectGradeEntries = new List<SubjectGradeEntry>
                {
                    new SubjectGradeEntry { Subject = "Math", Grade = 90 }
                }
            };

            bool isValid = student.Validate(out string errorMessage);

            Assert.IsFalse(isValid);
            Assert.IsFalse(string.IsNullOrWhiteSpace(errorMessage));
        }

        [TestMethod]
        public void ValidateAcceptsCorrectStudent()
        {
            var student = new Student
            {
                FullName = "Ivan Ivanov",
                GroupId = 1,
                BirthDate = DateTime.Today.AddYears(-18),
                SubjectGradeEntries = new List<SubjectGradeEntry>
                {
                    new SubjectGradeEntry { Subject = "Math", Grade = 90 }
                }
            };

            bool isValid = student.Validate(out string errorMessage);

            Assert.IsTrue(isValid);
            Assert.AreEqual(string.Empty, errorMessage);
        }
    }
}

