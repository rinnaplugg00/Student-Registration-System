using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using StudentRegistrationSystem.Services;

namespace StudentRegistrationSystem
{
    public partial class FormGroupAverage : Form
    {
        private sealed class GroupAverageItem
        {
            public string GroupName { get; set; }
            public string Text { get; set; }

            public override string ToString()
            {
                return Text ?? string.Empty;
            }
        }

        private DataTable _data;
        private string _subtitleText;
        private string _exportFilePrefix = "group-averages";

        private TableLayoutPanel tableLayoutPanel1;
        private Label lblTitle;
        private Label lblSubtitle;
        private Panel listPanel;
        private ListBox listAverages;
        private FlowLayoutPanel actionsPanel;
        private Button btnExport;
        private Button btnClose;

        public FormGroupAverage()
        {
            InitializeComponent();
            BuildUI();
        }

        public FormGroupAverage(DataTable data) : this()
        {
            _data = data;
        }

        private void BuildUI()
        {
            tableLayoutPanel1 = new TableLayoutPanel();
            lblTitle = new Label();
            lblSubtitle = new Label();
            listPanel = new Panel();
            listAverages = new ListBox();
            actionsPanel = new FlowLayoutPanel();
            btnExport = new Button();
            btnClose = new Button();

            tableLayoutPanel1.SuspendLayout();
            listPanel.SuspendLayout();
            actionsPanel.SuspendLayout();
            SuspendLayout();

            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(lblTitle, 0, 0);
            tableLayoutPanel1.Controls.Add(lblSubtitle, 0, 1);
            tableLayoutPanel1.Controls.Add(listPanel, 0, 2);
            tableLayoutPanel1.Controls.Add(actionsPanel, 0, 3);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.Padding = new Padding(22, 18, 22, 18);
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 66F));
            tableLayoutPanel1.Size = new Size(720, 430);
            tableLayoutPanel1.TabIndex = 0;

            lblTitle.Dock = DockStyle.Fill;
            lblTitle.Margin = new Padding(0, 0, 0, 6);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(676, 52);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Средний балл по группам";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;

            lblSubtitle.Dock = DockStyle.Fill;
            lblSubtitle.Margin = new Padding(0, 0, 0, 12);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(676, 34);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Дважды нажмите по группе, чтобы открыть её специальность и состав.";
            lblSubtitle.TextAlign = ContentAlignment.MiddleCenter;

            listPanel.Controls.Add(listAverages);
            listPanel.Dock = DockStyle.Fill;
            listPanel.Location = new Point(22, 116);
            listPanel.Margin = new Padding(0);
            listPanel.Name = "listPanel";
            listPanel.Padding = new Padding(14);
            listPanel.Size = new Size(676, 296);
            listPanel.TabIndex = 2;

            listAverages.Dock = DockStyle.Fill;
            listAverages.DrawMode = DrawMode.OwnerDrawFixed;
            listAverages.FormattingEnabled = true;
            listAverages.HorizontalScrollbar = false;
            listAverages.IntegralHeight = false;
            listAverages.ItemHeight = 36;
            listAverages.Margin = new Padding(0);
            listAverages.Name = "listAverages";
            listAverages.SelectionMode = SelectionMode.One;
            listAverages.TabIndex = 0;
            listAverages.DrawItem += listAverages_DrawItem;
            listAverages.DoubleClick += listAverages_DoubleClick;

            actionsPanel.AutoSize = true;
            actionsPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            actionsPanel.Dock = DockStyle.Right;
            actionsPanel.FlowDirection = FlowDirection.LeftToRight;
            actionsPanel.Margin = new Padding(0, 12, 0, 0);
            actionsPanel.WrapContents = false;

            btnExport.Text = "Экспорт CSV";
            btnExport.Size = new Size(140, 40);
            btnExport.Margin = new Padding(0, 0, 10, 0);
            btnExport.Click += btnExport_Click;

            btnClose.Text = "Закрыть";
            btnClose.Size = new Size(120, 40);
            btnClose.Margin = new Padding(0);
            btnClose.Click += (_, __) => Close();

