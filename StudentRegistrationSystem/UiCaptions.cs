namespace StudentRegistrationSystem
{
    /// <summary>Заголовки окон, подписи колонок и тексты сообщений.</summary>
    internal static class UiCaptions
    {
        public const string MainWindow = "Журнал студентов";
        public const string StudentCard = "Карточка студента";
        public const string NewStudent = "Новый студент";
        public const string EditStudent = "Редактирование студента";
        public const string StudentGradesJournal = "Журнал оценок студента";
        public const string NewGroup = "Новая группа";
        public const string SubjectsCatalog = "Предметы";
        public const string NewSubject = "Новый предмет";
        public const string GroupAverages = "Средний балл по группам";
        public const string TopStudents = "Лучшие студенты";
        public const string Statistics = "Статистика успеваемости";
        public const string DeleteConfirm = "Удаление записи";
        public const string Attention = "Внимание";
        public const string Error = "Ошибка";

        public const string ColumnGroup = "Группа";
        public const string ColumnFullName = "ФИО";
        public const string ColumnAverageGrade = "Средний балл";
        public const string ColumnGpa = "GPA";
        public const string ColumnRiskStatus = "Статус";

        public const string ContextMenuEdit = "Изменить запись";
        public const string ContextMenuDelete = "Удалить запись";

        public const string AllSemesters = "Все периоды";
        public const string NoGroup = "Без группы";
        public const string ThemeLight = "Светлая";
        public const string ThemeDark = "Тёмная";

        public const string FilterApplyError = "Не удалось применить фильтр списка.";
        public const string RiskSummaryFormat = "Период: {0}. В зоне риска: {1}. Без оценок: {2}.";
        public const string ExportNoData = "Нет данных для экспорта. Измените фильтр или добавьте студентов.";
        public const string ExportSaveTitle = "Сохранить список студентов";
        public const string ExportSuccessFormat = "Список студентов сохранён.\n\n{0}";
        public const string ExportSaveErrorFormat = "Не удалось сохранить файл.\n\n{0}";
        public const string StudentAdded = "Студент успешно добавлен в журнал.";
        public const string StudentAddError = "Не удалось добавить студента. Проверьте данные и подключение к базе.";
        public const string StudentLoadError = "Не удалось загрузить данные студента для редактирования.";
        public const string DeleteStudentConfirm = "Удалить выбранного студента из журнала? Это действие нельзя отменить.";
        public const string StudentDeleted = "Запись о студенте удалена из журнала.";
        public const string GroupSaved = "Группа успешно сохранена.";
        public const string GroupEnterName = "Введите название учебной группы.";
        public const string GroupSpecialtyUpdated = "Специальность группы обновлена.";
        public const string GroupAlreadyExists = "Такая группа уже существует.";
        public const string GroupSavedShort = "Группа сохранена.";
        public const string GroupSelectForDelete = "Выберите группу для удаления.";
        public const string GroupCannotDeleteWithStudents = "Нельзя удалить группу, пока в ней есть студенты.";
        public const string GroupDeleteConfirmFormat = "Удалить группу \"{0}\"?";
        public const string GroupDeleted = "Группа удалена.";
        public const string PeriodPrefix = "Период: ";
        public const string DatabaseConnectionError = "Не удалось подключиться к базе данных. Проверьте строку подключения StudentDb в App.config и доступность SQL Server.";
    }
}
