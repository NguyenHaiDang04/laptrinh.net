-- Mở script bằng SSMS rồi chạy toàn bộ.
USE master;
GO
IF DB_ID(N'ProjectTaskDB') IS NULL
    EXEC(N'CREATE DATABASE [ProjectTaskDB]');
GO
USE [ProjectTaskDB];
GO
SET XACT_ABORT ON;
IF OBJECT_ID(N'dbo.DuAn', N'U') IS NULL
BEGIN
    BEGIN TRANSACTION;
    CREATE TABLE dbo.DuAn (
        MaDuAn int IDENTITY PRIMARY KEY,
        TenDuAn nvarchar(150) NOT NULL,
        NgayBatDau datetime NOT NULL,
        NgayKetThuc datetime NOT NULL,
        TrangThai nvarchar(50) NOT NULL CHECK (TrangThai IN (N'Đang chạy',N'Hoàn thành',N'Tạm dừng')),
        CHECK (NgayKetThuc >= NgayBatDau)
    );
    CREATE TABLE dbo.ThanhVien (
        MaThanhVien int IDENTITY PRIMARY KEY,
        HoTen nvarchar(100) NOT NULL,
        Email varchar(100) NOT NULL UNIQUE,
        ChuyenMon nvarchar(100)
    );
    CREATE TABLE dbo.CongViec (
        MaTask int IDENTITY PRIMARY KEY,
        MaDuAn int NOT NULL REFERENCES dbo.DuAn(MaDuAn),
        TenTask nvarchar(200) NOT NULL,
        MoTa nvarchar(max),
        HanNop datetime NOT NULL,
        DoUuTien nvarchar(20) NOT NULL CHECK (DoUuTien IN (N'Cao',N'Trung bình',N'Thấp')),
        TrangThai nvarchar(50) NOT NULL CHECK (TrangThai IN (N'To Do',N'In Progress',N'Review',N'Done'))
    );
    CREATE TABLE dbo.PhanCong (
        MaTask int NOT NULL REFERENCES dbo.CongViec(MaTask) ON DELETE CASCADE,
        MaThanhVien int NOT NULL REFERENCES dbo.ThanhVien(MaThanhVien),
        NgayGiao datetime NOT NULL DEFAULT GETDATE(),
        PRIMARY KEY (MaTask, MaThanhVien)
    );

    INSERT dbo.DuAn (TenDuAn,NgayBatDau,NgayKetThuc,TrangThai) VALUES
    (N'Website giới thiệu trung tâm',DATEADD(day,-10,GETDATE()),DATEADD(day,20,GETDATE()),N'Đang chạy'),
    (N'Phần mềm quản lý thư viện',DATEADD(day,-5,GETDATE()),DATEADD(day,30,GETDATE()),N'Đang chạy');
    INSERT dbo.ThanhVien (HoTen,Email,ChuyenMon) VALUES
    (N'Nguyễn Văn An','an@example.com',N'Lập trình'),
    (N'Trần Thị Bình','binh@example.com',N'Thiết kế'),
    (N'Lê Hoàng Cường','cuong@example.com',N'Kiểm thử');
    INSERT dbo.CongViec (MaDuAn,TenTask,MoTa,HanNop,DoUuTien,TrangThai) VALUES
    (1,N'Khảo sát yêu cầu',N'Tổng hợp yêu cầu cho trang giới thiệu',DATEADD(day,-4,GETDATE()),N'Cao',N'Done'),
    (1,N'Dựng giao diện trang chủ',N'Tạo giao diện cơ bản',DATEADD(day,3,GETDATE()),N'Cao',N'In Progress'),
    (1,N'Chuẩn bị dữ liệu mẫu',N'Soạn nội dung giới thiệu',DATEADD(day,-2,GETDATE()),N'Trung bình',N'To Do'),
    (1,N'Kiểm tra biểu mẫu liên hệ',N'Kiểm tra nhập liệu',DATEADD(day,5,GETDATE()),N'Thấp',N'Review'),
    (2,N'Thiết kế cơ sở dữ liệu',N'Lập danh mục sách và độc giả',DATEADD(day,7,GETDATE()),N'Cao',N'To Do');
    INSERT dbo.PhanCong (MaTask,MaThanhVien) VALUES (1,1),(1,2),(2,1),(2,2),(3,3),(4,3),(5,1);
    COMMIT TRANSACTION;
END;
GO
