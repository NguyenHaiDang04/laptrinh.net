using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Garage.GUI
{
    internal static class FormSupport
    {
        public static void Run(Label message, Action action)
        {
            try
            {
                Notice(message, "", false);
                action();
            }
            catch (ArgumentException ex)
            {
                Notice(message, ex.Message, true);
            }
            catch (InvalidOperationException ex)
            {
                Notice(message, ex.Message, true);
            }
            catch (SqlException ex)
            {
                string text;
                switch (ex.Number)
                {
                    case 2601:
                    case 2627: text = "Dữ liệu bị trùng. Vui lòng kiểm tra lại."; break;
                    case 547: text = "Dữ liệu đang được sử dụng hoặc không thỏa ràng buộc."; break;
                    default: text = "Lỗi SQL Server (" + ex.Number + "). Kiểm tra CSDL hoặc App.config."; break;
                }
                Notice(message, text, true);
            }
        }

        public static void Notice(Label label, string text, bool error = false)
        {
            label.Text = text;
            label.ForeColor = error ? Color.Firebrick : Color.DarkGreen;
        }

        public static void Bind(ComboBox combo, DataTable table, string value, string display)
        {
            combo.DataSource = null;
            combo.DisplayMember = display;
            combo.ValueMember = value;
            combo.DataSource = table;
        }

        public static int Id(ComboBox combo)
        {
            return combo.SelectedValue is int ? (int)combo.SelectedValue : 0;
        }

        public static DataRow Row(DataGridView grid)
        {
            var view = grid.CurrentRow?.DataBoundItem as DataRowView;
            return view?.Row;
        }

        public static void Grid(DataGridView grid, DataTable table, params string[] headings)
        {
            grid.DataSource = table;
            for (int i = 0; i < headings.Length && i < grid.Columns.Count; i++)
                grid.Columns[i].HeaderText = headings[i];
            foreach (DataGridViewColumn column in grid.Columns)
            {
                Type type = column.ValueType;
                if (type == typeof(DateTime))
                    column.DefaultCellStyle.Format = column.Name.StartsWith("Gio")
                        ? "dd/MM/yyyy HH:mm:ss" : "dd/MM/yyyy HH:mm";
                if (type == typeof(decimal))
                    column.DefaultCellStyle.Format = "N2";
            }
        }
    }
}
