using System;

namespace AutoSpeed
{
    public class OTo : PhuongTien
    {
        public int SoChoNgoi { get; set; }
        public double DungTichDongCo { get; set; }

        public OTo(string maPT, string tenHang, int namSanXuat, decimal giaGoc, int soChoNgoi, double dungTichDongCo)
            : base(maPT, tenHang, namSanXuat, giaGoc)
        {
            if (soChoNgoi <= 0) throw new ArgumentException("Số chỗ ngồi phải > 0");
            if (dungTichDongCo <= 0) throw new ArgumentException("Dung tích động cơ phải > 0");

            SoChoNgoi = soChoNgoi;
            DungTichDongCo = dungTichDongCo;
        }

        // Đa hình: Ghi đè phương thức trừu tượng
        public override decimal TinhGiaLanBanh()
        {
            decimal thueTruocBa = 0;
            decimal thueTieuThuDacBiet = 0;

            if (SoChoNgoi <= 9)
            {
                thueTruocBa = GiaGoc * 0.12m;
                thueTieuThuDacBiet = GiaGoc * 0.30m;
            }
            else
            {
                thueTruocBa = GiaGoc * 0.10m;
                // > 9 chỗ không tính thuế tiêu thụ đặc biệt theo đề bài
            }

            return GiaGoc + thueTruocBa + thueTieuThuDacBiet;
        }

        // Mở rộng phương thức ảo
        public override string GetInfo()
        {
            return base.GetInfo() + $" | Chỗ ngồi: {SoChoNgoi} | Dung tích: {DungTichDongCo}L";
        }
    }
}