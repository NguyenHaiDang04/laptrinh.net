using System;

namespace ProjectTask.DTO
{
    internal sealed class DuAn
    {
        public int MaDuAn { get; set; }
        public string TenDuAn { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public string TrangThai { get; set; }
    }

    internal sealed class CongViec
    {
        public int MaTask { get; set; }
        public int MaDuAn { get; set; }
        public string TenTask { get; set; }
        public string MoTa { get; set; }
        public DateTime HanNop { get; set; }
        public string DoUuTien { get; set; }
        public string TrangThai { get; set; }
        public int[] ThanhVien { get; set; }
    }
}
