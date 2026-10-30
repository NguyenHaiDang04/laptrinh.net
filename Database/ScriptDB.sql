-- Nguyễn Hải Đăng | 24810320310 | D19QTANM1
-- Chạy toàn bộ trong SSMS. Không DROP DATABASE, không xóa dữ liệu hiện có.
USE master;
GO
IF DB_ID(N'CafePOSDB') IS NULL CREATE DATABASE CafePOSDB;
GO
IF DB_ID(N'HotelDB') IS NULL CREATE DATABASE HotelDB;
GO
IF DB_ID(N'InventoryDB') IS NULL CREATE DATABASE InventoryDB;
GO
USE CafePOSDB;
GO
IF OBJECT_ID(N'BanAn') IS NULL
BEGIN
 CREATE TABLE BanAn(MaBan int IDENTITY PRIMARY KEY, TenBan nvarchar(50) NOT NULL,
  TrangThai nvarchar(20) NOT NULL DEFAULT N'Trống' CHECK(TrangThai IN(N'Trống',N'Có khách')));
 CREATE TABLE DanhMuc(MaDanhMuc int IDENTITY PRIMARY KEY, TenDanhMuc nvarchar(100) NOT NULL CHECK(LEN(LTRIM(RTRIM(TenDanhMuc)))>0));
 CREATE TABLE MonAn(MaMon int IDENTITY PRIMARY KEY, TenMon nvarchar(100) NOT NULL CHECK(LEN(LTRIM(RTRIM(TenMon)))>0),
  MaDanhMuc int NOT NULL REFERENCES DanhMuc(MaDanhMuc), DonGia decimal(18,2) NOT NULL CHECK(DonGia>=0));
 CREATE TABLE HoaDon(MaHoaDon int IDENTITY PRIMARY KEY, MaBan int NOT NULL REFERENCES BanAn(MaBan),
  GioVao datetime2 NOT NULL DEFAULT SYSDATETIME(), GioRa datetime2 NULL, TongTien decimal(18,2) NOT NULL DEFAULT 0 CHECK(TongTien>=0),
  TrangThai bit NOT NULL DEFAULT 0, CHECK((TrangThai=0 AND GioRa IS NULL) OR (TrangThai=1 AND GioRa>=GioVao)));
 CREATE UNIQUE INDEX UX_HoaDon_BanChuaThanhToan ON HoaDon(MaBan) WHERE TrangThai=0;
 CREATE TABLE ChiTietHoaDon(MaHoaDon int NOT NULL REFERENCES HoaDon(MaHoaDon), MaMon int NOT NULL REFERENCES MonAn(MaMon),
  SoLuong int NOT NULL CHECK(SoLuong>0), DonGia decimal(18,2) NOT NULL CHECK(DonGia>=0), PRIMARY KEY(MaHoaDon,MaMon));
 INSERT BanAn(TenBan) VALUES(N'Bàn 01'),(N'Bàn 02'),(N'Bàn 03'),(N'Bàn 04'),(N'Bàn 05'),(N'Bàn 06'),(N'Bàn 07'),(N'Bàn 08');
 INSERT DanhMuc(TenDanhMuc) VALUES(N'Cà phê'),(N'Trà sữa'),(N'Đồ ăn nhẹ');
 INSERT MonAn(TenMon,MaDanhMuc,DonGia) VALUES(N'Cà phê đen',1,25000),(N'Cà phê sữa',1,30000),(N'Trà sữa trân châu',2,40000),(N'Bánh mì',3,20000);
