using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Garage.BUS;
using Garage.DTO;
using static Garage.GUI.FormSupport;

namespace Garage.GUI
{
    internal partial class MainForm : Form
    {
        private readonly Service service = new Service();
        private readonly List<DongSuaChua> draft = new List<DongSuaChua>();
        private bool loading = true;
        private int ownerId, partId;
        private string editingPlate;
        private DataTable parts;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainFormLoad(object sender, EventArgs e) => Run(lblMessage, () =>
        {
            LoadVehicles();
            LoadParts();
            LoadBills();
            Notice(lblMessage, "Tra cứu biển số hoặc nhập thông tin để tiếp nhận xe.");
        });

        private void LoadVehicles()
        {
            loading = true;
            try
            {
                DataTable owners = service.KhachHang();
                Grid(gridOwners, owners, "Mã", "Chủ xe", "Điện thoại", "Địa chỉ");
                Bind(cboOwner, owners.Copy(), "MaKH", "TenKH");
                DataTable cars = service.Xe();
                ShowCars(cars);
                Bind(cboCar, cars.Copy(), "BienSo", "BienSo");
                ClearOwner();
                ClearCar();
            }
            finally { loading = false; }
        }

        private void ShowCars(DataTable cars)
        {
            Grid(gridCars, cars, "Biển số", "Mã chủ", "Chủ xe", "Hiệu xe", "Năm");
            gridCars.Columns["MaKH"].Visible = false;
        }

        private void LoadParts()
        {
            loading = true;
            try
            {
                parts = service.PhuTung();
                Grid(gridParts, parts, "Mã", "Phụ tùng", "Đơn giá (đ)", "Tồn kho");
                Bind(cboPart, parts.Copy(), "MaPhuTung", "TenPhuTung");
                ClearPart();
                RefreshDraft();
            }
            finally { loading = false; }
        }

        private void LoadBills(int selected = 0)
        {
            loading = true;
            try
            {
                DataTable bills = service.Phieu();
                Grid(gridBills, bills, "Mã phiếu", "Biển số", "Chủ xe", "Ngày nhận",
                    "Nội dung", "Tổng tiền (đ)", "Trạng thái");
                gridBills.Columns["MaPhieu"].FillWeight = 50;
                gridBills.Columns["NoiDungSua"].FillWeight = 185;
                if (selected > 0)
                {
                    foreach (DataGridViewRow row in gridBills.Rows)
                    {
                        if ((int)row.Cells["MaPhieu"].Value != selected) continue;
                        gridBills.CurrentCell = row.Cells["MaPhieu"];
                        break;
                    }
                }
                var paid = bills.Rows.Cast<DataRow>().Where(r => (string)r["TrangThai"] == "Đã thanh toán");
                lblBillSummary.Text = string.Format("Số phiếu: {0}    Đã thanh toán: {1}    Doanh thu: {2:N2} đ",
                    bills.Rows.Count, paid.Count(), paid.Sum(r => (decimal)r["TongTien"]));
            }
            finally { loading = false; }
            LoadBillLines();
        }

        private void LoadBillLines()
        {
            DataRow row = Row(gridBills);
            int id = row == null ? 0 : (int)row["MaPhieu"];
            Grid(gridBillLines, service.ChiTiet(id), "Phụ tùng", "Số lượng", "Đơn giá (đ)", "Tiền công (đ)", "Thành tiền (đ)");
            btnPay.Enabled = row != null && (string)row["TrangThai"] == "Đã hoàn thành";
            btnExport.Enabled = row != null && (string)row["TrangThai"] == "Đã thanh toán";
        }

        private void OwnerSelected(object sender, EventArgs e)
        {
            if (loading) return;
            DataRow row = Row(gridOwners);
            if (row == null) return;
            ownerId = (int)row["MaKH"];
            txtOwner.Text = (string)row["TenKH"];
            txtPhone.Text = (string)row["DienThoai"];
            txtAddress.Text = Convert.ToString(row["DiaChi"]);
            cboOwner.SelectedValue = ownerId;
        }

        private void CarSelected(object sender, EventArgs e)
        {
            if (!loading) FillCar(Row(gridCars));
        }

        private void FillCar(DataRow row)
        {
            if (row == null) return;
            editingPlate = (string)row["BienSo"];
            txtPlate.Text = editingPlate;
            txtPlate.ReadOnly = true;
            txtBrand.Text = (string)row["HieuXe"];
            numYear.Value = (int)row["NamSanXuat"];
            cboOwner.SelectedValue = row["MaKH"];
        }

