using System.Drawing;
using System.Windows.Forms;

namespace StudentRegistrationSystem
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.topPanel = new Panel();
            this.topControlsFlow = new FlowLayoutPanel();
            this.averageFilterPanel = new FlowLayoutPanel();
            this.lblRiskSummary = new Label();
            this.txtSearch = new TextBox();
            this.btnReset = new Button();
            this.lblSemester = new Label();
            this.cmbSemester = new ComboBox();
            this.label1 = new Label();
            this.txtAverageFilter = new TextBox();
            this.btnTheme = new Button();

            this.dataGridStudents = new DataGridView();

            this.bottomPanel = new Panel();
            this.bottomButtonsFlow = new FlowLayoutPanel();
            this.btnAdd = new Button();
            this.btnEdit = new Button();
            this.btnDelete = new Button();
            this.btnSubjects = new Button();
            this.btnGroupAverage = new Button();
            this.btnAddGroup = new Button();
            this.btnTop = new Button();
            this.btnExport = new Button();

            ((System.ComponentModel.ISupportInitialize)(this.dataGridStudents)).BeginInit();

            this.SuspendLayout();

            // ===== TOP PANEL =====
            this.topPanel.Dock = DockStyle.Top;
            this.topPanel.Height = 110;
            this.topPanel.Padding = new Padding(24, 18, 24, 18);

            this.topControlsFlow.AutoSize = true;
            this.topControlsFlow.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.topControlsFlow.Dock = DockStyle.Left;
            this.topControlsFlow.Location = new Point(20, 14);
            this.topControlsFlow.Margin = new Padding(0);
            this.topControlsFlow.Name = "topControlsFlow";
            this.topControlsFlow.Size = new Size(0, 0);
            this.topControlsFlow.WrapContents = false;

            // поиск
            this.txtSearch.Margin = new Padding(0, 4, 14, 0);
            this.txtSearch.Size = new Size(240, 30);

            // сброс
            this.btnReset.Text = "Сбросить";
            this.btnReset.Margin = new Padding(0, 0, 18, 0);
            this.btnReset.Size = new Size(118, 34);
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);

            // фильтр
            this.averageFilterPanel.AutoSize = true;
            this.averageFilterPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.averageFilterPanel.Margin = new Padding(0, 2, 0, 0);
            this.averageFilterPanel.WrapContents = false;

            this.lblSemester.AutoSize = true;
            this.lblSemester.Text = "Семестр:";
            this.lblSemester.Margin = new Padding(0, 8, 8, 0);

            this.cmbSemester.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbSemester.Margin = new Padding(0, 0, 18, 0);
            this.cmbSemester.Size = new Size(180, 30);

            this.label1.AutoSize = true;
            this.label1.Text = "Средний балл от:";
            this.label1.Margin = new Padding(0, 8, 8, 0);

            this.txtAverageFilter.Margin = new Padding(0);
            this.txtAverageFilter.Size = new Size(82, 30);
            this.txtAverageFilter.Text = "0,00";
            this.txtAverageFilter.TextAlign = HorizontalAlignment.Center;

            // тема
            this.btnTheme.Text = "Тема";
            this.btnTheme.Size = new Size(72, 34);
            this.btnTheme.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnTheme.Location = new Point(1000, 20);
            this.btnTheme.Click += new System.EventHandler(this.btnTheme_Click_1);

            this.averageFilterPanel.Controls.Add(this.lblSemester);
            this.averageFilterPanel.Controls.Add(this.cmbSemester);
            this.averageFilterPanel.Controls.Add(this.label1);
            this.averageFilterPanel.Controls.Add(this.txtAverageFilter);

            this.topControlsFlow.Controls.Add(this.txtSearch);
            this.topControlsFlow.Controls.Add(this.btnReset);
            this.topControlsFlow.Controls.Add(this.averageFilterPanel);

            this.topPanel.Controls.Add(this.topControlsFlow);
            this.topPanel.Controls.Add(this.lblRiskSummary);
            this.topPanel.Controls.Add(this.btnTheme);

            this.lblRiskSummary.AutoSize = true;
            this.lblRiskSummary.Location = new Point(24, 62);
            this.lblRiskSummary.Margin = new Padding(0);
            this.lblRiskSummary.Name = "lblRiskSummary";
            this.lblRiskSummary.Size = new Size(0, 0);

            // ===== DATA GRID =====
            this.dataGridStudents.Dock = DockStyle.Fill;
            this.dataGridStudents.BorderStyle = BorderStyle.None;

            // ===== BOTTOM PANEL =====
            this.bottomPanel.Dock = DockStyle.Bottom;
            this.bottomPanel.Height = 96;
            this.bottomPanel.Padding = new Padding(24, 20, 24, 20);

            this.bottomButtonsFlow.Dock = DockStyle.Fill;
            this.bottomButtonsFlow.Margin = new Padding(0);
            this.bottomButtonsFlow.WrapContents = false;

            int btnWidth = 154;
            int spacing = 12;

            this.btnAdd.Text = "Новый студент";
            this.btnAdd.Size = new Size(btnWidth, 46);
            this.btnAdd.Margin = new Padding(0, 0, spacing, 0);
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            this.btnEdit.Text = "Редактировать";
            this.btnEdit.Size = new Size(btnWidth, 46);
            this.btnEdit.Margin = new Padding(0, 0, spacing, 0);
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);

            this.btnDelete.Text = "Удалить";
            this.btnDelete.Size = new Size(130, 46);
            this.btnDelete.Margin = new Padding(0, 0, spacing, 0);
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            this.btnSubjects.Text = "Предметы";
            this.btnSubjects.Size = new Size(136, 46);
            this.btnSubjects.Margin = new Padding(0, 0, spacing, 0);
            this.btnSubjects.Click += new System.EventHandler(this.btnSubjects_Click);

            this.btnGroupAverage.Text = "Средний по группам";
            this.btnGroupAverage.Size = new Size(178, 46);
            this.btnGroupAverage.Margin = new Padding(0, 0, spacing, 0);
            this.btnGroupAverage.Click += new System.EventHandler(this.btnGroupAverage_Click);

            this.btnAddGroup.Text = "Группы";
            this.btnAddGroup.Size = new Size(128, 46);
            this.btnAddGroup.Margin = new Padding(0, 0, spacing, 0);
            this.btnAddGroup.Click += new System.EventHandler(this.btnAddGroup_Click);

            this.btnTop = new Button();
            this.btnTop.Text = "Топ студентов";
            this.btnTop.Size = new Size(150, 46);
            this.btnTop.Margin = new Padding(0, 0, spacing, 0);
            this.btnTop.Click += new System.EventHandler(this.btnTop_Click);

            this.btnExport.Text = "Экспорт CSV";
            this.btnExport.Size = new Size(144, 46);
            this.btnExport.Margin = new Padding(0);
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);

            this.bottomButtonsFlow.Controls.Add(this.btnAdd);
            this.bottomButtonsFlow.Controls.Add(this.btnEdit);
            this.bottomButtonsFlow.Controls.Add(this.btnDelete);
            this.bottomButtonsFlow.Controls.Add(this.btnSubjects);
            this.bottomButtonsFlow.Controls.Add(this.btnGroupAverage);
            this.bottomButtonsFlow.Controls.Add(this.btnAddGroup);
            this.bottomButtonsFlow.Controls.Add(this.btnTop);
            this.bottomButtonsFlow.Controls.Add(this.btnExport);

            this.bottomPanel.Controls.Add(this.bottomButtonsFlow);

            // ===== FORM =====
            this.ClientSize = new Size(1100, 600);
            this.MinimumSize = new Size(900, 500);
            this.WindowState = FormWindowState.Maximized;

            this.Controls.Add(this.dataGridStudents);
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.topPanel);

            this.Text = "Журнал студентов";
            this.Load += new System.EventHandler(this.MainForm_Load);

            ((System.ComponentModel.ISupportInitialize)(this.dataGridStudents)).EndInit();

            this.ResumeLayout(false);
        }

        private Panel topPanel;
        private Panel bottomPanel;
        private FlowLayoutPanel topControlsFlow;
        private FlowLayoutPanel averageFilterPanel;
        private FlowLayoutPanel bottomButtonsFlow;

        private DataGridView dataGridStudents;
        private Label lblRiskSummary;
        private TextBox txtSearch;
        private Button btnReset;
        private Label lblSemester;
        private ComboBox cmbSemester;
        private Label label1;
        private TextBox txtAverageFilter;
        private Button btnTheme;

        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnSubjects;
        private Button btnGroupAverage;
        private Button btnAddGroup;
        private Button btnTop;
        private Button btnExport;
    }
}
