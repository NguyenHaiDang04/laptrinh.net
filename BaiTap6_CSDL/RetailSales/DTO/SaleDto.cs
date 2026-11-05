using System.Collections.Generic;
namespace RetailSales.DTO
{
 public sealed class SaleLine { public string MaSP {get;set;} public int SoLuong {get;set;} }
 public sealed class SaleDto { public int MaNV {get;set;} public int? MaKH {get;set;} public List<SaleLine> Lines {get;set;}=new List<SaleLine>(); }
 public sealed class ReceiptDto { public int MaHD {get;set;} public decimal TongTien {get;set;} public int Diem {get;set;} }
}
