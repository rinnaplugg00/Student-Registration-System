/*
  Демонстрационные данные для защиты проекта.

  Выполняйте ПОСЛЕ:
    1) CreateInitialSchema.sql
    2) SubjectsAndGradesExtension.sql

  Скрипт идempotent: повторный запуск не создаёт дубликаты.
*/

-- USE [StudentRegistrationDb];
-- GO

SET NOCOUNT ON;

DECLARE @Spring2026Id INT;
DECLARE @GroupIs21Id INT;
DECLARE @GroupPo22Id INT;
DECLARE @GroupBd23Id INT;

/* --- Группы --- */
IF NOT EXISTS (SELECT 1 FROM dbo.Groups WHERE GroupName = N'ИС-21')
    INSERT INTO dbo.Groups (GroupName, Specialty)
    VALUES (N'ИС-21', N'Информационные системы и технологии');

IF NOT EXISTS (SELECT 1 FROM dbo.Groups WHERE GroupName = N'ПО-22')
    INSERT INTO dbo.Groups (GroupName, Specialty)
    VALUES (N'ПО-22', N'Программная инженерия');

IF NOT EXISTS (SELECT 1 FROM dbo.Groups WHERE GroupName = N'БД-23')
    INSERT INTO dbo.Groups (GroupName, Specialty)
    VALUES (N'БД-23', N'Базы данных и аналитика');

SET @GroupIs21Id = (SELECT Id FROM dbo.Groups WHERE GroupName = N'ИС-21');
SET @GroupPo22Id = (SELECT Id FROM dbo.Groups WHERE GroupName = N'ПО-22');
SET @GroupBd23Id = (SELECT Id FROM dbo.Groups WHERE GroupName = N'БД-23');

/* --- Предметы --- */
IF OBJECT_ID(N'dbo.Subjects', N'U') IS NOT NULL
BEGIN
    IF NOT EXISTS (SELECT 1 FROM dbo.Subjects WHERE SubjectName = N'Математика')
        INSERT INTO dbo.Subjects (SubjectName) VALUES (N'Математика');
    IF NOT EXISTS (SELECT 1 FROM dbo.Subjects WHERE SubjectName = N'Программирование')
        INSERT INTO dbo.Subjects (SubjectName) VALUES (N'Программирование');
    IF NOT EXISTS (SELECT 1 FROM dbo.Subjects WHERE SubjectName = N'Базы данных')
        INSERT INTO dbo.Subjects (SubjectName) VALUES (N'Базы данных');
    IF NOT EXISTS (SELECT 1 FROM dbo.Subjects WHERE SubjectName = N'Английский язык')
        INSERT INTO dbo.Subjects (SubjectName) VALUES (N'Английский язык');
END;

/* --- Семестр (весна 2025/2026, актуален для демо в июне 2026) --- */
IF NOT EXISTS (
    SELECT 1 FROM dbo.Semesters
    WHERE Name = N'Весенний 2025/2026'
)
BEGIN
    INSERT INTO dbo.Semesters (Name, StartDate, EndDate)
    VALUES (N'Весенний 2025/2026', '2026-02-01', '2026-06-30');
END;

SET @Spring2026Id = (
    SELECT Id FROM dbo.Semesters WHERE Name = N'Весенний 2025/2026'
);

/* --- Студенты (только если ещё нет записей с такими ФИО) --- */
IF NOT EXISTS (SELECT 1 FROM dbo.Students WHERE FullName = N'Иванов Иван Иванович')
BEGIN
    INSERT INTO dbo.Students (FullName, GroupId, BirthDate)
    VALUES
        (N'Иванов Иван Иванович', @GroupIs21Id, '2004-03-15'),
        (N'Петров Пётр Петрович', @GroupIs21Id, '2004-07-22'),
        (N'Сидорова Анна Сергеевна', @GroupIs21Id, '2005-01-10'),
        (N'Козлов Дмитрий Андреевич', @GroupPo22Id, '2004-11-05'),
        (N'Новикова Елена Викторовна', @GroupPo22Id, '2004-05-18'),
        (N'Морозов Артём Игоревич', @GroupPo22Id, '2005-02-28'),
        (N'Волкова Мария Александровна', @GroupBd23Id, '2004-09-12'),
        (N'Лебедев Никита Олегович', @GroupBd23Id, '2004-12-01'),
        (N'Соколова Дарья Павловна', @GroupBd23Id, '2005-04-03'),
        (N'Кузнецов Максим Романович', @GroupBd23Id, '2004-06-20');
END;

/* --- Оценки (только если у студента ещё нет оценок) --- */
DECLARE @StudentId INT;

/* Отличник: Иванов */
SET @StudentId = (SELECT Id FROM dbo.Students WHERE FullName = N'Иванов Иван Иванович');
IF @StudentId IS NOT NULL AND NOT EXISTS (SELECT 1 FROM dbo.Grades WHERE StudentId = @StudentId)
BEGIN
    INSERT INTO dbo.Grades (StudentId, Subject, Grade, GradeDate, SemesterId) VALUES
        (@StudentId, N'Математика', 95, NULL, @Spring2026Id),
        (@StudentId, N'Программирование', 92, '2026-03-10', @Spring2026Id),
        (@StudentId, N'Программирование', 94, '2026-04-15', @Spring2026Id),
        (@StudentId, N'Базы данных', 90, NULL, @Spring2026Id),
        (@StudentId, N'Английский язык', 88, '2026-05-05', @Spring2026Id);
