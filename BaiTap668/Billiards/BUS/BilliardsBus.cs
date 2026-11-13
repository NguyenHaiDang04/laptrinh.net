using System;
using System.Data;
using System.Collections.Generic;
using BaiTap668.Shared.BUS;
using BaiTap668.Shared.DTO;
using Billiards.DAL;
using Billiards.DTO;
namespace Billiards.BUS
{
 public sealed class BilliardsBus
 {
  private readonly BilliardsDal dal=new BilliardsDal();public ICatalogService MenuEditor;
  public BilliardsBus(){MenuEditor=new ValidatedCatalog(dal.Database,new CatalogDefinition("Dịch vụ","MenuDichVu","MaDV",true,new Field("TenDV","Tên dịch vụ",typeof(string)),new Field("DonGia","Đơn giá",typeof(decimal)),new Field("TonKho","Tồn kho",typeof(decimal))),v=>{decimal n=Convert.ToDecimal(v[2]);if(n<0||decimal.Truncate(n)!=n)throw new ArgumentException("Tồn kho phải là số nguyên không âm.");});}
  public DataTable Tables(){return dal.Tables();}public DataTable Menu(){return dal.Menu();}public DataTable Services(int table){return dal.Services(table);}public DataTable Report(){return dal.Report();}public int Open(int table){return dal.Open(table);}public SessionDto Preview(int table){return dal.Preview(table);}public SessionDto Checkout(int table){return dal.Checkout(table);}
  public void Save(int table,DataTable data){if(data==null)throw new ArgumentException("Chọn bàn trước khi lưu.");var ids=new HashSet<int>();foreach(DataRow r in data.Rows){if(r.RowState==DataRowState.Deleted)continue;decimal n=Convert.ToDecimal(r["SoLuong"]);int id=Convert.ToInt32(r["MaDV"]);if(id<=0||!ids.Add(id)||n<=0||n>int.MaxValue||decimal.Truncate(n)!=n)throw new ArgumentException("Dịch vụ không trùng, số lượng phải là số nguyên dương.");}dal.Save(table,data);}
 }
}
