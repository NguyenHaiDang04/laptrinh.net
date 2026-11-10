-- Mở script bằng SSMS rồi chạy toàn bộ.
USE master;
GO
IF DB_ID(N'KaraokeDB') IS NULL
    EXEC(N'CREATE DATABASE [KaraokeDB]');
GO
USE [KaraokeDB];
GO
SET XACT_ABORT ON;
IF OBJECT_ID(N'dbo.PhongHat', N'U') IS NULL
BEGIN
    BEGIN TRANSACTION;
    CREATE TABLE dbo.PhongHat (
        MaPhong int IDENTITY PRIMARY KEY,
        TenPhong nvarchar(50) NOT NULL UNIQUE,
        LoaiPhong nvarchar(30) NOT NULL,
        GiaTheoGio decimal(18,2) NOT NULL CHECK (GiaTheoGio >= 0),
        TrangThai nvarchar(20) NOT NULL DEFAULT N'Trống' CHECK (TrangThai IN (N'Trống',N'Đang hát'))
    );
    CREATE TABLE dbo.DichVu (
        MaDV int IDENTITY PRIMARY KEY,
        TenDV nvarchar(100) NOT NULL,
        DonGia decimal(18,2) NOT NULL CHECK (DonGia >= 0),
        TonKho int NOT NULL CHECK (TonKho >= 0)
    );
    CREATE TABLE dbo.HoaDon (
        MaHD int IDENTITY PRIMARY KEY,
        MaPhong int NOT NULL REFERENCES dbo.PhongHat(MaPhong),
        GioVao datetime NOT NULL,
        GioRa datetime NULL,
        TongTienPhong decimal(18,2) NOT NULL DEFAULT 0 CHECK (TongTienPhong >= 0),
        TongTienDichVu decimal(18,2) NOT NULL DEFAULT 0 CHECK (TongTienDichVu >= 0),
        TrangThai bit NOT NULL DEFAULT 0
    );
    CREATE UNIQUE INDEX UX_HoaDon_Phong_DangHat ON dbo.HoaDon(MaPhong) WHERE TrangThai = 0;
    CREATE TABLE dbo.ChiTietDichVu (
        MaHD int NOT NULL REFERENCES dbo.HoaDon(MaHD),
        MaDV int NOT NULL REFERENCES dbo.DichVu(MaDV),
        SoLuong int NOT NULL CHECK (SoLuong > 0),
        DonGia decimal(18,2) NOT NULL CHECK (DonGia >= 0),
        PRIMARY KEY (MaHD, MaDV)
    );

    INSERT dbo.PhongHat (TenPhong,LoaiPhong,GiaTheoGio) VALUES
    (N'Phòng Thường 01',N'Thường',120000),(N'Phòng Thường 02',N'Thường',120000),
    (N'Phòng Thường 03',N'Thường',120000),(N'Phòng VIP 01',N'VIP',220000),
    (N'Phòng VIP 02',N'VIP',220000),(N'Phòng VIP 03',N'VIP',250000);
    INSERT dbo.DichVu (TenDV,DonGia,TonKho) VALUES
    (N'Nước suối',12000,30),(N'Bia Heineken',28000,24),(N'Trái cây dĩa',85000,8);
    COMMIT TRANSACTION;
END;
GO
