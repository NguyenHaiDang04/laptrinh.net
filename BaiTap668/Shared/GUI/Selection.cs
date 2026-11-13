using System;
using System.Data;
using System.Windows.Forms;
namespace BaiTap668.Shared.GUI
{
 public static class Selection
 {
  public static int Id(DataGridView grid,string column){var row=grid.CurrentRow==null?null:grid.CurrentRow.DataBoundItem as DataRowView;if(row==null)throw new ArgumentException("Chọn một dòng dữ liệu.");return Convert.ToInt32(row[column]);}
  public static void Wire(Button button,Action action){button.Click+=(s,e)=>Ui.Guard(action);}
 }
}
