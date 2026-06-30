using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Windows.Forms;

public static class Theme
{
    public static bool IsDarkTheme { get; private set; }

    public static Color BackColor { get; private set; }
    public static Color ForeColor { get; private set; }
    public static Color GridColor { get; private set; }
    public static Color ButtonColor { get; private set; }
    public static Color AccentColor { get; private set; }
    public static Color PanelColor { get; private set; }
    public static Color BorderColor { get; private set; }
    public static Color MutedTextColor { get; private set; }

    public static void SetLightTheme()
    {
        IsDarkTheme = false;
        BackColor = Color.FromArgb(245, 247, 250);
        ForeColor = Color.FromArgb(33, 37, 41);
        GridColor = Color.White;
        ButtonColor = Color.FromArgb(232, 236, 241);
        AccentColor = Color.FromArgb(32, 122, 196);
        PanelColor = Color.FromArgb(236, 240, 245);
        BorderColor = Color.FromArgb(210, 218, 228);
        MutedTextColor = Color.FromArgb(105, 114, 125);
    }

    public static void SetDarkTheme()
    {
        IsDarkTheme = true;
        BackColor = Color.FromArgb(24, 28, 34);
        ForeColor = Color.FromArgb(238, 242, 247);
        GridColor = Color.FromArgb(35, 40, 47);
        ButtonColor = Color.FromArgb(53, 60, 70);
        AccentColor = Color.FromArgb(67, 160, 224);
        PanelColor = Color.FromArgb(30, 35, 42);
        BorderColor = Color.FromArgb(72, 80, 92);
        MutedTextColor = Color.FromArgb(170, 178, 188);
    }

    public static void SetControlDoubleBuffered(Control control)
    {
        if (control == null)
            return;

        typeof(Control).InvokeMember(
            "DoubleBuffered",
            BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
            null,
            control,
            new object[] { true });
    }

    public static void ApplyRoundedRegion(Control control, int radius = 14)
    {
        if (control == null || control.Width <= 0 || control.Height <= 0)
            return;

        using (GraphicsPath path = CreateRoundedPath(new Rectangle(0, 0, control.Width, control.Height), radius))
        {
            var oldRegion = control.Region;
            control.Region = new Region(path);
            oldRegion?.Dispose();
        }
    }

    public static void StylePrimaryButton(Button button)
    {
        if (button == null)
            return;

        button.BackColor = AccentColor;
        button.ForeColor = Color.White;
        button.FlatStyle = FlatStyle.Flat;
        button.FlatAppearance.BorderSize = 0;
        button.Font = new Font("Segoe UI Semibold", 10.25f, FontStyle.Bold);
    }

    public static void StyleSecondaryButton(Button button)
    {
        if (button == null)
            return;

        button.BackColor = ButtonColor;
        button.ForeColor = ForeColor;
        button.FlatStyle = FlatStyle.Flat;
        button.FlatAppearance.BorderSize = 0;
        button.Font = new Font("Segoe UI", 10f, FontStyle.Regular);
    }

    public static void StyleTextBox(TextBox textBox)
    {
        if (textBox == null)
            return;

        textBox.BackColor = GridColor;
        textBox.ForeColor = ForeColor;
        textBox.BorderStyle = BorderStyle.FixedSingle;
        textBox.Font = new Font("Segoe UI", 10.5f);
    }

    public static void StyleComboBox(ComboBox comboBox)
    {
        if (comboBox == null)
            return;

        comboBox.BackColor = GridColor;
        comboBox.ForeColor = ForeColor;
        comboBox.FlatStyle = IsDarkTheme ? FlatStyle.Flat : FlatStyle.Standard;
        comboBox.Font = new Font("Segoe UI", 10.5f);
    }

    public static void StyleListBox(ListBox listBox)
    {
        if (listBox == null)
            return;

        listBox.BackColor = GridColor;
        listBox.ForeColor = ForeColor;
        listBox.BorderStyle = BorderStyle.FixedSingle;
        listBox.Font = new Font("Segoe UI", 10.5f);
    }

    public static void StyleDataGrid(DataGridView grid)
    {
        if (grid == null)
            return;

        grid.BackgroundColor = GridColor;
        grid.GridColor = BorderColor;
        grid.BorderStyle = BorderStyle.None;
        grid.EnableHeadersVisualStyles = false;
        grid.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
        grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
        grid.ColumnHeadersDefaultCellStyle.BackColor = PanelColor;
        grid.ColumnHeadersDefaultCellStyle.ForeColor = ForeColor;
        grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10.25f, FontStyle.Bold);
        grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = PanelColor;
        grid.ColumnHeadersDefaultCellStyle.SelectionForeColor = ForeColor;
        grid.DefaultCellStyle.BackColor = GridColor;
        grid.DefaultCellStyle.ForeColor = ForeColor;
        grid.DefaultCellStyle.SelectionBackColor = AccentColor;
        grid.DefaultCellStyle.SelectionForeColor = Color.White;
        grid.DefaultCellStyle.Font = new Font("Segoe UI", 10.25f);
        grid.AlternatingRowsDefaultCellStyle.BackColor = IsDarkTheme
            ? Color.FromArgb(40, 46, 54)
            : Color.FromArgb(249, 251, 253);
        grid.AlternatingRowsDefaultCellStyle.ForeColor = ForeColor;
        grid.RowTemplate.Height = 36;
        SetControlDoubleBuffered(grid);
    }

    public static void StyleCard(Control control, int radius = 18)
    {
        if (control == null)
            return;

        control.BackColor = GridColor;
        control.Resize -= Control_ResizeRoundCard;
        control.Resize += Control_ResizeRoundCard;
        ApplyRoundedRegion(control, radius);
    }

    private static void Control_ResizeRoundCard(object sender, System.EventArgs e)
    {
        ApplyRoundedRegion(sender as Control, 18);
    }

    private static GraphicsPath CreateRoundedPath(Rectangle bounds, int radius)
    {
        int diameter = radius * 2;
        var path = new GraphicsPath();

        path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
        path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
        path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
        path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);
        path.CloseFigure();

        return path;
    }
}
