-- SQL Server / LocalDB. Chạy trong CSDL LTNET_Bai4_CSDL.
-- Script được ứng dụng chạy tự động; chạy lại không xóa dữ liệu hiện có.
IF OBJECT_ID(N'dbo.AppUsers', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.AppUsers (
        UserId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        Username NVARCHAR(50) COLLATE Latin1_General_100_CI_AS NOT NULL UNIQUE,
        FullName NVARCHAR(100) NOT NULL,
        PasswordHash VARBINARY(32) NOT NULL,
        PasswordSalt VARBINARY(16) NOT NULL,
        PasswordIterations INT NOT NULL CHECK (PasswordIterations >= 100000),
        IsActive BIT NOT NULL DEFAULT 1,
        CreatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
        LastLoginAt DATETIME2 NULL
    );
END;

IF OBJECT_ID(N'dbo.LoginAttempts', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.LoginAttempts (
        AttemptId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        UserId INT NULL REFERENCES dbo.AppUsers(UserId),
        Username NVARCHAR(50) NOT NULL,
        Succeeded BIT NOT NULL,
        OccurredAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
    );
END;

IF OBJECT_ID(N'dbo.Courses', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Courses (
        CourseId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        CourseCode VARCHAR(20) NOT NULL UNIQUE,
        CourseName NVARCHAR(100) NOT NULL,
        IsActive BIT NOT NULL DEFAULT 1
    );
END;

IF OBJECT_ID(N'dbo.Registrations', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Registrations (
        RegistrationId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        FullName NVARCHAR(100) NOT NULL CHECK (LEN(LTRIM(RTRIM(FullName))) > 0),
        Phone VARCHAR(10) NOT NULL CHECK (LEN(Phone) = 10 AND Phone NOT LIKE '%[^0-9]%'),
        BirthDate DATE NOT NULL,
        Gender NVARCHAR(10) NOT NULL CHECK (Gender IN (N'Nam', N'Nữ', N'Khác')),
        CourseId INT NOT NULL REFERENCES dbo.Courses(CourseId),
        ReceiveNews BIT NOT NULL,
        CreatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
        CONSTRAINT UQ_Registrations_PhoneCourse UNIQUE (Phone, CourseId)
    );
END;

IF NOT EXISTS (SELECT 1 FROM dbo.Courses WHERE CourseCode = 'CS01')
    INSERT dbo.Courses(CourseCode, CourseName) VALUES ('CS01', N'Lập trình C# căn bản');
IF NOT EXISTS (SELECT 1 FROM dbo.Courses WHERE CourseCode = 'WF01')
    INSERT dbo.Courses(CourseCode, CourseName) VALUES ('WF01', N'Lập trình Windows Forms');
IF NOT EXISTS (SELECT 1 FROM dbo.Courses WHERE CourseCode = 'SQL01')
    INSERT dbo.Courses(CourseCode, CourseName) VALUES ('SQL01', N'Cơ sở dữ liệu SQL Server');

-- Tài khoản mẫu được ứng dụng tạo bằng PBKDF2 và salt ngẫu nhiên.
-- Không ghi mật khẩu dạng rõ trong bảng hay nhật ký.