END;

/* Зона риска: Петров */
SET @StudentId = (SELECT Id FROM dbo.Students WHERE FullName = N'Петров Пётр Петрович');
IF @StudentId IS NOT NULL AND NOT EXISTS (SELECT 1 FROM dbo.Grades WHERE StudentId = @StudentId)
BEGIN
    INSERT INTO dbo.Grades (StudentId, Subject, Grade, GradeDate, SemesterId) VALUES
        (@StudentId, N'Математика', 52, NULL, @Spring2026Id),
        (@StudentId, N'Программирование', 58, '2026-03-12', @Spring2026Id),
        (@StudentId, N'Базы данных', 55, NULL, @Spring2026Id);
END;

/* Без оценок: Сидорова — намеренно пусто */

/* Средний уровень: Козлов */
SET @StudentId = (SELECT Id FROM dbo.Students WHERE FullName = N'Козлов Дмитрий Андреевич');
IF @StudentId IS NOT NULL AND NOT EXISTS (SELECT 1 FROM dbo.Grades WHERE StudentId = @StudentId)
BEGIN
    INSERT INTO dbo.Grades (StudentId, Subject, Grade, GradeDate, SemesterId) VALUES
        (@StudentId, N'Математика', 72, NULL, @Spring2026Id),
        (@StudentId, N'Программирование', 78, '2026-04-01', @Spring2026Id),
        (@StudentId, N'Английский язык', 75, NULL, @Spring2026Id);
END;

/* Новикова — хорошист */
SET @StudentId = (SELECT Id FROM dbo.Students WHERE FullName = N'Новикова Елена Викторовна');
IF @StudentId IS NOT NULL AND NOT EXISTS (SELECT 1 FROM dbo.Grades WHERE StudentId = @StudentId)
BEGIN
    INSERT INTO dbo.Grades (StudentId, Subject, Grade, GradeDate, SemesterId) VALUES
        (@StudentId, N'Математика', 85, NULL, @Spring2026Id),
        (@StudentId, N'Программирование', 88, '2026-03-20', @Spring2026Id),
        (@StudentId, N'Базы данных', 82, '2026-05-10', @Spring2026Id);
END;

/* Морозов — зона риска */
SET @StudentId = (SELECT Id FROM dbo.Students WHERE FullName = N'Морозов Артём Игоревич');
IF @StudentId IS NOT NULL AND NOT EXISTS (SELECT 1 FROM dbo.Grades WHERE StudentId = @StudentId)
BEGIN
    INSERT INTO dbo.Grades (StudentId, Subject, Grade, GradeDate, SemesterId) VALUES
        (@StudentId, N'Математика', 48, NULL, @Spring2026Id),
        (@StudentId, N'Программирование', 57, NULL, @Spring2026Id);
END;

/* БД-23: разный уровень */
SET @StudentId = (SELECT Id FROM dbo.Students WHERE FullName = N'Волкова Мария Александровна');
IF @StudentId IS NOT NULL AND NOT EXISTS (SELECT 1 FROM dbo.Grades WHERE StudentId = @StudentId)
BEGIN
    INSERT INTO dbo.Grades (StudentId, Subject, Grade, GradeDate, SemesterId) VALUES
        (@StudentId, N'Базы данных', 91, NULL, @Spring2026Id),
        (@StudentId, N'Математика', 87, '2026-04-22', @Spring2026Id);
END;

SET @StudentId = (SELECT Id FROM dbo.Students WHERE FullName = N'Лебедев Никита Олегович');
IF @StudentId IS NOT NULL AND NOT EXISTS (SELECT 1 FROM dbo.Grades WHERE StudentId = @StudentId)
BEGIN
    INSERT INTO dbo.Grades (StudentId, Subject, Grade, GradeDate, SemesterId) VALUES
        (@StudentId, N'Базы данных', 63, NULL, @Spring2026Id),
        (@StudentId, N'Программирование', 68, NULL, @Spring2026Id);
END;

SET @StudentId = (SELECT Id FROM dbo.Students WHERE FullName = N'Соколова Дарья Павловна');
IF @StudentId IS NOT NULL AND NOT EXISTS (SELECT 1 FROM dbo.Grades WHERE StudentId = @StudentId)
BEGIN
    INSERT INTO dbo.Grades (StudentId, Subject, Grade, GradeDate, SemesterId) VALUES
        (@StudentId, N'Базы данных', 96, NULL, @Spring2026Id),
        (@StudentId, N'Математика', 93, '2026-05-18', @Spring2026Id),
        (@StudentId, N'Английский язык', 90, NULL, @Spring2026Id);
END;

SET @StudentId = (SELECT Id FROM dbo.Students WHERE FullName = N'Кузнецов Максим Романович');
IF @StudentId IS NOT NULL AND NOT EXISTS (SELECT 1 FROM dbo.Grades WHERE StudentId = @StudentId)
BEGIN
    INSERT INTO dbo.Grades (StudentId, Subject, Grade, GradeDate, SemesterId) VALUES
        (@StudentId, N'Базы данных', 59, NULL, @Spring2026Id),
        (@StudentId, N'Математика', 61, NULL, @Spring2026Id);
END;

PRINT N'Демо-данные загружены. Групп: 3, студентов: 10, семестр: Весенний 2025/2026.';
