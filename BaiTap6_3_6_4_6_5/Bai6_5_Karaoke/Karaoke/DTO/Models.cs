using System;

namespace Karaoke.DTO
{
    internal sealed class PhongHat
    {
        public int MaPhong { get; set; }
        public string TenPhong { get; set; }
        public string LoaiPhong { get; set; }
        public decimal GiaTheoGio { get; set; }
    }

    internal sealed class DichVu
    {
        public int MaDV { get; set; }
        public string TenDV { get; set; }
        public decimal DonGia { get; set; }
        public int TonKho { get; set; }
    }

    internal sealed class HoaDon
    {
        public int MaHD { get; set; }
        public DateTime GioVao { get; set; }
        public DateTime GioRa { get; set; }
        public decimal TongTienPhong { get; set; }
        public decimal TongTienDichVu { get; set; }
        public decimal TongTien => TongTienPhong + TongTienDichVu;
    }
}