        private void PartSelected(object sender, EventArgs e)
        {
            if (loading) return;
            DataRow row = Row(gridParts);
            if (row == null) return;
            partId = (int)row["MaPhuTung"];
            txtPart.Text = (string)row["TenPhuTung"];
            numPrice.Value = (decimal)row["DonGia"];
            numStock.Value = (int)row["SoLuongTon"];
        }

        private void BillSelected(object sender, EventArgs e)
        {
            if (!loading) Run(lblMessage, LoadBillLines);
        }

        private void ClearOwner()
        {
            gridOwners.CurrentCell = null;
            ownerId = 0;
            txtOwner.Clear();
            txtPhone.Clear();
            txtAddress.Clear();
        }

        private void ClearCar()
        {
            gridCars.CurrentCell = null;
            editingPlate = null;
            txtPlate.Clear();
            txtPlate.ReadOnly = false;
            txtBrand.Clear();
            numYear.Value = DateTime.Today.Year;
        }

        private void ClearPart()
        {
            gridParts.CurrentCell = null;
            partId = 0;
            txtPart.Clear();
            numPrice.Value = numStock.Value = 0;
        }

        private KhachHang ReadOwner() => new KhachHang
        {
            MaKH = ownerId, TenKH = txtOwner.Text, DienThoai = txtPhone.Text, DiaChi = txtAddress.Text
        };

        private Xe ReadCar() => new Xe
        {
            BienSo = txtPlate.Text, MaKH = Id(cboOwner), HieuXe = txtBrand.Text, NamSanXuat = (int)numYear.Value
        };

        private void NewOwnerClick(object sender, EventArgs e) => Run(lblMessage, ClearOwner);
        private void NewCarClick(object sender, EventArgs e) => Run(lblMessage, ClearCar);
        private void NewPartClick(object sender, EventArgs e) => Run(lblMessage, ClearPart);
        private void AllCarsClick(object sender, EventArgs e) => Run(lblMessage, LoadVehicles);
        private void RefreshBillsClick(object sender, EventArgs e) => Run(lblMessage, () => LoadBills());

        private void LookupClick(object sender, EventArgs e) => Run(lblMessage, () =>
        {
            DataTable result = service.TraXe(txtLookup.Text);
            loading = true;
            try
            {
                ShowCars(result);
                ClearCar();
                if (result.Rows.Count > 0)
                {
                    gridCars.CurrentCell = gridCars.Rows[0].Cells["BienSo"];
                    FillCar(result.Rows[0]);
                }
                else
                    txtPlate.Text = txtLookup.Text.Trim().ToUpperInvariant();
            }
            finally { loading = false; }
            Notice(lblMessage, result.Rows.Count == 0
                ? "Không tìm thấy biển số. Nếu là xe mới, nhập thông tin để tiếp nhận."
                : "Đã tìm thấy xe và thông tin chủ xe.");
        });

        private void SaveOwnerClick(object sender, EventArgs e) => Run(lblMessage, () =>
        {
            service.LuuKhach(ReadOwner());
            LoadVehicles();
            Notice(lblMessage, "Đã lưu chủ xe.");
        });

        private void DeleteOwnerClick(object sender, EventArgs e) => Run(lblMessage, () =>
        {
            service.XoaKhach(ownerId);
            LoadVehicles();
            Notice(lblMessage, "Đã xóa chủ xe.");
        });

        private void SaveCarClick(object sender, EventArgs e) => Run(lblMessage, () =>
        {
            service.LuuXe(ReadCar(), editingPlate == null);
            LoadVehicles();
            Notice(lblMessage, "Đã lưu xe.");
        });

        private void DeleteCarClick(object sender, EventArgs e) => Run(lblMessage, () =>
        {
            service.XoaXe(editingPlate);
            LoadVehicles();
            Notice(lblMessage, "Đã xóa xe.");
        });

        private void ReceiveClick(object sender, EventArgs e) => Run(lblMessage, () =>
        {
            if (editingPlate != null || ownerId != 0)
                throw new ArgumentException("Chọn Chủ xe mới và Xe mới trước khi tiếp nhận cả hai.");
            service.TiepNhanMoi(ReadOwner(), ReadCar());
            LoadVehicles();
            Notice(lblMessage, "Đã tiếp nhận chủ xe và xe mới trong cùng giao dịch.");
        });

        private void SavePartClick(object sender, EventArgs e) => Run(lblMessage, () =>
        {
            service.LuuPhuTung(new PhuTung
            {
                MaPhuTung = partId, TenPhuTung = txtPart.Text,
                DonGia = numPrice.Value, SoLuongTon = (int)numStock.Value
            });
            LoadParts();
            Notice(lblMessage, "Đã lưu phụ tùng.");
        });

