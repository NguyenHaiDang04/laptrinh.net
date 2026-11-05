using System;
using System.Data;
using System.Windows.Forms;
using BaiTap6.Shared.GUI;
using RetailSales.BUS;
using RetailSales.DTO;
namespace RetailSales.GUI
{
 public sealed class MainForm:Form
 {
  public readonly TabControl Pages; public readonly DataTable Cart=new DataTable();
  private readonly SalesBus bus=new SalesBus();private readonly ComboBox employee,customer;private readonly TextBox code;private readonly NumericUpDown qty;private readonly Label total,revenue;
  private readonly DataGridView stock,top,bills;private readonly CatalogPanel products,customers,employees;private readonly DateTimePicker date;
  public MainForm()
  {
   Ui.Style(this,"Bài 6.1 - Bán hàng siêu thị mini");Pages=Ui.Tabs(this);
   var main=Ui.Tab(Pages,"Tổng quan");stock=Ui.Grid(main);var find=Ui.Row(main);var search=Ui.Text(find,"Mã / tên hàng",250);Ui.Button(find,"Tìm",()=>stock.DataSource=bus.Products(search.Text));Ui.Button(find,"Tải lại",RefreshData);
   var p=Ui.Tab(Pages,"Sản phẩm");products=new CatalogPanel(bus.ProductEditor);p.Controls.Add(products);
   var c=Ui.Tab(Pages,"Khách hàng");customers=new CatalogPanel(bus.CustomerEditor);c.Controls.Add(customers);
   var e=Ui.Tab(Pages,"Nhân viên");employees=new CatalogPanel(bus.EmployeeEditor);e.Controls.Add(employees);
   var sale=Ui.Tab(Pages,"Bán hàng");var grid=Ui.Grid(sale);
   Cart.Columns.Add("MaSP",typeof(string));Cart.Columns.Add("TenSP",typeof(string));Cart.Columns.Add("SoLuong",typeof(int));Cart.Columns.Add("DonGia",typeof(decimal));Cart.Columns.Add("ThanhTien",typeof(decimal),"SoLuong*DonGia");Cart.PrimaryKey=new[]{Cart.Columns["MaSP"]};grid.DataSource=Cart;
   var actions=Ui.Row(sale);Ui.Button(actions,"Xóa dòng",()=>{if(grid.CurrentRow!=null)((DataRowView)grid.CurrentRow.DataBoundItem).Row.Delete();UpdateTotal();});
   Ui.Button(actions,"Thanh toán",()=>{if(!Ui.Confirm("Xác nhận thanh toán?"))return;var dto=new SaleDto{MaNV=Ui.Id(employee),MaKH=Ui.Id(customer)==0?(int?)null:Ui.Id(customer)};
    foreach(DataRow r in Cart.Rows)dto.Lines.Add(new SaleLine{MaSP=(string)r["MaSP"],SoLuong=(int)r["SoLuong"]});var receipt=bus.Pay(dto);Cart.Clear();RefreshData();UpdateTotal();MessageBox.Show("Hóa đơn #"+receipt.MaHD+" | "+receipt.TongTien.ToString("N0")+" đ | Điểm: "+receipt.Diem);});
   total=new Label{AutoSize=true,Margin=new Padding(10)};actions.Controls.Add(total);
   var add=Ui.Row(sale);code=Ui.Text(add,"Quét / nhập mã",180);qty=Ui.Number(add,"Số lượng",1);Ui.Button(add,"Thêm vào giỏ",()=>AddProduct(code.Text,(int)qty.Value));code.KeyDown+=(s,a)=>{if(a.KeyCode==Keys.Enter){Ui.Guard(()=>AddProduct(code.Text,(int)qty.Value));a.SuppressKeyPress=true;}};
   var people=Ui.Row(sale);employee=Ui.Combo(people,"Thu ngân");customer=Ui.Combo(people,"Khách hàng");customer.SelectedIndexChanged+=(s,a)=>UpdateTotal();
   var report=Ui.Tab(Pages,"Báo cáo");top=Ui.Grid(report);var rr=Ui.Row(report);date=new DateTimePicker{Format=DateTimePickerFormat.Short};rr.Controls.Add(date);Ui.Button(rr,"Xem doanh thu / Top 5",RefreshReport);revenue=new Label{AutoSize=true,Margin=new Padding(10)};rr.Controls.Add(revenue);
   bills=Ui.Grid(Ui.Tab(Pages,"Lịch sử hóa đơn"));Shown+=(s,a)=>Ui.Guard(RefreshData);Pages.SelectedIndexChanged+=(s,a)=>Ui.Guard(RefreshData);
  }
  public void AddProduct(string key,int quantity)
  {
   var dt=bus.Products(key);DataRow item=null;foreach(DataRow r in dt.Rows)if(string.Equals(Convert.ToString(r["MaSP"]),key,StringComparison.OrdinalIgnoreCase))item=r;
   if(item==null)throw new ArgumentException("Không tìm thấy mã sản phẩm.");if(quantity<=0)throw new ArgumentException("Số lượng phải dương.");
   var old=Cart.Rows.Find(item["MaSP"]);if(old==null)Cart.Rows.Add(item["MaSP"],item["TenSP"],quantity,item["GiaBan"]);else old["SoLuong"]=checked((int)old["SoLuong"]+quantity);UpdateTotal();
  }
  private void UpdateTotal(){if(total==null)return;decimal sum=0;foreach(DataRow r in Cart.Rows)sum+=Convert.ToDecimal(r["ThanhTien"]);total.Text="Tạm tính: "+sum.ToString("N0")+" đ | Điểm dự kiến: "+(customer!=null&&customer.SelectedValue is int&&Ui.Id(customer)!=0?decimal.Floor(sum/100000):0);}
  private void RefreshReport(){var day=date.Value.Date;var month=new DateTime(day.Year,day.Month,1);revenue.Text="Ngày: "+bus.Revenue(day,day.AddDays(1)).ToString("N0")+" đ | Tháng: "+bus.Revenue(month,month.AddMonths(1)).ToString("N0")+" đ";top.DataSource=bus.Top5();}
  public void RefreshData(){stock.DataSource=bus.Products("");products.Reload();customers.Reload();employees.Reload();Ui.Bind(employee,bus.Employees(),"MaNV","TenNV");var dt=bus.Customers();var r=dt.NewRow();r["MaKH"]=0;r["TenKH"]="Khách lẻ";r["DienThoai"]="";r["DiemTichLuy"]=0;dt.Rows.InsertAt(r,0);Ui.Bind(customer,dt,"MaKH","TenKH");RefreshReport();bills.DataSource=bus.Bills();}
 }
}
