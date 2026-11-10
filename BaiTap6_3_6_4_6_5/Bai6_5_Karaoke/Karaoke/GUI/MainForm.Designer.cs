using System.Drawing;
using System.Windows.Forms;

namespace Karaoke.GUI
{
    partial class MainForm
    {
        private TabControl tabs;
        private Label lblMessage;
        private TabPage tabRooms;
        private FlowLayoutPanel roomGrid;
        private Label lblRoom;
        private Label lblClock;
        private ComboBox cboService;
        private NumericUpDown numOrder;
        private Button btnOrder;
        private DataGridView gridOrders;
        private Label lblEstimate;
        private Button btnOpen;
        private Button btnReload;
        private Button btnRemove;
        private Button btnPay;
        private TabPage tabRoomCatalog;
        private DataGridView gridRooms;
        private TextBox txtRoom;
        private ComboBox cboType;
        private NumericUpDown numRate;
        private Button btnNewRoom;
        private Button btnSaveRoom;
        private Button btnDeleteRoom;
        private TabPage tabServiceCatalog;
        private DataGridView gridServices;
        private TextBox txtService;
        private NumericUpDown numPrice;
        private NumericUpDown numStock;
        private Button btnNewService;
        private Button btnSaveService;
        private Button btnDeleteService;
        private TabPage tabReports;
        private Label lblReport;
        private Button btnReport;
        private DataGridView gridBills;

