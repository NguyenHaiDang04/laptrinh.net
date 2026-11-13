USE master;
GO
IF DB_ID(N'LibraryDB') IS NULL CREATE DATABASE LibraryDB;
GO
IF DB_ID(N'ClinicDB') IS NULL CREATE DATABASE ClinicDB;
GO
IF DB_ID(N'BilliardsDB') IS NULL CREATE DATABASE BilliardsDB;
GO
USE LibraryDB;
GO
IF OBJECT_ID('DocGia') IS NULL
BEGIN
 CREATE TABLE DocGia(MaDocGia int IDENTITY PRIMARY KEY,HoTen nvarchar(100) NOT NULL,Email varchar(100) NOT NULL UNIQUE,NgayHetHanThe datetime NOT NULL);
 CREATE TABLE DauSach(MaSach int IDENTITY PRIMARY KEY,TenSach nvarchar(200) NOT NULL,TheLoai nvarchar(50) NOT NULL,SoLuongCon int NOT NULL CHECK(SoLuongCon>=0));
 CREATE TABLE PhieuMuon(MaPhieu int IDENTITY PRIMARY KEY,MaDocGia int NOT NULL REFERENCES DocGia(MaDocGia),NgayMuon datetime NOT NULL DEFAULT GETDATE(),HanTra datetime NOT NULL,TrangThai nvarchar(50) NOT NULL DEFAULT N'Đang mượn' CHECK(TrangThai IN(N'Đang mượn',N'Đã trả',N'Quá hạn')));
 CREATE TABLE ChiTietMuon(MaPhieu int NOT NULL REFERENCES PhieuMuon(MaPhieu),MaSach int NOT NULL REFERENCES DauSach(MaSach),SoLuong int NOT NULL CHECK(SoLuong>0),PRIMARY KEY(MaPhieu,MaSach));
 INSERT DocGia(HoTen,Email,NgayHetHanThe) VALUES(N'Độc giả mẫu','docgia@example.com',DATEADD(year,1,GETDATE())),(N'Thẻ hết hạn','hethan@example.com',DATEADD(day,-1,GETDATE()));
 INSERT DauSach VALUES(N'Lập trình C#',N'Công nghệ thông tin',10),(N'Kinh tế cơ bản',N'Kinh tế',5),(N'Truyện ngắn',N'Văn học',2);
END;
GO
USE ClinicDB;
GO
IF OBJECT_ID('BenhNhan') IS NULL
BEGIN
 CREATE TABLE BenhNhan(MaBN int IDENTITY PRIMARY KEY,HoTen nvarchar(100) NOT NULL,SoDienThoai varchar(15) NOT NULL UNIQUE,NamSinh int NOT NULL CHECK(NamSinh BETWEEN 1900 AND 2100));
 CREATE TABLE HoSoKham(MaHoSo int IDENTITY PRIMARY KEY,MaBN int NOT NULL REFERENCES BenhNhan(MaBN),NgayKham datetime NOT NULL DEFAULT GETDATE(),TrieuChung nvarchar(max) NOT NULL,ChanDoan nvarchar(250) NULL);
 CREATE TABLE Thuoc(MaThuoc int IDENTITY PRIMARY KEY,TenThuoc nvarchar(150) NOT NULL,DonViTinh nvarchar(20) NOT NULL,DonGia decimal(18,2) NOT NULL CHECK(DonGia>=0),SoLuongTon int NOT NULL CHECK(SoLuongTon>=0));
 CREATE TABLE DonThuoc(MaDon int IDENTITY PRIMARY KEY,MaHoSo int NOT NULL UNIQUE REFERENCES HoSoKham(MaHoSo),TienKham decimal(18,2) NOT NULL CHECK(TienKham>=0),TongTien decimal(18,2) NOT NULL CHECK(TongTien>=0),TrangThai nvarchar(30) NOT NULL DEFAULT N'Chờ lấy thuốc' CHECK(TrangThai IN(N'Chờ lấy thuốc',N'Đã thanh toán')));
 CREATE TABLE ChiTietDonThuoc(MaDon int NOT NULL REFERENCES DonThuoc(MaDon),MaThuoc int NOT NULL REFERENCES Thuoc(MaThuoc),SoLuong int NOT NULL CHECK(SoLuong>0),LieuDung nvarchar(100) NOT NULL,DonGia decimal(18,2) NOT NULL CHECK(DonGia>=0),PRIMARY KEY(MaDon,MaThuoc));
 INSERT BenhNhan(HoTen,SoDienThoai,NamSinh) VALUES(N'Bệnh nhân mẫu','0900000001',2000);
 INSERT Thuoc VALUES(N'Thuốc mẫu A',N'Viên',1000,100),(N'Thuốc mẫu B',N'Chai',25000,5);
END;
GO
USE BilliardsDB;
GO
IF OBJECT_ID('BanBida') IS NULL
BEGIN
 CREATE TABLE BanBida(MaBan int IDENTITY PRIMARY KEY,TenBan nvarchar(50) NOT NULL,LoaiBan nvarchar(30) NOT NULL,GiaTheoGio decimal(18,2) NOT NULL CHECK(GiaTheoGio>=0),TrangThai nvarchar(20) NOT NULL DEFAULT N'Trống' CHECK(TrangThai IN(N'Trống',N'Đang chơi')));
 CREATE TABLE MenuDichVu(MaDV int IDENTITY PRIMARY KEY,TenDV nvarchar(100) NOT NULL,DonGia decimal(18,2) NOT NULL CHECK(DonGia>=0),TonKho int NOT NULL CHECK(TonKho>=0),QuanLyTon bit NOT NULL DEFAULT 1);
 CREATE TABLE HoaDon(MaHD int IDENTITY PRIMARY KEY,MaBan int NOT NULL REFERENCES BanBida(MaBan),GioBatDau datetime NOT NULL,GioKetThuc datetime NULL,TienGioChoi decimal(18,2) NOT NULL DEFAULT 0,TienDichVu decimal(18,2) NOT NULL DEFAULT 0,TrangThai bit NOT NULL DEFAULT 0,GiaGio decimal(18,2) NOT NULL CHECK(GiaGio>=0));
 CREATE UNIQUE INDEX UX_BanDangChoi ON HoaDon(MaBan) WHERE TrangThai=0;
 CREATE TABLE ChiTietHoaDon(MaHD int NOT NULL REFERENCES HoaDon(MaHD),MaDV int NOT NULL REFERENCES MenuDichVu(MaDV),SoLuong int NOT NULL CHECK(SoLuong>0),DonGia decimal(18,2) NOT NULL CHECK(DonGia>=0),PRIMARY KEY(MaHD,MaDV));
 INSERT BanBida(TenBan,LoaiBan,GiaTheoGio) VALUES(N'Bàn Lỗ 01',N'Pool (Lỗ)',60000),(N'Bàn Carom 02',N'Carom (Phăng)',80000),(N'Bàn Lỗ 03',N'Pool (Lỗ)',60000),(N'Bàn Snooker 04',N'Snooker',100000);
 INSERT MenuDichVu VALUES(N'Nước tăng lực',15000,20,1),(N'Mì xào',40000,5,1),(N'Thuê gậy Carbon',30000,0,0);
END;
GO
