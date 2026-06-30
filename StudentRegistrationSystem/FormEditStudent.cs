using StudentRegistrationSystem.Models;

namespace StudentRegistrationSystem
{
    /// <summary>Форма редактирования студента (данные загружаются из БД в MainForm через StudentService.LoadForEdit).</summary>
    public sealed class FormEditStudent : FormStudent
    {
        public FormEditStudent(Student student, int? semesterId = null) : base(student, semesterId)
        {
        }
    }
}