        private void InitializeComponent()
        {
            this.tabs = new TabControl();
            this.lblMessage = new Label();
            this.tabRooms = new TabPage();
            var label1 = new Label();
            this.roomGrid = new FlowLayoutPanel();
            this.lblRoom = new Label();
            this.lblClock = new Label();
            var label2 = new Label();
            this.cboService = new ComboBox();
            var label3 = new Label();
            this.numOrder = new NumericUpDown();
            this.btnOrder = new Button();
            this.gridOrders = new DataGridView();
            this.lblEstimate = new Label();
            this.btnOpen = new Button();
            this.btnReload = new Button();
            this.btnRemove = new Button();
            this.btnPay = new Button();
            var label4 = new Label();
            this.tabRoomCatalog = new TabPage();
            this.gridRooms = new DataGridView();
            var label5 = new Label();
            this.txtRoom = new TextBox();
            var label6 = new Label();
            this.cboType = new ComboBox();
            var label7 = new Label();
            this.numRate = new NumericUpDown();
            this.btnNewRoom = new Button();
            this.btnSaveRoom = new Button();
            this.btnDeleteRoom = new Button();
            this.tabServiceCatalog = new TabPage();
            this.gridServices = new DataGridView();
            var label8 = new Label();
            this.txtService = new TextBox();
            var label9 = new Label();
            this.numPrice = new NumericUpDown();
            var label10 = new Label();
            this.numStock = new NumericUpDown();
            this.btnNewService = new Button();
            this.btnSaveService = new Button();
            this.btnDeleteService = new Button();
            this.tabReports = new TabPage();
            this.lblReport = new Label();
            this.btnReport = new Button();
            this.gridBills = new DataGridView();
            this.SuspendLayout();

            this.tabs.Name = "tabs";
            this.tabs.Location = new Point(12, 12);
            this.tabs.Size = new Size(1032, 578);
            this.tabRooms.Name = "tabRooms";
            this.tabRooms.Text = "Sơ đồ phòng";
            this.tabRooms.UseVisualStyleBackColor = true;
            this.tabs.Controls.Add(this.tabRooms);
            label1.Name = "label1";
            label1.Location = new Point(12, 12);
            label1.Size = new Size(450, 23);
            label1.Text = "Chọn phòng   •   Xanh: Trống   •   Đỏ: Đang hát";
            this.tabRooms.Controls.Add(label1);

            this.roomGrid.Name = "roomGrid";
            this.roomGrid.Location = new Point(12, 42);
            this.roomGrid.Size = new Size(446, 304);
            this.roomGrid.AutoScroll = true;
            this.roomGrid.BorderStyle = BorderStyle.FixedSingle;
            this.tabRooms.Controls.Add(this.roomGrid);

            this.lblRoom.Name = "lblRoom";
            this.lblRoom.Location = new Point(475, 13);
            this.lblRoom.Size = new Size(535, 23);
            this.tabRooms.Controls.Add(this.lblRoom);

            this.lblClock.Name = "lblClock";
            this.lblClock.Location = new Point(475, 39);
            this.lblClock.Size = new Size(535, 26);
            this.tabRooms.Controls.Add(this.lblClock);

            label2.Name = "label2";
            label2.Location = new Point(475, 81);
            label2.Size = new Size(58, 23);
            label2.Text = "Dịch vụ";
            this.tabRooms.Controls.Add(label2);

            this.cboService.Name = "cboService";
            this.cboService.Location = new Point(537, 78);
            this.cboService.Size = new Size(212, 26);
            this.cboService.DropDownStyle = ComboBoxStyle.DropDownList;
            this.tabRooms.Controls.Add(this.cboService);

            label3.Name = "label3";
            label3.Location = new Point(762, 81);
            label3.Size = new Size(30, 23);
            label3.Text = "SL";
            this.tabRooms.Controls.Add(label3);

            this.numOrder.Name = "numOrder";
            this.numOrder.Location = new Point(792, 78);
            this.numOrder.Size = new Size(57, 26);
            this.numOrder.Minimum = 1;
            this.numOrder.Maximum = 1000000;
            this.numOrder.Value = 1;
            this.numOrder.DecimalPlaces = 0;
            this.numOrder.ThousandsSeparator = true;
            this.tabRooms.Controls.Add(this.numOrder);

            this.btnOrder.Name = "btnOrder";
            this.btnOrder.Location = new Point(861, 75);
            this.btnOrder.Size = new Size(149, 30);
            this.btnOrder.Text = "Gọi dịch vụ";
            this.btnOrder.UseVisualStyleBackColor = true;
            this.btnOrder.Click += this.OrderClick;
            this.tabRooms.Controls.Add(this.btnOrder);

            this.gridOrders.Name = "gridOrders";
            this.gridOrders.Location = new Point(475, 124);
            this.gridOrders.Size = new Size(535, 241);
            this.gridOrders.AllowUserToAddRows = false;
            this.gridOrders.AllowUserToDeleteRows = false;
            this.gridOrders.ReadOnly = true;
            this.gridOrders.MultiSelect = false;
            this.gridOrders.RowHeadersVisible = false;
            this.gridOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.gridOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.gridOrders.BackgroundColor = SystemColors.Window;
            this.tabRooms.Controls.Add(this.gridOrders);

            this.lblEstimate.Name = "lblEstimate";
            this.lblEstimate.Location = new Point(475, 378);
            this.lblEstimate.Size = new Size(535, 26);
            this.tabRooms.Controls.Add(this.lblEstimate);

            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Location = new Point(12, 374);
            this.btnOpen.Size = new Size(132, 30);
            this.btnOpen.Text = "Mở phòng";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += this.OpenClick;
            this.tabRooms.Controls.Add(this.btnOpen);

            this.btnReload.Name = "btnReload";
            this.btnReload.Location = new Point(154, 374);
            this.btnReload.Size = new Size(146, 30);
            this.btnReload.Text = "Tải lại sơ đồ";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += this.ReloadClick;
            this.tabRooms.Controls.Add(this.btnReload);

            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Location = new Point(475, 424);
            this.btnRemove.Size = new Size(158, 30);
            this.btnRemove.Text = "Bỏ dòng đã chọn";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += this.RemoveClick;
            this.tabRooms.Controls.Add(this.btnRemove);

            this.btnPay.Name = "btnPay";
            this.btnPay.Location = new Point(856, 424);
            this.btnPay.Size = new Size(154, 30);
            this.btnPay.Text = "Thanh toán";
            this.btnPay.UseVisualStyleBackColor = true;
            this.btnPay.Click += this.PayClick;
            this.tabRooms.Controls.Add(this.btnPay);

            label4.Name = "label4";
            label4.Location = new Point(12, 435);
            label4.Size = new Size(442, 23);
            label4.Text = "Tiền phòng tính theo thời gian sử dụng thực tế.";
            this.tabRooms.Controls.Add(label4);

            this.tabRoomCatalog.Name = "tabRoomCatalog";
            this.tabRoomCatalog.Text = "Danh mục phòng";
            this.tabRoomCatalog.UseVisualStyleBackColor = true;
            this.tabs.Controls.Add(this.tabRoomCatalog);
            this.gridRooms.Name = "gridRooms";
            this.gridRooms.Location = new Point(12, 13);
            this.gridRooms.Size = new Size(998, 310);
            this.gridRooms.AllowUserToAddRows = false;
            this.gridRooms.AllowUserToDeleteRows = false;
            this.gridRooms.ReadOnly = true;
            this.gridRooms.MultiSelect = false;
            this.gridRooms.RowHeadersVisible = false;
            this.gridRooms.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.gridRooms.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.gridRooms.BackgroundColor = SystemColors.Window;
            this.gridRooms.CurrentCellChanged += this.RoomRowSelected;
            this.tabRoomCatalog.Controls.Add(this.gridRooms);

            label5.Name = "label5";
            label5.Location = new Point(12, 345);
            label5.Size = new Size(83, 23);
            label5.Text = "Tên phòng";
            this.tabRoomCatalog.Controls.Add(label5);

            this.txtRoom.Name = "txtRoom";
            this.txtRoom.Location = new Point(99, 342);
            this.txtRoom.Size = new Size(291, 26);
            this.txtRoom.MaxLength = 50;
            this.tabRoomCatalog.Controls.Add(this.txtRoom);

            label6.Name = "label6";
            label6.Location = new Point(408, 345);
            label6.Size = new Size(83, 23);
            label6.Text = "Loại phòng";
            this.tabRoomCatalog.Controls.Add(label6);

            this.cboType.Name = "cboType";
            this.cboType.Location = new Point(497, 342);
            this.cboType.Size = new Size(163, 26);
            this.cboType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.tabRoomCatalog.Controls.Add(this.cboType);

            label7.Name = "label7";
            label7.Location = new Point(677, 345);
            label7.Size = new Size(91, 23);
            label7.Text = "Giá/giờ (đ)";
            this.tabRoomCatalog.Controls.Add(label7);

            this.numRate.Name = "numRate";
            this.numRate.Location = new Point(774, 342);
            this.numRate.Size = new Size(235, 26);
            this.numRate.Minimum = 0;
            this.numRate.Maximum = 999999999;
            this.numRate.Value = 0;
            this.numRate.DecimalPlaces = 2;
            this.numRate.ThousandsSeparator = true;
            this.tabRoomCatalog.Controls.Add(this.numRate);

            this.btnNewRoom.Name = "btnNewRoom";
            this.btnNewRoom.Location = new Point(12, 414);
            this.btnNewRoom.Size = new Size(122, 30);
            this.btnNewRoom.Text = "Phòng mới";
            this.btnNewRoom.UseVisualStyleBackColor = true;
            this.btnNewRoom.Click += this.NewRoomClick;
            this.tabRoomCatalog.Controls.Add(this.btnNewRoom);

            this.btnSaveRoom.Name = "btnSaveRoom";
            this.btnSaveRoom.Location = new Point(145, 414);
            this.btnSaveRoom.Size = new Size(122, 30);
            this.btnSaveRoom.Text = "Lưu phòng";
            this.btnSaveRoom.UseVisualStyleBackColor = true;
            this.btnSaveRoom.Click += this.SaveRoomClick;
            this.tabRoomCatalog.Controls.Add(this.btnSaveRoom);

            this.btnDeleteRoom.Name = "btnDeleteRoom";
            this.btnDeleteRoom.Location = new Point(278, 414);
            this.btnDeleteRoom.Size = new Size(125, 30);
            this.btnDeleteRoom.Text = "Xóa phòng";
            this.btnDeleteRoom.UseVisualStyleBackColor = true;
            this.btnDeleteRoom.Click += this.DeleteRoomClick;
            this.tabRoomCatalog.Controls.Add(this.btnDeleteRoom);

            this.tabServiceCatalog.Name = "tabServiceCatalog";
            this.tabServiceCatalog.Text = "Danh mục dịch vụ";
            this.tabServiceCatalog.UseVisualStyleBackColor = true;
            this.tabs.Controls.Add(this.tabServiceCatalog);
            this.gridServices.Name = "gridServices";
            this.gridServices.Location = new Point(12, 13);
            this.gridServices.Size = new Size(998, 310);
            this.gridServices.AllowUserToAddRows = false;
            this.gridServices.AllowUserToDeleteRows = false;
            this.gridServices.ReadOnly = true;
            this.gridServices.MultiSelect = false;
            this.gridServices.RowHeadersVisible = false;
            this.gridServices.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.gridServices.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.gridServices.BackgroundColor = SystemColors.Window;
            this.gridServices.CurrentCellChanged += this.ServiceRowSelected;
            this.tabServiceCatalog.Controls.Add(this.gridServices);

            label8.Name = "label8";
            label8.Location = new Point(12, 345);
            label8.Size = new Size(87, 23);
            label8.Text = "Tên dịch vụ";
            this.tabServiceCatalog.Controls.Add(label8);

            this.txtService.Name = "txtService";
            this.txtService.Location = new Point(102, 342);
            this.txtService.Size = new Size(290, 26);
            this.txtService.MaxLength = 100;
            this.tabServiceCatalog.Controls.Add(this.txtService);

            label9.Name = "label9";
            label9.Location = new Point(410, 345);
            label9.Size = new Size(85, 23);
            label9.Text = "Đơn giá (đ)";
            this.tabServiceCatalog.Controls.Add(label9);

            this.numPrice.Name = "numPrice";
            this.numPrice.Location = new Point(497, 342);
            this.numPrice.Size = new Size(163, 26);
            this.numPrice.Minimum = 0;
            this.numPrice.Maximum = 999999999;
            this.numPrice.Value = 0;
            this.numPrice.DecimalPlaces = 2;
            this.numPrice.ThousandsSeparator = true;
            this.tabServiceCatalog.Controls.Add(this.numPrice);

            label10.Name = "label10";
            label10.Location = new Point(677, 345);
            label10.Size = new Size(89, 23);
            label10.Text = "Tồn kho";
            this.tabServiceCatalog.Controls.Add(label10);

            this.numStock.Name = "numStock";
            this.numStock.Location = new Point(774, 342);
            this.numStock.Size = new Size(235, 26);
            this.numStock.Minimum = 0;
            this.numStock.Maximum = 1000000;
            this.numStock.Value = 0;
            this.numStock.DecimalPlaces = 0;
            this.numStock.ThousandsSeparator = true;
            this.tabServiceCatalog.Controls.Add(this.numStock);

            this.btnNewService.Name = "btnNewService";
            this.btnNewService.Location = new Point(12, 414);
            this.btnNewService.Size = new Size(122, 30);
            this.btnNewService.Text = "Dịch vụ mới";
            this.btnNewService.UseVisualStyleBackColor = true;
            this.btnNewService.Click += this.NewServiceClick;
            this.tabServiceCatalog.Controls.Add(this.btnNewService);

            this.btnSaveService.Name = "btnSaveService";
            this.btnSaveService.Location = new Point(145, 414);
            this.btnSaveService.Size = new Size(122, 30);
            this.btnSaveService.Text = "Lưu dịch vụ";
            this.btnSaveService.UseVisualStyleBackColor = true;
            this.btnSaveService.Click += this.SaveServiceClick;
            this.tabServiceCatalog.Controls.Add(this.btnSaveService);

            this.btnDeleteService.Name = "btnDeleteService";
            this.btnDeleteService.Location = new Point(278, 414);
            this.btnDeleteService.Size = new Size(125, 30);
            this.btnDeleteService.Text = "Xóa dịch vụ";
            this.btnDeleteService.UseVisualStyleBackColor = true;
            this.btnDeleteService.Click += this.DeleteServiceClick;
            this.tabServiceCatalog.Controls.Add(this.btnDeleteService);

            this.tabReports.Name = "tabReports";
            this.tabReports.Text = "Báo cáo hóa đơn";
            this.tabReports.UseVisualStyleBackColor = true;
            this.tabs.Controls.Add(this.tabReports);
            this.lblReport.Name = "lblReport";
            this.lblReport.Location = new Point(12, 13);
            this.lblReport.Size = new Size(852, 25);
            this.tabReports.Controls.Add(this.lblReport);

            this.btnReport.Name = "btnReport";
            this.btnReport.Location = new Point(897, 10);
            this.btnReport.Size = new Size(113, 30);
            this.btnReport.Text = "Tải lại";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += this.ReportClick;
            this.tabReports.Controls.Add(this.btnReport);

            this.gridBills.Name = "gridBills";
            this.gridBills.Location = new Point(12, 55);
            this.gridBills.Size = new Size(998, 452);
            this.gridBills.AllowUserToAddRows = false;
            this.gridBills.AllowUserToDeleteRows = false;
            this.gridBills.ReadOnly = true;
            this.gridBills.MultiSelect = false;
            this.gridBills.RowHeadersVisible = false;
            this.gridBills.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.gridBills.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.gridBills.BackgroundColor = SystemColors.Window;
            this.tabReports.Controls.Add(this.gridBills);

            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Location = new Point(15, 598);
            this.lblMessage.Size = new Size(1024, 38);
            this.lblMessage.BorderStyle = BorderStyle.FixedSingle;
            this.lblMessage.TextAlign = ContentAlignment.MiddleLeft;
            this.lblMessage.Padding = new Padding(8, 0, 8, 0);

            this.AutoScaleMode = AutoScaleMode.Font;
            this.Font = new Font("Segoe UI", 9F);
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.ClientSize = new Size(1056, 646);
            this.Controls.Add(this.tabs);
            this.Controls.Add(this.lblMessage);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Name = "MainForm";
            this.Text = "Bài 6.5 - Quản lý phòng hát và dịch vụ karaoke";
            this.Load += this.MainFormLoad;
            this.ResumeLayout(false);
        }
    }
}
