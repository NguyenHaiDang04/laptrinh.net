using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Bai5_4_QuanLyTapTin
{
    // Lớp chứa dữ liệu Nhân viên
    public class Employee
    {
        public string MaNV { get; set; }
        public string HoTen { get; set; }
        public string ChucVu { get; set; }
        public string NgayVaoLam { get; set; }
        public string Nhom { get; set; } // Dùng để đối chiếu với TreeView
    }

    public partial class Form1 : Form
    {
        private List<Employee> dsNhanVien = new List<Employee>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 1. Tự động tạo Icon cho TreeView và ListView (Không cần file ảnh ngoài)
            TaoIconTuDong();

            // 2. Cài đặt ComboBox chuyển đổi View
            cboViewMode.Items.AddRange(new string[] { "Details (Chi tiết)", "LargeIcon (Biểu tượng lớn)", "SmallIcon (Biểu tượng nhỏ)", "Tile (Dạng khối)", "List (Danh sách)" });
            cboViewMode.SelectedIndex = 0; // Mặc định là Details

            // 3. Khởi tạo cấu trúc cây phòng ban (TreeView)
            KhoiTaoTreeView();

            // 4. Tạo dữ liệu nhân viên mẫu
            TaoDuLieuNhanVien();

            // 5. Cấu hình cột cho ListView
            CaiDatListView();
        }

        // Tạo cấu trúc cây (TreeView)
        private void KhoiTaoTreeView()
        {
            tvDepartments.Nodes.Clear();

            TreeNode root = new TreeNode("Công ty ABC", 0, 0); // Node gốc

            // Phòng Kỹ thuật
            TreeNode nodeIT = new TreeNode("Phòng Kỹ thuật", 1, 1);
            nodeIT.Nodes.Add(new TreeNode("Nhóm Dev", 2, 2));
            nodeIT.Nodes.Add(new TreeNode("Nhóm Test", 2, 2));

            // Phòng Nhân sự
            TreeNode nodeHR = new TreeNode("Phòng Nhân sự", 1, 1);
            nodeHR.Nodes.Add(new TreeNode("Nhóm Tuyển dụng", 2, 2));
            nodeHR.Nodes.Add(new TreeNode("Nhóm C&B", 2, 2));

            root.Nodes.Add(nodeIT);
            root.Nodes.Add(nodeHR);

            tvDepartments.Nodes.Add(root);
            tvDepartments.ExpandAll(); // Mở rộng toàn bộ cây
        }

        // Dữ liệu mẫu nhân viên
        private void TaoDuLieuNhanVien()
        {
            dsNhanVien.Add(new Employee { MaNV = "NV01", HoTen = "Nguyễn Hải Đăng", ChucVu = "Trưởng nhóm", NgayVaoLam = "15/05/2020", Nhom = "Nhóm Dev" });
            dsNhanVien.Add(new Employee { MaNV = "NV02", HoTen = "Kiều Đức Dương", ChucVu = "Lập trình viên", NgayVaoLam = "01/08/2021", Nhom = "Nhóm Dev" });
            dsNhanVien.Add(new Employee { MaNV = "NV03", HoTen = "Đoàn Thành Đạt ", ChucVu = "Tester", NgayVaoLam = "10/02/2022", Nhom = "Nhóm Test" });
            dsNhanVien.Add(new Employee { MaNV = "NV04", HoTen = "Nguyễn Trung Hiếu", ChucVu = "Trưởng phòng", NgayVaoLam = "05/01/2018", Nhom = "Phòng Kỹ thuật" });
            dsNhanVien.Add(new Employee { MaNV = "NV05", HoTen = "Hoàng Nghĩa Bảo", ChucVu = "Chuyên viên Tuyển dụng", NgayVaoLam = "20/11/2019", Nhom = "Nhóm Tuyển dụng" });
        }

        // Cài đặt các cột của ListView
        private void CaiDatListView()
        {
            lsvEmployees.View = View.Details;
            lsvEmployees.FullRowSelect = true;
            lsvEmployees.GridLines = true;
            lsvEmployees.Columns.Add("Mã NV", 80);
            lsvEmployees.Columns.Add("Họ Tên", 180);
            lsvEmployees.Columns.Add("Chức vụ", 150);
            lsvEmployees.Columns.Add("Ngày vào làm", 120);
        }

        // SỰ KIỆN: Bấm vào 1 nhánh trên TreeView -> Đổ dữ liệu sang ListView
        private void tvDepartments_AfterSelect(object sender, TreeViewEventArgs e)
        {
            lsvEmployees.Items.Clear();
            string selectedNode = e.Node.Text;

            List<Employee> locNhanVien = new List<Employee>();

            // Nếu chọn "Công ty ABC", hiện tất cả nhân viên
            if (selectedNode == "Công ty ABC")
            {
                locNhanVien = dsNhanVien;
            }
            else
            {
                // Lọc nhân viên có nhóm/phòng ban khớp với Node vừa chọn
                locNhanVien = dsNhanVien.Where(nv => nv.Nhom == selectedNode).ToList();
            }

            // Đổ vào ListView
            foreach (var nv in locNhanVien)
            {
                ListViewItem item = new ListViewItem(nv.MaNV, 3); // Số 3 là index của icon Nhân viên
                item.SubItems.Add(nv.HoTen);
                item.SubItems.Add(nv.ChucVu);
                item.SubItems.Add(nv.NgayVaoLam);

                lsvEmployees.Items.Add(item);
            }
        }

        // SỰ KIỆN: Đổi chế độ hiển thị bằng ComboBox
        private void cboViewMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cboViewMode.SelectedIndex)
            {
                case 0: lsvEmployees.View = View.Details; break;
                case 1: lsvEmployees.View = View.LargeIcon; break;
                case 2: lsvEmployees.View = View.SmallIcon; break;
                case 3: lsvEmployees.View = View.Tile; break;
                case 4: lsvEmployees.View = View.List; break;
            }
        }

        // Hàm phụ: Tự động vẽ các ô màu làm Icon thay thế file ảnh
        private void TaoIconTuDong()
        {
            ImageList smallList = new ImageList();
            smallList.ImageSize = new Size(16, 16);
            ImageList largeList = new ImageList();
            largeList.ImageSize = new Size(48, 48);

            Color[] colors = { Color.SteelBlue, Color.Orange, Color.SeaGreen, Color.MediumPurple };

            for (int i = 0; i < colors.Length; i++)
            {
                Bitmap bmpSmall = new Bitmap(16, 16);
                using (Graphics g = Graphics.FromImage(bmpSmall)) { g.Clear(colors[i]); }
                smallList.Images.Add(bmpSmall);

                Bitmap bmpLarge = new Bitmap(48, 48);
                using (Graphics g = Graphics.FromImage(bmpLarge)) { g.Clear(colors[i]); }
                largeList.Images.Add(bmpLarge);
            }

            // Gán ImageList cho các Control
            tvDepartments.ImageList = smallList;
            lsvEmployees.SmallImageList = smallList;
            lsvEmployees.LargeImageList = largeList;
        }
    }
}