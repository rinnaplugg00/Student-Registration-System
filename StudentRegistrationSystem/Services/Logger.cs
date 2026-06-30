using System;
using System.IO;

namespace StudentRegistrationSystem.Services
{
    public static class Logger
    {
        private static readonly object SyncRoot = new object();

        private static string LogFilePath =>
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log.txt");

        public static void Log(Exception ex)
        {
            if (ex == null)
                return;

            try
            {
                lock (SyncRoot)
                {
                    File.AppendAllText(
                        LogFilePath,
                        $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {ex.GetType().Name} | {ex.Message}{Environment.NewLine}{ex.StackTrace}{Environment.NewLine}{Environment.NewLine}");
                }
            }
            catch
            {
            }
        }

        public static void Log(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                return;

            try
            {
                lock (SyncRoot)
                {
                    File.AppendAllText(
                        LogFilePath,
                        $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {message}{Environment.NewLine}");
                }
            }
            catch
            {
            }
        }
    }
}
