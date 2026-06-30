namespace StudentRegistrationSystem
{
    partial class FormTopStudents
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTopStudents));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.listPanel = new System.Windows.Forms.Panel();
            this.listTop = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.listPanel.SuspendLayout();
            this.SuspendLayout();

            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lblTitle, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblSubtitle, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.listPanel, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(22, 18, 22, 18);
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(720, 430);
            this.tableLayoutPanel1.TabIndex = 0;

            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(676, 52);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Лучшие студенты";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.lblSubtitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblSubtitle.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(676, 34);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Дважды нажмите по студенту, чтобы открыть его полную информацию.";
            this.lblSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.listPanel.Controls.Add(this.listTop);
            this.listPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listPanel.Location = new System.Drawing.Point(22, 116);
            this.listPanel.Margin = new System.Windows.Forms.Padding(0);
            this.listPanel.Name = "listPanel";
            this.listPanel.Padding = new System.Windows.Forms.Padding(14);
            this.listPanel.Size = new System.Drawing.Size(676, 296);
            this.listPanel.TabIndex = 2;

            this.listTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listTop.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listTop.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listTop.FormattingEnabled = true;
            this.listTop.HorizontalScrollbar = false;
            this.listTop.IntegralHeight = false;
            this.listTop.ItemHeight = 36;
            this.listTop.Margin = new System.Windows.Forms.Padding(0);
            this.listTop.Name = "listTop";
            this.listTop.SelectionMode = System.Windows.Forms.SelectionMode.One;
            this.listTop.Size = new System.Drawing.Size(648, 268);
            this.listTop.TabIndex = 0;
            this.listTop.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listTop_DrawItem);
            this.listTop.DoubleClick += new System.EventHandler(this.listTop_DoubleClick);

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 430);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(980, 760);
            this.MinimumSize = new System.Drawing.Size(680, 360);
            this.Name = "FormTopStudents";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Лучшие студенты";
            this.Load += new System.EventHandler(this.FormTopStudents_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.listPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel listPanel;
        private System.Windows.Forms.ListBox listTop;
    }
}
