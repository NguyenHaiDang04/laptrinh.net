namespace Garage.DTO
{
    internal sealed class KhachHang
    {
        public int MaKH { get; set; }
        public string TenKH { get; set; }
        public string DienThoai { get; set; }
        public string DiaChi { get; set; }
    }

    internal sealed class Xe
    {
        public string BienSo { get; set; }
        public int MaKH { get; set; }
        public string HieuXe { get; set; }
        public int NamSanXuat { get; set; }
    }

    internal sealed class PhuTung
    {
        public int MaPhuTung { get; set; }
        public string TenPhuTung { get; set; }
        public decimal DonGia { get; set; }
        public int SoLuongTon { get; set; }
    }

    internal sealed class DongSuaChua
    {
        public int MaPhuTung { get; set; }
        public string TenPhuTung { get; set; }
        public int SoLuong { get; set; }
        public decimal TienCong { get; set; }
    }
}
