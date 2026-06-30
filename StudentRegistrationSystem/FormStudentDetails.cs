using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using StudentRegistrationSystem.Services;

namespace StudentRegistrationSystem
{
    public partial class FormStudentDetails : Form
    {
        private int _studentId;
        private int _selectedSemesterId;
        private bool _extendedJournal;
        private bool _calendarBusy;
        private Dictionary<string, int> _subjectSummaryGrades = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        private Dictionary<string, double> _journalSubjectAverages = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase);

        private sealed class MonthOption
        {
            public DateTime FirstDay { get; }

            public MonthOption(DateTime firstDay)
            {
                FirstDay = new DateTime(firstDay.Year, firstDay.Month, 1);
            }

            public override string ToString()
            {
                return FirstDay.ToString("MMMM yyyy", CultureInfo.GetCultureInfo("ru-RU"));
            }
        }

        public FormStudentDetails(int studentId, int? semesterId = null)
        {
            InitializeComponent();
            Text = UiCaptions.StudentGradesJournal;
            _studentId = studentId;
            _selectedSemesterId = semesterId ?? SemesterService.GetDefaultSemesterId();
            FormBorderStyle = FormBorderStyle.Sizable;
        }

        protected override void WndProc(ref Message m)
        {
            if (FormMaximizeHelper.TryHandleMaximizeToLimitedSize(this, ref m))
                return;

            base.WndProc(ref m);
        }

        private void listGrades_DrawItem(object sender, DrawItemEventArgs e)
        {
            ListBoxOwnerDrawHelper.DrawCenteredItem(listGrades, e);
        }

        private void FormStudentDetails_Load(object sender, EventArgs e)
        {
            ApplyTheme();
            InitCalendarGridChrome();

            _extendedJournal = SubjectGradeRepository.HasExtendedGradesSchema();
            if (!_extendedJournal)
            {
                lblJournalHint.Text =
                    "Журнал по предметам недоступен: выполните скрипт Sql\\SubjectsAndGradesExtension.sql в SQL Server, затем перезапустите приложение.";
                panelFilters.Enabled = false;
                dgvCalendar.Enabled = false;
            }
            else
            {
                btnAddSubject.Visible = false;
                btnAddSubject.Enabled = false;
                dgvCalendar.ReadOnly = false;
                lblJournalHint.Text = "Оценки можно редактировать прямо в журнале. В одной ячейке разрешено несколько баллов через запятую.";
                LoadSemesters();
                FillMonthCombo();
                LoadSubjectsCombo();
                cmbSubject.SelectedIndexChanged += (_, __) =>
                {
                    RebuildCalendar();
                    UpdateSelectedSubjectSummary();
                };
                cmbSemester.SelectedIndexChanged += (_, __) =>
                {
                    if (cmbSemester.SelectedItem is SemesterInfo semester)
                    {
                        _selectedSemesterId = semester.Id;
                        LoadStudentData();
                    }
                };
                cmbMonth.SelectedIndexChanged += (_, __) => RebuildCalendar();
            }

            LoadStudentData();
        }

        private void InitCalendarGridChrome()
        {
            dgvCalendar.AutoGenerateColumns = false;
            dgvCalendar.ColumnHeadersVisible = true;
            dgvCalendar.RowHeadersVisible = true;
            dgvCalendar.AllowUserToResizeColumns = false;
            dgvCalendar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvCalendar.ScrollBars = ScrollBars.Both;
            dgvCalendar.ColumnHeadersHeight = 36;
            dgvCalendar.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvCalendar.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvCalendar.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCalendar.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCalendar.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCalendar.DefaultCellStyle.Padding = new Padding(2);
            Theme.SetControlDoubleBuffered(dgvCalendar);
        }