            actionsPanel.Controls.Add(btnExport);
            actionsPanel.Controls.Add(btnClose);

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(720, 430);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.Sizable;
            MinimumSize = new Size(680, 360);
            MaximumSize = new Size(980, 760);
            StartPosition = FormStartPosition.CenterParent;
            Text = UiCaptions.GroupAverages;
            Load += FormGroupAverage_Load;

            actionsPanel.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            listPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        private void FormGroupAverage_Load(object sender, EventArgs e)
        {
            ApplyTheme();
            LoadData();
        }

        public void SetData(DataTable data)
        {
            _data = data;
            if (IsHandleCreated)
                LoadData();
        }

        public void SetContext(string subtitleText, string exportFilePrefix)
        {
            _subtitleText = subtitleText;
            if (!string.IsNullOrWhiteSpace(exportFilePrefix))
                _exportFilePrefix = exportFilePrefix;

            if (lblSubtitle != null && !string.IsNullOrWhiteSpace(_subtitleText))
                lblSubtitle.Text = _subtitleText;
        }

        private void ApplyTheme()
        {
            BackColor = Theme.BackColor;
            ForeColor = Theme.ForeColor;
            tableLayoutPanel1.BackColor = Theme.BackColor;

            lblTitle.Font = new Font("Segoe UI", 16f, FontStyle.Bold);
            lblTitle.ForeColor = Theme.ForeColor;
            lblSubtitle.Font = new Font("Segoe UI", 9.75f);
            lblSubtitle.ForeColor = Theme.MutedTextColor;
            if (!string.IsNullOrWhiteSpace(_subtitleText))
                lblSubtitle.Text = _subtitleText;

            listPanel.BackColor = Theme.GridColor;
            Theme.ApplyRoundedRegion(listPanel, 18);
            actionsPanel.BackColor = Theme.BackColor;

            Theme.StyleListBox(listAverages);
            listAverages.BorderStyle = BorderStyle.None;
            listAverages.Font = new Font("Segoe UI", 11.25f, FontStyle.Regular, GraphicsUnit.Point);
            Theme.SetControlDoubleBuffered(listAverages);
            Theme.StyleSecondaryButton(btnClose);
            Theme.StyleSecondaryButton(btnExport);
            Theme.ApplyRoundedRegion(btnClose, 16);
            Theme.ApplyRoundedRegion(btnExport, 16);
        }

        private void LoadData()
        {
            listAverages.Items.Clear();
            if (_data == null)
                return;

            foreach (DataRow row in _data.Rows)
            {
                string groupName = DisplayTextHelper.SanitizeNameForGrid(row["GroupName"]);
                double avg = Convert.ToDouble(row["AverageGrade"]);

                listAverages.Items.Add(new GroupAverageItem
                {
                    GroupName = groupName,
                    Text = $"{groupName} | {avg:F2}"
                });
            }
        }

        private void listAverages_DrawItem(object sender, DrawItemEventArgs e)
        {
            ListBoxOwnerDrawHelper.DrawCenteredItem(listAverages, e);
        }

        private void listAverages_DoubleClick(object sender, EventArgs e)
        {
            GroupAverageItem item = listAverages.SelectedItem as GroupAverageItem;
            if (item == null || string.IsNullOrWhiteSpace(item.GroupName))
                return;

            using (var form = new FormGroupInfo(item.GroupName))
                form.ShowDialog(this);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (_data == null || _data.Rows.Count == 0)
            {
                MessageBox.Show("Нет данных для экспорта.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var dialog = new SaveFileDialog())
            {
                dialog.Title = "Сохранить средний балл по группам";
                dialog.Filter = "CSV files (*.csv)|*.csv";
                dialog.FileName = $"{_exportFilePrefix}-{DateTime.Now:yyyyMMdd-HHmm}.csv";

                if (dialog.ShowDialog(this) != DialogResult.OK)
                    return;

                try
                {
                    CsvExportService.ExportDataTable(_data, dialog.FileName);
                    MessageBox.Show("Отчет по группам сохранен.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Не удалось сохранить отчет.\n\n" + ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
