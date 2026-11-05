USE master;
GO
IF DB_ID(N'SalesDB') IS NULL CREATE DATABASE SalesDB;
GO
IF DB_ID(N'HRMDB') IS NULL CREATE DATABASE HRMDB;
GO
USE SalesDB;
GO
IF OBJECT_ID('SanPham') IS NULL
BEGIN
 CREATE TABLE NhanVien(MaNV int IDENTITY PRIMARY KEY,TenNV nvarchar(100) NOT NULL,SoDienThoai varchar(15) NOT NULL UNIQUE,ChucVu nvarchar(50) NOT NULL);
 CREATE TABLE KhachHang(MaKH int IDENTITY PRIMARY KEY,TenKH nvarchar(100) NOT NULL,DienThoai varchar(15) NOT NULL UNIQUE,DiemTichLuy int NOT NULL DEFAULT 0 CHECK(DiemTichLuy>=0));
 CREATE TABLE SanPham(MaSP varchar(20) PRIMARY KEY,TenSP nvarchar(150) NOT NULL,GiaBan decimal(18,2) NOT NULL CHECK(GiaBan>0),SoLuongTon int NOT NULL CHECK(SoLuongTon>=0));
 CREATE TABLE HoaDon(MaHD int IDENTITY PRIMARY KEY,NgayLap datetime NOT NULL DEFAULT GETDATE(),MaNV int NOT NULL REFERENCES NhanVien(MaNV),MaKH int NULL REFERENCES KhachHang(MaKH),TongTien decimal(18,2) NOT NULL CHECK(TongTien>=0));
 CREATE TABLE ChiTietHoaDon(MaHD int NOT NULL REFERENCES HoaDon(MaHD),MaSP varchar(20) NOT NULL REFERENCES SanPham(MaSP),SoLuong int NOT NULL CHECK(SoLuong>0),DonGia decimal(18,2) NOT NULL CHECK(DonGia>0),PRIMARY KEY(MaHD,MaSP));
 INSERT NhanVien(TenNV,SoDienThoai,ChucVu) VALUES(N'Nguyễn Hải Đăng','0900000001',N'Thu ngân'),(N'Quản lý mẫu','0900000002',N'Quản lý');
 INSERT KhachHang(TenKH,DienThoai) VALUES(N'Khách hàng mẫu','0910000001');
 INSERT SanPham VALUES('SP001',N'Sữa tươi',30000,100),('SP002',N'Gạo thơm',150000,50),('SP003',N'Nước suối',10000,5);
END;
GO
USE HRMDB;
GO
IF OBJECT_ID('PhongBan') IS NULL
BEGIN
 CREATE TABLE PhongBan(MaPB int IDENTITY PRIMARY KEY,TenPB nvarchar(100) NOT NULL,Mota nvarchar(255) NULL);
 CREATE TABLE NhanVien(MaNV int IDENTITY PRIMARY KEY,HoTen nvarchar(100) NOT NULL,NgaySinh datetime NOT NULL,CCCD varchar(20) NOT NULL UNIQUE,MaPB int NOT NULL REFERENCES PhongBan(MaPB),LuongCoBan decimal(18,2) NOT NULL CHECK(LuongCoBan>0),HeSoLuong float NOT NULL DEFAULT 1 CHECK(HeSoLuong>0));
 CREATE TABLE BangChamCong(MaCong int IDENTITY PRIMARY KEY,MaNV int NOT NULL REFERENCES NhanVien(MaNV),ThangNam varchar(7) NOT NULL,SoNgayCong float NOT NULL DEFAULT 0 CHECK(SoNgayCong BETWEEN 0 AND 31),SoGioOT float NOT NULL DEFAULT 0 CHECK(SoGioOT>=0),UNIQUE(MaNV,ThangNam));
 -- Đề ghi cả MaLuong PK lẫn PK ghép: dùng PK ghép và MaLuong IDENTITY UNIQUE.
 CREATE TABLE BangLuong(MaLuong int IDENTITY NOT NULL UNIQUE,MaNV int NOT NULL REFERENCES NhanVien(MaNV),ThangNam varchar(7) NOT NULL,ThucLinh decimal(18,2) NOT NULL CHECK(ThucLinh>=0),PRIMARY KEY(MaNV,ThangNam));
 CREATE TABLE KyLuong(ThangNam varchar(7) PRIMARY KEY,DaChot bit NOT NULL DEFAULT 0,NgayChot datetime NULL);
 INSERT PhongBan(TenPB,Mota) VALUES(N'Kinh doanh',N'Bán hàng'),(N'Kế toán',N'Tài chính');
 INSERT NhanVien(HoTen,NgaySinh,CCCD,MaPB,LuongCoBan,HeSoLuong) VALUES(N'Nguyễn Hải Đăng','2004-01-01','000000000001',1,11000000,1),(N'Nhân viên mẫu','2000-01-01','000000000002',2,8800000,1.5);
END;
GO
CREATE OR ALTER TRIGGER TR_ChamCong_Khoa ON BangChamCong AFTER INSERT,UPDATE,DELETE AS
BEGIN
 SET NOCOUNT ON;
 IF EXISTS(SELECT 1 FROM KyLuong k JOIN (SELECT ThangNam FROM inserted UNION SELECT ThangNam FROM deleted) m ON m.ThangNam=k.ThangNam WHERE k.DaChot=1)
 THROW 51001,N'Tháng đã chốt lương; không được sửa chấm công.',1;
END;
GO
