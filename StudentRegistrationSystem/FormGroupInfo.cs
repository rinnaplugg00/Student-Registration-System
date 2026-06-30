using StudentRegistrationSystem.Services;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace StudentRegistrationSystem
{
    public sealed class FormGroupInfo : Form
    {
        private readonly string _groupName;
        private readonly GroupService _groupService = new GroupService();

        private TableLayoutPanel _rootLayout;
        private Label _lblTitle;
        private Label _lblSubtitle;
        private Panel _detailsPanel;
        private TableLayoutPanel _detailsLayout;
        private Label _lblNameCaption;
        private Label _lblNameValue;
        private Label _lblSpecialtyCaption;
        private Label _lblSpecialtyValue;
        private Label _lblCountCaption;
        private Label _lblCountValue;
        private Label _lblStudentsCaption;
        private ListBox _lstStudents;

        public FormGroupInfo(string groupName)
        {
            _groupName = groupName;
            BuildUi();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            ApplyTheme();
            LoadGroupInfo();
        }

        private void BuildUi()
        {
            _rootLayout = new TableLayoutPanel();
            _lblTitle = new Label();
            _lblSubtitle = new Label();
            _detailsPanel = new Panel();
            _detailsLayout = new TableLayoutPanel();
            _lblNameCaption = new Label();
            _lblNameValue = new Label();
            _lblSpecialtyCaption = new Label();
            _lblSpecialtyValue = new Label();
            _lblCountCaption = new Label();
            _lblCountValue = new Label();
            _lblStudentsCaption = new Label();
            _lstStudents = new ListBox();

            SuspendLayout();
            _rootLayout.SuspendLayout();
            _detailsPanel.SuspendLayout();
            _detailsLayout.SuspendLayout();

            _rootLayout.ColumnCount = 1;
            _rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            _rootLayout.Controls.Add(_lblTitle, 0, 0);
            _rootLayout.Controls.Add(_lblSubtitle, 0, 1);
            _rootLayout.Controls.Add(_detailsPanel, 0, 2);
            _rootLayout.Dock = DockStyle.Fill;
            _rootLayout.Padding = new Padding(22, 18, 22, 18);
            _rootLayout.RowCount = 3;
            _rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            _rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            _rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            _lblTitle.Dock = DockStyle.Fill;
            _lblTitle.Font = new Font("Segoe UI", 16f, FontStyle.Bold);
            _lblTitle.Margin = new Padding(0, 0, 0, 6);
            _lblTitle.Text = "Информация о группе";
            _lblTitle.TextAlign = ContentAlignment.MiddleCenter;

            _lblSubtitle.Dock = DockStyle.Fill;
            _lblSubtitle.Font = new Font("Segoe UI", 9.75f);
            _lblSubtitle.Margin = new Padding(0, 0, 0, 12);
            _lblSubtitle.Text = "Здесь отображаются специальность, состав и количество студентов.";
            _lblSubtitle.TextAlign = ContentAlignment.MiddleCenter;

            _detailsPanel.Controls.Add(_detailsLayout);
            _detailsPanel.Dock = DockStyle.Fill;
            _detailsPanel.Padding = new Padding(18);

            _detailsLayout.ColumnCount = 2;
            _detailsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 190F));
            _detailsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            _detailsLayout.Controls.Add(_lblNameCaption, 0, 0);
            _detailsLayout.Controls.Add(_lblNameValue, 1, 0);
            _detailsLayout.Controls.Add(_lblSpecialtyCaption, 0, 1);
            _detailsLayout.Controls.Add(_lblSpecialtyValue, 1, 1);
            _detailsLayout.Controls.Add(_lblCountCaption, 0, 2);
            _detailsLayout.Controls.Add(_lblCountValue, 1, 2);
            _detailsLayout.Controls.Add(_lblStudentsCaption, 0, 3);
            _detailsLayout.Controls.Add(_lstStudents, 1, 3);
            _detailsLayout.Dock = DockStyle.Fill;
            _detailsLayout.RowCount = 4;
            _detailsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            _detailsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            _detailsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            _detailsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            ConfigureCaption(_lblNameCaption, "Название группы:");
            ConfigureCaption(_lblSpecialtyCaption, "Специальность:");
            ConfigureCaption(_lblCountCaption, "Количество студентов:");
            ConfigureCaption(_lblStudentsCaption, "Список студентов:");
            _lblStudentsCaption.TextAlign = ContentAlignment.TopLeft;

            ConfigureValue(_lblNameValue);
            ConfigureValue(_lblSpecialtyValue);
            ConfigureValue(_lblCountValue);

            _lstStudents.Dock = DockStyle.Fill;
            _lstStudents.IntegralHeight = false;
            _lstStudents.SelectionMode = SelectionMode.None;

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(760, 500);
            Controls.Add(_rootLayout);
            FormBorderStyle = FormBorderStyle.Sizable;
            MaximumSize = new Size(1040, 780);
            MinimumSize = new Size(700, 460);
            StartPosition = FormStartPosition.CenterParent;
            Text = "Информация о группе";

            _detailsLayout.ResumeLayout(false);
            _detailsPanel.ResumeLayout(false);
            _rootLayout.ResumeLayout(false);
            ResumeLayout(false);
        }

        private static void ConfigureCaption(Label label, string text)
        {
            label.Dock = DockStyle.Fill;
            label.Font = new Font("Segoe UI", 10.25f, FontStyle.Bold);
            label.Margin = new Padding(0);
            label.Text = text;
            label.TextAlign = ContentAlignment.MiddleLeft;
        }

        private static void ConfigureValue(Label label)
        {
            label.Dock = DockStyle.Fill;
            label.Font = new Font("Segoe UI", 10.25f);
            label.Margin = new Padding(0);
            label.TextAlign = ContentAlignment.MiddleLeft;
        }

        private void ApplyTheme()
        {
            BackColor = Theme.BackColor;
            ForeColor = Theme.ForeColor;
            _rootLayout.BackColor = Theme.BackColor;
            _lblTitle.ForeColor = Theme.ForeColor;
            _lblSubtitle.ForeColor = Theme.MutedTextColor;
            _detailsPanel.BackColor = Theme.GridColor;
            Theme.ApplyRoundedRegion(_detailsPanel, 18);
            Theme.StyleListBox(_lstStudents);
        }

        private void LoadGroupInfo()
        {
            if (string.IsNullOrWhiteSpace(_groupName))
                return;

            DataTable details = _groupService.GetDetailsByName(_groupName);

            if (details.Rows.Count == 0)
            {
                _lblNameValue.Text = "Группа не найдена";
                _lblSpecialtyValue.Text = "-";
                _lblCountValue.Text = "0";
                _lstStudents.Items.Clear();
                _lstStudents.Items.Add("Нет данных");
                return;
            }

            DataRow row = details.Rows[0];
            int groupId = Convert.ToInt32(row["Id"]);
            string specialty = Convert.ToString(row["Specialty"]);

            _lblNameValue.Text = DisplayTextHelper.SanitizeNameForGrid(row["GroupName"]);
            _lblSpecialtyValue.Text = string.IsNullOrWhiteSpace(specialty) ? "Не указана" : specialty;
            _lblCountValue.Text = Convert.ToString(row["StudentCount"]);

            DataTable students = _groupService.GetStudents(groupId, false);

            _lstStudents.Items.Clear();
            if (students.Rows.Count == 0)
            {
                _lstStudents.Items.Add("Студентов пока нет");
                return;
            }

            foreach (DataRow student in students.Rows)
                _lstStudents.Items.Add(DisplayTextHelper.SanitizeNameForGrid(student["FullName"]));
        }
    }
}
