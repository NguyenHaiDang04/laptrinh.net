using System;
using System.Windows.Forms;

namespace Bai4_7_MDIManagement
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            timerTime.Start();
            lblUser.Text = "Người dùng: Admin |";
            lblStatus.Text = "Sẵn sàng";
            MoFormCon(typeof(FormAsyncLoad));
        }

        // Cập nhật đồng hồ thời gian thực
        private void TimerTime_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }
        
        

        // HÀM QUAN TRỌNG: Form con sẽ gọi hàm này để cập nhật StatusStrip của Form cha
        public void CapNhatTrangThai(string thongBao)
        {
            lblStatus.Text = thongBao;
        }

        // THUẬT TOÁN KIỂM TRA MỞ FORM CON
        private void MoFormCon(Type typeForm)
        {
            // Kiểm tra xem form đã được mở chưa
            foreach (Form f in this.MdiChildren)
            {
                if (f.GetType() == typeForm)
                {
                    f.Activate(); // Đã mở -> Đưa lên trên cùng
                    return;
                }
            }

            // Chưa mở -> Khởi tạo và hiển thị
            Form frm = (Form)Activator.CreateInstance(typeForm);
            frm.MdiParent = this; // Xác định cha là FormMain
            frm.Show();
            CapNhatTrangThai("Mở cửa sổ " + frm.Text);
        }

        // Sự kiện Menu & Shortcut: Quản lý Sinh viên
        private void MnuQuanLySinhVien_Click(object sender, EventArgs e)
        {
            MoFormCon(typeof(FormStudent));
        }

        // Sự kiện Menu: Thoát
        private void mnuThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Sự kiện Menu Sắp xếp cửa sổ
        private void mnuCascade_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade); // Xếp chồng chéo
        }

        private void mnuTileHorizontal_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal); // Xếp ngang
        }

        private void mnuTileVertical_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical); // Xếp dọc
        }
        // Sự kiện Menu: Quản lý Sản phẩm (Dùng để mở Bài 4.8)
        private void mnuQuanLySanPham_Click(object sender, EventArgs e)
        {
            MoFormCon(typeof(FormAsyncLoad));
        }
    }
}