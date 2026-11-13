using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BaiTap668.Shared.GUI;
using Billiards.BUS;
using Billiards.DTO;
namespace Billiards.GUI
{
 public partial class MainForm:Form
 {
  private BilliardsBus bus;private CatalogPanel menuEditor;private int selected;private bool playing;private Timer clock;
  public DataTable Draft {get;private set;}
  public MainForm()
  {
   InitializeComponent();if(LicenseManager.UsageMode==LicenseUsageMode.Designtime)return;
   bus=new BilliardsBus();menuEditor=new CatalogPanel(bus.MenuEditor);menuPage.Controls.Add(menuEditor);quantity.Minimum=1;quantity.Value=1;services.ReadOnly=false;
   services.DataError+=(s,e)=>{e.ThrowException=false;MessageBox.Show("Số lượng phải là số nguyên dương.");};
   Selection.Wire(refreshButton,()=>SelectTable(selected));Selection.Wire(openButton,()=>{bus.Open(selected);SelectTable(selected);});
   Selection.Wire(addButton,()=>AddService(Ui.Id(service),(int)quantity.Value));Selection.Wire(saveButton,SaveDraft);
   Selection.Wire(removeButton,()=>{if(services.CurrentRow!=null)((DataRowView)services.CurrentRow.DataBoundItem).Row.Delete();});
   Selection.Wire(checkoutButton,()=>{if(Ui.Confirm("Thanh toán bàn đã chọn?")){SaveDraft();var invoice=bus.Checkout(selected);SelectTable(selected);MessageBox.Show("Hóa đơn #"+invoice.MaHD+" | Tiền giờ: "+invoice.TienGio.ToString("N0")+" đ | Dịch vụ: "+invoice.TienDichVu.ToString("N0")+" đ | Tổng: "+invoice.TongTien.ToString("N0")+" đ");}});
   clock=new Timer(components){Interval=1000};clock.Tick+=(s,e)=>{if(playing)Ui.Guard(UpdateSummary);};Shown+=(s,e)=>Ui.Guard(()=>{RefreshData();SelectTable(selected);clock.Start();});Pages.SelectedIndexChanged+=(s,e)=>{if(bus!=null)Ui.Guard(RefreshData);};
  }
  public void SelectTable(int id)
  {
   services.EndEdit();if(Draft!=null&&Draft.GetChanges()!=null&&!Ui.Confirm("Bỏ thay đổi dịch vụ chưa lưu để tải bàn?"))return;
   selected=id;Draft=bus.Services(selected);Draft.PrimaryKey=new[]{Draft.Columns["MaDV"]};Draft.Columns["ThanhTien"].Expression="SoLuong*DonGia";services.DataSource=Draft;
   foreach(DataGridViewColumn c in services.Columns)c.ReadOnly=c.Name!="SoLuong";RefreshData();UpdateSummary();
  }
  public void AddService(int id,int count)
  {
   if(!playing||Draft==null)throw new InvalidOperationException("Mở bàn trước khi gọi dịch vụ.");if(count<=0)throw new ArgumentException("Số lượng phải dương.");
   var menu=bus.Menu();DataRow item=null;foreach(DataRow r in menu.Rows)if(Convert.ToInt32(r["MaDV"])==id)item=r;if(item==null)throw new ArgumentException("Dịch vụ không tồn tại.");var old=Draft.Rows.Find(id);if(old==null)Draft.Rows.Add(id,item["TenDV"],count,item["DonGia"]);else old["SoLuong"]=checked(Convert.ToInt32(old["SoLuong"])+count);
  }
  public void SaveDraft(){services.EndEdit();if(Draft==null)throw new ArgumentException("Chọn bàn.");((CurrencyManager)BindingContext[Draft]).EndCurrentEdit();bus.Save(selected,Draft);Draft.AcceptChanges();SelectTable(selected);}
  private void UpdateSummary(){if(!playing){summary.Text="Bàn #"+selected+" | Trống";return;}var invoice=bus.Preview(selected);summary.Text="Bàn #"+selected+" | "+invoice.SoBlock+" block 15 phút | Đã lưu: "+invoice.TongTien.ToString("N0")+" đ";}
  public void RefreshData()
  {
   var tables=bus.Tables();if(selected==0&&tables.Rows.Count>0)selected=Convert.ToInt32(tables.Rows[0]["MaBan"]);playing=false;
   while(tableButtons.Controls.Count>0)tableButtons.Controls[0].Dispose();foreach(DataRow r in tables.Rows){int id=Convert.ToInt32(r["MaBan"]);bool active=Convert.ToString(r["TrangThai"])=="Đang chơi";if(id==selected)playing=active;var button=Ui.Button(tableButtons,r["TenBan"]+"\n"+r["TrangThai"],()=>SelectTable(id));button.AutoSize=false;button.Size=new Size(125,85);button.BackColor=active?Color.LightSalmon:Color.LightGreen;}
   Ui.Bind(service,bus.Menu(),"MaDV","TenDV");menuEditor.Reload();report.DataSource=bus.Report();
  }
 }
}
