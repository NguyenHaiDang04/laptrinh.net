using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using BaiTap668.Shared.GUI;
using Clinic.BUS;
using Clinic.DTO;
namespace Clinic.GUI
{
 public partial class MainForm:Form
 {
  private ClinicBus bus;private CatalogPanel drugEditor;public readonly BindingList<DrugLine> Cart=new BindingList<DrugLine>();
  public MainForm()
  {
   InitializeComponent();if(LicenseManager.UsageMode==LicenseUsageMode.Designtime)return;
   bus=new ClinicBus();drugEditor=new CatalogPanel(bus.DrugEditor);drugPage.Controls.Add(drugEditor);drugCart.DataSource=Cart;quantity.Minimum=1;quantity.Value=1;birthYear.Minimum=1900;birthYear.Maximum=DateTime.Today.Year;birthYear.Value=2000;examFee.Value=100000;
   Selection.Wire(refreshButton,RefreshData);Selection.Wire(lookupButton,()=>{var t=bus.Patient(phone.Text.Trim());if(t.Rows.Count==0){patientName.Clear();MessageBox.Show("Bệnh nhân mới: nhập họ tên và năm sinh.");}else{patientName.Text=Convert.ToString(t.Rows[0]["HoTen"]);birthYear.Value=Convert.ToInt32(t.Rows[0]["NamSinh"]);}});
   Selection.Wire(admitButton,()=>{int id=bus.Admit(new AdmissionDto{HoTen=patientName.Text.Trim(),SoDienThoai=phone.Text.Trim(),NamSinh=(int)birthYear.Value,TrieuChung=symptoms.Text});RefreshData();record.SelectedValue=id;Pages.SelectedIndex=3;MessageBox.Show("Đã tiếp nhận hồ sơ #"+id);});
   Selection.Wire(addButton,()=>{int id=Ui.Id(drug);foreach(var l in Cart)if(l.MaThuoc==id)throw new ArgumentException("Thuốc đã có trong đơn. Xóa dòng để nhập lại.");if(string.IsNullOrWhiteSpace(dose.Text))throw new ArgumentException("Nhập liều dùng.");Cart.Add(new DrugLine{MaThuoc=id,SoLuong=(int)quantity.Value,LieuDung=dose.Text});});
   Selection.Wire(removeButton,()=>{if(drugCart.CurrentRow!=null)Cart.Remove((DrugLine)drugCart.CurrentRow.DataBoundItem);});
   Selection.Wire(prescribeButton,()=>{var dto=new PrescriptionDto{MaHoSo=Ui.Id(record),ChanDoan=diagnosis.Text,TienKham=examFee.Value};foreach(var l in Cart)dto.Lines.Add(new DrugLine{MaThuoc=l.MaThuoc,SoLuong=l.SoLuong,LieuDung=l.LieuDung});int id=bus.Prescribe(dto);Cart.Clear();RefreshData();ShowInvoice(id);});
   Selection.Wire(payButton,()=>{int id=Selection.Id(orders,"MaDon");if(Ui.Confirm("Xác nhận thanh toán viện phí?")){bus.Pay(id);RefreshData();ShowInvoice(id);}});
   Selection.Wire(invoiceButton,()=>ShowInvoice(Selection.Id(orders,"MaDon")));
   Selection.Wire(exportButton,()=>{if(invoiceText.Text.Length==0)throw new ArgumentException("Chưa có hóa đơn.");using(var dlg=new SaveFileDialog{Filter="Văn bản|*.txt",FileName="HoaDonPhongKham.txt"})if(dlg.ShowDialog()==DialogResult.OK)File.WriteAllText(dlg.FileName,invoiceText.Text,Encoding.UTF8);});
   Selection.Wire(printButton,PrintInvoice);Shown+=(s,e)=>Ui.Guard(RefreshData);Pages.SelectedIndexChanged+=(s,e)=>{if(bus!=null)Ui.Guard(RefreshData);};
  }
  public void ShowInvoice(int id){invoiceText.Text=bus.Invoice(id);Pages.SelectedIndex=5;}
  private void PrintInvoice()
  {
   if(invoiceText.Text.Length==0)throw new ArgumentException("Chưa có hóa đơn.");using(var document=new PrintDocument())using(var preview=new PrintPreviewDialog())
   {string remaining=invoiceText.Text;document.BeginPrint+=(s,e)=>remaining=invoiceText.Text;document.PrintPage+=(s,e)=>{using(var font=new Font("Segoe UI",11)){int chars,lines;e.Graphics.MeasureString(remaining,font,e.MarginBounds.Size,StringFormat.GenericTypographic,out chars,out lines);if(chars<=0)throw new InvalidOperationException("Khổ giấy quá nhỏ.");e.Graphics.DrawString(remaining.Substring(0,chars),font,Brushes.Black,e.MarginBounds,StringFormat.GenericTypographic);remaining=remaining.Substring(chars);e.HasMorePages=remaining.Length>0;}};preview.Document=document;preview.Width=1000;preview.Height=700;preview.ShowDialog(this);}
  }
  public void RefreshData(){records.DataSource=bus.Records();orders.DataSource=bus.Orders();Ui.Bind(record,bus.Records(),"MaHoSo","MaHoSo");Ui.Bind(drug,bus.Drugs(),"MaThuoc","TenThuoc");drugEditor.Reload();}
 }
}
