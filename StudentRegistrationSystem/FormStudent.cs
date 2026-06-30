using StudentRegistrationSystem.Models;
using StudentRegistrationSystem.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace StudentRegistrationSystem
{
    public partial class FormStudent : Form
    {
        private const int MinimumStudentAge = 15;
        private int? _pendingGroupId;
        private int? _pendingSemesterId;
        private List<string> _availableSubjects = new List<string>();

        public Student StudentData { get; private set; }
        public event Action<Student> StudentSaved;

        public FormStudent(int? semesterId = null)
        {
            InitializeComponent();
            _pendingSemesterId = semesterId;
            ConfigureForCreate();
            InitializeFormCore(new Student());
        }

        public FormStudent(Student student, int? semesterId = null)
        {
            InitializeComponent();
            if (student == null)
                throw new ArgumentNullException(nameof(student));

            _pendingSemesterId = semesterId;
            ConfigureForEdit(student);
            InitializeFormCore(student);
        }

        private void ConfigureForCreate()
        {
            Text = UiCaptions.NewStudent;
            lblFormTitle.Text = UiCaptions.NewStudent;
            lblFormSubtitle.Text = "ФИО и группа обязательны. Предметные оценки задаются в таблице, дату можно выбрать из календаря.";
            lblSubjectGrades.Text = "Предмет, балл и дата";
            btnSave.Text = "Добавить студента";
            dtpBirthDate.Checked = false;
        }

        private void ConfigureForEdit(Student student)
        {
            Text = UiCaptions.EditStudent;
            lblFormTitle.Text = UiCaptions.EditStudent;
            lblFormSubtitle.Text = "Изменяйте предметные оценки здесь: каждая строка может содержать дату из календаря для попадания в журнал.";
            lblSubjectGrades.Text = "Предмет, балл и дата";
            btnSave.Text = "Сохранить изменения";

            StudentData = student;
            _pendingGroupId = student.GroupId;
            txtFullName.Text = student.FullName ?? string.Empty;

            if (student.BirthDate.HasValue)
            {
                dtpBirthDate.Checked = true;
                dtpBirthDate.Value = student.BirthDate.Value;
            }
            else
            {
                dtpBirthDate.Checked = false;
            }

            _pendingSemesterId = student.SubjectGradeEntries
                .Where(e => e != null && e.SemesterId.HasValue)
                .Select(e => e.SemesterId)
                .FirstOrDefault() ?? _pendingSemesterId;
        }

        private void InitializeFormCore(Student student)
        {
            DateTime latestBirthDate = DateTime.Today.AddYears(-MinimumStudentAge);
            if (latestBirthDate < dtpBirthDate.MinDate)
                latestBirthDate = dtpBirthDate.MinDate;

            dtpBirthDate.MaxDate = latestBirthDate;
            if (!dtpBirthDate.Checked)
                dtpBirthDate.Value = latestBirthDate;

            KeyPreview = true;
            KeyDown += FormStudent_KeyDown;
            dgvSubjectGrades.KeyDown += DgvSubjectGrades_KeyDown;
            dgvSubjectGrades.DataError += DgvSubjectGrades_DataError;
            btnAddSubjectRow.Click += (_, __) => AddSubjectRow();
            btnRemoveSubjectRow.Click += (_, __) => RemoveSelectedSubjectRow();
            Shown += FormStudent_Shown;

            LoadAvailableSubjects();
            InitSubjectGradesGrid();
            LoadSubjectGrid(student);
            LoadGroups();
            LoadSemesters();
            ApplyThemeRecursive(this);
        }

        private void FormStudent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                e.SuppressKeyPress = true;
                btnSave.PerformClick();
            }
        }

        private void DgvSubjectGrades_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                RemoveSelectedSubjectRow();
                e.Handled = true;
            }
        }

        private void DgvSubjectGrades_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
            e.Cancel = false;
        }

        private void LoadAvailableSubjects()
        {
            _availableSubjects = SubjectGradeRepository.GetAllSubjectNames();
        }

        private void InitSubjectGradesGrid()
        {
            dgvSubjectGrades.AutoGenerateColumns = false;
            dgvSubjectGrades.RowTemplate.Height = 34;
            dgvSubjectGrades.BorderStyle = BorderStyle.None;
            dgvSubjectGrades.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
            dgvSubjectGrades.Columns.Clear();

            var subjectColumn = new DataGridViewComboBoxColumn
            {
                Name = "Subject",
                HeaderText = "Предмет",
                SortMode = DataGridViewColumnSortMode.NotSortable,
                DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton,
                DisplayStyleForCurrentCellOnly = false,
                FlatStyle = FlatStyle.Flat,
                DataSource = _availableSubjects.ToList(),
                MinimumWidth = 210,
                FillWeight = 48
            };

            var gradeColumn = new DataGridViewTextBoxColumn
            {
                Name = "Grade",
                HeaderText = "Балл",
                SortMode = DataGridViewColumnSortMode.NotSortable,
                Width = 90,
                FillWeight = 18
            };

            var dateColumn = new DataGridViewCalendarColumn
            {
                Name = "GradeDate",
                HeaderText = "Дата",
                Width = 140,
                FillWeight = 34
            };

            dgvSubjectGrades.Columns.Add(subjectColumn);
            dgvSubjectGrades.Columns.Add(gradeColumn);
            dgvSubjectGrades.Columns.Add(dateColumn);
            dgvSubjectGrades.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSubjectGrades.Columns["Grade"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvSubjectGrades.Columns["GradeDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        }

        private void LoadSubjectGrid(Student student)
        {
            dgvSubjectGrades.Rows.Clear();
            if (student?.SubjectGradeEntries == null || student.SubjectGradeEntries.Count == 0)
                return;

            foreach (SubjectGradeEntry entry in student.SubjectGradeEntries
                         .OrderBy(e => e.Subject)
                         .ThenBy(e => e.GradeDate ?? DateTime.MaxValue))
            {
                AddSubjectRow(entry.Subject, entry.Grade, entry.GradeDate);
            }
        }

        private void AddSubjectRow(string subject = null, object grade = null, DateTime? gradeDate = null)
        {
            if (_availableSubjects.Count == 0)
            {
                MessageBox.Show(
                    "Сначала добавьте хотя бы один предмет в справочник предметов.",
                    UiCaptions.StudentCard,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            int rowIndex = dgvSubjectGrades.Rows.Add();
            if (rowIndex < 0)
                return;

            DataGridViewRow row = dgvSubjectGrades.Rows[rowIndex];
            string selectedSubject = string.IsNullOrWhiteSpace(subject) ? _availableSubjects[0] : subject.Trim();
            if (!_availableSubjects.Contains(selectedSubject, StringComparer.OrdinalIgnoreCase))
                selectedSubject = _availableSubjects[0];

            row.Cells["Subject"].Value = selectedSubject;
            row.Cells["Grade"].Value = grade;
            row.Cells["GradeDate"].Value = gradeDate.HasValue ? (object)gradeDate.Value.Date : null;
            dgvSubjectGrades.CurrentCell = row.Cells["Subject"];
        }

        private void RemoveSelectedSubjectRow()
        {
            if (dgvSubjectGrades.CurrentRow == null)
                return;

            int index = dgvSubjectGrades.CurrentRow.Index;
            if (index >= 0 && index < dgvSubjectGrades.Rows.Count)
                dgvSubjectGrades.Rows.RemoveAt(index);
        }

        private bool TryBuildSubjectGradeEntries(out List<SubjectGradeEntry> entries, out string error)
        {
            entries = new List<SubjectGradeEntry>();
            error = null;

            foreach (DataGridViewRow row in dgvSubjectGrades.Rows)
            {
                if (row.IsNewRow)
                    continue;

                string subject = row.Cells["Subject"].Value?.ToString()?.Trim();
                string rawGrade = row.Cells["Grade"].Value?.ToString()?.Trim();
                object rawDateValue = row.Cells["GradeDate"].Value;
                bool emptySubject = string.IsNullOrWhiteSpace(subject);
                bool emptyGrade = string.IsNullOrWhiteSpace(rawGrade);
                bool emptyDate = rawDateValue == null
                    || rawDateValue == DBNull.Value
                    || string.IsNullOrWhiteSpace(rawDateValue.ToString());

                if (emptySubject && emptyGrade && emptyDate)
                    continue;

                if (emptySubject)
                {
                    error = "Выберите предмет для каждой заполненной строки.";
                    return false;
                }

                if (emptyGrade)
                {
                    error = $"Укажите балл для предмета «{subject}».";
                    return false;
                }

                if (!int.TryParse(rawGrade, NumberStyles.Integer, CultureInfo.InvariantCulture, out int grade))
                {
                    error = "Балл должен быть целым числом.";
                    return false;
                }

                if (grade < 0 || grade > 100)
                {
                    error = "Балл должен быть от 0 до 100.";
                    return false;
                }

                DateTime? gradeDate = null;
                if (!emptyDate)
                {
                    if (rawDateValue is DateTime dateTime)
                        gradeDate = dateTime.Date;
                    else if (DateTime.TryParse(rawDateValue.ToString(), CultureInfo.GetCultureInfo("ru-RU"), DateTimeStyles.None, out DateTime parsedDate)
                          || DateTime.TryParse(rawDateValue.ToString(), CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
                        gradeDate = parsedDate.Date;
                    else
                    {
                        error = $"Не удалось прочитать дату для предмета «{subject}».";
                        return false;
                    }
                }

                entries.Add(new SubjectGradeEntry
                {
                    Subject = subject,
                    Grade = grade,
                    GradeDate = gradeDate
                });
            }

            return true;
        }

        private void FormStudent_Shown(object sender, EventArgs e)
        {
            Shown -= FormStudent_Shown;
            BeginInvoke(new Action(() =>
            {
                panelBody.AutoScrollPosition = new Point(0, 0);
                txtFullName.Focus();
                txtFullName.SelectAll();
            }));

            if (_pendingGroupId.HasValue)
            {
                SelectGroupById(_pendingGroupId.Value);
                _pendingGroupId = null;
            }

            if (_pendingSemesterId.HasValue)
            {
                SelectSemesterById(_pendingSemesterId.Value);
                _pendingSemesterId = null;
            }
        }

        private static void SelectGroupById(ComboBox box, int groupId)
        {
            if (box?.DataSource == null)
                return;

            foreach (object item in box.Items)
            {
                if (item is DataRowView drv && Convert.ToInt32(drv["Id"]) == groupId)
                {
                    box.SelectedItem = item;
                    return;
                }
            }
        }

        private void SelectGroupById(int groupId)
        {
            SelectGroupById(cmbGroup, groupId);
        }

        private void SelectSemesterById(int semesterId)
        {
            if (cmbSemester.DataSource == null)
                return;

            foreach (object item in cmbSemester.Items)
            {
                if (item is SemesterInfo semester && semester.Id == semesterId)
                {
                    cmbSemester.SelectedItem = item;
                    return;
                }
            }
        }

        private void LoadGroups()
        {
            DataTable table = DatabaseHelper.ExecuteQuery(@"
SELECT Id, GroupName
FROM Groups
ORDER BY
    CASE
        WHEN CHARINDEX('-', GroupName) > 0
             AND ISNUMERIC(SUBSTRING(GroupName, CHARINDEX('-', GroupName) + 1, 50)) = 1
            THEN CONVERT(INT, SUBSTRING(GroupName, CHARINDEX('-', GroupName) + 1, 50))
        WHEN ISNUMERIC(GroupName) = 1
            THEN CONVERT(INT, GroupName)
        ELSE 2147483647
    END,
    GroupName");
            cmbGroup.DataSource = table;
            cmbGroup.DisplayMember = "GroupName";
            cmbGroup.ValueMember = "Id";
        }

        private void LoadSemesters()
        {
            List<SemesterInfo> semesters = SemesterService.GetAllSemesters();
            cmbSemester.DataSource = semesters;
            cmbSemester.DisplayMember = "Name";

            int semesterId = _pendingSemesterId ?? SemesterService.GetDefaultSemesterId();
            SelectSemesterById(semesterId);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtFullName.Text.Trim();
                if (string.IsNullOrWhiteSpace(name))
                {
                    MessageBox.Show("Введите ФИО.", UiCaptions.StudentCard, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string[] parts = name.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length < 2)
                {
                    MessageBox.Show("Введите минимум фамилию и имя.", UiCaptions.StudentCard, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                foreach (char symbol in name)
                {
                    if (!char.IsLetter(symbol) && !char.IsWhiteSpace(symbol) && symbol != '-')
                    {
                        MessageBox.Show("В ФИО можно использовать только буквы, пробелы и дефис.", UiCaptions.StudentCard, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                for (int i = 0; i < parts.Length; i++)
                    parts[i] = char.ToUpper(parts[i][0]) + parts[i].Substring(1).ToLower();
                name = string.Join(" ", parts);

                if (cmbGroup.SelectedIndex < 0 || cmbGroup.SelectedItem == null)
                {
                    MessageBox.Show("Выберите учебную группу.", UiCaptions.StudentCard, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int groupId = Convert.ToInt32(((DataRowView)cmbGroup.SelectedItem)["Id"]);
                const string checkQuery = @"
SELECT COUNT(*)
FROM Students
WHERE LOWER(LTRIM(RTRIM(FullName))) = @FullName
  AND GroupId = @GroupId
  AND Id != @StudentId";

                int count = Convert.ToInt32(DatabaseHelper.ExecuteScalar(
                    checkQuery,
                    new SqlParameter("@FullName", SqlDbType.NVarChar, 100) { Value = name.ToLowerInvariant() },
                    new SqlParameter("@GroupId", SqlDbType.Int) { Value = groupId },
                    new SqlParameter("@StudentId", SqlDbType.Int) { Value = StudentData?.Id ?? 0 }));
                if (count > 0)
                {
                    MessageBox.Show("Студент с таким ФИО уже есть в этой группе.", UiCaptions.StudentCard, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!TryBuildSubjectGradeEntries(out List<SubjectGradeEntry> entries, out string entriesError))
                {
                    MessageBox.Show(entriesError, UiCaptions.StudentCard, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (entries.Count == 0)
                {
                    MessageBox.Show("Добавьте хотя бы одну строку с предметом и баллом.", UiCaptions.StudentCard, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!(cmbSemester.SelectedItem is SemesterInfo selectedSemester))
                {
                    MessageBox.Show("Выберите учебный семестр.", UiCaptions.StudentCard, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                foreach (SubjectGradeEntry entry in entries)
                    entry.SemesterId = selectedSemester.Id;

                DateTime? birthDate = dtpBirthDate.Checked ? dtpBirthDate.Value.Date : (DateTime?)null;
                if (birthDate.HasValue && birthDate.Value > DateTime.Today.AddYears(-MinimumStudentAge))
                {
                    MessageBox.Show($"Студент должен быть не младше {MinimumStudentAge} лет.", UiCaptions.StudentCard, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var tempStudent = new Student
                {
                    Id = StudentData?.Id ?? 0,
                    FullName = name,
                    GroupId = groupId,
                    BirthDate = birthDate,
                    Grades = new List<int>(),
                    SubjectGradeEntries = entries
                };

                if (!tempStudent.Validate(out string error))
                {
                    MessageBox.Show(error, UiCaptions.StudentCard, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                StudentData = tempStudent;
                StudentSaved?.Invoke(StudentData);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сохранить данные: " + ex.Message, UiCaptions.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyThemeRecursive(Control parent)
        {
            parent.BackColor = Theme.BackColor;
            parent.ForeColor = Theme.ForeColor;

            foreach (Control control in parent.Controls)
            {
                switch (control)
                {
                    case Button btn:
                        if (btn == btnSave)
                            Theme.StylePrimaryButton(btn);
                        else
                            Theme.StyleSecondaryButton(btn);

                        btn.MouseEnter -= BtnThemeEnter;
                        btn.MouseLeave -= BtnThemeLeave;
                        btn.MouseEnter += BtnThemeEnter;
                        btn.MouseLeave += BtnThemeLeave;
                        Theme.ApplyRoundedRegion(btn, 16);
                        break;

                    case TextBox textBox:
                        Theme.StyleTextBox(textBox);
                        break;

                    case ComboBox comboBox:
                        Theme.StyleComboBox(comboBox);
                        break;

                    case DateTimePicker picker:
                        picker.CalendarMonthBackground = Theme.IsDarkTheme ? Theme.GridColor : Color.White;
                        picker.CalendarForeColor = Theme.ForeColor;
                        picker.BackColor = Theme.IsDarkTheme ? Theme.GridColor : Color.White;
                        picker.ForeColor = Theme.ForeColor;
                        break;

                    case Label label:
                        label.ForeColor = label.Name == "lblFormSubtitle"
                            ? Theme.MutedTextColor
                            : Theme.ForeColor;
                        label.BackColor = label.Parent != null ? label.Parent.BackColor : Theme.BackColor;
                        break;

                    case DataGridView grid:
                        Theme.StyleDataGrid(grid);
                        grid.DefaultCellStyle.Padding = new Padding(4, 2, 4, 2);
                        break;

                    case TableLayoutPanel tableLayout:
                        tableLayout.BackColor = Theme.BackColor;
                        break;

                    case Panel panel:
                        panel.BackColor = panel.Name == "panelHeader" || panel.Name == "panelFooter"
                            ? Theme.PanelColor
                            : Theme.BackColor;
                        if (panel == panelFioBlock)
                            Theme.StyleCard(panel, 18);
                        break;
                }

                if (control.HasChildren)
                    ApplyThemeRecursive(control);
            }
        }

        private void BtnThemeEnter(object sender, EventArgs e)
        {
            if (sender is Button button)
                button.BackColor = button == btnSave
                    ? ControlPaint.Light(Theme.AccentColor)
                    : ControlPaint.Light(Theme.ButtonColor);
        }

        private void BtnThemeLeave(object sender, EventArgs e)
        {
            if (sender is Button button)
                button.BackColor = button == btnSave ? Theme.AccentColor : Theme.ButtonColor;
        }
    }
}
