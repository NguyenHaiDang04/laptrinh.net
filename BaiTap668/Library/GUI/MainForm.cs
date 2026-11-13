using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using BaiTap668.Shared.GUI;
using Library.BUS;
using Library.DTO;
namespace Library.GUI
{
 public partial class MainForm:Form
 {
  private LibraryBus bus;private CatalogPanel booksEditor,readersEditor;
  public readonly BindingList<BookLine> Cart=new BindingList<BookLine>();
  public MainForm()
  {
   InitializeComponent();if(LicenseManager.UsageMode==LicenseUsageMode.Designtime)return;
   bus=new LibraryBus();booksEditor=new CatalogPanel(bus.BookEditor);booksPage.Controls.Add(booksEditor);readersEditor=new CatalogPanel(bus.ReaderEditor);readersPage.Controls.Add(readersEditor);
   cartGrid.DataSource=Cart;quantity.Minimum=1;quantity.Value=1;due.Value=DateTime.Today.AddDays(7);stateFilter.Items.AddRange(new object[]{"Tất cả","Đang mượn","Đã trả","Quá hạn"});stateFilter.SelectedIndex=0;
   Selection.Wire(refreshButton,RefreshData);Selection.Wire(filterButton,Filter);Selection.Wire(returnButton,()=>{if(Ui.Confirm("Xác nhận trả toàn bộ sách của phiếu?")){bus.Return(Selection.Id(loans,"MaPhieu"));RefreshData();}});
   Selection.Wire(addButton,()=>{int id=Ui.Id(book);foreach(var l in Cart)if(l.MaSach==id){l.SoLuong=checked(l.SoLuong+(int)quantity.Value);Cart.ResetBindings();return;}Cart.Add(new BookLine{MaSach=id,SoLuong=(int)quantity.Value});});
   Selection.Wire(removeButton,()=>{if(cartGrid.CurrentRow!=null)Cart.Remove((BookLine)cartGrid.CurrentRow.DataBoundItem);});
   Selection.Wire(borrowButton,()=>{var dto=new LoanDto{MaDocGia=Ui.Id(reader),HanTra=due.Value};foreach(var l in Cart)dto.Lines.Add(new BookLine{MaSach=l.MaSach,SoLuong=l.SoLuong});int id=bus.Borrow(dto);Cart.Clear();RefreshData();MessageBox.Show("Đã lập phiếu #"+id);});
   Shown+=(s,e)=>Ui.Guard(RefreshData);Pages.SelectedIndexChanged+=(s,e)=>{if(bus!=null)Ui.Guard(RefreshData);};
  }
  private void Filter(){loans.DataSource=bus.Loans(readerFilter.SelectedValue is int?Ui.Id(readerFilter):0,Convert.ToString(stateFilter.SelectedItem));}
  public void RefreshData()
  {
   int selected=readerFilter.SelectedValue is int?Ui.Id(readerFilter):0;var readers=bus.Readers();Ui.Bind(reader,readers.Copy(),"MaDocGia","HoTen");var row=readers.NewRow();row["MaDocGia"]=0;row["HoTen"]="Tất cả";row["Email"]="";row["NgayHetHanThe"]=DateTime.Today;readers.Rows.InsertAt(row,0);Ui.Bind(readerFilter,readers,"MaDocGia","HoTen");readerFilter.SelectedValue=selected;
   Ui.Bind(book,bus.Books(),"MaSach","TenSach");Filter();overdue.DataSource=bus.Overdue();statistics.DataSource=bus.Statistics();booksEditor.Reload();readersEditor.Reload();
  }
 }
}
