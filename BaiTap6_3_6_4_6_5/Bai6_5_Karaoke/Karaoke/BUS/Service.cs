using System;
using System.Data;
using Karaoke.DAL;
using Karaoke.DTO;

namespace Karaoke.BUS
{
    internal sealed class Service
    {
        private readonly Repository repository = new Repository();
        public DataTable Phong() => repository.Phong();
        public DataTable DichVu() => repository.DichVu();
        public DataTable PhienMo(int id) => repository.PhienMo(id);
        public DataTable ChiTiet(int id) => repository.ChiTiet(id);
        public DataTable HoaDon() => repository.HoaDon();

        public void LuuPhong(PhongHat phong)
        {
            phong.TenPhong = BatBuoc(phong.TenPhong, 50, "tên phòng");
            phong.LoaiPhong = BatBuoc(phong.LoaiPhong, 30, "loại phòng");
            if (phong.GiaTheoGio < 0) throw new ArgumentException("Giá theo giờ phải không âm.");
            KiemTraPhong(repository.LuuPhong(phong));
        }

        public void XoaPhong(int id) => KiemTraPhong(repository.XoaPhong(id));

        public void LuuDichVu(DichVu dichVu)
        {
            dichVu.TenDV = BatBuoc(dichVu.TenDV, 100, "tên dịch vụ");
            if (dichVu.DonGia < 0 || dichVu.TonKho < 0)
                throw new ArgumentException("Đơn giá và tồn kho phải không âm.");
            if (repository.LuuDichVu(dichVu) != 1)
                throw new InvalidOperationException("Dịch vụ đã bị xóa.");
        }

        public void XoaDichVu(int id)
        {
            if (repository.XoaDichVu(id) != 1) throw new InvalidOperationException("Dịch vụ đã bị xóa.");
        }

        public int MoPhong(int phong)
        {
            if (phong <= 0) throw new ArgumentException("Chọn phòng trước.");
            return repository.MoPhong(phong);
        }

        public void GoiDichVu(int phong, int hd, int dv, int soLuong)
        {
            if (phong <= 0 || hd <= 0 || dv <= 0 || soLuong <= 0)
                throw new ArgumentException("Chọn phòng đang hát, dịch vụ và số lượng hợp lệ.");
            repository.GoiDichVu(phong, hd, dv, soLuong);
        }

        public void BoDichVu(int phong, int hd, int dv) => repository.BoDichVu(phong, hd, dv);

        public HoaDon ThanhToan(int phong, int hd)
        {
            if (phong <= 0 || hd <= 0) throw new ArgumentException("Chọn phòng đang hát để thanh toán.");
            return repository.ThanhToan(phong, hd, TinhTienPhong);
        }

        public static decimal TinhTienPhong(DateTime vao, DateTime ra, decimal gia)
        {
            decimal gio = Math.Max(0L, (ra - vao).Ticks) / (decimal)TimeSpan.TicksPerHour;
            return Math.Round(gio * gia, 2, MidpointRounding.AwayFromZero);
        }

        private static void KiemTraPhong(int rows)
        {
            if (rows != 1) throw new InvalidOperationException("Phòng không còn tồn tại hoặc đang hát.");
        }

        private static string BatBuoc(string text, int max, string field)
        {
            text = (text ?? "").Trim();
            if (text.Length == 0 || text.Length > max)
                throw new ArgumentException("Nhập " + field + " từ 1 đến " + max + " ký tự.");
            return text;
        }
    }
}
