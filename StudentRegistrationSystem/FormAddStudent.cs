using StudentRegistrationSystem.Models;

namespace StudentRegistrationSystem
{
    /// <summary>Форма добавления студента (отдельный тип в решении для ясности).</summary>
    public sealed class FormAddStudent : FormStudent
    {
        public FormAddStudent(int? semesterId = null) : base(semesterId)
        {
        }
    }
}
