/*
  Расширение для журнала оценок по дням (FormStudentDetails: предмет + месяц + сетка по числам).

  В SSMS: выберите вашу базу (как в App.config / connectionStrings StudentDb), затем выполните скрипт (F5).
  После этого перезапустите приложение — появится колонка GradeDate и при необходимости таблица Subjects.
*/

-- USE [pm4students67];
-- GO

/* Справочник предметов (кнопка «добавить предмет» в карточке студента). */
IF OBJECT_ID(N'dbo.Subjects', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Subjects (
        Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        SubjectName NVARCHAR(100) NOT NULL
    );
END;
GO

/* Дата оценки в журнале (одна строка = один день + предмет). */
IF COL_LENGTH(N'dbo.Grades', N'GradeDate') IS NULL
BEGIN
    ALTER TABLE dbo.Grades ADD GradeDate DATE NULL;
END;
GO

IF OBJECT_ID(N'dbo.Semesters', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Semesters (
        Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        Name NVARCHAR(120) NOT NULL,
        StartDate DATE NOT NULL,
        EndDate DATE NOT NULL
    );
END;
GO

IF COL_LENGTH(N'dbo.Grades', N'SemesterId') IS NULL
BEGIN
    ALTER TABLE dbo.Grades ADD SemesterId INT NULL;
END;
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.foreign_keys
    WHERE name = N'FK_Grades_Semesters'
)
BEGIN
    ALTER TABLE dbo.Grades
    ADD CONSTRAINT FK_Grades_Semesters
        FOREIGN KEY (SemesterId) REFERENCES dbo.Semesters (Id);
END;
GO
