using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Karaoke.BUS;
using Karaoke.DTO;
using static Karaoke.GUI.FormSupport;

namespace Karaoke.GUI
{
    internal partial class MainForm : Form
    {
        private readonly Service service = new Service();
        private readonly Timer timer = new Timer { Interval = 1000 };
        private readonly Stopwatch clock = new Stopwatch();
        private bool loading = true;
        private int selectedRoom, invoiceId, roomId, serviceId;
        private DateTime startedAt, serverAtLoad;
        private decimal hourlyRate, serviceTotal;

        public MainForm()
        {
            InitializeComponent();
            timer.Tick += (sender, e) => DisplayClock();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            timer.Dispose();
            base.OnFormClosed(e);
        }

        private void MainFormLoad(object sender, EventArgs e) => Run(lblMessage, () =>
        {
            cboType.Items.AddRange(new[] { "Thường", "VIP" });
            LoadRooms();
            LoadServices();
            LoadReport();
            timer.Start();
            Notice(lblMessage, "Chọn một phòng trên sơ đồ để mở phòng hoặc gọi dịch vụ.");
        });

        private void LoadRooms()
        {
            DataTable rooms = service.Phong();
            loading = true;
            roomGrid.SuspendLayout();
            try
            {
                Grid(gridRooms, rooms, "Mã", "Tên phòng", "Loại", "Giá/giờ (đ)", "Trạng thái");
                ClearRoom();
                while (roomGrid.Controls.Count > 0)
                    roomGrid.Controls[0].Dispose();

                foreach (DataRow room in rooms.Rows)
                {
                    int id = (int)room["MaPhong"];
                    bool occupied = (string)room["TrangThai"] == "Đang hát";
                    var button = new Button
                    {
                        Name = "room" + id, Width = 131, Height = 79, Margin = new Padding(5),
                        Tag = id, Text = room["TenPhong"] + Environment.NewLine + room["TrangThai"],
                        BackColor = occupied ? Color.IndianRed : Color.LightGreen,
                        ForeColor = Color.Black, UseVisualStyleBackColor = false
                    };
                    button.Click += RoomButtonClick;
                    roomGrid.Controls.Add(button);
                }

                if (!rooms.Rows.Cast<DataRow>().Any(r => (int)r["MaPhong"] == selectedRoom))
                    selectedRoom = rooms.Rows.Count == 0 ? 0 : (int)rooms.Rows[0]["MaPhong"];
            }
            finally
            {
                roomGrid.ResumeLayout();
                loading = false;
            }
            LoadSelectedRoom();
        }

        private void LoadServices()
        {
            int selected = Id(cboService);
            loading = true;
            try
            {
                DataTable items = service.DichVu();
                Grid(gridServices, items, "Mã", "Dịch vụ", "Đơn giá (đ)", "Tồn kho");
                Bind(cboService, items.Copy(), "MaDV", "TenDV");
                if (items.Rows.Cast<DataRow>().Any(r => (int)r["MaDV"] == selected))
                    cboService.SelectedValue = selected;
                ClearService();
            }
            finally { loading = false; }
        }

        private void LoadSelectedRoom()
        {
            clock.Reset();
            invoiceId = 0;
            btnOpen.Enabled = btnPay.Enabled = btnOrder.Enabled = btnRemove.Enabled = false;
            foreach (Button button in roomGrid.Controls)
                button.FlatStyle = (int)button.Tag == selectedRoom ? FlatStyle.Popup : FlatStyle.Standard;

            DataTable session = service.PhienMo(selectedRoom);
            DataRow room = session.Rows.Count == 0 ? null : session.Rows[0];
            if (room == null)
            {
                lblRoom.Text = "Chưa có phòng được chọn.";
            }
            else
            {
                hourlyRate = (decimal)room["GiaTheoGio"];
                lblRoom.Text = string.Format("{0}  |  {1}  |  {2:N2} đ/giờ", room["TenPhong"], room["TrangThai"], hourlyRate);
                btnOpen.Enabled = (string)room["TrangThai"] == "Trống";
                if (room["MaHD"] is int)
                {
                    invoiceId = (int)room["MaHD"];
                    startedAt = (DateTime)room["GioVao"];
                    serverAtLoad = (DateTime)room["HienTai"];
                    clock.Start();
                    btnPay.Enabled = btnOrder.Enabled = btnRemove.Enabled = true;
                }
            }

            DataTable items = service.ChiTiet(invoiceId);
            Grid(gridOrders, items, "Mã", "Dịch vụ", "SL", "Đơn giá (đ)", "Thành tiền (đ)");
            gridOrders.Columns["MaDV"].Visible = false;
            serviceTotal = items.Rows.Cast<DataRow>().Sum(r => (decimal)r["ThanhTien"]);
            DisplayClock();
        }

        private void DisplayClock()
        {
            if (invoiceId == 0)
            {
                lblClock.Text = "Chưa có phiên hát.";
                lblEstimate.Text = "Tạm tính: 0 đ";
                return;
            }
            DateTime current = serverAtLoad.Add(clock.Elapsed);
            TimeSpan elapsed = current < startedAt ? TimeSpan.Zero : current - startedAt;
            lblClock.Text = string.Format("Hóa đơn #{0}  |  Vào {1:HH:mm:ss}  |  Đã hát {2:00}:{3:00}:{4:00}",
                invoiceId, startedAt, (int)elapsed.TotalHours, elapsed.Minutes, elapsed.Seconds);
            decimal roomCharge = Service.TinhTienPhong(startedAt, current, hourlyRate);
            lblEstimate.Text = string.Format("Phòng: {0:N2} đ    Dịch vụ: {1:N2} đ    Tổng: {2:N2} đ",
                roomCharge, serviceTotal, roomCharge + serviceTotal);
        }

