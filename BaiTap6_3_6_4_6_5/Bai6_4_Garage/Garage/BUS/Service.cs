using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Garage.DAL;
using Garage.DTO;

namespace Garage.BUS
{
    internal sealed class Service
    {
        private readonly Repository repository = new Repository();
        public DataTable KhachHang() => repository.KhachHang();
        public DataTable Xe() => repository.Xe("");
        public DataTable TraXe(string bien) => repository.Xe(ChuanBienSo(bien));
        public DataTable PhuTung() => repository.PhuTung();
        public DataTable Phieu() => repository.Phieu();
        public DataTable ChiTiet(int maPhieu) => repository.ChiTiet(maPhieu);

        public void LuuKhach(KhachHang khach)
        {
            KiemTraKhach(khach);
            MotDong(repository.LuuKhach(khach));
        }

        public void XoaKhach(int id) => MotDong(repository.XoaKhach(id));

        public void LuuXe(Xe xe, bool them)
        {
            KiemTraXe(xe);
            if (xe.MaKH <= 0) throw new ArgumentException("Chọn chủ xe.");
            MotDong(repository.LuuXe(xe, them));
        }

        public void TiepNhanMoi(KhachHang khach, Xe xe)
        {
            KiemTraKhach(khach);
            KiemTraXe(xe);
            repository.TiepNhanMoi(khach, xe);
        }

        public void XoaXe(string bien) => MotDong(repository.XoaXe(ChuanBienSo(bien)));

        public void LuuPhuTung(PhuTung pt)
        {
            pt.TenPhuTung = ChuoiBatBuoc(pt.TenPhuTung, 150, "tên phụ tùng");
            if (pt.DonGia < 0 || pt.SoLuongTon < 0)
                throw new ArgumentException("Đơn giá và số lượng tồn phải không âm.");
            MotDong(repository.LuuPhuTung(pt));
        }

        public void XoaPhuTung(int id) => MotDong(repository.XoaPhuTung(id));

        public int LapPhieu(string bien, string noiDung, IEnumerable<DongSuaChua> chiTiet)
        {
            bien = ChuanBienSo(bien);
            noiDung = ChuoiBatBuoc(noiDung, 1000, "nội dung sửa");
            var danhSach = chiTiet.OrderBy(d => d.MaPhuTung).ToArray();
            if (danhSach.Length == 0) throw new ArgumentException("Thêm ít nhất một dòng phụ tùng.");
            if (danhSach.Any(d => d.MaPhuTung <= 0 || d.SoLuong <= 0 || d.TienCong < 0)
                || danhSach.GroupBy(d => d.MaPhuTung).Any(g => g.Count() > 1))
                throw new ArgumentException("Kiểm tra mã phụ tùng, số lượng và tiền công.");
            return repository.LapPhieu(bien, noiDung, danhSach);
        }

        public void ThanhToan(int maPhieu)
        {
            if (repository.ThanhToan(maPhieu) != 1)
                throw new InvalidOperationException("Chỉ thanh toán được phiếu đã hoàn thành và chưa thanh toán.");
        }

        public void XuatHoaDon(DataRow phieu, string path)
        {
            if ((string)phieu["TrangThai"] != "Đã thanh toán")
                throw new InvalidOperationException("Thanh toán phiếu trước khi xuất hóa đơn.");
            var text = new StringBuilder();
            text.AppendLine("HÓA ĐƠN DỊCH VỤ GARAGE");
            text.AppendLine("Mã phiếu: " + phieu["MaPhieu"]);
            text.AppendLine("Biển số: " + phieu["BienSo"] + " | Chủ xe: " + phieu["TenKH"]);
            text.AppendLine("Ngày nhận: " + ((DateTime)phieu["NgayNhan"]).ToString("dd/MM/yyyy HH:mm"));
            text.AppendLine("Nội dung sửa: " + phieu["NoiDungSua"]);
            foreach (DataRow line in ChiTiet((int)phieu["MaPhieu"]).Rows)
                text.AppendLine(string.Format("- {0}: {1} × {2:N2} + công {3:N2} = {4:N2} đ",
                    line[0], line[1], line[2], line[3], line[4]));
            text.AppendLine(string.Format("Tổng tiền: {0:N2} đ", phieu["TongTien"]));
            text.AppendLine("Trạng thái: Đã thanh toán");
            File.WriteAllText(path, text.ToString(), new UTF8Encoding(true));
        }

        private static void KiemTraKhach(KhachHang k)
        {
            k.TenKH = ChuoiBatBuoc(k.TenKH, 100, "tên chủ xe");
            k.DienThoai = (k.DienThoai ?? "").Trim();
            if (!Regex.IsMatch(k.DienThoai, @"^[0-9]{8,15}$"))
                throw new ArgumentException("Số điện thoại phải có 8 đến 15 chữ số.");
            k.DiaChi = (k.DiaChi ?? "").Trim();
            if (k.DiaChi.Length > 200) throw new ArgumentException("Địa chỉ tối đa 200 ký tự.");
        }

        private static void KiemTraXe(Xe x)
        {
            x.BienSo = ChuanBienSo(x.BienSo);
            x.HieuXe = ChuoiBatBuoc(x.HieuXe, 50, "hiệu xe");
            if (x.NamSanXuat < 1900 || x.NamSanXuat > DateTime.Today.Year + 1)
                throw new ArgumentException("Năm sản xuất không hợp lệ.");
        }

        private static string ChuanBienSo(string bien) =>
            ChuoiBatBuoc(bien, 20, "biển số").ToUpperInvariant();

        private static string ChuoiBatBuoc(string value, int max, string field)
        {
            value = (value ?? "").Trim();
            if (value.Length == 0 || value.Length > max)
                throw new ArgumentException("Nhập " + field + " từ 1 đến " + max + " ký tự.");
            return value;
        }

        private static void MotDong(int rows)
        {
            if (rows != 1) throw new InvalidOperationException("Bản ghi không còn tồn tại.");
        }
    }
}
