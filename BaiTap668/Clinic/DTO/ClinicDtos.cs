using System.Collections.Generic;
namespace Clinic.DTO
{
 public sealed class AdmissionDto {public string HoTen {get;set;}public string SoDienThoai {get;set;}public int NamSinh {get;set;}public string TrieuChung {get;set;}}
 public sealed class DrugLine {public int MaThuoc {get;set;}public int SoLuong {get;set;}public string LieuDung {get;set;}}
 public sealed class PrescriptionDto {public int MaHoSo {get;set;}public string ChanDoan {get;set;}public decimal TienKham {get;set;}public List<DrugLine> Lines {get;set;}=new List<DrugLine>();}
}
