using System.Drawing;
using System.Windows.Forms;

namespace StudentRegistrationSystem
{
    partial class FormGroup
    {
        private System.ComponentModel.IContainer components = null;
        private SplitContainer splitMain;
        private Label lblNewGroupTitle;
        private Label lblGroupName;
        private TextBox txtGroupName;
        private Label lblSpecialty;
        private TextBox txtSpecialty;
        private TableLayoutPanel flowFormButtons;
        private Button btnSave;
        private Button btnDelete;
        private Button btnClose;
        private Label lblExistingGroupsTitle;
        private ListBox lstGroups;
        private GroupBox groupDetails;
        private Label lblDetailNameCaption;
        private Label lblDetailName;
        private Label lblDetailSpecialtyCaption;
        private Label lblDetailSpecialty;
        private Label lblDetailCountCaption;
        private Label lblDetailCount;
        private Label lblStudentsCaption;
        private ListBox lstStudents;
        private TableLayoutPanel leftLayout;
        private TableLayoutPanel rightLayout;
        private TableLayoutPanel detailsLayout;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGroup));
            this.splitMain = new SplitContainer();
            this.leftLayout = new TableLayoutPanel();
            this.lblNewGroupTitle = new Label();
            this.lblGroupName = new Label();
            this.txtGroupName = new TextBox();
            this.lblSpecialty = new Label();
            this.txtSpecialty = new TextBox();
            this.flowFormButtons = new TableLayoutPanel();
            this.btnSave = new Button();
            this.btnDelete = new Button();
            this.btnClose = new Button();
            this.rightLayout = new TableLayoutPanel();
            this.lblExistingGroupsTitle = new Label();
            this.lstGroups = new ListBox();
            this.groupDetails = new GroupBox();
            this.detailsLayout = new TableLayoutPanel();
            this.lblDetailNameCaption = new Label();
            this.lblDetailName = new Label();
            this.lblDetailSpecialtyCaption = new Label();
            this.lblDetailSpecialty = new Label();
            this.lblDetailCountCaption = new Label();
            this.lblDetailCount = new Label();
            this.lblStudentsCaption = new Label();
            this.lstStudents = new ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.leftLayout.SuspendLayout();
            this.flowFormButtons.SuspendLayout();
            this.rightLayout.SuspendLayout();
            this.groupDetails.SuspendLayout();
            this.detailsLayout.SuspendLayout();
            this.SuspendLayout();

            // splitMain
            this.splitMain.Dock = DockStyle.Fill;
            this.splitMain.FixedPanel = FixedPanel.Panel1;
            this.splitMain.Location = new Point(0, 0);
            this.splitMain.Name = "splitMain";
            this.splitMain.Panel1.Controls.Add(this.leftLayout);
            this.splitMain.Panel2.Controls.Add(this.rightLayout);
            this.splitMain.Size = new Size(1040, 620);
            this.splitMain.SplitterDistance = 380;
            this.splitMain.TabIndex = 0;

            // leftLayout
            this.leftLayout.ColumnCount = 1;
            this.leftLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.leftLayout.Controls.Add(this.lblNewGroupTitle, 0, 0);
            this.leftLayout.Controls.Add(this.lblGroupName, 0, 1);
            this.leftLayout.Controls.Add(this.txtGroupName, 0, 2);
            this.leftLayout.Controls.Add(this.lblSpecialty, 0, 3);
            this.leftLayout.Controls.Add(this.txtSpecialty, 0, 4);
            this.leftLayout.Controls.Add(this.flowFormButtons, 0, 5);
            this.leftLayout.Dock = DockStyle.Fill;
            this.leftLayout.Padding = new Padding(24, 24, 24, 24);
            this.leftLayout.RowCount = 7;
            this.leftLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            this.leftLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            this.leftLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            this.leftLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            this.leftLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            this.leftLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 108F));
            this.leftLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.leftLayout.TabIndex = 0;

            // lblNewGroupTitle
            this.lblNewGroupTitle.AutoSize = true;
            this.lblNewGroupTitle.Dock = DockStyle.Fill;
            this.lblNewGroupTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            this.lblNewGroupTitle.Location = new Point(24, 24);
            this.lblNewGroupTitle.Margin = new Padding(0, 0, 0, 8);
            this.lblNewGroupTitle.Name = "lblNewGroupTitle";
            this.lblNewGroupTitle.Size = new Size(332, 42);
            this.lblNewGroupTitle.TabIndex = 0;
            this.lblNewGroupTitle.Text = "Добавление группы";
            this.lblNewGroupTitle.TextAlign = ContentAlignment.MiddleLeft;

            // lblGroupName
            this.lblGroupName.AutoSize = true;
            this.lblGroupName.Dock = DockStyle.Fill;
            this.lblGroupName.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblGroupName.Location = new Point(24, 66);
            this.lblGroupName.Margin = new Padding(0);
            this.lblGroupName.Name = "lblGroupName";
            this.lblGroupName.Size = new Size(332, 28);
            this.lblGroupName.TabIndex = 1;
            this.lblGroupName.Text = "Название группы";
            this.lblGroupName.TextAlign = ContentAlignment.MiddleLeft;

            // txtGroupName
            this.txtGroupName.Dock = DockStyle.Fill;
            this.txtGroupName.Font = new Font("Segoe UI", 11F);
            this.txtGroupName.Location = new Point(24, 98);
            this.txtGroupName.Margin = new Padding(0, 4, 0, 6);
            this.txtGroupName.Name = "txtGroupName";
            this.txtGroupName.Size = new Size(332, 32);
            this.txtGroupName.TabIndex = 2;

            // lblSpecialty
            this.lblSpecialty.AutoSize = true;
            this.lblSpecialty.Dock = DockStyle.Fill;
            this.lblSpecialty.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblSpecialty.Location = new Point(24, 136);
            this.lblSpecialty.Margin = new Padding(0);
            this.lblSpecialty.Name = "lblSpecialty";
            this.lblSpecialty.Size = new Size(332, 28);
            this.lblSpecialty.TabIndex = 3;
            this.lblSpecialty.Text = "Специальность";
            this.lblSpecialty.TextAlign = ContentAlignment.MiddleLeft;

            // txtSpecialty
            this.txtSpecialty.Dock = DockStyle.Fill;
            this.txtSpecialty.Font = new Font("Segoe UI", 11F);
            this.txtSpecialty.Location = new Point(24, 168);
            this.txtSpecialty.Margin = new Padding(0, 4, 0, 6);
            this.txtSpecialty.Name = "txtSpecialty";
            this.txtSpecialty.Size = new Size(332, 32);
            this.txtSpecialty.TabIndex = 4;

            // flowFormButtons
            this.flowFormButtons.ColumnCount = 2;
            this.flowFormButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            this.flowFormButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            this.flowFormButtons.Controls.Add(this.btnSave, 0, 0);
            this.flowFormButtons.Controls.Add(this.btnDelete, 0, 1);
            this.flowFormButtons.Controls.Add(this.btnClose, 1, 1);
            this.flowFormButtons.Dock = DockStyle.Fill;
            this.flowFormButtons.Location = new Point(24, 206);
            this.flowFormButtons.Margin = new Padding(0, 10, 0, 0);
            this.flowFormButtons.Name = "flowFormButtons";
            this.flowFormButtons.RowCount = 2;
            this.flowFormButtons.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));
            this.flowFormButtons.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));
            this.flowFormButtons.Size = new Size(332, 98);
            this.flowFormButtons.TabIndex = 5;
            this.flowFormButtons.SetColumnSpan(this.btnSave, 2);

            // btnSave
            this.btnSave.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            this.btnSave.Dock = DockStyle.Fill;
            this.btnSave.Margin = new Padding(0, 0, 0, 10);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new Size(332, 36);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Сохранить группу";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            // btnDelete
            this.btnDelete.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnDelete.Dock = DockStyle.Fill;
            this.btnDelete.Margin = new Padding(0, 0, 8, 0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(158, 46);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "Удалить";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            // btnClose
            this.btnClose.Font = new Font("Segoe UI", 10F);
            this.btnClose.Dock = DockStyle.Fill;
            this.btnClose.Margin = new Padding(0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(166, 46);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            // rightLayout
            this.rightLayout.ColumnCount = 1;
            this.rightLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.rightLayout.Controls.Add(this.lblExistingGroupsTitle, 0, 0);
            this.rightLayout.Controls.Add(this.lstGroups, 0, 1);
            this.rightLayout.Controls.Add(this.groupDetails, 0, 2);
            this.rightLayout.Dock = DockStyle.Fill;
            this.rightLayout.Padding = new Padding(20, 24, 24, 24);
            this.rightLayout.RowCount = 3;
            this.rightLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            this.rightLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 42F));
            this.rightLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 58F));
            this.rightLayout.TabIndex = 0;

            // lblExistingGroupsTitle
            this.lblExistingGroupsTitle.AutoSize = true;
            this.lblExistingGroupsTitle.Dock = DockStyle.Fill;
            this.lblExistingGroupsTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            this.lblExistingGroupsTitle.Location = new Point(20, 24);
            this.lblExistingGroupsTitle.Margin = new Padding(0, 0, 0, 8);
            this.lblExistingGroupsTitle.Name = "lblExistingGroupsTitle";
            this.lblExistingGroupsTitle.Size = new Size(616, 42);
            this.lblExistingGroupsTitle.TabIndex = 0;
            this.lblExistingGroupsTitle.Text = "Существующие группы";
            this.lblExistingGroupsTitle.TextAlign = ContentAlignment.MiddleLeft;

            // lstGroups
            this.lstGroups.Dock = DockStyle.Fill;
            this.lstGroups.Font = new Font("Segoe UI", 10.5F);
            this.lstGroups.FormattingEnabled = true;
            this.lstGroups.ItemHeight = 23;
            this.lstGroups.Location = new Point(20, 74);
            this.lstGroups.Margin = new Padding(0, 0, 0, 14);
            this.lstGroups.Name = "lstGroups";
            this.lstGroups.Size = new Size(616, 198);
            this.lstGroups.TabIndex = 1;
            this.lstGroups.SelectedIndexChanged += new System.EventHandler(this.lstGroups_SelectedIndexChanged);
            this.lstGroups.DoubleClick += new System.EventHandler(this.lstGroups_DoubleClick);

            // groupDetails
            this.groupDetails.Controls.Add(this.detailsLayout);
            this.groupDetails.Dock = DockStyle.Fill;
            this.groupDetails.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            this.groupDetails.Location = new Point(20, 286);
            this.groupDetails.Margin = new Padding(0);
            this.groupDetails.Name = "groupDetails";
            this.groupDetails.Padding = new Padding(16, 12, 16, 16);
            this.groupDetails.Size = new Size(616, 310);
            this.groupDetails.TabIndex = 2;
            this.groupDetails.TabStop = false;
            this.groupDetails.Text = "Данные группы";

            // detailsLayout
            this.detailsLayout.ColumnCount = 2;
            this.detailsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
            this.detailsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.detailsLayout.Controls.Add(this.lblDetailNameCaption, 0, 0);
            this.detailsLayout.Controls.Add(this.lblDetailName, 1, 0);
            this.detailsLayout.Controls.Add(this.lblDetailSpecialtyCaption, 0, 1);
            this.detailsLayout.Controls.Add(this.lblDetailSpecialty, 1, 1);
            this.detailsLayout.Controls.Add(this.lblDetailCountCaption, 0, 2);
            this.detailsLayout.Controls.Add(this.lblDetailCount, 1, 2);
            this.detailsLayout.Controls.Add(this.lblStudentsCaption, 0, 3);
            this.detailsLayout.Controls.Add(this.lstStudents, 1, 3);
            this.detailsLayout.Dock = DockStyle.Fill;
            this.detailsLayout.RowCount = 4;
            this.detailsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            this.detailsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            this.detailsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            this.detailsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.detailsLayout.TabIndex = 0;

            // lblDetailNameCaption
            this.lblDetailNameCaption.AutoSize = true;
            this.lblDetailNameCaption.Dock = DockStyle.Fill;
            this.lblDetailNameCaption.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblDetailNameCaption.Location = new Point(0, 0);
            this.lblDetailNameCaption.Margin = new Padding(0);
            this.lblDetailNameCaption.Name = "lblDetailNameCaption";
            this.lblDetailNameCaption.Size = new Size(160, 34);
            this.lblDetailNameCaption.TabIndex = 0;
            this.lblDetailNameCaption.Text = "Название:";
            this.lblDetailNameCaption.TextAlign = ContentAlignment.MiddleLeft;

            // lblDetailName
            this.lblDetailName.AutoSize = true;
            this.lblDetailName.Dock = DockStyle.Fill;
            this.lblDetailName.Font = new Font("Segoe UI", 10F);
            this.lblDetailName.Location = new Point(160, 0);
            this.lblDetailName.Margin = new Padding(0);
            this.lblDetailName.Name = "lblDetailName";
            this.lblDetailName.Size = new Size(410, 34);
            this.lblDetailName.TabIndex = 1;
            this.lblDetailName.Text = "Выберите группу";
            this.lblDetailName.TextAlign = ContentAlignment.MiddleLeft;

            // lblDetailSpecialtyCaption
            this.lblDetailSpecialtyCaption.AutoSize = true;
            this.lblDetailSpecialtyCaption.Dock = DockStyle.Fill;
            this.lblDetailSpecialtyCaption.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblDetailSpecialtyCaption.Location = new Point(0, 34);
            this.lblDetailSpecialtyCaption.Margin = new Padding(0);
            this.lblDetailSpecialtyCaption.Name = "lblDetailSpecialtyCaption";
            this.lblDetailSpecialtyCaption.Size = new Size(160, 34);
            this.lblDetailSpecialtyCaption.TabIndex = 2;
            this.lblDetailSpecialtyCaption.Text = "Специальность:";
            this.lblDetailSpecialtyCaption.TextAlign = ContentAlignment.MiddleLeft;

            // lblDetailSpecialty
            this.lblDetailSpecialty.AutoSize = true;
            this.lblDetailSpecialty.Dock = DockStyle.Fill;
            this.lblDetailSpecialty.Font = new Font("Segoe UI", 10F);
            this.lblDetailSpecialty.Location = new Point(160, 34);
            this.lblDetailSpecialty.Margin = new Padding(0);
            this.lblDetailSpecialty.Name = "lblDetailSpecialty";
            this.lblDetailSpecialty.Size = new Size(410, 34);
            this.lblDetailSpecialty.TabIndex = 3;
            this.lblDetailSpecialty.Text = "Не указана";
            this.lblDetailSpecialty.TextAlign = ContentAlignment.MiddleLeft;

            // lblDetailCountCaption
            this.lblDetailCountCaption.AutoSize = true;
            this.lblDetailCountCaption.Dock = DockStyle.Fill;
            this.lblDetailCountCaption.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblDetailCountCaption.Location = new Point(0, 68);
            this.lblDetailCountCaption.Margin = new Padding(0);
            this.lblDetailCountCaption.Name = "lblDetailCountCaption";
            this.lblDetailCountCaption.Size = new Size(160, 34);
            this.lblDetailCountCaption.TabIndex = 4;
            this.lblDetailCountCaption.Text = "Количество студентов:";
            this.lblDetailCountCaption.TextAlign = ContentAlignment.MiddleLeft;

            // lblDetailCount
            this.lblDetailCount.AutoSize = true;
            this.lblDetailCount.Dock = DockStyle.Fill;
            this.lblDetailCount.Font = new Font("Segoe UI", 10F);
            this.lblDetailCount.Location = new Point(160, 68);
            this.lblDetailCount.Margin = new Padding(0);
            this.lblDetailCount.Name = "lblDetailCount";
            this.lblDetailCount.Size = new Size(410, 34);
            this.lblDetailCount.TabIndex = 5;
            this.lblDetailCount.Text = "0";
            this.lblDetailCount.TextAlign = ContentAlignment.MiddleLeft;

            // lblStudentsCaption
            this.lblStudentsCaption.AutoSize = true;
            this.lblStudentsCaption.Dock = DockStyle.Fill;
            this.lblStudentsCaption.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblStudentsCaption.Location = new Point(0, 102);
            this.lblStudentsCaption.Margin = new Padding(0, 0, 14, 0);
            this.lblStudentsCaption.Name = "lblStudentsCaption";
            this.lblStudentsCaption.Size = new Size(146, 183);
            this.lblStudentsCaption.TabIndex = 6;
            this.lblStudentsCaption.Text = "Список студентов:";
            this.lblStudentsCaption.TextAlign = ContentAlignment.TopLeft;

            // lstStudents
            this.lstStudents.Dock = DockStyle.Fill;
            this.lstStudents.Font = new Font("Segoe UI", 10F);
            this.lstStudents.FormattingEnabled = true;
            this.lstStudents.ItemHeight = 22;
            this.lstStudents.Location = new Point(160, 106);
            this.lstStudents.Margin = new Padding(0, 4, 0, 0);
            this.lstStudents.Name = "lstStudents";
            this.lstStudents.Size = new Size(410, 179);
            this.lstStudents.TabIndex = 7;
            this.lstStudents.DoubleClick += new System.EventHandler(this.lstStudents_DoubleClick);

            // FormGroup
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1040, 620);
            this.Controls.Add(this.splitMain);
            this.Icon = ((Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new Size(980, 560);
            this.Name = "FormGroup";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Новая группа";
            this.Load += new System.EventHandler(this.FormGroup_Load);
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.leftLayout.ResumeLayout(false);
            this.leftLayout.PerformLayout();
            this.flowFormButtons.ResumeLayout(false);
            this.rightLayout.ResumeLayout(false);
            this.rightLayout.PerformLayout();
            this.groupDetails.ResumeLayout(false);
            this.detailsLayout.ResumeLayout(false);
            this.detailsLayout.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
