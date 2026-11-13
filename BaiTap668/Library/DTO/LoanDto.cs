using System;
using System.Collections.Generic;
namespace Library.DTO
{
 public sealed class BookLine { public int MaSach {get;set;} public int SoLuong {get;set;} }
 public sealed class LoanDto { public int MaDocGia {get;set;} public DateTime HanTra {get;set;} public List<BookLine> Lines {get;set;}=new List<BookLine>(); }
}
