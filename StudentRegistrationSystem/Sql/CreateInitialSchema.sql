/*
  Начальная схема под приложение (учебная БД с INT-ключами).

  1) В SSMS: создайте базу при необходимости, затем выберите её в списке (или раскомментируйте USE).
  2) Выполните весь скрипт (F5).
  3) В App.config укажите тот же Server и имя базы (connectionStrings / StudentDb).

  Если видите Invalid object name 'Students' — приложение смотрит не в ту базу или скрипт не выполняли.
*/

-- USE [StudentRegistrationDb];
-- GO

IF OBJECT_ID(N'dbo.Groups', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Groups (
        Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        GroupName NVARCHAR(50) NOT NULL,
        Specialty NVARCHAR(200) NULL
    );
END;

IF COL_LENGTH(N'dbo.Groups', N'Specialty') IS NULL
BEGIN
    ALTER TABLE dbo.Groups ADD Specialty NVARCHAR(200) NULL;
END;

IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = N'UX_Groups_GroupName'
      AND object_id = OBJECT_ID(N'dbo.Groups')
)
BEGIN
    CREATE UNIQUE INDEX UX_Groups_GroupName ON dbo.Groups (GroupName);
END;

IF OBJECT_ID(N'dbo.Students', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Students (
        Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        FullName NVARCHAR(100) NOT NULL,
        BirthDate DATE NULL,
        GroupId INT NULL,
        CONSTRAINT FK_Students_Groups FOREIGN KEY (GroupId) REFERENCES dbo.Groups (Id)
    );
END;

IF OBJECT_ID(N'dbo.Grades', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Grades (
        Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        StudentId INT NOT NULL,
        Subject NVARCHAR(50) NULL,
        Grade INT NOT NULL,
        CONSTRAINT FK_Grades_Students FOREIGN KEY (StudentId) REFERENCES dbo.Students (Id)
    );
END;
