-- Mở script bằng SSMS rồi chạy toàn bộ.
USE master;
GO
IF DB_ID(N'GarageDB') IS NULL
    EXEC(N'CREATE DATABASE [GarageDB]');
GO
USE [GarageDB];
GO
SET XACT_ABORT ON;
IF OBJECT_ID(N'dbo.KhachHang', N'U') IS NULL
BEGIN
    BEGIN TRANSACTION;
    CREATE TABLE dbo.KhachHang (
        MaKH int IDENTITY PRIMARY KEY,
        TenKH nvarchar(100) NOT NULL,
        DienThoai varchar(15) NOT NULL UNIQUE,
        DiaChi nvarchar(200)
    );
    CREATE TABLE dbo.Xe (
        BienSo varchar(20) PRIMARY KEY,
        MaKH int NOT NULL REFERENCES dbo.KhachHang(MaKH),
        HieuXe nvarchar(50) NOT NULL,
        NamSanXuat int NOT NULL
    );
    CREATE TABLE dbo.PhuTung (
        MaPhuTung int IDENTITY PRIMARY KEY,
        TenPhuTung nvarchar(150) NOT NULL,
        DonGia decimal(18,2) NOT NULL CHECK (DonGia >= 0),
        SoLuongTon int NOT NULL CHECK (SoLuongTon >= 0)
    );
    CREATE TABLE dbo.PhieuSuaChua (
        MaPhieu int IDENTITY PRIMARY KEY,
        BienSo varchar(20) NOT NULL REFERENCES dbo.Xe(BienSo),
        NgayNhan datetime NOT NULL DEFAULT GETDATE(),
        NoiDungSua nvarchar(max),
        TongTien decimal(18,2) NOT NULL DEFAULT 0 CHECK (TongTien >= 0),
        TrangThai nvarchar(30) NOT NULL CHECK (TrangThai IN (N'Đang sửa',N'Đã hoàn thành',N'Đã thanh toán'))
    );
    CREATE TABLE dbo.ChiTietSuaChua (
        MaPhieu int NOT NULL REFERENCES dbo.PhieuSuaChua(MaPhieu),
        MaPhuTung int NOT NULL REFERENCES dbo.PhuTung(MaPhuTung),
        SoLuong int NOT NULL CHECK (SoLuong > 0),
        TienCong decimal(18,2) NOT NULL CHECK (TienCong >= 0),
        DonGia decimal(18,2) NOT NULL CHECK (DonGia >= 0),
        PRIMARY KEY (MaPhieu, MaPhuTung)
    );

    INSERT dbo.KhachHang (TenKH,DienThoai,DiaChi) VALUES
    (N'Nguyễn Văn An','0901000001',N'Hà Nội'), (N'Trần Thị Bình','0901000002',N'Hà Nội');
    INSERT dbo.Xe (BienSo,MaKH,HieuXe,NamSanXuat) VALUES
    ('30A-12345',1,N'Toyota',2021), ('29B-56789',2,N'Honda',2022);
    INSERT dbo.PhuTung (TenPhuTung,DonGia,SoLuongTon) VALUES
    (N'Lọc dầu',125000,12), (N'Dầu động cơ 1 lít',180000,20), (N'Má phanh',480000,4),
    (N'Gạt mưa',150000,0);
    COMMIT TRANSACTION;
END;
GO
