namespace StudentRegistrationSystem
{
    partial class FormStudentDetails
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblName = new System.Windows.Forms.Label();
            this.lblGroup = new System.Windows.Forms.Label();
            this.lblBirthDate = new System.Windows.Forms.Label();
            this.lblSubjects = new System.Windows.Forms.Label();
            this.lblAverage1 = new System.Windows.Forms.Label();
            this.panelFilters = new System.Windows.Forms.FlowLayoutPanel();
            this.lblSubject = new System.Windows.Forms.Label();
            this.cmbSubject = new System.Windows.Forms.ComboBox();
            this.lblSemester = new System.Windows.Forms.Label();
            this.cmbSemester = new System.Windows.Forms.ComboBox();
            this.lblMonth = new System.Windows.Forms.Label();
            this.cmbMonth = new System.Windows.Forms.ComboBox();
            this.btnAddSubject = new System.Windows.Forms.Button();
            this.lblSubjectSummary = new System.Windows.Forms.Label();
            this.lblJournalHint = new System.Windows.Forms.Label();
            this.dgvCalendar = new System.Windows.Forms.DataGridView();
            this.listGrades = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelFilters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCalendar)).BeginInit();
            this.SuspendLayout();

            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lblName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblGroup, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblBirthDate, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblSubjects, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblAverage1, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.panelFilters, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.lblJournalHint, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.dgvCalendar, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.listGrades, 0, 8);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(18, 14, 18, 14);
            this.tableLayoutPanel1.RowCount = 9;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 56F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 44F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(840, 654);
            this.tableLayoutPanel1.TabIndex = 0;

            this.lblName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblName.Location = new System.Drawing.Point(21, 14);
            this.lblName.Margin = new System.Windows.Forms.Padding(3, 0, 3, 4);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(718, 48);
            this.lblName.TabIndex = 0;
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.lblGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblGroup.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblGroup.Location = new System.Drawing.Point(21, 66);
            this.lblGroup.Margin = new System.Windows.Forms.Padding(3, 0, 3, 4);
            this.lblGroup.Name = "lblGroup";
            this.lblGroup.Size = new System.Drawing.Size(718, 34);
            this.lblGroup.TabIndex = 1;
            this.lblGroup.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.lblBirthDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBirthDate.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.lblBirthDate.Location = new System.Drawing.Point(21, 104);
            this.lblBirthDate.Margin = new System.Windows.Forms.Padding(3, 0, 3, 2);
            this.lblBirthDate.Name = "lblBirthDate";
            this.lblBirthDate.Size = new System.Drawing.Size(798, 32);
            this.lblBirthDate.TabIndex = 2;
            this.lblBirthDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.lblSubjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSubjects.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.lblSubjects.Location = new System.Drawing.Point(21, 138);
            this.lblSubjects.Margin = new System.Windows.Forms.Padding(3, 0, 3, 2);
            this.lblSubjects.Name = "lblSubjects";
            this.lblSubjects.Size = new System.Drawing.Size(798, 40);
            this.lblSubjects.TabIndex = 3;
            this.lblSubjects.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.lblAverage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAverage1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblAverage1.Location = new System.Drawing.Point(21, 180);
            this.lblAverage1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 10);
            this.lblAverage1.Name = "lblAverage1";
            this.lblAverage1.Size = new System.Drawing.Size(798, 32);
            this.lblAverage1.TabIndex = 4;
            this.lblAverage1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.panelFilters.AutoSize = true;
            this.panelFilters.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelFilters.Controls.Add(this.lblSubject);
            this.panelFilters.Controls.Add(this.cmbSubject);
            this.panelFilters.Controls.Add(this.lblSemester);
            this.panelFilters.Controls.Add(this.cmbSemester);
            this.panelFilters.Controls.Add(this.lblMonth);
            this.panelFilters.Controls.Add(this.cmbMonth);
            this.panelFilters.Controls.Add(this.btnAddSubject);
            this.panelFilters.Controls.Add(this.lblSubjectSummary);
            this.panelFilters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFilters.Location = new System.Drawing.Point(21, 222);
            this.panelFilters.Margin = new System.Windows.Forms.Padding(3, 0, 3, 6);
            this.panelFilters.Name = "panelFilters";
            this.panelFilters.Padding = new System.Windows.Forms.Padding(0, 6, 0, 6);
            this.panelFilters.Size = new System.Drawing.Size(798, 56);
            this.panelFilters.TabIndex = 5;
            this.panelFilters.WrapContents = true;

            this.lblSubject.AutoSize = true;
            this.lblSubject.Margin = new System.Windows.Forms.Padding(0, 10, 8, 0);
            this.lblSubject.Name = "lblSubject";
            this.lblSubject.Size = new System.Drawing.Size(58, 15);
            this.lblSubject.TabIndex = 0;
            this.lblSubject.Text = "Предмет:";

            this.cmbSubject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSubject.FormattingEnabled = true;
            this.cmbSubject.Margin = new System.Windows.Forms.Padding(0, 6, 18, 0);
            this.cmbSubject.Name = "cmbSubject";
            this.cmbSubject.Size = new System.Drawing.Size(220, 23);
            this.cmbSubject.TabIndex = 1;
            
            this.lblSemester.AutoSize = true;
            this.lblSemester.Margin = new System.Windows.Forms.Padding(0, 10, 8, 0);
            this.lblSemester.Name = "lblSemester";
            this.lblSemester.Size = new System.Drawing.Size(59, 15);
            this.lblSemester.TabIndex = 2;
            this.lblSemester.Text = "Семестр:";

            this.cmbSemester.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSemester.FormattingEnabled = true;
            this.cmbSemester.Margin = new System.Windows.Forms.Padding(0, 6, 18, 0);
            this.cmbSemester.Name = "cmbSemester";
            this.cmbSemester.Size = new System.Drawing.Size(200, 23);
            this.cmbSemester.TabIndex = 3;

            this.lblMonth.AutoSize = true;
            this.lblMonth.Margin = new System.Windows.Forms.Padding(0, 10, 8, 0);
            this.lblMonth.Name = "lblMonth";
            this.lblMonth.Size = new System.Drawing.Size(49, 15);
            this.lblMonth.TabIndex = 4;
            this.lblMonth.Text = "Месяц:";

            this.cmbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMonth.FormattingEnabled = true;
            this.cmbMonth.Margin = new System.Windows.Forms.Padding(0, 6, 18, 0);
            this.cmbMonth.Name = "cmbMonth";
            this.cmbMonth.Size = new System.Drawing.Size(180, 23);
            this.cmbMonth.TabIndex = 5;

            this.btnAddSubject.AutoSize = true;
            this.btnAddSubject.Margin = new System.Windows.Forms.Padding(0, 4, 18, 0);
            this.btnAddSubject.Name = "btnAddSubject";
            this.btnAddSubject.Padding = new System.Windows.Forms.Padding(10, 4, 10, 4);
            this.btnAddSubject.Size = new System.Drawing.Size(108, 31);
            this.btnAddSubject.TabIndex = 4;
            this.btnAddSubject.Text = "+ Предмет";
            this.btnAddSubject.UseVisualStyleBackColor = true;
            this.btnAddSubject.Click += new System.EventHandler(this.btnAddSubject_Click);

            this.lblSubjectSummary.AutoSize = true;
            this.lblSubjectSummary.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.lblSubjectSummary.Name = "lblSubjectSummary";
            this.lblSubjectSummary.Size = new System.Drawing.Size(116, 15);
            this.lblSubjectSummary.TabIndex = 5;
            this.lblSubjectSummary.Text = "Сводный балл: нет";

            this.lblJournalHint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblJournalHint.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblJournalHint.Location = new System.Drawing.Point(21, 284);
            this.lblJournalHint.Margin = new System.Windows.Forms.Padding(3, 0, 3, 6);
            this.lblJournalHint.Name = "lblJournalHint";
            this.lblJournalHint.Size = new System.Drawing.Size(798, 28);
            this.lblJournalHint.TabIndex = 6;
            this.lblJournalHint.Text = "В нижней строке указываются оценки за конкретные дни. Пустое значение удаляет оценку, а сводный балл по предмету обновляется автоматически.";
            this.lblJournalHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.dgvCalendar.AllowUserToAddRows = false;
            this.dgvCalendar.AllowUserToDeleteRows = false;
            this.dgvCalendar.AllowUserToResizeRows = false;
            this.dgvCalendar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvCalendar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCalendar.Location = new System.Drawing.Point(21, 318);
            this.dgvCalendar.Margin = new System.Windows.Forms.Padding(3, 0, 3, 10);
            this.dgvCalendar.MultiSelect = false;
            this.dgvCalendar.Name = "dgvCalendar";
            this.dgvCalendar.RowHeadersWidth = 82;
            this.dgvCalendar.RowTemplate.Height = 32;
            this.dgvCalendar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvCalendar.Size = new System.Drawing.Size(798, 161);
            this.dgvCalendar.TabIndex = 7;
            this.dgvCalendar.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCalendar_CellEndEdit);

            this.listGrades.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listGrades.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listGrades.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listGrades.FormattingEnabled = true;
            this.listGrades.HorizontalScrollbar = false;
            this.listGrades.IntegralHeight = false;
            this.listGrades.ItemHeight = 32;
            this.listGrades.Location = new System.Drawing.Point(21, 492);
            this.listGrades.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.listGrades.Name = "listGrades";
            this.listGrades.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listGrades.Size = new System.Drawing.Size(798, 148);
            this.listGrades.TabIndex = 8;
            this.listGrades.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listGrades_DrawItem);

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 654);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximumSize = new System.Drawing.Size(1100, 860);
            this.MinimumSize = new System.Drawing.Size(720, 600);
            this.Name = "FormStudentDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Журнал оценок студента";
            this.Load += new System.EventHandler(this.FormStudentDetails_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panelFilters.ResumeLayout(false);
            this.panelFilters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCalendar)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListBox listGrades;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblGroup;
        private System.Windows.Forms.Label lblBirthDate;
        private System.Windows.Forms.Label lblSubjects;
        private System.Windows.Forms.Label lblAverage1;
        private System.Windows.Forms.FlowLayoutPanel panelFilters;
        private System.Windows.Forms.Label lblSubject;
        private System.Windows.Forms.ComboBox cmbSubject;
        private System.Windows.Forms.Label lblSemester;
        private System.Windows.Forms.ComboBox cmbSemester;
        private System.Windows.Forms.Label lblMonth;
        private System.Windows.Forms.ComboBox cmbMonth;
        private System.Windows.Forms.Button btnAddSubject;
        private System.Windows.Forms.Label lblSubjectSummary;
        private System.Windows.Forms.Label lblJournalHint;
        private System.Windows.Forms.DataGridView dgvCalendar;
    }
}
