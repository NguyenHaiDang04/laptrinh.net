using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Bai4_6_QuanLySinhVien
{
    // 1. Khởi tạo đối tượng Student
    public class Student
    {
        public string StudentId { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string AvatarPath { get; set; }
    }

    public partial class Form1 : Form
    {
        // Danh sách sinh viên và bộ trung gian BindingSource
        private BindingList<Student> danhSachSV = new BindingList<Student>();
        private BindingSource bs = new BindingSource();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Thiết lập ComboBox
            cboGioiTinh.Items.AddRange(new string[] { "Nam", "Nữ" });
            cboGioiTinh.SelectedIndex = 0;

            // Kết nối BindingSource với List và DataGridView
            bs.DataSource = danhSachSV;
            dgvStudents.DataSource = bs;

            // Đổi tên cột cho DataGridView
            dgvStudents.Columns["StudentId"].HeaderText = "Mã SV";
            dgvStudents.Columns["FullName"].HeaderText = "Họ Tên";
            dgvStudents.Columns["Age"].HeaderText = "Tuổi";
            dgvStudents.Columns["Gender"].HeaderText = "Giới tính";
            dgvStudents.Columns["AvatarPath"].HeaderText = "Đường dẫn ảnh";

            // THIẾT LẬP DATA BINDING 2 CHIỀU (Điều kỳ diệu nằm ở đây)
            txtMaSV.DataBindings.Add("Text", bs, "StudentId", true, DataSourceUpdateMode.OnPropertyChanged);
            txtHoTen.DataBindings.Add("Text", bs, "FullName", true, DataSourceUpdateMode.OnPropertyChanged);
            txtTuoi.DataBindings.Add("Text", bs, "Age", true, DataSourceUpdateMode.OnPropertyChanged);
            cboGioiTinh.DataBindings.Add("Text", bs, "Gender", true, DataSourceUpdateMode.OnPropertyChanged);

            // Bắt sự kiện khi người dùng click sang dòng khác trên DataGridView
            bs.CurrentChanged += Bs_CurrentChanged;
        }

        // Tự động load ảnh lên PictureBox khi chọn 1 dòng
        private void Bs_CurrentChanged(object sender, EventArgs e)
        {
            if (bs.Current is Student sv)
            {
                // Kiểm tra xem đường dẫn ảnh có tồn tại thật trong máy không
                if (!string.IsNullOrEmpty(sv.AvatarPath) && File.Exists(sv.AvatarPath))
                {
                    picAvatar.Image = Image.FromFile(sv.AvatarPath);
                }
                else
                {
                    picAvatar.Image = null; // Trống nếu không có ảnh
                }
            }
        }

        // Chức năng: THÊM
        private void btnThem_Click(object sender, EventArgs e)
        {
            // Tạo 1 sinh viên trắng và đẩy vào BindingSource
            Student svMoi = new Student { StudentId = "SV", FullName = "Nguyễn Văn A", Age = 18, Gender = "Nam" };
            bs.Add(svMoi);

            // Di chuyển thanh sáng tới dòng vừa thêm
            bs.MoveLast();
            txtMaSV.Focus();
        }

        // Chức năng: CẬP NHẬT
        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            // Vì đã dùng Binding 2 chiều, khi bạn gõ trên TextBox thì dữ liệu đã tự cập nhật.
            // Hàm ResetCurrentItem() chỉ giúp DataGridView làm mới lại hiển thị cho chắc chắn.
            bs.ResetCurrentItem();
            MessageBox.Show("Đã cập nhật dữ liệu sinh viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Chức năng: XÓA
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (bs.Current != null)
            {
                DialogResult dr = MessageBox.Show("Xóa sinh viên này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    bs.RemoveCurrent();
                }
            }
        }

        // Chức năng: CHỌN ẢNH (OpenFileDialog)
        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            if (bs.Current == null)
            {
                MessageBox.Show("Vui lòng thêm hoặc chọn một sinh viên trước khi gắn ảnh!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
            ofd.Title = "Chọn ảnh đại diện";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                // Ép kiểu dòng hiện tại về đối tượng Student
                Student svHienTai = (Student)bs.Current;
                svHienTai.AvatarPath = ofd.FileName; // Lưu đường dẫn

                picAvatar.Image = Image.FromFile(ofd.FileName); // Hiển thị ảnh
                bs.ResetCurrentItem(); // Cập nhật lại Grid
            }
        }

        // Chức năng: XUẤT CSV (SaveFileDialog)
        private void btnXuatCSV_Click(object sender, EventArgs e)
        {
            if (danhSachSV.Count == 0)
            {
                MessageBox.Show("Danh sách trống, không có gì để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV Files (*.csv)|*.csv";
            sfd.Title = "Lưu danh sách sinh viên";
            sfd.FileName = "DanhSachSinhVien.csv";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Dùng StreamWriter với Encoding.UTF8 để hỗ trợ Tiếng Việt
                    using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8))
                    {
                        // Ghi dòng tiêu đề
                        sw.WriteLine("Mã SV,Họ Tên,Tuổi,Giới tính,Đường dẫn ảnh");

                        // Ghi từng dòng dữ liệu
                        foreach (Student sv in danhSachSV)
                        {
                            sw.WriteLine($"{sv.StudentId},{sv.FullName},{sv.Age},{sv.Gender},{sv.AvatarPath}");
                        }
                    }
                    MessageBox.Show("Xuất file CSV thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xuất file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}