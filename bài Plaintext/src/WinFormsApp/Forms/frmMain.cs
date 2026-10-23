using System;
using System.Windows.Forms;

namespace WinFormsApp.Forms
{
    public sealed class frmMain : Form
    {
        public frmMain()
        {
            Ui.Setup(this, "TEST 02 - Bài tập C# WinForms");
            var p = Ui.Page(this, 55, 45, 65, 65, 65, 65, 65, -1);
            p.Controls.Add(Ui.Heading("BÀI TẬP THỰC HÀNH C# WINFORMS"), 0, 0);
            p.Controls.Add(new Label { Text = "Chọn bài tập để mở. Các màn hình có sẵn dữ liệu minh họa.", Dock = DockStyle.Fill }, 0, 1);
            Add(p, 2, "Bài 1 - Quản lý hội viên Gym", () => new frmGymMemberManager());
            Add(p, 3, "Bài 2 - Quản lý sách và file CSV", () => new frmBookManager());
            Add(p, 4, "Bài 3 - Quản lý bệnh viện MDI", () => new frmHospitalMain());
            Add(p, 5, "Bài 4 - Thống kê doanh thu bất đồng bộ", () => new frmRevenueAnalytics());
            Add(p, 6, "Bài 5 - Giám sát tỷ giá thời gian thực", () => new frmCryptoStockTicker());
        }
        private void Add(TableLayoutPanel p, int row, string text, Func<Form> create)
        {
            var b = Ui.Button("btnExercise" + (row - 1), text, (s, e) => { using (var form = create()) form.ShowDialog(this); });
            b.Dock = DockStyle.Fill; p.Controls.Add(b, 0, row);
        }
    }
}
