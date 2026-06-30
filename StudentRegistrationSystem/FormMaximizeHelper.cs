using System;
using System.Drawing;
using System.Windows.Forms;

namespace StudentRegistrationSystem
{
    /// <summary>
    /// Кнопка «Развернуть» даёт окно размером MaximumSize по центру экрана, без настоящего полноэкранного режима.
    /// </summary>
    internal static class FormMaximizeHelper
    {
        private const int WM_SYSCOMMAND = 0x0112;
        private const int SC_MAXIMIZE = 0xF030;

        public static bool TryHandleMaximizeToLimitedSize(Form form, ref Message m)
        {
            if (m.Msg != WM_SYSCOMMAND)
                return false;
            if ((m.WParam.ToInt32() & 0xFFF0) != SC_MAXIMIZE)
                return false;

            var max = form.MaximumSize;
            if (max.Width <= 0 || max.Height <= 0)
                return false;

            form.WindowState = FormWindowState.Normal;
            form.Size = max;

            Rectangle wa = Screen.FromHandle(form.Handle).WorkingArea;
            form.Location = new Point(
                wa.Left + (wa.Width - form.Width) / 2,
                wa.Top + (wa.Height - form.Height) / 2);

            m.Result = IntPtr.Zero;
            return true;
        }
    }
}
