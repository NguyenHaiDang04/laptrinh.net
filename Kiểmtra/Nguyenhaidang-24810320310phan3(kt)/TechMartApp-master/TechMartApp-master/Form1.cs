using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace TechMartApp
{
    public partial class Form1 : Form
    {
        // Sử dụng BindingList để DataGridView tự động cập nhật khi có thay đổi (TC03)
        private BindingList<Product> productList;
        private BindingSource bindingSource;

        public Form1()
        {
            InitializeComponent();

            // Tự động gán các sự kiện (Events)
            this.Load += Form1_Load;
            btnAdd.Click += BtnAdd_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnDelete.Click += BtnDelete_Click;
            btnChooseImage.Click += BtnChooseImage_Click;
            dgvProducts.SelectionChanged += DgvProducts_SelectionChanged;
            txtSearch.TextChanged += TxtSearch_TextChanged;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Khởi tạo danh sách và Binding
            productList = new BindingList<Product>();
            bindingSource = new BindingSource { DataSource = productList };
            dgvProducts.DataSource = bindingSource;

            // Cấu hình DataGridView tự định nghĩa cột
            dgvProducts.AutoGenerateColumns = false;
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ProductId", HeaderText = "Mã SP" });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ProductName", HeaderText = "Tên SP", Width = 150 });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Category", HeaderText = "Danh Mục" });

            var priceCol = new DataGridViewTextBoxColumn { DataPropertyName = "UnitPrice", HeaderText = "Đơn Giá" };
            priceCol.DefaultCellStyle.Format = "N0"; // Định dạng tiền tệ có dấu phẩy (TC03)
            dgvProducts.Columns.Add(priceCol);

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Quantity", HeaderText = "Số Lượng" });

            // Nạp dữ liệu cho ComboBox
            cboCategory.Items.AddRange(new string[] { "Điện thoại", "Laptop", "Phụ kiện" });
            if (cboCategory.Items.Count > 0) cboCategory.SelectedIndex = 0;

            UpdateStatus();
        }

        // Validate dữ liệu bằng ErrorProvider (TC02)
        private bool ValidateInput()
        {
            bool isValid = true;
            errorProvider1.Clear();

            if (string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                errorProvider1.SetError(txtProductName, "Tên sản phẩm không được để trống!");
                isValid = false;
            }

            if (!decimal.TryParse(txtUnitPrice.Text, out decimal price) || price <= 0)
            {
                errorProvider1.SetError(txtUnitPrice, "Đơn giá phải là số lớn hơn 0!");
                isValid = false;
            }

            if (!int.TryParse(txtQuantity.Text, out int qty) || qty < 0)
            {
                errorProvider1.SetError(txtQuantity, "Số lượng không hợp lệ!");
                isValid = false;
            }

            return isValid;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            var sp = new Product
            {
                ProductId = txtProductId.Text,
                ProductName = txtProductName.Text,
                Category = cboCategory.SelectedItem?.ToString(),
                UnitPrice = decimal.Parse(txtUnitPrice.Text),
                Quantity = int.Parse(txtQuantity.Text),
                ImagePath = picAvatar.ImageLocation
            };

            productList.Add(sp);
            UpdateStatus();
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (bindingSource.Current is Product sp && ValidateInput())
            {
                sp.ProductId = txtProductId.Text;
                sp.ProductName = txtProductName.Text;
                sp.Category = cboCategory.SelectedItem?.ToString();
                sp.UnitPrice = decimal.Parse(txtUnitPrice.Text);
                sp.Quantity = int.Parse(txtQuantity.Text);
                sp.ImagePath = picAvatar.ImageLocation;

                // Refresh lại Grid
                bindingSource.ResetBindings(false);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            // Hiển thị Dialog xác nhận Yes/No (TC05)
            if (bindingSource.Current != null)
            {
                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    bindingSource.RemoveCurrent();
                    UpdateStatus();
                    ClearInputs();
                }
            }
        }

        private void DgvProducts_SelectionChanged(object sender, EventArgs e)
        {
            // Bắn dữ liệu ngược lên Khung nhập liệu khi click vào dòng
            if (bindingSource.Current is Product sp)
            {
                txtProductId.Text = sp.ProductId;
                txtProductName.Text = sp.ProductName;
                cboCategory.SelectedItem = sp.Category;
                txtUnitPrice.Text = sp.UnitPrice.ToString("0");
                txtQuantity.Text = sp.Quantity.ToString();
                picAvatar.ImageLocation = sp.ImagePath;
            }
        }

        private void BtnChooseImage_Click(object sender, EventArgs e)
        {
            // Load ảnh chế độ Zoom mượt mà (TC04)
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    picAvatar.ImageLocation = ofd.FileName;
                }
            }
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            // Live Search (Tìm kiếm thời gian thực)
            string keyword = txtSearch.Text.ToLower();
            if (string.IsNullOrWhiteSpace(keyword))
            {
                bindingSource.DataSource = productList;
            }
            else
            {
                var filtered = productList.Where(p => p.ProductName.ToLower().Contains(keyword)).ToList();
                bindingSource.DataSource = new BindingList<Product>(filtered);
            }
        }

        private void UpdateStatus()
        {
            lblTotal.Text = $"Tổng số sản phẩm: {productList.Count}";
        }

        private void ClearInputs()
        {
            txtProductId.Clear();
            txtProductName.Clear();
            txtUnitPrice.Clear();
            txtQuantity.Clear();
            picAvatar.Image = null;
        }
    }
}