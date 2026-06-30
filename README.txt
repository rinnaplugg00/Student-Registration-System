# StudentRegistrationSystem

WinForms-приложение для учёта студентов, групп и оценок с базой данных Microsoft SQL Server.

**Автор:** *Каирлынов Рустин Амангельдинович*  
**Группа:** *ПО 210*  
**Дата:** 2026

## Требования

- Windows
- .NET Framework 4.8
- SQL Server (LocalDB, Express или полноценный экземпляр)
- Visual Studio 2022 или новее (для сборки из IDE)

## Настройка базы данных

1. Создайте базу данных в SQL Server Management Studio (SSMS), например `StudentRegistrationDb`.
2. Выполните скрипты из папки `StudentRegistrationSystem/Sql/` **в указанном порядке**:
  - `CreateInitialSchema.sql` — таблицы `Groups`, `Students`, `Grades`
  - `SubjectsAndGradesExtension.sql` — журнал оценок по дням, семестры, справочник предметов
  - `SeedDemoData.sql` — демо-данные для защиты (3 группы, 10 студентов, оценки)
3. Скопируйте `StudentRegistrationSystem/App.config.example` в `App.config` (если файла ещё нет) и укажите строку подключения:

```xml
<connectionStrings>
  <add name="StudentDb"
       connectionString="Server=ВАШ_СЕРВЕР;Database=StudentRegistrationDb;Trusted_Connection=True;"
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

**Примеры сервера:**


| Вариант     | Строка                                                                                  |
| ----------- | --------------------------------------------------------------------------------------- |
| LocalDB     | `Server=(localdb)\MSSQLLocalDB;Database=StudentRegistrationDb;Trusted_Connection=True;` |
| SQL Express | `Server=ИМЯ_ПК\SQLEXPRESS;Database=StudentRegistrationDb;Trusted_Connection=True;`      |


Приложение читает только настройку `StudentDb`. Если она не задана, запуск завершится с понятным сообщением об ошибке.

## Сборка и запуск

### Из Visual Studio

1. Откройте `StudentRegistration67.slnx`.
2. Выберите конфигурацию **Release** (рекомендуется для сдачи) или **Debug**.
3. Запустите проект `StudentRegistrationSystem`.

### Из командной строки

```powershell
dotnet build StudentRegistrationSystem\StudentRegistration67.csproj -c Release
dotnet test StudentRegistrationSystem.Tests\StudentRegistrationSystem.Tests.csproj -c Release
```

Исполняемый файл: `StudentRegistrationSystem\bin\Release\StudentRegistrationSystem.exe`.

## Тесты

```powershell
dotnet test StudentRegistrationSystem.Tests\StudentRegistrationSystem.Tests.csproj
```

Покрыты (24 теста):

- валидация модели `Student` (ФИО, группа, возраст, оценки)
- расчёт GPA и статусов риска
- экспорт CSV
- форматирование ошибок базы данных

## Основные возможности

- журнал студентов с фильтрацией по ФИО, группе и минимальному баллу
- выбор семестра или режим **«Все периоды»**
- учёт оценок по предметам и семестрам
- карточка студента с журналом оценок по дням
- управление группами (с указанием специальности) и предметами
- топ студентов, средний балл по группам
- экспорт списка в CSV (с колонкой «Статус»)
- светлая и тёмная тема
- подсветка зоны риска (средний балл ниже 60)

## Сценарий демонстрации (5–7 минут)

1. **Запуск** — открывается журнал с демо-данными (10 студентов, 3 группы).
2. **Фильтр** — введите «ИС» в поиск, установите минимальный балл `70` → остаются лучшие студенты группы ИС-21.
3. **Семестр** — переключите «Все периоды» и обратно на «Весенний 2025/2026».
4. **Карточка** — двойной клик по студенту → журнал оценок по предметам и датам.
5. **Добавление** — кнопка «Добавить» → новый студент с оценкой по предмету.
6. **Статистика** — «Топ студентов» и «Средний по группам».
7. **Экспорт** — сохранение CSV в «Документы».
8. **Тема** — переключение светлой/тёмной темы.

Обратите внимание на строку статуса внизу: «В зоне риска» и «Без оценок» (студент Сидорова).

## Логи

Ошибки записываются в файл `log.txt` рядом с исполняемым файлом приложения.

## Чеклист перед сдачей

- [x] Выполнены все три SQL-скрипта
- [x] В `App.config` указан рабочий сервер и имя базы
- [x] `dotnet build -c Release` — без ошибок
- [x] `dotnet test` — 24/24 пройдено
- [ ] В README указаны ФИО и группа
- [ ] Архив не содержит папок `bin/`, `obj/`, `.vs/`

## Структура проекта

```
StudentRegistrationSystem/          # WinForms-приложение
  Services/                         # работа с БД и бизнес-логика
  Models/                           # модели данных
  Sql/                              # SQL-скрипты
    CreateInitialSchema.sql
    SubjectsAndGradesExtension.sql
    SeedDemoData.sql
  App.config.example                # шаблон строки подключения
StudentRegistrationSystem.Tests/    # модульные тесты
```

## Архитектура

```
WinForms (MainForm, FormStudent, …)
        ↓
Services (StudentService, GroupService, SemesterService, …)
        ↓
DatabaseHelper → SQL Server
```

Модель `Student` реализует валидацию (`IValidatable`). Расчёт GPA и статусов риска вынесен в `AcademicMetricsService`.