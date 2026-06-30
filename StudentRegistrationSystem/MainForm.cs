using StudentRegistrationSystem.Models;
using StudentRegistrationSystem.Services;
using System;
using System.Data;
using System.Drawing;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentRegistrationSystem
{
    public partial class MainForm : Form
    {
        private readonly StudentService _studentService = new StudentService();
        private readonly Timer _filterDebounceTimer = new Timer();

        private ContextMenuStrip rowMenu;
        private int _contextRowIndex = -1;
        private int? _selectedSemesterId;
        private bool _isRefreshingGrid;

        public MainForm()
        {
            InitializeComponent();
            Text = UiCaptions.MainWindow;

            WindowState = FormWindowState.Maximized;
            MinimumSize = new Size(800, 600);

            _filterDebounceTimer.Interval = 400;
            _filterDebounceTimer.Tick += async (s, e) =>
            {
                _filterDebounceTimer.Stop();
                await ApplyFilterAsync();
            };

            InitializeGrid();
            InitializeContextMenu();

            topPanel.Resize += (s, a) =>
            {
                btnTheme.Location = new Point(topPanel.Width - btnTheme.Width - 24, 24);
            };
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            Theme.SetLightTheme();
            ApplyTheme();
            LoadSemesters();

            txtSearch.TextChanged += (s, a) => ScheduleFilter();
            txtAverageFilter.TextChanged += (s, a) => ScheduleFilter();
            cmbSemester.SelectedIndexChanged += async (s, a) =>
            {
                if (cmbSemester.SelectedItem is SemesterInfo semester)
                    _selectedSemesterId = semester.IsAllPeriods ? (int?)null : semester.Id;
                else
                    _selectedSemesterId = null;

                await RefreshGridAsync();
            };

            dataGridStudents.DoubleClick += async (s, e2) =>
            {
                if (dataGridStudents.CurrentRow == null) return;

                int id = Convert.ToInt32(dataGridStudents.CurrentRow.Cells["Id"].Value);
                using (var detailsForm = new FormStudentDetails(id, _selectedSemesterId))
                    detailsForm.ShowDialog(this);

                await RefreshGridAsync();
            };

            Shown += (s, e2) => ApplyRoundedButtons();

            await RefreshGridAsync();
        }

        private void ScheduleFilter()
        {
            _filterDebounceTimer.Stop();
            _filterDebounceTimer.Start();
        }

        private void InitializeContextMenu()
        {
            rowMenu = new ContextMenuStrip();
            rowMenu.ShowImageMargin = false;
            rowMenu.RenderMode = ToolStripRenderMode.System;
            rowMenu.Font = new Font("Segoe UI", 10f);

            var editItem = new ToolStripMenuItem(UiCaptions.ContextMenuEdit);
            editItem.Click += (s, e) => btnEdit_Click(null, null);

            var deleteItem = new ToolStripMenuItem(UiCaptions.ContextMenuDelete);
            deleteItem.Click += (s, e) => btnDelete_Click(null, null);

            rowMenu.Items.Add(editItem);
            rowMenu.Items.Add(deleteItem);
            rowMenu.Opening += RowMenu_Opening;

            dataGridStudents.ContextMenuStrip = rowMenu;
            dataGridStudents.CellMouseDown += dataGridStudents_CellMouseDown;
        }

        private void RowMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = _contextRowIndex < 0 || _contextRowIndex >= dataGridStudents.Rows.Count;
        }

        private void dataGridStudents_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                _contextRowIndex = -1;
                return;
            }

            _contextRowIndex = e.RowIndex;
            dataGridStudents.ClearSelection();
            dataGridStudents.CurrentCell = dataGridStudents.Rows[e.RowIndex].Cells[e.ColumnIndex];
            dataGridStudents.Rows[e.RowIndex].Selected = true;
        }

        private void InitializeGrid()
        {
            dataGridStudents.AutoGenerateColumns = false;
            dataGridStudents.Columns.Clear();

            dataGridStudents.Columns.Add("Id", "Id");
            dataGridStudents.Columns["Id"].Visible = false;

            dataGridStudents.Columns.Add("GroupId", "GroupId");
            dataGridStudents.Columns["GroupId"].Visible = false;

            dataGridStudents.Columns.Add("GroupName", UiCaptions.ColumnGroup);
            dataGridStudents.Columns.Add("FullName", UiCaptions.ColumnFullName);
            dataGridStudents.Columns.Add("AverageGrade", UiCaptions.ColumnAverageGrade);
            dataGridStudents.Columns.Add("Gpa", UiCaptions.ColumnGpa);
            dataGridStudents.Columns.Add("RiskStatus", UiCaptions.ColumnRiskStatus);

            dataGridStudents.ReadOnly = true;
            dataGridStudents.AllowUserToAddRows = false;
            dataGridStudents.AllowUserToDeleteRows = false;
            dataGridStudents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridStudents.MultiSelect = false;
            dataGridStudents.RowHeadersVisible = false;

            dataGridStudents.BorderStyle = BorderStyle.None;
            dataGridStudents.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            dataGridStudents.EnableHeadersVisualStyles = false;
            dataGridStudents.ColumnHeadersHeight = 40;
            dataGridStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridStudents.RowTemplate.Height = 35;
            dataGridStudents.DefaultCellStyle.Font = new Font("Segoe UI", 10.25f);
            dataGridStudents.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10.25f, FontStyle.Bold);

            dataGridStudents.DefaultCellStyle.SelectionBackColor = Theme.AccentColor;
            dataGridStudents.DefaultCellStyle.SelectionForeColor = Color.White;

            txtAverageFilter.BorderStyle = BorderStyle.FixedSingle;
            txtAverageFilter.Font = new Font("Segoe UI", 10.5f);
            txtSearch.Font = new Font("Segoe UI", 10.5f);
            cmbSemester.Font = new Font("Segoe UI", 10.5f);
        }

        private void LoadSemesters()
        {
            List<SemesterInfo> semesters = SemesterService.GetAllSemesters();
            var items = new List<SemesterInfo>
            {
                SemesterInfo.CreateAllPeriods(UiCaptions.AllSemesters)
            };
            items.AddRange(semesters);

            cmbSemester.DataSource = items;
            cmbSemester.DisplayMember = "Name";

            int defaultSemesterId = SemesterService.GetDefaultSemesterId();
            SemesterInfo selected = semesters.Find(s => s.Id == defaultSemesterId)
                ?? (semesters.Count > 0 ? semesters[0] : items[0]);

            _selectedSemesterId = selected.IsAllPeriods ? (int?)null : selected.Id;
            cmbSemester.SelectedItem = selected;
        }

        private async Task RefreshGridAsync()
        {
            if (_isRefreshingGrid)
                return;

            _isRefreshingGrid = true;
            UseWaitCursor = true;
            try
            {
                string search = txtSearch.Text;
                double minAvg = ParseAverageFilter();
                var table = await Task.Run(() => _studentService.GetFiltered(search, minAvg, _selectedSemesterId));
                BindStudentsTable(table);
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                MessageBox.Show(
                    DatabaseHelper.FormatUserMessage(ex),
                    UiCaptions.Error,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                _isRefreshingGrid = false;
                UseWaitCursor = false;
            }
        }

        private async Task ApplyFilterAsync()
        {
            await RefreshGridAsync();
        }

        private void BindStudentsTable(DataTable table)
        {
            dataGridStudents.Rows.Clear();

            foreach (DataRow row in table.Rows)
            {
                double averageGrade = Convert.ToDouble(row["AverageGrade"]);
                int gradeCount = row.Table.Columns.Contains("GradeCount") && row["GradeCount"] != DBNull.Value
                    ? Convert.ToInt32(row["GradeCount"])
                    : 0;

                string groupName = Convert.ToString(row["GroupName"]);
                if (string.IsNullOrWhiteSpace(groupName))
                    groupName = UiCaptions.NoGroup;

                dataGridStudents.Rows.Add(
                    row["Id"],
                    row["GroupId"],
                    groupName,
                    DisplayTextHelper.SanitizeNameForGrid(row["FullName"]),
                    averageGrade,
                    AcademicMetricsService.FormatGpa(averageGrade),
                    AcademicMetricsService.GetRiskStatusText(averageGrade, gradeCount)
                );
            }

            UpdateRiskSummary(table);
            ApplyGradeColors();
        }

        private double ParseAverageFilter()
        {
            string raw = txtAverageFilter.Text.Trim();
            if (string.IsNullOrWhiteSpace(raw))
                return 0;

            raw = raw.Replace('.', ',');

            if (!double.TryParse(raw, NumberStyles.Number, CultureInfo.GetCultureInfo("ru-RU"), out double value))
                return 0;

            if (value < 0)
                return 0;

            if (value > 100)
                return 100;

            return value;
        }

        private void ApplyGradeColors()
        {
            Color baseRowColor = Theme.IsDarkTheme
                ? Color.FromArgb(36, 41, 48)
                : Color.White;
            Color alternateRowColor = Theme.IsDarkTheme
                ? Color.FromArgb(46, 51, 58)
                : Color.FromArgb(245, 247, 250);

            foreach (DataGridViewRow row in dataGridStudents.Rows)
            {
                Color rowColor = row.Index % 2 == 0 ? baseRowColor : alternateRowColor;
                row.DefaultCellStyle.BackColor = rowColor;
                row.DefaultCellStyle.ForeColor = Theme.ForeColor;

                foreach (DataGridViewCell gridCell in row.Cells)
                {
                    if (gridCell.OwningColumn.Name == "AverageGrade")
                        continue;

                    gridCell.Style.BackColor = rowColor;
                    gridCell.Style.ForeColor = Theme.ForeColor;
                    gridCell.Style.SelectionBackColor = Theme.AccentColor;
                    gridCell.Style.SelectionForeColor = Color.White;
                }

                var cell = row.Cells["AverageGrade"];
                if (cell.Value == null) continue;

                double avg = Convert.ToDouble(cell.Value);
                cell.Value = avg.ToString("F2");
                string riskStatus = Convert.ToString(row.Cells["RiskStatus"].Value);

                if (avg < 50)
                    cell.Style.BackColor = Theme.IsDarkTheme ? Color.FromArgb(120, 45, 45) : Color.FromArgb(198, 40, 40);
                else if (avg < 55)
                    cell.Style.BackColor = Theme.IsDarkTheme ? Color.FromArgb(135, 68, 42) : Color.FromArgb(216, 92, 42);
                else if (avg < 60)
                    cell.Style.BackColor = Theme.IsDarkTheme ? Color.FromArgb(150, 90, 40) : Color.FromArgb(230, 126, 34);
                else if (avg < 65)
                    cell.Style.BackColor = Theme.IsDarkTheme ? Color.FromArgb(145, 112, 44) : Color.FromArgb(241, 196, 15);
                else if (avg < 70)
                    cell.Style.BackColor = Theme.IsDarkTheme ? Color.FromArgb(126, 128, 44) : Color.FromArgb(183, 196, 56);
                else if (avg < 75)
                    cell.Style.BackColor = Theme.IsDarkTheme ? Color.FromArgb(100, 138, 46) : Color.FromArgb(139, 195, 74);
                else if (avg < 80)
                    cell.Style.BackColor = Theme.IsDarkTheme ? Color.FromArgb(74, 146, 52) : Color.FromArgb(102, 187, 106);
                else if (avg < 85)
                    cell.Style.BackColor = Theme.IsDarkTheme ? Color.FromArgb(52, 147, 71) : Color.FromArgb(67, 160, 71);
                else if (avg < 90)
                    cell.Style.BackColor = Theme.IsDarkTheme ? Color.FromArgb(43, 134, 94) : Color.FromArgb(38, 166, 154);
                else if (avg < 95)
                    cell.Style.BackColor = Theme.IsDarkTheme ? Color.FromArgb(37, 122, 118) : Color.FromArgb(0, 151, 167);
                else
                    cell.Style.BackColor = Theme.IsDarkTheme ? Color.FromArgb(33, 110, 138) : Color.FromArgb(30, 136, 229);

                cell.Style.ForeColor = Color.White;
                cell.Style.SelectionBackColor = Theme.AccentColor;
                cell.Style.SelectionForeColor = Color.White;

                DataGridViewCell riskCell = row.Cells["RiskStatus"];
                if (string.Equals(riskStatus, AcademicMetricsService.NoGradesStatus, StringComparison.Ordinal))
                {
                    riskCell.Style.BackColor = Theme.IsDarkTheme ? Color.FromArgb(91, 58, 122) : Color.FromArgb(123, 97, 255);
                    riskCell.Style.ForeColor = Color.White;
                    row.DefaultCellStyle.BackColor = Theme.IsDarkTheme ? Color.FromArgb(49, 43, 61) : Color.FromArgb(244, 239, 255);
                }
                else if (string.Equals(riskStatus, AcademicMetricsService.RiskStatus, StringComparison.Ordinal))
                {
                    riskCell.Style.BackColor = Theme.IsDarkTheme ? Color.FromArgb(128, 52, 52) : Color.FromArgb(211, 47, 47);
                    riskCell.Style.ForeColor = Color.White;
                    row.DefaultCellStyle.BackColor = Theme.IsDarkTheme ? Color.FromArgb(57, 40, 40) : Color.FromArgb(255, 238, 238);
                }
                else
                {
                    riskCell.Style.BackColor = Theme.IsDarkTheme ? Color.FromArgb(53, 113, 66) : Color.FromArgb(76, 175, 80);
                    riskCell.Style.ForeColor = Color.White;
                }

                foreach (DataGridViewCell gridCell in row.Cells)
                {
                    if (gridCell.OwningColumn.Name == "AverageGrade" || gridCell.OwningColumn.Name == "RiskStatus")
                        continue;

                    gridCell.Style.BackColor = row.DefaultCellStyle.BackColor;
                    gridCell.Style.ForeColor = Theme.ForeColor;
                    gridCell.Style.SelectionBackColor = Theme.AccentColor;
                    gridCell.Style.SelectionForeColor = Color.White;
                }

                riskCell.Style.SelectionBackColor = Theme.AccentColor;
                riskCell.Style.SelectionForeColor = Color.White;
            }
        }

        private void UpdateRiskSummary(DataTable table)
        {
            int noGradesCount = 0;
            int riskCount = 0;

            foreach (DataRow row in table.Rows)
            {
                double averageGrade = Convert.ToDouble(row["AverageGrade"]);
                int gradeCount = row.Table.Columns.Contains("GradeCount") && row["GradeCount"] != DBNull.Value
                    ? Convert.ToInt32(row["GradeCount"])
                    : 0;

                if (gradeCount <= 0)
                {
                    noGradesCount++;
                    continue;
                }

                if (averageGrade < AcademicMetricsService.RiskAverageThreshold)
                    riskCount++;
            }

            lblRiskSummary.Text = string.Format(
                UiCaptions.RiskSummaryFormat,
                GetSelectedSemesterDisplayName(),
                riskCount,
                noGradesCount);
        }

        private static int ExtractGroupOrder(string groupName)
        {
            if (string.IsNullOrWhiteSpace(groupName) || groupName == UiCaptions.NoGroup)
                return int.MaxValue;

            string[] parts = groupName.Split('-');
            string numericPart = parts.Length > 1 ? parts[parts.Length - 1] : groupName;
            return int.TryParse(numericPart, out int order) ? order : int.MaxValue;
        }

        private DataTable BuildCurrentStudentsExportTable()
        {
            string search = txtSearch.Text;
            double minAvg = ParseAverageFilter();
            DataTable source = _studentService.GetFiltered(search, minAvg, _selectedSemesterId);
            DataTable export = new DataTable();

            export.Columns.Add(UiCaptions.ColumnGroup, typeof(string));
            export.Columns.Add(UiCaptions.ColumnFullName, typeof(string));
            export.Columns.Add(UiCaptions.ColumnAverageGrade, typeof(double));
            export.Columns.Add(UiCaptions.ColumnGpa, typeof(string));
            export.Columns.Add(UiCaptions.ColumnRiskStatus, typeof(string));

            foreach (DataRow row in source.Rows)
            {
                double averageGrade = Convert.ToDouble(row["AverageGrade"]);
                int gradeCount = row.Table.Columns.Contains("GradeCount") && row["GradeCount"] != DBNull.Value
                    ? Convert.ToInt32(row["GradeCount"])
                    : 0;
                string groupName = Convert.ToString(row["GroupName"]);
                if (string.IsNullOrWhiteSpace(groupName))
                    groupName = UiCaptions.NoGroup;

                export.Rows.Add(
                    groupName,
                    DisplayTextHelper.SanitizeNameForGrid(row["FullName"]),
                    Math.Round(averageGrade, 2),
                    AcademicMetricsService.FormatGpa(averageGrade),
                    AcademicMetricsService.GetRiskStatusText(averageGrade, gradeCount));
            }

            return export;
        }

        private string GetSelectedSemesterSlug()
        {
            if (cmbSemester.SelectedItem is SemesterInfo semester)
            {
                if (semester.IsAllPeriods)
                    return "all-periods";

                if (!string.IsNullOrWhiteSpace(semester.Name))
                {
                    string slug = semester.Name.ToLowerInvariant();
                    foreach (char invalid in Path.GetInvalidFileNameChars())
                        slug = slug.Replace(invalid.ToString(), string.Empty);

                    slug = slug.Replace(' ', '-').Replace('/', '-');
                    return string.IsNullOrWhiteSpace(slug) ? "semester" : slug;
                }
            }

            return "all-periods";
        }

        private string GetSelectedSemesterDisplayName()
        {
            return (cmbSemester.SelectedItem as SemesterInfo)?.Name ?? UiCaptions.AllSemesters;
        }

        private void ExportStudentsToCsv()
        {
            try
            {
                DataTable exportTable = BuildCurrentStudentsExportTable();
                if (exportTable.Rows.Count == 0)
                {
                    MessageBox.Show(UiCaptions.ExportNoData, UiCaptions.MainWindow, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (var dialog = new SaveFileDialog())
                {
                    dialog.Title = UiCaptions.ExportSaveTitle;
                    dialog.Filter = "CSV files (*.csv)|*.csv";
                    dialog.FileName = $"students-{GetSelectedSemesterSlug()}-{DateTime.Now:yyyyMMdd-HHmm}.csv";
                    dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                    if (dialog.ShowDialog(this) != DialogResult.OK)
                        return;

                    CsvExportService.ExportDataTable(exportTable, dialog.FileName);
                    MessageBox.Show(
                        string.Format(UiCaptions.ExportSuccessFormat, Path.GetFileName(dialog.FileName)),
                        UiCaptions.MainWindow,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                MessageBox.Show(
                    string.Format(UiCaptions.ExportSaveErrorFormat, DatabaseHelper.FormatUserMessage(ex)),
                    UiCaptions.Error,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ApplyTheme()
        {
            BackColor = Theme.BackColor;
            ForeColor = Theme.ForeColor;

            ApplyThemeToControls(this);

            topPanel.BackColor = Theme.PanelColor;
            bottomPanel.BackColor = Theme.PanelColor;
            topControlsFlow.BackColor = Theme.PanelColor;
            bottomButtonsFlow.BackColor = Theme.PanelColor;
            averageFilterPanel.BackColor = Theme.PanelColor;
            label1.ForeColor = Theme.MutedTextColor;
            lblRiskSummary.ForeColor = Theme.MutedTextColor;
            btnTheme.Text = Theme.IsDarkTheme ? UiCaptions.ThemeLight : UiCaptions.ThemeDark;

            Theme.StyleDataGrid(dataGridStudents);
            dataGridStudents.BackgroundColor = Theme.BackColor;
            dataGridStudents.ColumnHeadersHeight = 44;
            dataGridStudents.DefaultCellStyle.Padding = new Padding(6, 3, 6, 3);
            ApplyGradeColors();
            ApplyRoundedButtons();
        }

        private void ApplyThemeToControls(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is Button btn)
                {
                    if (btn == btnAdd)
                        Theme.StylePrimaryButton(btn);
                    else
                        Theme.StyleSecondaryButton(btn);

                    btn.MouseEnter -= Btn_MouseEnter;
                    btn.MouseLeave -= Btn_MouseLeave;

                    btn.MouseEnter += Btn_MouseEnter;
                    btn.MouseLeave += Btn_MouseLeave;
                }

                if (c is TextBox tb)
                    Theme.StyleTextBox(tb);

                if (c.HasChildren)
                    ApplyThemeToControls(c);
            }
        }

        private void Btn_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Button btn)
                btn.BackColor = btn == btnAdd
                    ? ControlPaint.Light(Theme.AccentColor)
                    : ControlPaint.Light(Theme.ButtonColor);
        }

        private void Btn_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Button btn)
                btn.BackColor = btn == btnAdd ? Theme.AccentColor : Theme.ButtonColor;
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                using (var form = new FormAddStudent(_selectedSemesterId))
                {
                    if (form.ShowDialog() != DialogResult.OK)
                        return;

                    if (!form.StudentData.Validate(out string addErr))
                    {
                        MessageBox.Show(addErr, UiCaptions.MainWindow, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    _studentService.AddStudent(form.StudentData);

                    await RefreshGridAsync();
                    MessageBox.Show(UiCaptions.StudentAdded, UiCaptions.MainWindow, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                MessageBox.Show(
                    DatabaseHelper.FormatUserMessage(ex) + Environment.NewLine + Environment.NewLine + UiCaptions.StudentAddError,
                    UiCaptions.Error,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridStudents.CurrentRow == null) return;

            try
            {
                int id = Convert.ToInt32(dataGridStudents.CurrentRow.Cells["Id"].Value);

                Student selectedStudent = _studentService.LoadForEdit(id, _selectedSemesterId);
                if (selectedStudent == null)
                {
                    MessageBox.Show(UiCaptions.StudentLoadError, UiCaptions.MainWindow, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (var form = new FormEditStudent(selectedStudent, _selectedSemesterId))
                {
                    if (form.ShowDialog() != DialogResult.OK)
                        return;

                    if (!form.StudentData.Validate(out string editErr))
                    {
                        MessageBox.Show(editErr, UiCaptions.MainWindow, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    _studentService.UpdateStudent(form.StudentData);

                    await RefreshGridAsync();
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                MessageBox.Show(DatabaseHelper.FormatUserMessage(ex), UiCaptions.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridStudents.CurrentRow == null) return;

            var result = MessageBox.Show(
                UiCaptions.DeleteStudentConfirm,
                UiCaptions.DeleteConfirm,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result != DialogResult.Yes)
                return;

            try
            {
                int id = Convert.ToInt32(dataGridStudents.CurrentRow.Cells["Id"].Value);

                _studentService.DeleteStudent(id);

                await RefreshGridAsync();

                MessageBox.Show(UiCaptions.StudentDeleted, UiCaptions.MainWindow, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                MessageBox.Show(DatabaseHelper.FormatUserMessage(ex), UiCaptions.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnReset_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            txtAverageFilter.Text = "0,00";
            await ApplyFilterAsync();
        }

        private void btnGroupAverage_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable students = _studentService.GetAll(_selectedSemesterId);
                DataTable table = new DataTable();
                table.Columns.Add("GroupName", typeof(string));
                table.Columns.Add("AverageGrade", typeof(double));

                var grouped = students.AsEnumerable()
                    .GroupBy(r =>
                    {
                        string name = Convert.ToString(r["GroupName"]);
                        return string.IsNullOrWhiteSpace(name) ? UiCaptions.NoGroup : name;
                    })
                    .Select(g => new
                    {
                        GroupName = g.Key,
                        AverageGrade = g.Average(r => Convert.ToDouble(r["AverageGrade"]))
                    })
                    .OrderBy(x => ExtractGroupOrder(x.GroupName))
                    .ThenBy(x => x.GroupName);

                foreach (var item in grouped)
                    table.Rows.Add(item.GroupName, item.AverageGrade);

                FormGroupAverage form = new FormGroupAverage();
                form.SetData(table);
                form.SetContext(UiCaptions.PeriodPrefix + GetSelectedSemesterDisplayName(), "group-averages-" + GetSelectedSemesterSlug());
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                MessageBox.Show(DatabaseHelper.FormatUserMessage(ex), UiCaptions.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSubjects_Click(object sender, EventArgs e)
        {
            using (var form = new FormSubjectsCatalog())
                form.ShowDialog(this);
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            using (var form = new FormTopStudents(_selectedSemesterId))
                form.ShowDialog(this);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportStudentsToCsv();
        }

        private async void btnAddGroup_Click(object sender, EventArgs e)
        {
            using (FormGroup form = new FormGroup())
            {
                form.ShowDialog();

                if (form.GroupsChanged)
                {
                    await RefreshGridAsync();
                    MessageBox.Show(UiCaptions.GroupSaved, UiCaptions.MainWindow, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnTheme_Click_1(object sender, EventArgs e)
        {
            if (Theme.IsDarkTheme)
                Theme.SetLightTheme();
            else
                Theme.SetDarkTheme();

            ApplyTheme();
        }

        private void ApplyRoundedButtons()
        {
            Theme.ApplyRoundedRegion(btnAdd, 16);
            Theme.ApplyRoundedRegion(btnEdit, 16);
            Theme.ApplyRoundedRegion(btnDelete, 16);
            Theme.ApplyRoundedRegion(btnSubjects, 16);
            Theme.ApplyRoundedRegion(btnGroupAverage, 16);
            Theme.ApplyRoundedRegion(btnAddGroup, 16);
            Theme.ApplyRoundedRegion(btnReset, 16);
            Theme.ApplyRoundedRegion(btnTheme, 16);
            Theme.ApplyRoundedRegion(btnTop, 16);
            Theme.ApplyRoundedRegion(btnExport, 16);
        }
    }
}
