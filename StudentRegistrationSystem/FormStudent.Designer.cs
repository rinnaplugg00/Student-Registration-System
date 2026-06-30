namespace StudentRegistrationSystem
{
    partial class FormStudent
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStudent));
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblFormSubtitle = new System.Windows.Forms.Label();
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.panelBody = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblSectionGrades = new System.Windows.Forms.Label();
            this.lblSubjectGrades = new System.Windows.Forms.Label();
            this.dgvSubjectGrades = new System.Windows.Forms.DataGridView();
            this.flowSubjectRowButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnRemoveSubjectRow = new System.Windows.Forms.Button();
            this.btnAddSubjectRow = new System.Windows.Forms.Button();
            this.panelFioBlock = new System.Windows.Forms.Panel();
            this.tableLayoutInnerFio = new System.Windows.Forms.TableLayoutPanel();
            this.lblSectionMain = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbGroup = new System.Windows.Forms.ComboBox();
            this.lblBirth = new System.Windows.Forms.Label();
            this.dtpBirthDate = new System.Windows.Forms.DateTimePicker();
            this.lblSemester = new System.Windows.Forms.Label();
            this.cmbSemester = new System.Windows.Forms.ComboBox();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.flowButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panelHeader.SuspendLayout();
            this.panelBody.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubjectGrades)).BeginInit();
            this.flowSubjectRowButtons.SuspendLayout();
            this.panelFioBlock.SuspendLayout();
            this.tableLayoutInnerFio.SuspendLayout();
            this.panelFooter.SuspendLayout();
            this.flowButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.lblFormSubtitle);
            this.panelHeader.Controls.Add(this.lblFormTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Padding = new System.Windows.Forms.Padding(24, 16, 24, 14);
            this.panelHeader.Size = new System.Drawing.Size(789, 76);
            this.panelHeader.TabIndex = 0;
            // 
            // lblFormSubtitle
            // 
            this.lblFormSubtitle.AutoSize = true;
            this.lblFormSubtitle.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblFormSubtitle.Location = new System.Drawing.Point(24, 43);
            this.lblFormSubtitle.MaximumSize = new System.Drawing.Size(703, 0);
            this.lblFormSubtitle.Name = "lblFormSubtitle";
            this.lblFormSubtitle.Size = new System.Drawing.Size(681, 17);
            this.lblFormSubtitle.TabIndex = 1;
            this.lblFormSubtitle.Text = "Обязательно: ФИО и группа. Укажите хотя бы одну оценку — общие баллы и/или строки" +
    " в таблице предметов.";
            // 
            // lblFormTitle
            // 
            this.lblFormTitle.AutoSize = true;
            this.lblFormTitle.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblFormTitle.Location = new System.Drawing.Point(24, 16);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Size = new System.Drawing.Size(159, 28);
            this.lblFormTitle.TabIndex = 0;
            this.lblFormTitle.Text = "Новый студент";
            // 
            // panelBody
            // 
            this.panelBody.AutoScroll = true;
            this.panelBody.Controls.Add(this.tableLayoutPanel1);
            this.panelBody.Controls.Add(this.panelFioBlock);
            this.panelBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBody.Location = new System.Drawing.Point(0, 76);
            this.panelBody.Name = "panelBody";
            this.panelBody.Padding = new System.Windows.Forms.Padding(24, 21, 24, 21);
            this.panelBody.Size = new System.Drawing.Size(789, 552);
            this.panelBody.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 161F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lblSectionGrades, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblSubjectGrades, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.dgvSubjectGrades, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowSubjectRowButtons, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(24, 248);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 205F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(741, 295);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // lblSectionGrades
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.lblSectionGrades, 2);
            this.lblSectionGrades.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSectionGrades.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblSectionGrades.Location = new System.Drawing.Point(0, 7);
            this.lblSectionGrades.Margin = new System.Windows.Forms.Padding(0, 7, 0, 5);
            this.lblSectionGrades.Name = "lblSectionGrades";
            this.lblSectionGrades.Size = new System.Drawing.Size(741, 21);
            this.lblSectionGrades.TabIndex = 4;
            this.lblSectionGrades.Text = "Оценки по предметам";
            this.lblSectionGrades.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSubjectGrades
            // 
            this.lblSubjectGrades.AutoSize = true;
            this.lblSubjectGrades.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSubjectGrades.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.lblSubjectGrades.Location = new System.Drawing.Point(0, 35);
            this.lblSubjectGrades.Margin = new System.Windows.Forms.Padding(0, 2, 10, 2);
            this.lblSubjectGrades.Name = "lblSubjectGrades";
            this.lblSubjectGrades.Size = new System.Drawing.Size(151, 201);
            this.lblSubjectGrades.TabIndex = 6;
            this.lblSubjectGrades.Text = "Предмет, балл и дата";
            // 
            // dgvSubjectGrades
            // 
            this.dgvSubjectGrades.AllowUserToAddRows = false;
            this.dgvSubjectGrades.AllowUserToDeleteRows = false;
            this.dgvSubjectGrades.ColumnHeadersHeight = 38;
            this.dgvSubjectGrades.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvSubjectGrades.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSubjectGrades.Location = new System.Drawing.Point(164, 38);
            this.dgvSubjectGrades.Margin = new System.Windows.Forms.Padding(3, 5, 10, 7);
            this.dgvSubjectGrades.MultiSelect = false;
            this.dgvSubjectGrades.Name = "dgvSubjectGrades";
            this.dgvSubjectGrades.RowHeadersVisible = false;
            this.dgvSubjectGrades.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSubjectGrades.Size = new System.Drawing.Size(567, 193);
            this.dgvSubjectGrades.TabIndex = 5;
            // 
            // flowSubjectRowButtons
            // 
            this.flowSubjectRowButtons.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.flowSubjectRowButtons, 2);
            this.flowSubjectRowButtons.Controls.Add(this.btnRemoveSubjectRow);
            this.flowSubjectRowButtons.Controls.Add(this.btnAddSubjectRow);
            this.flowSubjectRowButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowSubjectRowButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowSubjectRowButtons.Location = new System.Drawing.Point(0, 238);
            this.flowSubjectRowButtons.Margin = new System.Windows.Forms.Padding(0);
            this.flowSubjectRowButtons.Name = "flowSubjectRowButtons";
            this.flowSubjectRowButtons.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.flowSubjectRowButtons.Size = new System.Drawing.Size(741, 47);
            this.flowSubjectRowButtons.TabIndex = 6;
            // 
            // btnRemoveSubjectRow
            // 
            this.btnRemoveSubjectRow.AutoSize = true;
            this.btnRemoveSubjectRow.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnRemoveSubjectRow.Location = new System.Drawing.Point(577, 8);
            this.btnRemoveSubjectRow.Name = "btnRemoveSubjectRow";
            this.btnRemoveSubjectRow.Padding = new System.Windows.Forms.Padding(12, 4, 12, 4);
            this.btnRemoveSubjectRow.Size = new System.Drawing.Size(161, 35);
            this.btnRemoveSubjectRow.TabIndex = 0;
            this.btnRemoveSubjectRow.Text = "Удалить выбранное";
            this.btnRemoveSubjectRow.UseVisualStyleBackColor = true;
            // 
            // btnAddSubjectRow
            // 
            this.btnAddSubjectRow.AutoSize = true;
            this.btnAddSubjectRow.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnAddSubjectRow.Location = new System.Drawing.Point(445, 8);
            this.btnAddSubjectRow.Name = "btnAddSubjectRow";
            this.btnAddSubjectRow.Padding = new System.Windows.Forms.Padding(12, 4, 12, 4);
            this.btnAddSubjectRow.Size = new System.Drawing.Size(126, 35);
            this.btnAddSubjectRow.TabIndex = 1;
            this.btnAddSubjectRow.Text = "+ Предмет";
            this.btnAddSubjectRow.UseVisualStyleBackColor = true;
            // 
            // panelFioBlock
            // 
            this.panelFioBlock.Controls.Add(this.tableLayoutInnerFio);
            this.panelFioBlock.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFioBlock.Location = new System.Drawing.Point(24, 21);
            this.panelFioBlock.Margin = new System.Windows.Forms.Padding(0, 0, 0, 14);
            this.panelFioBlock.MinimumSize = new System.Drawing.Size(0, 227);
            this.panelFioBlock.Name = "panelFioBlock";
            this.panelFioBlock.Padding = new System.Windows.Forms.Padding(0, 0, 0, 7);
            this.panelFioBlock.Size = new System.Drawing.Size(741, 227);
            this.panelFioBlock.TabIndex = 0;
            // 
            // tableLayoutInnerFio
            // 
            this.tableLayoutInnerFio.ColumnCount = 2;
            this.tableLayoutInnerFio.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 161F));
            this.tableLayoutInnerFio.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutInnerFio.Controls.Add(this.lblSectionMain, 0, 0);
            this.tableLayoutInnerFio.Controls.Add(this.label1, 0, 1);
            this.tableLayoutInnerFio.Controls.Add(this.txtFullName, 1, 1);
            this.tableLayoutInnerFio.Controls.Add(this.label3, 0, 2);
            this.tableLayoutInnerFio.Controls.Add(this.cmbGroup, 1, 2);
            this.tableLayoutInnerFio.Controls.Add(this.lblBirth, 0, 3);
            this.tableLayoutInnerFio.Controls.Add(this.dtpBirthDate, 1, 3);
            this.tableLayoutInnerFio.Controls.Add(this.lblSemester, 0, 4);
            this.tableLayoutInnerFio.Controls.Add(this.cmbSemester, 1, 4);
            this.tableLayoutInnerFio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutInnerFio.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutInnerFio.Name = "tableLayoutInnerFio";
            this.tableLayoutInnerFio.RowCount = 5;
            this.tableLayoutInnerFio.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutInnerFio.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.tableLayoutInnerFio.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutInnerFio.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutInnerFio.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutInnerFio.Size = new System.Drawing.Size(741, 220);
            this.tableLayoutInnerFio.TabIndex = 0;
            // 
            // lblSectionMain
            // 
            this.tableLayoutInnerFio.SetColumnSpan(this.lblSectionMain, 2);
            this.lblSectionMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSectionMain.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblSectionMain.Location = new System.Drawing.Point(0, 0);
            this.lblSectionMain.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.lblSectionMain.Name = "lblSectionMain";
            this.lblSectionMain.Size = new System.Drawing.Size(741, 30);
            this.lblSectionMain.TabIndex = 0;
            this.lblSectionMain.Text = "Личные данные";
            this.lblSectionMain.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(0, 38);
            this.label1.Margin = new System.Windows.Forms.Padding(0, 3, 10, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 43);
            this.label1.TabIndex = 1;
            this.label1.Text = "Фамилия, имя, отчество";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtFullName
            // 
            this.txtFullName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFullName.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtFullName.Location = new System.Drawing.Point(164, 40);
            this.txtFullName.Margin = new System.Windows.Forms.Padding(3, 5, 10, 5);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(567, 27);
            this.txtFullName.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.label3.Location = new System.Drawing.Point(0, 86);
            this.label3.Margin = new System.Windows.Forms.Padding(0, 2, 10, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(151, 41);
            this.label3.TabIndex = 2;
            this.label3.Text = "Учебная группа";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbGroup
            // 
            this.cmbGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGroup.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cmbGroup.FormattingEnabled = true;
            this.cmbGroup.Location = new System.Drawing.Point(164, 91);
            this.cmbGroup.Margin = new System.Windows.Forms.Padding(3, 7, 10, 7);
            this.cmbGroup.MaximumSize = new System.Drawing.Size(412, 0);
            this.cmbGroup.Name = "cmbGroup";
            this.cmbGroup.Size = new System.Drawing.Size(412, 28);
            this.cmbGroup.TabIndex = 2;
            // 
            // lblBirth
            // 
            this.lblBirth.AutoSize = true;
            this.lblBirth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBirth.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.lblBirth.Location = new System.Drawing.Point(0, 131);
            this.lblBirth.Margin = new System.Windows.Forms.Padding(0, 2, 10, 2);
            this.lblBirth.Name = "lblBirth";
            this.lblBirth.Size = new System.Drawing.Size(151, 41);
            this.lblBirth.TabIndex = 3;
            this.lblBirth.Text = "День рождения";
            this.lblBirth.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpBirthDate
            // 
            this.dtpBirthDate.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.dtpBirthDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpBirthDate.Location = new System.Drawing.Point(164, 136);
            this.dtpBirthDate.Margin = new System.Windows.Forms.Padding(3, 7, 10, 7);
            this.dtpBirthDate.Name = "dtpBirthDate";
            this.dtpBirthDate.ShowCheckBox = true;
            this.dtpBirthDate.Size = new System.Drawing.Size(258, 27);
            this.dtpBirthDate.TabIndex = 3;
            // 
            // lblSemester
            // 
            this.lblSemester.AutoSize = true;
            this.lblSemester.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSemester.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.lblSemester.Location = new System.Drawing.Point(0, 176);
            this.lblSemester.Margin = new System.Windows.Forms.Padding(0, 2, 10, 2);
            this.lblSemester.Name = "lblSemester";
            this.lblSemester.Size = new System.Drawing.Size(151, 42);
            this.lblSemester.TabIndex = 4;
            this.lblSemester.Text = "Семестр";
            this.lblSemester.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbSemester
            // 
            this.cmbSemester.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSemester.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSemester.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cmbSemester.FormattingEnabled = true;
            this.cmbSemester.Location = new System.Drawing.Point(164, 181);
            this.cmbSemester.Margin = new System.Windows.Forms.Padding(3, 7, 10, 7);
            this.cmbSemester.MaximumSize = new System.Drawing.Size(412, 0);
            this.cmbSemester.Name = "cmbSemester";
            this.cmbSemester.Size = new System.Drawing.Size(412, 28);
            this.cmbSemester.TabIndex = 4;
            // 
            // panelFooter
            // 
            this.panelFooter.Controls.Add(this.flowButtons);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 628);
            this.panelFooter.MinimumSize = new System.Drawing.Size(0, 83);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Padding = new System.Windows.Forms.Padding(24, 12, 24, 17);
            this.panelFooter.Size = new System.Drawing.Size(789, 83);
            this.panelFooter.TabIndex = 2;
            // 
            // flowButtons
            // 
            this.flowButtons.AutoSize = true;
            this.flowButtons.Controls.Add(this.btnSave);
            this.flowButtons.Controls.Add(this.btnCancel);
            this.flowButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowButtons.Location = new System.Drawing.Point(532, 12);
            this.flowButtons.Name = "flowButtons";
            this.flowButtons.Size = new System.Drawing.Size(233, 54);
            this.flowButtons.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.AutoSize = true;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnSave.Location = new System.Drawing.Point(9, 3);
            this.btnSave.Margin = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(24, 8, 24, 8);
            this.btnSave.Size = new System.Drawing.Size(224, 45);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Сохранить изменения";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.btnCancel.Location = new System.Drawing.Point(120, 54);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(19, 8, 19, 8);
            this.btnCancel.Size = new System.Drawing.Size(110, 45);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Закрыть";
            // 
            // FormStudent
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(789, 711);
            this.Controls.Add(this.panelBody);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.panelHeader);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(671, 629);
            this.Name = "FormStudent";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Карточка студента";
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelBody.ResumeLayout(false);
            this.panelBody.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubjectGrades)).EndInit();
            this.flowSubjectRowButtons.ResumeLayout(false);
            this.flowSubjectRowButtons.PerformLayout();
            this.panelFioBlock.ResumeLayout(false);
            this.tableLayoutInnerFio.ResumeLayout(false);
            this.tableLayoutInnerFio.PerformLayout();
            this.panelFooter.ResumeLayout(false);
            this.panelFooter.PerformLayout();
            this.flowButtons.ResumeLayout(false);
            this.flowButtons.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.Label lblFormSubtitle;
        private System.Windows.Forms.Panel panelBody;
        private System.Windows.Forms.Panel panelFioBlock;
        private System.Windows.Forms.TableLayoutPanel tableLayoutInnerFio;
        private System.Windows.Forms.Label lblSectionMain;
        private System.Windows.Forms.Label lblSectionGrades;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.Label lblBirth;
        private System.Windows.Forms.DateTimePicker dtpBirthDate;
        private System.Windows.Forms.Label lblSemester;
        private System.Windows.Forms.ComboBox cmbSemester;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbGroup;
        private System.Windows.Forms.Label lblSubjectGrades;
        private System.Windows.Forms.DataGridView dgvSubjectGrades;
        private System.Windows.Forms.FlowLayoutPanel flowSubjectRowButtons;
        private System.Windows.Forms.Button btnAddSubjectRow;
        private System.Windows.Forms.Button btnRemoveSubjectRow;
        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.FlowLayoutPanel flowButtons;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}
