using System;
namespace Billiards.DTO
{
 public sealed class SessionDto
 {
  public int MaHD {get;set;}public DateTime BatDau {get;set;}public DateTime KetThuc {get;set;}public decimal SoBlock {get;set;}public decimal TienGio {get;set;}public decimal TienDichVu {get;set;}public decimal TongTien {get{return TienGio+TienDichVu;}}
 }
}