        private void DeletePartClick(object sender, EventArgs e) => Run(lblMessage, () =>
        {
            service.XoaPhuTung(partId);
            LoadParts();
            Notice(lblMessage, "Đã xóa phụ tùng.");
        });

        private void RefreshDraft()
        {
            var table = new DataTable();
            table.Columns.Add("MaPhuTung", typeof(int));
            table.Columns.Add("TenPhuTung", typeof(string));
            table.Columns.Add("SoLuong", typeof(int));
            table.Columns.Add("DonGia", typeof(decimal));
            table.Columns.Add("TienCong", typeof(decimal));
            table.Columns.Add("ThanhTien", typeof(decimal));
            decimal estimate = 0;
            foreach (var line in draft)
            {
                var part = parts.Rows.Cast<DataRow>().FirstOrDefault(r => (int)r["MaPhuTung"] == line.MaPhuTung);
                decimal price = part == null ? 0 : (decimal)part["DonGia"];
                decimal amount = price * line.SoLuong + line.TienCong;
                table.Rows.Add(line.MaPhuTung, line.TenPhuTung, line.SoLuong, price, line.TienCong, amount);
                estimate += amount;
            }
            Grid(gridDraft, table, "Mã", "Phụ tùng", "Số lượng", "Đơn giá (đ)", "Tiền công (đ)", "Thành tiền (đ)");
            lblEstimate.Text = string.Format("Tạm tính: {0:N2} đ", estimate);
        }

        private void AddLineClick(object sender, EventArgs e) => Run(lblMessage, () =>
        {
            int id = Id(cboPart);
            if (id <= 0) throw new ArgumentException("Chọn phụ tùng trước.");
            int qty = (int)numQty.Value;
            var line = draft.FirstOrDefault(d => d.MaPhuTung == id);
            if (line == null)
                draft.Add(new DongSuaChua { MaPhuTung = id, TenPhuTung = cboPart.Text, SoLuong = qty, TienCong = numLabor.Value });
            else
            {
                line.SoLuong = checked(line.SoLuong + qty);
                line.TienCong += numLabor.Value;
            }
            RefreshDraft();
            Notice(lblMessage, "Đã thêm dòng vào phiếu. Kho sẽ được trừ khi hoàn tất.");
        });

        private void RemoveLineClick(object sender, EventArgs e) => Run(lblMessage, () =>
        {
            DataRow row = Row(gridDraft);
            if (row == null) throw new ArgumentException("Chọn dòng cần bỏ.");
            draft.RemoveAll(d => d.MaPhuTung == (int)row["MaPhuTung"]);
            RefreshDraft();
        });

        private void FinishClick(object sender, EventArgs e) => Run(lblMessage, () =>
        {
            int id = service.LapPhieu(Convert.ToString(cboCar.SelectedValue), txtWork.Text, draft);
            draft.Clear();
            txtWork.Clear();
            LoadParts();
            LoadBills(id);
            tabs.SelectedTab = tabBills;
            Notice(lblMessage, "Đã lưu phiếu #" + id + " và trừ kho.");
        });

        private void PayClick(object sender, EventArgs e) => Run(lblMessage, () =>
        {
            DataRow row = Row(gridBills);
            if (row == null) throw new ArgumentException("Chọn phiếu cần thanh toán.");
            int id = (int)row["MaPhieu"];
            service.ThanhToan(id);
            LoadBills(id);
            Notice(lblMessage, "Đã thanh toán phiếu #" + id + ".");
        });

        private void ExportClick(object sender, EventArgs e) => Run(lblMessage, () =>
        {
            DataRow row = Row(gridBills);
            if (row == null) throw new ArgumentException("Chọn phiếu cần xuất.");
            using (var dialog = new SaveFileDialog())
            {
                dialog.Filter = "Tệp văn bản (*.txt)|*.txt";
                dialog.FileName = "HoaDon_" + row["MaPhieu"] + ".txt";
                if (dialog.ShowDialog(this) != DialogResult.OK) return;
                try
                {
                    service.XuatHoaDon(row, dialog.FileName);
                    Notice(lblMessage, "Đã xuất hóa đơn.");
                }
                catch (IOException ex)
                {
                    Notice(lblMessage, "Không ghi được tệp: " + ex.Message, true);
                }
                catch (UnauthorizedAccessException)
                {
                    Notice(lblMessage, "Không có quyền ghi tại thư mục đã chọn.", true);
                }
            }
        });
    }
}
