using StudentRegistrationSystem.Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace StudentRegistrationSystem
{
    public partial class FormSubject : Form
    {
        private readonly SubjectService _subjectService = new SubjectService();

        public FormSubject()
        {
            InitializeComponent();
            Text = UiCaptions.NewSubject;
            BackColor = Theme.BackColor;
            ForeColor = Theme.ForeColor;
            txtName.BackColor = Theme.GridColor;
            txtName.ForeColor = Theme.ForeColor;
            btnSave.BackColor = Theme.ButtonColor;
            btnSave.ForeColor = Theme.ForeColor;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.FlatAppearance.BorderSize = 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Введите название предмета.", UiCaptions.NewSubject, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _subjectService.Add(name);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сохранить предмет: " + ex.Message, UiCaptions.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
