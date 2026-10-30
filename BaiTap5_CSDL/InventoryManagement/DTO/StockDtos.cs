using System.Collections.Generic;

namespace InventoryManagement.DTO
{
    public sealed class StockLineDto
    {
        public string MaVatTu { get; set; }
        public decimal SoLuong { get; set; }
        public decimal DonGia { get; set; }
    }
    public sealed class StockVoucherDto
    {
        public string LoaiPhieu { get; set; }
        public int? MaNCC { get; set; }
        public List<StockLineDto> Lines { get; set; } = new List<StockLineDto>();
    }
}
