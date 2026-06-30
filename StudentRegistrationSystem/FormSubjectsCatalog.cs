using StudentRegistrationSystem.Services;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace StudentRegistrationSystem
{
    public partial class FormSubjectsCatalog : Form
    {
        private readonly SubjectService _subjectService = new SubjectService();

        public FormSubjectsCatalog()
        {
            InitializeComponent();
            Text = UiCaptions.SubjectsCatalog;
        }

        private void FormSubjectsCatalog_Load(object sender, EventArgs e)
        {
            ApplyTheme();
            btnAddSubject.Resize += (_, __) => Theme.ApplyRoundedRegion(btnAddSubject, 16);
            btnDeleteSubject.Resize += (_, __) => Theme.ApplyRoundedRegion(btnDeleteSubject, 16);
            btnClose.Resize += (_, __) => Theme.ApplyRoundedRegion(btnClose, 16);
            Theme.ApplyRoundedRegion(btnAddSubject, 16);
            Theme.ApplyRoundedRegion(btnDeleteSubject, 16);
            Theme.ApplyRoundedRegion(btnClose, 16);
            Theme.ApplyRoundedRegion(listPanel, 18);
            LoadSubjects();
        }

        private void ApplyTheme()
        {
            BackColor = Theme.BackColor;
            ForeColor = Theme.ForeColor;

            rootLayout.BackColor = Theme.BackColor;
            lblTitle.ForeColor = Theme.ForeColor;
            lblSubtitle.ForeColor = Theme.MutedTextColor;

            listPanel.BackColor = Theme.GridColor;
            lstSubjects.BackColor = Theme.GridColor;
            lstSubjects.ForeColor = Theme.ForeColor;
            lstSubjects.BorderStyle = BorderStyle.None;
            actionsPanel.BackColor = Theme.BackColor;
            Theme.StyleListBox(lstSubjects);
            lstSubjects.BorderStyle = BorderStyle.None;

            StyleButton(btnAddSubject, true);
            StyleButton(btnDeleteSubject, false);
            StyleButton(btnClose, false);
        }

        private void StyleButton(Button button, bool primary)
        {
            if (primary)
                Theme.StylePrimaryButton(button);
            else
                Theme.StyleSecondaryButton(button);
        }

        private void LoadSubjects()
        {
            lstSubjects.DataSource = null;
            lstSubjects.DisplayMember = null;
            lstSubjects.ValueMember = null;
            lstSubjects.Items.Clear();

            DataTable table = BuildSubjectsTable();
            if (table.Rows.Count == 0)
            {
                lstSubjects.Items.Add("Предметы пока не добавлены");
                return;
            }

            lstSubjects.DisplayMember = "SubjectName";
            lstSubjects.ValueMember = "SubjectName";
            lstSubjects.DataSource = table;
        }

        private static DataTable BuildSubjectsTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("SubjectName", typeof(string));

            foreach (string subject in SubjectGradeRepository.GetAllSubjectNames())
                table.Rows.Add(subject);

            return table;
        }

        private void btnAddSubject_Click(object sender, EventArgs e)
        {
            using (var form = new FormSubject())
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                    LoadSubjects();
            }
        }

        private void btnDeleteSubject_Click(object sender, EventArgs e)
        {
            string subjectName = GetSelectedSubjectName();
            if (string.IsNullOrWhiteSpace(subjectName))
            {
                MessageBox.Show("Выберите предмет для удаления.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int gradeCount = _subjectService.CountGrades(subjectName);

            if (gradeCount > 0)
            {
                MessageBox.Show(
                    "Нельзя удалить предмет, пока он используется в журнале оценок.",
                    Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (!_subjectService.HasCatalogTable())
            {
                MessageBox.Show("Справочник предметов ещё не создан в базе данных.", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!_subjectService.ExistsInCatalog(subjectName))
            {
                MessageBox.Show(
                    "Этот предмет отсутствует в справочнике и не может быть удалён отсюда.",
                    Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Удалить предмет \"{subjectName}\"?",
                Text,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes)
                return;

            _subjectService.DeleteFromCatalog(subjectName);
            LoadSubjects();

            MessageBox.Show("Предмет удалён.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private string GetSelectedSubjectName()
        {
            if (lstSubjects.SelectedItem is DataRowView rowView)
                return Convert.ToString(rowView["SubjectName"]);

            return lstSubjects.SelectedItem?.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
