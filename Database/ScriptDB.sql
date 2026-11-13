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

-- Bai 6.1-6.2
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

-- Bai 6.6-6.8
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
