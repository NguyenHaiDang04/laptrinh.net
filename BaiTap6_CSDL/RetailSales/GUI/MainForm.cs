using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using BaiTap6.Shared.GUI;
using RetailSales.BUS;
using RetailSales.DTO;

namespace RetailSales.GUI
{
 public partial class MainForm : Form
 {
  public readonly DataTable Cart = new DataTable();
  private SalesBus bus;
  private CatalogPanel products, customers, employees;

  public MainForm()
  {
   InitializeComponent();
   if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
   bus = new SalesBus();
   products = new CatalogPanel(bus.ProductEditor);
   customers = new CatalogPanel(bus.CustomerEditor);
   employees = new CatalogPanel(bus.EmployeeEditor);
   productPage.Controls.Add(products);
   customerPage.Controls.Add(customers);
   employeePage.Controls.Add(employees);
   Cart.Columns.Add("MaSP", typeof(string));
   Cart.Columns.Add("TenSP", typeof(string));
   Cart.Columns.Add("SoLuong", typeof(int));
   Cart.Columns.Add("DonGia", typeof(decimal));
   Cart.Columns.Add("ThanhTien", typeof(decimal), "SoLuong*DonGia");
   Cart.PrimaryKey = new[] { Cart.Columns["MaSP"] };
   grid.DataSource = Cart;
  }

  private void SearchButton_Click(object sender, EventArgs e)
  { Ui.Guard(() => stock.DataSource = bus.Products(search.Text)); }
  private void ReloadButton_Click(object sender, EventArgs e)
  { Ui.Guard(RefreshData); }
  private void AddButton_Click(object sender, EventArgs e)
  { Ui.Guard(() => AddProduct(code.Text, (int)qty.Value)); }
  private void RemoveButton_Click(object sender, EventArgs e)
  {
   Ui.Guard(() =>
   {
    if (grid.CurrentRow != null) ((DataRowView)grid.CurrentRow.DataBoundItem).Row.Delete();
    UpdateTotal();
   });
  }
  private void PayButton_Click(object sender, EventArgs e)
  {
   Ui.Guard(() =>
   {
    if (!Ui.Confirm("Xác nhận thanh toán?")) return;
    var dto = new SaleDto
    {
     MaNV = Ui.Id(employee),
     MaKH = Ui.Id(customer) == 0 ? (int?)null : Ui.Id(customer)
    };
    foreach (DataRow row in Cart.Rows)
     dto.Lines.Add(new SaleLine { MaSP = (string)row["MaSP"], SoLuong = (int)row["SoLuong"] });
    var receipt = bus.Pay(dto);
    Cart.Clear();
    RefreshData();
    UpdateTotal();
    MessageBox.Show("Hóa đơn #" + receipt.MaHD + " | " + receipt.TongTien.ToString("N0") + " đ | Điểm: " + receipt.Diem);
   });
  }
  private void Code_KeyDown(object sender, KeyEventArgs e)
  {
   if (e.KeyCode != Keys.Enter) return;
   Ui.Guard(() => AddProduct(code.Text, (int)qty.Value));
   e.SuppressKeyPress = true;
  }
  private void Customer_SelectedIndexChanged(object sender, EventArgs e) { UpdateTotal(); }
  private void ReportButton_Click(object sender, EventArgs e) { Ui.Guard(RefreshReport); }
  private void MainForm_Shown(object sender, EventArgs e) { if (bus != null) Ui.Guard(RefreshData); }
  private void Pages_SelectedIndexChanged(object sender, EventArgs e) { if (bus != null) Ui.Guard(RefreshData); }
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
