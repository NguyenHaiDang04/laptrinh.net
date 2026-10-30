using System;
using System.Data;
using BaiTap5.Shared.BUS;
using BaiTap5.Shared.DTO;
using CafePOS.DAL;
using CafePOS.DTO;

namespace CafePOS.BUS
{
    public sealed class CafeBus
    {
        private readonly CafeDal dal = new CafeDal();
        public ICatalogService CategoriesEditor { get; private set; }
        public ICatalogService MenuEditor { get; private set; }
        public CafeBus()
        {
            CategoriesEditor = new CatalogBus(dal.Database, new CatalogDefinition("Danh mục", "DanhMuc", "MaDanhMuc", true,
                new Field("TenDanhMuc","Tên danh mục",typeof(string))));
            MenuEditor = new CatalogBus(dal.Database, new CatalogDefinition("Thực đơn", "MonAn", "MaMon", true,
                new Field("TenMon","Tên món",typeof(string)),new Field("MaDanhMuc","Mã danh mục",typeof(int)),new Field("DonGia","Đơn giá",typeof(decimal))));
        }
        public DataTable Tables() { return dal.Tables(); }
        public DataTable Categories() { return dal.Categories(); }
        public DataTable Menu(int category) { return dal.Menu(category); }
        public DataTable Details(int table) { return dal.Details(table); }
        public DataTable Report() { return dal.Report(); }
        public void Order(OrderDto order)
        { if (order.MaBan<=0 || order.MaMon<=0 || order.SoLuong<=0) throw new ArgumentException("Chọn bàn, món và số lượng lớn hơn 0."); dal.Order(order); }
        public void Move(int source,int target)
        { if(source<=0 || target<=0 || source==target) throw new ArgumentException("Chọn hai bàn khác nhau."); dal.Move(source,target); }
        public decimal Checkout(int table) { if(table<=0) throw new ArgumentException("Chưa chọn bàn."); return dal.Checkout(table); }
    }
}
