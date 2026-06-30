using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using StudentRegistrationSystem.Services;

namespace StudentRegistrationSystem
{
    public partial class FormTopStudents : Form
    {
        private readonly int? _semesterId;

        private sealed class TopStudentItem
        {
            public int StudentId { get; set; }
            public string Text { get; set; }

            public override string ToString()
            {
                return Text ?? string.Empty;
            }
        }

        public FormTopStudents(int? semesterId = null)
        {
            InitializeComponent();
            _semesterId = semesterId;
            Text = UiCaptions.TopStudents;
            FormBorderStyle = FormBorderStyle.Sizable;
        }

        protected override void WndProc(ref Message m)
        {
            if (FormMaximizeHelper.TryHandleMaximizeToLimitedSize(this, ref m))
                return;
            base.WndProc(ref m);
        }

        private void listTop_DrawItem(object sender, DrawItemEventArgs e)
        {
            ListBoxOwnerDrawHelper.DrawCenteredItem(listTop, e);
        }

        private void listTop_DoubleClick(object sender, EventArgs e)
        {
            if (!(listTop.SelectedItem is TopStudentItem item))
                return;

            using (var form = new FormStudentDetails(item.StudentId, _semesterId))
                form.ShowDialog(this);
        }

        private void FormTopStudents_Load(object sender, EventArgs e)
        {
            ApplyTheme();
            LoadTopStudents();
        }

        private void ApplyTheme()
        {
            BackColor = Theme.BackColor;
            ForeColor = Theme.ForeColor;

            lblTitle.Font = new Font("Segoe UI", 16f, FontStyle.Bold);
            lblTitle.ForeColor = Theme.ForeColor;
            lblSubtitle.ForeColor = Theme.MutedTextColor;

            tableLayoutPanel1.BackColor = Theme.BackColor;
            listPanel.BackColor = Theme.GridColor;
            Theme.ApplyRoundedRegion(listPanel, 18);

            SemesterInfo semester = _semesterId.HasValue ? SemesterService.GetSemesterById(_semesterId.Value) : null;
            if (semester != null)
                lblSubtitle.Text = "Период: " + semester.Name;

            Theme.StyleListBox(listTop);
            listTop.BackColor = Theme.GridColor;
            listTop.BorderStyle = BorderStyle.None;
            listTop.Font = new Font("Segoe UI", 11.25f, FontStyle.Regular, GraphicsUnit.Point);
            Theme.SetControlDoubleBuffered(listTop);
        }

        private void LoadTopStudents()
        {
            var service = new StudentService();
            var table = service.GetAll(_semesterId);
            var topStudents = table.AsEnumerable()
                .OrderByDescending(r => Convert.ToDouble(r["AverageGrade"]))
                .ThenBy(r => Convert.ToString(r["FullName"]))
                .Take(5);

            listTop.Items.Clear();

            int place = 1;

            foreach (DataRow row in topStudents)
            {
                int studentId = Convert.ToInt32(row["Id"]);
                string name = DisplayTextHelper.SanitizeNameForGrid(row["FullName"]);
                string groupName = Convert.ToString(row["GroupName"]);
                double avg = Convert.ToDouble(row["AverageGrade"]);

                listTop.Items.Add(new TopStudentItem
                {
                    StudentId = studentId,
                    Text = $"{place}. {name} | {groupName} | {avg:F2}"
                });

                place++;
            }
        }
    }
}