        private void ApplyTheme()
        {
            BackColor = Theme.BackColor;
            ForeColor = Theme.ForeColor;
            tableLayoutPanel1.BackColor = Theme.BackColor;

            lblName.ForeColor = Theme.ForeColor;
            lblGroup.ForeColor = Theme.IsDarkTheme ? Color.LightGray : Color.DimGray;
            lblBirthDate.ForeColor = Theme.MutedTextColor;
            lblSubjects.ForeColor = Theme.MutedTextColor;
            lblAverage1.ForeColor = Theme.IsDarkTheme ? Color.LightGreen : Color.DarkGreen;
            lblSubject.ForeColor = Theme.ForeColor;
            lblSemester.ForeColor = Theme.ForeColor;
            lblMonth.ForeColor = Theme.ForeColor;
            lblJournalHint.ForeColor = Theme.IsDarkTheme ? Color.LightGray : Color.DimGray;
            lblSubjectSummary.ForeColor = Theme.AccentColor;
            lblSubjectSummary.Font = new Font("Segoe UI Semibold", 10.5f, FontStyle.Bold);

            cmbSubject.BackColor = Theme.GridColor;
            cmbSubject.ForeColor = Theme.ForeColor;
            cmbSemester.BackColor = Theme.GridColor;
            cmbSemester.ForeColor = Theme.ForeColor;
            cmbMonth.BackColor = Theme.GridColor;
            cmbMonth.ForeColor = Theme.ForeColor;

            btnAddSubject.BackColor = Theme.ButtonColor;
            btnAddSubject.ForeColor = Theme.ForeColor;
            btnAddSubject.FlatStyle = FlatStyle.Flat;
            btnAddSubject.FlatAppearance.BorderSize = 0;

            listGrades.BackColor = Theme.GridColor;
            listGrades.ForeColor = Theme.ForeColor;
            listGrades.BorderStyle = BorderStyle.None;
            Theme.SetControlDoubleBuffered(listGrades);

            dgvCalendar.BackgroundColor = Theme.GridColor;
            dgvCalendar.GridColor = Theme.IsDarkTheme ? Color.FromArgb(60, 60, 60) : Color.LightSteelBlue;
            dgvCalendar.BorderStyle = BorderStyle.FixedSingle;
            dgvCalendar.EnableHeadersVisualStyles = false;
            dgvCalendar.ColumnHeadersDefaultCellStyle.BackColor = Theme.IsDarkTheme ? Color.FromArgb(55, 75, 95) : Color.FromArgb(200, 220, 245);
            dgvCalendar.ColumnHeadersDefaultCellStyle.ForeColor = Theme.ForeColor;
            dgvCalendar.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            dgvCalendar.RowHeadersDefaultCellStyle.BackColor = Theme.PanelColor;
            dgvCalendar.RowHeadersDefaultCellStyle.ForeColor = Theme.ForeColor;
            dgvCalendar.RowHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.75f, FontStyle.Bold);
            dgvCalendar.DefaultCellStyle.BackColor = Theme.GridColor;
            dgvCalendar.DefaultCellStyle.ForeColor = Theme.ForeColor;
            dgvCalendar.DefaultCellStyle.Font = new Font("Segoe UI", 10.5f, FontStyle.Regular);
            dgvCalendar.DefaultCellStyle.SelectionBackColor = Theme.AccentColor;
            dgvCalendar.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvCalendar.AlternatingRowsDefaultCellStyle.BackColor = Theme.IsDarkTheme
                ? Color.FromArgb(40, 45, 52)
                : Color.FromArgb(245, 248, 253);
            dgvCalendar.AlternatingRowsDefaultCellStyle.ForeColor = Theme.ForeColor;
        }

        private void FillMonthCombo()
        {
            cmbMonth.Items.Clear();
            DateTime today = DateTime.Today;
            for (int i = 0; i < 24; i++)
            {
                DateTime month = today.AddMonths(-i);
                if (month.Month >= 6 && month.Month <= 8)
                    continue;

                cmbMonth.Items.Add(new MonthOption(month));
            }

            if (cmbMonth.Items.Count > 0)
                cmbMonth.SelectedIndex = 0;
        }

        private void LoadSubjectsCombo()
        {
            try
            {
                List<string> names = SubjectGradeRepository.GetAllSubjectNames();
                cmbSubject.DataSource = null;
                cmbSubject.Items.Clear();
                foreach (string name in names)
                    cmbSubject.Items.Add(name);

                if (cmbSubject.Items.Count > 0)
                    cmbSubject.SelectedIndex = 0;
            }
            catch
            {
                cmbSubject.DataSource = null;
                cmbSubject.Items.Clear();
            }
        }

        private string GetSelectedSubjectName()
        {
            return cmbSubject.SelectedItem?.ToString();
        }

        private void LoadSemesters()
        {
            List<SemesterInfo> semesters = SemesterService.GetAllSemesters();
            cmbSemester.DataSource = semesters;
            cmbSemester.DisplayMember = "Name";

            SemesterInfo selected = semesters.FirstOrDefault(s => s.Id == _selectedSemesterId)
                ?? semesters.FirstOrDefault();

            if (selected != null)
            {
                _selectedSemesterId = selected.Id;
                cmbSemester.SelectedItem = selected;
            }
        }

        private void RebuildCalendar()
        {
            if (!_extendedJournal || _calendarBusy)
                return;

            string subjectName = GetSelectedSubjectName();
            if (string.IsNullOrWhiteSpace(subjectName) || cmbMonth.SelectedItem == null)
                return;

            MonthOption month = (MonthOption)cmbMonth.SelectedItem;
            int year = month.FirstDay.Year;
            int monthNumber = month.FirstDay.Month;
            int days = DateTime.DaysInMonth(year, monthNumber);
            List<DateTime> studyDays = new List<DateTime>();

            for (int day = 1; day <= days; day++)
            {
                DateTime current = new DateTime(year, monthNumber, day);
                if (current.DayOfWeek == DayOfWeek.Saturday || current.DayOfWeek == DayOfWeek.Sunday)
                    continue;

                studyDays.Add(current);
            }

            _calendarBusy = true;
            dgvCalendar.SuspendLayout();
            try
            {
                dgvCalendar.Columns.Clear();
                dgvCalendar.Rows.Clear();
                dgvCalendar.ColumnCount = studyDays.Count;
                dgvCalendar.RowCount = 2;

                dgvCalendar.Rows[0].HeaderCell.Value = "Дата";
                dgvCalendar.Rows[1].HeaderCell.Value = "Балл";

                Dictionary<int, List<int>> grades = SubjectGradeRepository.GetGradesForMonth(_studentId, subjectName, year, monthNumber, _selectedSemesterId);

                for (int index = 0; index < studyDays.Count; index++)
                {
                    DateTime currentDate = studyDays[index];
                    DataGridViewColumn column = dgvCalendar.Columns[index];
                    column.Name = "d" + currentDate.Day;
                    column.HeaderText = currentDate.ToString("dd.MM", CultureInfo.InvariantCulture);
                    column.Width = 58;
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;

                    dgvCalendar.Rows[0].Cells[index].Value = currentDate.ToString("dd.MM", CultureInfo.InvariantCulture);
                    dgvCalendar.Rows[0].Cells[index].ReadOnly = true;
                    dgvCalendar.Rows[0].Cells[index].Style.BackColor = Theme.IsDarkTheme
                        ? Color.FromArgb(50, 55, 60)
                        : Color.FromArgb(235, 242, 252);
                    dgvCalendar.Rows[0].Cells[index].Style.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);

                    List<int> dayGrades = grades[currentDate.Day];
                    dgvCalendar.Rows[1].Cells[index].Value = dayGrades.Count > 0
                        ? string.Join(", ", dayGrades)
                        : "";
                    dgvCalendar.Rows[1].Cells[index].Style.Font = new Font("Segoe UI", 10.5f, FontStyle.Regular);
                    dgvCalendar.Rows[1].Cells[index].Tag = currentDate.Day;
                }

                dgvCalendar.Rows[0].Height = 34;
                dgvCalendar.Rows[1].Height = 40;
            }
            finally
            {
                dgvCalendar.ResumeLayout();
                _calendarBusy = false;
            }
        }

        private void dgvCalendar_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (!_extendedJournal || e.RowIndex != 1 || e.ColumnIndex < 0)
                return;

            string subjectName = GetSelectedSubjectName();
            if (string.IsNullOrWhiteSpace(subjectName) || cmbMonth.SelectedItem == null)
                return;

            MonthOption month = (MonthOption)cmbMonth.SelectedItem;
            int year = month.FirstDay.Year;
            int monthNumber = month.FirstDay.Month;
            object dayTag = dgvCalendar.Rows[1].Cells[e.ColumnIndex].Tag;
            int day = dayTag == null ? 0 : Convert.ToInt32(dayTag);
            if (day <= 0)
            {
                RebuildCalendar();
                return;
            }

            string raw = dgvCalendar.Rows[1].Cells[e.ColumnIndex].Value?.ToString()?.Trim();
            List<int> grades = new List<int>();
            if (!string.IsNullOrEmpty(raw))
            {
                string[] parts = raw
                    .Split(new[] { ',', ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string part in parts)
                {
                    if (!int.TryParse(part, NumberStyles.Integer, CultureInfo.InvariantCulture, out int value) || value < 0 || value > 100)
                    {
                        MessageBox.Show(
                            "Введите целые числа от 0 до 100. Несколько оценок указывайте через запятую.",
                            UiCaptions.StudentGradesJournal,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        RebuildCalendar();
                        return;
                    }

                    grades.Add(value);
                }
            }

            try
            {
                SubjectGradeRepository.SetGradesForDay(_studentId, subjectName, year, monthNumber, day, grades, _selectedSemesterId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Не удалось сохранить оценку: " + ex.Message,
                    UiCaptions.Error,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            RebuildCalendar();
            LoadStudentData();
        }

        private void btnAddSubject_Click(object sender, EventArgs e)
        {
            if (!_extendedJournal)
            {
                MessageBox.Show(
                    "Сначала выполните SQL-скрипт расширения базы (папка Sql).",
                    UiCaptions.StudentGradesJournal,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            using (FormSubject form = new FormSubject())
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                    LoadSubjectsCombo();
            }
        }

        private void UpdateSelectedSubjectSummary()
        {
            string subjectName = GetSelectedSubjectName();
            if (string.IsNullOrWhiteSpace(subjectName))
            {
                lblSubjectSummary.Text = "Сводный балл: нет";
                return;
            }

            if (_subjectSummaryGrades.TryGetValue(subjectName, out int summaryGrade))
            {
                lblSubjectSummary.Text = $"Сводный балл: {summaryGrade}";
                return;
            }

            if (_journalSubjectAverages.TryGetValue(subjectName, out double journalAverage))
            {
                lblSubjectSummary.Text = $"Среднее по журналу: {journalAverage:F1}";
                return;
            }

            lblSubjectSummary.Text = "Сводный балл: нет";
        }

        private static bool IsLegacyRow(DataRow row, DataTable table)
        {
            string subject = row["Subject"] == DBNull.Value
                ? ""
                : row["Subject"].ToString().Trim();
            bool hasDate = table.Columns.Contains("GradeDate") && row["GradeDate"] != DBNull.Value;

            if (hasDate)
                return false;

            if (string.IsNullOrEmpty(subject))
                return true;

            return string.Equals(subject, StudentService.LegacyGeneralSubject, StringComparison.OrdinalIgnoreCase)
                || string.Equals(subject, "Зачётная (общая)", StringComparison.OrdinalIgnoreCase);
        }

        private void LoadStudentData()
        {
            const string query = @"
                SELECT s.FullName, s.BirthDate, g.GroupName, gr.Grade, gr.Subject, gr.GradeDate, gr.SemesterId
                FROM Students s
                JOIN Groups g ON s.GroupId = g.Id
                LEFT JOIN Grades gr ON s.Id = gr.StudentId
                WHERE s.Id = @StudentId
                  AND (gr.SemesterId = @SemesterId OR gr.SemesterId IS NULL)";

            DataTable table;
            try
            {
                table = DatabaseHelper.ExecuteQuery(
                    query,
                    new SqlParameter("@StudentId", SqlDbType.Int) { Value = _studentId },
                    new SqlParameter("@SemesterId", SqlDbType.Int) { Value = _selectedSemesterId });
            }
            catch
            {
                const string legacyQuery = @"
                    SELECT s.FullName, s.BirthDate, g.GroupName, gr.Grade, gr.Subject
                    FROM Students s
                    JOIN Groups g ON s.GroupId = g.Id
                    LEFT JOIN Grades gr ON s.Id = gr.StudentId
                    WHERE s.Id = @StudentId";
                table = DatabaseHelper.ExecuteQuery(
                    legacyQuery,
                    new SqlParameter("@StudentId", SqlDbType.Int) { Value = _studentId });
            }

            if (table.Rows.Count == 0)
            {
                MessageBox.Show("Данные о студенте не найдены.", UiCaptions.Attention, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
                return;
            }

            lblName.Text = DisplayTextHelper.SanitizeNameForGrid(table.Rows[0]["FullName"]);
            lblGroup.Text = "Группа: " + DisplayTextHelper.SanitizeNameForGrid(table.Rows[0]["GroupName"]);
            lblBirthDate.Text = table.Columns.Contains("BirthDate") && table.Rows[0]["BirthDate"] != DBNull.Value
                ? "Дата рождения: " + Convert.ToDateTime(table.Rows[0]["BirthDate"]).ToString("dd.MM.yyyy", CultureInfo.InvariantCulture)
                : "Дата рождения: не указана";

            SemesterInfo semester = SemesterService.GetSemesterById(_selectedSemesterId);
            if (semester != null)
                lblJournalHint.Text = "Период: " + semester.Name + ". Оценки можно редактировать прямо в журнале.";

            listGrades.Items.Clear();
            _subjectSummaryGrades = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            Dictionary<string, List<int>> datedSubjectGrades = new Dictionary<string, List<int>>(StringComparer.OrdinalIgnoreCase);
            List<int> legacyGrades = new List<int>();
            List<string> datedEntries = new List<string>();

            foreach (DataRow row in table.Rows)
            {
                if (row["Grade"] == DBNull.Value)
                    continue;

                int grade = Convert.ToInt32(row["Grade"]);
                bool isLegacy = IsLegacyRow(row, table);

                if (isLegacy)
                {
                    legacyGrades.Add(grade);
                    continue;
                }

                string subject = row["Subject"] == DBNull.Value ? "Предмет" : row["Subject"].ToString().Trim();
                bool hasDate = table.Columns.Contains("GradeDate") && row["GradeDate"] != DBNull.Value;

                if (hasDate)
                {
                    if (!datedSubjectGrades.TryGetValue(subject, out List<int> grades))
                    {
                        grades = new List<int>();
                        datedSubjectGrades[subject] = grades;
                    }

                    grades.Add(grade);
                    string datePart = Convert.ToDateTime(row["GradeDate"]).ToString("dd.MM.yyyy", CultureInfo.InvariantCulture);
                    datedEntries.Add($"{subject} | {datePart} | {grade}");
                }
                else
                {
                    _subjectSummaryGrades[subject] = grade;
                }
            }

            foreach (KeyValuePair<string, int> summary in _subjectSummaryGrades.OrderBy(x => x.Key))
                listGrades.Items.Add($"{summary.Key} | сводный балл: {summary.Value}");

            foreach (string entry in datedEntries.OrderBy(x => x))
                listGrades.Items.Add(entry);

            foreach (int legacyGrade in legacyGrades)
                listGrades.Items.Add("Общая оценка: " + legacyGrade);

            var subjects = _subjectSummaryGrades.Keys
                .Concat(datedSubjectGrades.Keys)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(x => x)
                .ToList();

            lblSubjects.Text = subjects.Count > 0
                ? "Предметы: " + string.Join(", ", subjects)
                : "Предметы: не указаны";

            _journalSubjectAverages = datedSubjectGrades.ToDictionary(
                pair => pair.Key,
                pair => pair.Value.Average(),
                StringComparer.OrdinalIgnoreCase);

            double average;
            if (_subjectSummaryGrades.Count > 0)
            {
                average = _subjectSummaryGrades.Values.Average();
            }
            else if (_journalSubjectAverages.Count > 0)
            {
                average = _journalSubjectAverages.Values.Average();
            }
            else if (legacyGrades.Count > 0)
            {
                average = legacyGrades.Average();
            }
            else
            {
                average = 0;
            }

            lblAverage1.Text = $"Средний балл: {average:F2}";

            if (Theme.IsDarkTheme)
            {
                lblAverage1.ForeColor = average < 50
                    ? Color.Red
                    : average < 75
                        ? Color.Gold
                        : Color.LightGreen;
            }
            else
            {
                lblAverage1.ForeColor = average < 50
                    ? Color.DarkRed
                    : average < 75
                        ? Color.DarkGoldenrod
                        : Color.DarkGreen;
            }

            UpdateSelectedSubjectSummary();

            if (_extendedJournal && cmbSubject.Items.Count > 0 && cmbMonth.Items.Count > 0 && !_calendarBusy)
                RebuildCalendar();
        }
    }
}
