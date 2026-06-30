using System.Drawing;
using System.Windows.Forms;

namespace StudentRegistrationSystem
{
    partial class FormSubjectsCatalog
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel rootLayout;
        private Label lblTitle;
        private Label lblSubtitle;
        private Panel listPanel;
        private ListBox lstSubjects;
        private FlowLayoutPanel actionsPanel;
        private Button btnAddSubject;
        private Button btnDeleteSubject;
        private Button btnClose;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.rootLayout = new TableLayoutPanel();
            this.lblTitle = new Label();
            this.lblSubtitle = new Label();
            this.listPanel = new Panel();
            this.lstSubjects = new ListBox();
            this.actionsPanel = new FlowLayoutPanel();
            this.btnAddSubject = new Button();
            this.btnDeleteSubject = new Button();
            this.btnClose = new Button();
            this.rootLayout.SuspendLayout();
            this.listPanel.SuspendLayout();
            this.actionsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // rootLayout
            // 
            this.rootLayout.ColumnCount = 1;
            this.rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.rootLayout.Controls.Add(this.lblTitle, 0, 0);
            this.rootLayout.Controls.Add(this.lblSubtitle, 0, 1);
            this.rootLayout.Controls.Add(this.listPanel, 0, 2);
            this.rootLayout.Controls.Add(this.actionsPanel, 0, 3);
            this.rootLayout.Dock = DockStyle.Fill;
            this.rootLayout.Padding = new Padding(24, 22, 24, 24);
            this.rootLayout.RowCount = 4;
            this.rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            this.rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            this.rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.rootLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.rootLayout.Size = new Size(544, 468);
            this.rootLayout.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Dock = DockStyle.Fill;
            this.lblTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            this.lblTitle.Location = new Point(24, 22);
            this.lblTitle.Margin = new Padding(0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(496, 40);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Предметы";
            this.lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Dock = DockStyle.Fill;
            this.lblSubtitle.Font = new Font("Segoe UI", 9.75F);
            this.lblSubtitle.Location = new Point(24, 62);
            this.lblSubtitle.Margin = new Padding(0, 0, 0, 12);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new Size(496, 34);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Здесь отображаются все предметы из справочника и журнала оценок.";
            this.lblSubtitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // listPanel
            // 
            this.listPanel.Controls.Add(this.lstSubjects);
            this.listPanel.Dock = DockStyle.Fill;
            this.listPanel.Location = new Point(24, 96);
            this.listPanel.Margin = new Padding(0, 0, 0, 14);
            this.listPanel.Name = "listPanel";
            this.listPanel.Padding = new Padding(10);
            this.listPanel.Size = new Size(496, 276);
            this.listPanel.TabIndex = 2;
            // 
            // lstSubjects
            // 
            this.lstSubjects.Dock = DockStyle.Fill;
            this.lstSubjects.Font = new Font("Segoe UI", 10.5F);
            this.lstSubjects.FormattingEnabled = true;
            this.lstSubjects.ItemHeight = 23;
            this.lstSubjects.Location = new Point(10, 10);
            this.lstSubjects.Margin = new Padding(0);
            this.lstSubjects.Name = "lstSubjects";
            this.lstSubjects.Size = new Size(476, 256);
            this.lstSubjects.TabIndex = 0;
            // 
            // actionsPanel
            // 
            this.actionsPanel.AutoSize = true;
            this.actionsPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.actionsPanel.Controls.Add(this.btnAddSubject);
            this.actionsPanel.Controls.Add(this.btnDeleteSubject);
            this.actionsPanel.Controls.Add(this.btnClose);
            this.actionsPanel.Dock = DockStyle.Fill;
            this.actionsPanel.Location = new Point(24, 386);
            this.actionsPanel.Margin = new Padding(0);
            this.actionsPanel.Name = "actionsPanel";
            this.actionsPanel.Size = new Size(496, 42);
            this.actionsPanel.TabIndex = 3;
            this.actionsPanel.WrapContents = true;
            // 
            // btnAddSubject
            // 
            this.btnAddSubject.AutoSize = true;
            this.btnAddSubject.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            this.btnAddSubject.Location = new Point(0, 0);
            this.btnAddSubject.Margin = new Padding(0, 0, 12, 8);
            this.btnAddSubject.MinimumSize = new Size(180, 44);
            this.btnAddSubject.Name = "btnAddSubject";
            this.btnAddSubject.Padding = new Padding(18, 8, 18, 8);
            this.btnAddSubject.Size = new Size(184, 44);
            this.btnAddSubject.TabIndex = 0;
            this.btnAddSubject.Text = "Добавить предмет";
            this.btnAddSubject.UseVisualStyleBackColor = true;
            this.btnAddSubject.Click += new System.EventHandler(this.btnAddSubject_Click);
            // 
            // btnDeleteSubject
            // 
            this.btnDeleteSubject.AutoSize = true;
            this.btnDeleteSubject.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            this.btnDeleteSubject.Location = new Point(196, 0);
            this.btnDeleteSubject.Margin = new Padding(0, 0, 12, 8);
            this.btnDeleteSubject.MinimumSize = new Size(170, 44);
            this.btnDeleteSubject.Name = "btnDeleteSubject";
            this.btnDeleteSubject.Padding = new Padding(18, 8, 18, 8);
            this.btnDeleteSubject.Size = new Size(173, 44);
            this.btnDeleteSubject.TabIndex = 1;
            this.btnDeleteSubject.Text = "Удалить предмет";
            this.btnDeleteSubject.UseVisualStyleBackColor = true;
            this.btnDeleteSubject.Click += new System.EventHandler(this.btnDeleteSubject_Click);
            // 
            // btnClose
            // 
            this.btnClose.AutoSize = true;
            this.btnClose.Font = new Font("Segoe UI", 10F);
            this.btnClose.Location = new Point(381, 0);
            this.btnClose.Margin = new Padding(0);
            this.btnClose.MinimumSize = new Size(115, 44);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new Padding(18, 8, 18, 8);
            this.btnClose.Size = new Size(115, 44);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FormSubjectsCatalog
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(544, 468);
            this.Controls.Add(this.rootLayout);
            this.MinimumSize = new Size(500, 420);
            this.Name = "FormSubjectsCatalog";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Предметы";
            this.Load += new System.EventHandler(this.FormSubjectsCatalog_Load);
            this.rootLayout.ResumeLayout(false);
            this.rootLayout.PerformLayout();
            this.listPanel.ResumeLayout(false);
            this.actionsPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}