        private void LoadReport()
        {
            DataTable bills = service.HoaDon();
            Grid(gridBills, bills, "Mã HĐ", "Tên phòng", "Giờ vào", "Giờ ra", "Tiền phòng (đ)", "Dịch vụ (đ)", "Tổng tiền (đ)");
            lblReport.Text = string.Format("Hóa đơn đã thanh toán: {0}     Tổng doanh thu: {1:N2} đ",
                bills.Rows.Count, bills.Rows.Cast<DataRow>().Sum(r => (decimal)r["TongTien"]));
        }

        private void RoomButtonClick(object sender, EventArgs e) => Run(lblMessage, () =>
        {
            selectedRoom = (int)((Button)sender).Tag;
            LoadSelectedRoom();
        });

        private void ReloadClick(object sender, EventArgs e) => Run(lblMessage, () =>
        {
            LoadRooms();
            LoadServices();
            LoadReport();
        });

        private void OpenClick(object sender, EventArgs e) => Run(lblMessage, () =>
        {
            int id = service.MoPhong(selectedRoom);
            LoadRooms();
            Notice(lblMessage, "Đã mở phòng, hóa đơn #" + id + ".");
        });

        private void OrderClick(object sender, EventArgs e) => Run(lblMessage, () =>
        {
            service.GoiDichVu(selectedRoom, invoiceId, Id(cboService), (int)numOrder.Value);
            LoadSelectedRoom();
            Notice(lblMessage, "Đã gọi dịch vụ. Kho sẽ được trừ khi thanh toán.");
        });

        private void RemoveClick(object sender, EventArgs e) => Run(lblMessage, () =>
        {
            DataRow row = Row(gridOrders);
            if (row == null) throw new ArgumentException("Chọn một dòng dịch vụ cần bỏ.");
            service.BoDichVu(selectedRoom, invoiceId, (int)row["MaDV"]);
            LoadSelectedRoom();
            Notice(lblMessage, "Đã bỏ dòng dịch vụ.");
        });

        private void PayClick(object sender, EventArgs e) => Run(lblMessage, () =>
        {
            HoaDon bill = service.ThanhToan(selectedRoom, invoiceId);
            LoadRooms();
            LoadServices();
            LoadReport();
            Notice(lblMessage, string.Format("Đã thanh toán hóa đơn #{0}: {1:N2} đ. Phòng đã trống.",
                bill.MaHD, bill.TongTien));
        });

        private void ReportClick(object sender, EventArgs e) => Run(lblMessage, LoadReport);
        private void NewRoomClick(object sender, EventArgs e) => Run(lblMessage, ClearRoom);
        private void NewServiceClick(object sender, EventArgs e) => Run(lblMessage, ClearService);

        private void ClearRoom()
        {
            gridRooms.CurrentCell = null;
            roomId = 0;
            txtRoom.Clear();
            cboType.SelectedIndex = 0;
            numRate.Value = 0;
        }

        private void ClearService()
        {
            gridServices.CurrentCell = null;
            serviceId = 0;
            txtService.Clear();
            numPrice.Value = numStock.Value = 0;
        }

        private void RoomRowSelected(object sender, EventArgs e)
        {
            if (loading) return;
            DataRow row = Row(gridRooms);
            if (row == null) return;
            roomId = (int)row["MaPhong"];
            txtRoom.Text = (string)row["TenPhong"];
            cboType.SelectedItem = row["LoaiPhong"];
            numRate.Value = (decimal)row["GiaTheoGio"];
        }

        private void ServiceRowSelected(object sender, EventArgs e)
        {
            if (loading) return;
            DataRow row = Row(gridServices);
            if (row == null) return;
            serviceId = (int)row["MaDV"];
            txtService.Text = (string)row["TenDV"];
            numPrice.Value = (decimal)row["DonGia"];
            numStock.Value = (int)row["TonKho"];
        }

        private void SaveRoomClick(object sender, EventArgs e) => Run(lblMessage, () =>
        {
            service.LuuPhong(new PhongHat
            {
                MaPhong = roomId, TenPhong = txtRoom.Text, LoaiPhong = cboType.Text, GiaTheoGio = numRate.Value
            });
            LoadRooms();
            Notice(lblMessage, "Đã lưu phòng.");
        });

        private void DeleteRoomClick(object sender, EventArgs e) => Run(lblMessage, () =>
        {
            if (roomId == 0) throw new ArgumentException("Chọn phòng cần xóa.");
            service.XoaPhong(roomId);
            LoadRooms();
            Notice(lblMessage, "Đã xóa phòng.");
        });

        private void SaveServiceClick(object sender, EventArgs e) => Run(lblMessage, () =>
        {
            service.LuuDichVu(new DichVu
            {
                MaDV = serviceId, TenDV = txtService.Text, DonGia = numPrice.Value, TonKho = (int)numStock.Value
            });
            LoadServices();
            Notice(lblMessage, "Đã lưu dịch vụ.");
        });

        private void DeleteServiceClick(object sender, EventArgs e) => Run(lblMessage, () =>
        {
            if (serviceId == 0) throw new ArgumentException("Chọn dịch vụ cần xóa.");
            service.XoaDichVu(serviceId);
            LoadServices();
            Notice(lblMessage, "Đã xóa dịch vụ.");
        });
    }
}
