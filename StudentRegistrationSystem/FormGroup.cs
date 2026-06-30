using StudentRegistrationSystem.Services;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace StudentRegistrationSystem
{
    public partial class FormGroup : Form
    {
        public bool GroupsChanged { get; private set; }
        private int? _selectedGroupId;
        private readonly string _initialGroupName;
        private readonly GroupService _groupService = new GroupService();

        public FormGroup(string initialGroupName = null)
        {
            InitializeComponent();
            Text = UiCaptions.NewGroup;
            _initialGroupName = initialGroupName;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            _groupService.EnsureSchema();
            LoadGroups();
            ApplyTheme();
            Theme.ApplyRoundedRegion(btnSave, 16);
            Theme.ApplyRoundedRegion(btnDelete, 16);
            Theme.ApplyRoundedRegion(btnClose, 16);
            Theme.ApplyRoundedRegion(groupDetails, 18);
        }

        private void ApplyTheme()
        {
            BackColor = Theme.BackColor;
            ForeColor = Theme.ForeColor;

            splitMain.BackColor = Theme.BackColor;
            splitMain.Panel1.BackColor = Theme.BackColor;
            splitMain.Panel2.BackColor = Theme.BackColor;
            leftLayout.BackColor = Theme.BackColor;
            rightLayout.BackColor = Theme.BackColor;
            groupDetails.BackColor = Theme.GridColor;
            groupDetails.ForeColor = Theme.ForeColor;
            lblNewGroupTitle.ForeColor = Theme.ForeColor;
            lblExistingGroupsTitle.ForeColor = Theme.ForeColor;

            ApplyTextBoxTheme(txtGroupName);
            ApplyTextBoxTheme(txtSpecialty);
            ApplyListBoxTheme(lstGroups);
            ApplyListBoxTheme(lstStudents);
            ApplyButtonTheme(btnSave, true);
            ApplyButtonTheme(btnDelete, false);
            ApplyButtonTheme(btnClose, false);
        }

        private static void ApplyTextBoxTheme(TextBox box)
        {
            Theme.StyleTextBox(box);
        }

        private static void ApplyListBoxTheme(ListBox listBox)
        {
            Theme.StyleListBox(listBox);
        }

        private void ApplyButtonTheme(Button button, bool primary)
        {
            if (primary)
                Theme.StylePrimaryButton(button);
            else
                Theme.StyleSecondaryButton(button);
        }

        private void FormGroup_Load(object sender, EventArgs e)
        {
        }

        private void LoadGroups()
        {
            DataTable groups = _groupService.GetAll();
            lstGroups.DisplayMember = "GroupName";
            lstGroups.ValueMember = "Id";
            lstGroups.DataSource = groups;

            if (!string.IsNullOrWhiteSpace(_initialGroupName))
            {
                SelectGroupByName(_initialGroupName);
                return;
            }

            if (lstGroups.Items.Count > 0)
                ShowSelectedGroupDetails();
            else
                ClearGroupDetails();
        }

        private void SelectGroupByName(string groupName)
        {
            if (string.IsNullOrWhiteSpace(groupName))
                return;

            for (int i = 0; i < lstGroups.Items.Count; i++)
            {
                DataRowView item = lstGroups.Items[i] as DataRowView;
                if (item == null)
                    continue;

                if (string.Equals(Convert.ToString(item["GroupName"]), groupName, StringComparison.OrdinalIgnoreCase))
                {
                    lstGroups.SelectedIndex = i;
                    ShowSelectedGroupDetails();
                    return;
                }
            }
        }

        private void FillEditorFromSelectedGroup(DataRowView selectedGroup)
        {
            _selectedGroupId = Convert.ToInt32(selectedGroup["Id"]);
            txtGroupName.Text = Convert.ToString(selectedGroup["GroupName"]);
            txtSpecialty.Text = Convert.ToString(selectedGroup["Specialty"]);
            btnSave.Text = "Сохранить изменения";
        }

        private void ResetEditor()
        {
            _selectedGroupId = null;
            txtGroupName.Clear();
            txtSpecialty.Clear();
            btnSave.Text = "Сохранить группу";
        }

        private void ShowSelectedGroupDetails()
        {
            DataRowView selectedGroup = lstGroups.SelectedItem as DataRowView;
            if (selectedGroup == null)
            {
                ClearGroupDetails();
                return;
            }

            int groupId = Convert.ToInt32(selectedGroup["Id"]);

            DataTable details = _groupService.GetDetailsById(groupId);

            if (details.Rows.Count == 0)
            {
                ClearGroupDetails();
                return;
            }

            DataRow row = details.Rows[0];
            string specialty = Convert.ToString(row["Specialty"]);
            FillEditorFromSelectedGroup(selectedGroup);

            lblDetailName.Text = DisplayTextHelper.SanitizeNameForGrid(row["GroupName"]);
            lblDetailSpecialty.Text = string.IsNullOrWhiteSpace(specialty) ? "Не указана" : specialty;
            lblDetailCount.Text = Convert.ToString(row["StudentCount"]);

            DataTable students = _groupService.GetStudents(groupId, true);

            lstStudents.DataSource = null;
            lstStudents.Items.Clear();

            if (students.Rows.Count == 0)
            {
                lstStudents.Items.Add("Студентов пока нет");
                return;
            }

            foreach (DataRow student in students.Rows)
                student["FullName"] = DisplayTextHelper.SanitizeNameForGrid(student["FullName"]);

            lstStudents.DisplayMember = "FullName";
            lstStudents.ValueMember = "Id";
            lstStudents.DataSource = students;
        }

        private void ClearGroupDetails()
        {
            lblDetailName.Text = "Выберите группу";
            lblDetailSpecialty.Text = "Не указана";
            lblDetailCount.Text = "0";
            lstStudents.DataSource = null;
            lstStudents.Items.Clear();
            lstStudents.Items.Add("Студентов пока нет");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtGroupName.Text.Trim();
                string specialty = txtSpecialty.Text.Trim();

                if (string.IsNullOrWhiteSpace(name))
                {
                    MessageBox.Show(UiCaptions.GroupEnterName, UiCaptions.NewGroup, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataRowView selectedGroup = lstGroups.SelectedItem as DataRowView;
                string selectedGroupName = selectedGroup == null ? string.Empty : Convert.ToString(selectedGroup["GroupName"]).Trim();

                if (_selectedGroupId.HasValue && string.Equals(name, selectedGroupName, StringComparison.OrdinalIgnoreCase))
                {
                    _groupService.UpdateSpecialty(_selectedGroupId.Value, specialty);
                    GroupsChanged = true;
                    LoadGroups();

                    for (int i = 0; i < lstGroups.Items.Count; i++)
                    {
                        DataRowView item = lstGroups.Items[i] as DataRowView;
                        if (item != null && Convert.ToInt32(item["Id"]) == _selectedGroupId.Value)
                        {
                            lstGroups.SelectedIndex = i;
                            break;
                        }
                    }

                    MessageBox.Show(UiCaptions.GroupSpecialtyUpdated, UiCaptions.NewGroup, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (_groupService.Exists(name))
                {
                    MessageBox.Show(UiCaptions.GroupAlreadyExists, UiCaptions.NewGroup, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                _groupService.Add(name, specialty);

                GroupsChanged = true;
                ResetEditor();
                LoadGroups();

                MessageBox.Show(UiCaptions.GroupSavedShort, UiCaptions.NewGroup, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                MessageBox.Show(ex.Message, UiCaptions.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstGroups_DoubleClick(object sender, EventArgs e)
        {
            ShowSelectedGroupDetails();
        }

        private void lstGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowSelectedGroupDetails();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DataRowView selectedGroup = lstGroups.SelectedItem as DataRowView;
                if (selectedGroup == null)
                {
                    MessageBox.Show(UiCaptions.GroupSelectForDelete, UiCaptions.NewGroup, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                int groupId = Convert.ToInt32(selectedGroup["Id"]);
                string groupName = Convert.ToString(selectedGroup["GroupName"]);

                int studentsCount = _groupService.CountStudents(groupId);
                if (studentsCount > 0)
                {
                    MessageBox.Show(UiCaptions.GroupCannotDeleteWithStudents, UiCaptions.NewGroup, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show(
                    string.Format(UiCaptions.GroupDeleteConfirmFormat, groupName),
                    UiCaptions.NewGroup,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result != DialogResult.Yes)
                    return;

                _groupService.Delete(groupId);

                GroupsChanged = true;
                ResetEditor();
                LoadGroups();

                MessageBox.Show(UiCaptions.GroupDeleted, UiCaptions.NewGroup, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                MessageBox.Show(ex.Message, UiCaptions.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstStudents_DoubleClick(object sender, EventArgs e)
        {
            DataRowView selectedStudent = lstStudents.SelectedItem as DataRowView;
            if (selectedStudent == null)
                return;

            int studentId = Convert.ToInt32(selectedStudent["Id"]);
            using (var form = new FormStudentDetails(studentId))
                form.ShowDialog(this);
        }
    }
}
