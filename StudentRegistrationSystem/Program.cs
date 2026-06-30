using StudentRegistrationSystem.Services;
using System;
using System.Windows.Forms;

namespace StudentRegistrationSystem
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                DatabaseHelper.EnsureConnectionAvailable();
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                MessageBox.Show(
                    UiCaptions.DatabaseConnectionError + Environment.NewLine + Environment.NewLine + DatabaseHelper.FormatUserMessage(ex),
                    UiCaptions.Error,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            Application.Run(new MainForm());
        }
    }
}
