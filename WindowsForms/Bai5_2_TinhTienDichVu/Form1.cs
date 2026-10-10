using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Bai5_2_TinhTienDichVu
{
    public partial class Form1 : Form
    {
        // Tạo một cấu trúc dữ liệu nhỏ để lưu Tên dịch vụ và Giá tiền
        public class DichVu
        {
            public string Ten { get; set; }
            public double Gia { get; set; }

            // Hiển thị chữ trong ListBox
            public override string ToString()
            {
                return $"{Ten} - {Gia:N0} VNĐ";
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Tải danh mục vào ComboBox
            cboCategory.Items.AddRange(new string[] { "Khám bệnh", "Xét nghiệm", "Chụp X-Quang", "Vắc-xin" });
            cboCategory.SelectedIndex = 0; // Mặc định chọn mục đầu tiên
        }

        // 1. Thay đổi ComboBox -> Đổi danh sách dịch vụ
        private void cboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstAvailableServices.Items.Clear();

            string loai = cboCategory.SelectedItem.ToString();
            switch (loai)
            {
                case "Khám bệnh":
                    lstAvailableServices.Items.Add(new DichVu { Ten = "Khám Nội tổng quát", Gia = 150000 });
                    lstAvailableServices.Items.Add(new DichVu { Ten = "Khám Răng Hàm Mặt", Gia = 100000 });
                    lstAvailableServices.Items.Add(new DichVu { Ten = "Khám Tai Mũi Họng", Gia = 120000 });
                    break;
                case "Xét nghiệm":
                    lstAvailableServices.Items.Add(new DichVu { Ten = "Xét nghiệm Máu cơ bản", Gia = 250000 });
                    lstAvailableServices.Items.Add(new DichVu { Ten = "Xét nghiệm Nước tiểu", Gia = 80000 });
                    break;
                case "Chụp X-Quang":
                    lstAvailableServices.Items.Add(new DichVu { Ten = "Chụp X-Quang Phổi", Gia = 200000 });
                    lstAvailableServices.Items.Add(new DichVu { Ten = "Chụp X-Quang Xương", Gia = 250000 });
                    break;
                case "Vắc-xin":
                    lstAvailableServices.Items.Add(new DichVu { Ten = "Tiêm Vắc-xin Cúm", Gia = 350000 });
                    lstAvailableServices.Items.Add(new DichVu { Ten = "Tiêm Vắc-xin Viêm gan B", Gia = 400000 });
                    break;
            }
        }

        // 2. Chuyển dịch vụ sang danh sách đã chọn (Nút >)
        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (lstAvailableServices.SelectedItem != null)
            {
                DichVu dv = (DichVu)lstAvailableServices.SelectedItem;

                // Không cho phép thêm trùng dịch vụ
                if (!lstSelectedServices.Items.Contains(dv))
                {
                    lstSelectedServices.Items.Add(dv);
                    TinhTien();
                }
            }
        }

        // Hỗ trợ Double Click để chọn nhanh
        private void lstAvailableServices_DoubleClick(object sender, EventArgs e)
        {
            btnSelect_Click(sender, e);
        }

        // 3. Xóa dịch vụ khỏi danh sách đã chọn (Nút <)
        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstSelectedServices.SelectedItem != null)
            {
                lstSelectedServices.Items.Remove(lstSelectedServices.SelectedItem);
                TinhTien();
            }
        }

        // Hỗ trợ Double Click để xóa nhanh
        private void lstSelectedServices_DoubleClick(object sender, EventArgs e)
        {
            btnRemove_Click(sender, e);
        }

        // 4. Nút Xóa tất cả (Nút <<)
        private void btnClearAll_Click(object sender, EventArgs e)
        {
            lstSelectedServices.Items.Clear();
            TinhTien();
        }

        // 5. Hàm tính toán tiền bạc
        private void TinhTien()
        {
            double tongTien = 0;

            // Cộng dồn giá của các dịch vụ trong list bên phải
            foreach (DichVu dv in lstSelectedServices.Items)
            {
                tongTien += dv.Gia;
            }

            // Lấy % chiết khấu
            double chietKhau = (double)nudChietKhau.Value;
            double tienGiam = tongTien * (chietKhau / 100);
            double thanhTien = tongTien - tienGiam;

            lblTongTien.Text = tongTien.ToString("N0") + " đ";
            lblThanhTien.Text = thanhTien.ToString("N0") + " đ";
        }

        // Tính lại tiền khi người dùng thay đổi số % chiết khấu
        private void nudChietKhau_ValueChanged(object sender, EventArgs e)
        {
            TinhTien();
        }
    }
}