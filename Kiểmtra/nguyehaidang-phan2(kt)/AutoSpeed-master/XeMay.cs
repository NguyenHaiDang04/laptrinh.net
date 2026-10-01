using System;

namespace AutoSpeed
{
    public class XeMay : PhuongTien
    {
        public int DungTichXylanh { get; set; }

        public XeMay(string maPT, string tenHang, int namSanXuat, decimal giaGoc, int dungTichXylanh)
            : base(maPT, tenHang, namSanXuat, giaGoc)
        {
            if (dungTichXylanh <= 0) throw new ArgumentException("Dung tích xy lanh phải > 0");
            DungTichXylanh = dungTichXylanh;
        }

        // Đa hình: Ghi đè phương thức trừu tượng
        public override decimal TinhGiaLanBanh()
        {
            decimal thueTruocBa = DungTichXylanh < 175 ? GiaGoc * 0.02m : GiaGoc * 0.05m;
            return GiaGoc + thueTruocBa;
        }

        public override string GetInfo()
        {
            return base.GetInfo() + $" | Phân khối: {DungTichXylanh}cc";
        }
    }
}