END;
GO
USE HotelDB;
GO
IF OBJECT_ID(N'Phong') IS NULL
BEGIN
 CREATE TABLE Phong(SoPhong varchar(10) PRIMARY KEY, LoaiPhong nvarchar(50) NOT NULL,
  GiaTheoGio decimal(18,2) NOT NULL CHECK(GiaTheoGio>=0), TrangThai nvarchar(30) NOT NULL DEFAULT N'Trống'
  CHECK(TrangThai IN(N'Trống',N'Đang ở',N'Đang dọn dẹp')));
 CREATE TABLE KhachHang(MaKH int IDENTITY PRIMARY KEY, HoTen nvarchar(100) NOT NULL,
  CCCD varchar(20) UNIQUE NOT NULL, SoDienThoai varchar(15) NULL);
 CREATE TABLE DichVu(MaDV int IDENTITY PRIMARY KEY, TenDV nvarchar(100) NOT NULL, DonGia decimal(18,2) NOT NULL CHECK(DonGia>=0));
 CREATE TABLE PhieuDatPhong(MaPhieu int IDENTITY PRIMARY KEY, SoPhong varchar(10) NOT NULL REFERENCES Phong(SoPhong),
  MaKH int NOT NULL REFERENCES KhachHang(MaKH), NgayNhanPhong datetime2 NOT NULL DEFAULT SYSDATETIME(),
  NgayTraPhong datetime2 NULL, TongTienThanhToan decimal(18,2) NOT NULL DEFAULT 0 CHECK(TongTienThanhToan>=0),
  GiaPhong decimal(18,2) NOT NULL CHECK(GiaPhong>=0), CHECK(NgayTraPhong IS NULL OR NgayTraPhong>=NgayNhanPhong));
 CREATE UNIQUE INDEX UX_PhieuDatPhong_DangO ON PhieuDatPhong(SoPhong) WHERE NgayTraPhong IS NULL;
 CREATE TABLE SuDungDichVu(MaSuDung int IDENTITY PRIMARY KEY, MaPhieu int NOT NULL REFERENCES PhieuDatPhong(MaPhieu),
  MaDV int NOT NULL REFERENCES DichVu(MaDV), SoLuong int NOT NULL CHECK(SoLuong>0), ThoiGian datetime2 NOT NULL DEFAULT SYSDATETIME(),
  DonGia decimal(18,2) NOT NULL CHECK(DonGia>=0));
 INSERT Phong VALUES('P101',N'Đơn',100000,N'Trống'),('P102',N'Đơn',100000,N'Trống'),('P201',N'Đôi',150000,N'Trống'),('P202',N'Đôi',150000,N'Trống'),('P301',N'VIP',250000,N'Trống');
 INSERT KhachHang(HoTen,CCCD,SoDienThoai) VALUES(N'Khách mẫu','000000000001','0900000001');
 INSERT DichVu(TenDV,DonGia) VALUES(N'Nước suối',15000),(N'Giặt là',50000),(N'Ăn sáng',60000);
END;
GO
USE InventoryDB;
GO
IF OBJECT_ID(N'VatTu') IS NULL
BEGIN
 CREATE TABLE NhaCungCap(MaNCC int IDENTITY PRIMARY KEY, TenNCC nvarchar(150) NOT NULL CHECK(LEN(LTRIM(RTRIM(TenNCC)))>0),
  DienThoai varchar(15) NULL, DiaChi nvarchar(255) NULL);
 CREATE UNIQUE INDEX UX_NCC_DienThoai ON NhaCungCap(DienThoai) WHERE DienThoai IS NOT NULL;
 CREATE TABLE VatTu(MaVatTu varchar(20) PRIMARY KEY, TenVatTu nvarchar(150) NOT NULL CHECK(LEN(LTRIM(RTRIM(TenVatTu)))>0),
  DVT nvarchar(20) NOT NULL, SoLuongTon decimal(18,2) NOT NULL DEFAULT 0 CHECK(SoLuongTon>=0),
  GiaTonKho decimal(18,2) NOT NULL DEFAULT 0 CHECK(GiaTonKho>=0));
 CREATE TABLE PhieuKho(MaPhieu int IDENTITY PRIMARY KEY, LoaiPhieu nvarchar(10) NOT NULL CHECK(LoaiPhieu IN(N'NHAP',N'XUAT')),
  NgayLap datetime2 NOT NULL DEFAULT SYSDATETIME(), MaNCC int NULL REFERENCES NhaCungCap(MaNCC),
  TongTien decimal(18,2) NOT NULL CHECK(TongTien>=0), CHECK(LoaiPhieu<>N'NHAP' OR MaNCC IS NOT NULL));
 CREATE TABLE ChiTietPhieuKho(MaPhieu int NOT NULL REFERENCES PhieuKho(MaPhieu), MaVatTu varchar(20) NOT NULL REFERENCES VatTu(MaVatTu),
  SoLuong decimal(18,2) NOT NULL CHECK(SoLuong>0), DonGia decimal(18,2) NOT NULL CHECK(DonGia>=0), PRIMARY KEY(MaPhieu,MaVatTu));
 INSERT NhaCungCap(TenNCC,DienThoai,DiaChi) VALUES(N'Công ty vật tư An Phát','0901000001',N'Hà Nội'),(N'Nhà cung cấp Bình Minh','0901000002',N'Hà Nội');
 INSERT VatTu VALUES('VT001',N'Xi măng',N'Bao',100,85000),('VT002',N'Thép xây dựng',N'Kg',500,18000),('VT003',N'Sơn nước',N'Thùng',5,350000);
END;
GO
