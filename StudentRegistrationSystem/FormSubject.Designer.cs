namespace StudentRegistrationSystem
{
    partial class FormSubject
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
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.lbl = new System.Windows.Forms.Label();
            this.SuspendLayout();

            this.lbl.AutoSize = true;
            this.lbl.Location = new System.Drawing.Point(16, 20);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(115, 15);
            this.lbl.TabIndex = 0;
            this.lbl.Text = "Название предмета";

            this.txtName.Location = new System.Drawing.Point(16, 42);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(320, 23);
            this.txtName.TabIndex = 1;

            this.btnSave.Location = new System.Drawing.Point(16, 82);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 32);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Добавить предмет";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 130);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSubject";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Новый предмет";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lbl;
    }
}
