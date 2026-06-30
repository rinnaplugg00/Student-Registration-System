using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentRegistrationSystem;

namespace StudentRegistrationSystem.Tests
{
    [TestClass]
    public class DatabaseHelperTests
    {
        [TestMethod]
        public void FormatUserMessageReturnsMessageForGenericException()
        {
            var exception = new InvalidOperationException("Тестовая ошибка");

            string message = DatabaseHelper.FormatUserMessage(exception);

            Assert.AreEqual("Тестовая ошибка", message);
        }

        [TestMethod]
        public void FormatUserMessageReturnsEmptyStringForNull()
        {
            Assert.AreEqual(string.Empty, DatabaseHelper.FormatUserMessage(null));
        }
    }
}
