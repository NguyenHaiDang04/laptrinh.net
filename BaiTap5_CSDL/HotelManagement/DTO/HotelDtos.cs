using System;

namespace HotelManagement.DTO
{
    public sealed class CheckInDto
    {
        public string SoPhong { get; set; }
        public string HoTen { get; set; }
        public string CCCD { get; set; }
        public string SoDienThoai { get; set; }
    }
    public sealed class ServiceDto
    {
        public string SoPhong { get; set; }
        public int MaDV { get; set; }
        public int SoLuong { get; set; }
    }
    public sealed class InvoiceDto
    {
        public int MaPhieu { get; set; }
        public string SoPhong { get; set; }
        public string HoTen { get; set; }
        public DateTime NgayNhan { get; set; }
        public DateTime NgayTra { get; set; }
        public decimal SoGio { get; set; }
        public decimal GiaPhong { get; set; }
        public decimal TienPhong { get; set; }
        public decimal TienDichVu { get; set; }
        public decimal TongTien { get { return TienPhong + TienDichVu; } }
    }
}
