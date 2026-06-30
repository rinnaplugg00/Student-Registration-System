using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace StudentRegistrationSystem
{
    /// <summary>
    /// Центрирование + многоточие без артефактов TextRenderer (эмодзи, «обрывки» справа).
    /// </summary>
    internal static class ListBoxOwnerDrawHelper
    {
        public static void DrawCenteredItem(ListBox listBox, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;

            var g = e.Graphics;
            var state = g.Save();
            try
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

                using (var back = new SolidBrush(listBox.BackColor))
                    g.FillRectangle(back, e.Bounds);

                g.SetClip(e.Bounds);

                var text = listBox.Items[e.Index].ToString();
                var rect = new RectangleF(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);
                using (var fmt = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center,
                    Trimming = StringTrimming.EllipsisCharacter,
                    FormatFlags = StringFormatFlags.NoWrap
                })
                using (var brush = new SolidBrush(listBox.ForeColor))
                {
                    g.DrawString(text, listBox.Font, brush, rect, fmt);
                }
            }
            finally
            {
                g.Restore(state);
            }
        }
    }
}
