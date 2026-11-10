using System.Drawing;
using System.Windows.Forms;

namespace Garage.GUI
{
    partial class MainForm
    {
        private TabControl tabs;
        private Label lblMessage;
        private TabPage tabReceive;
        private TextBox txtLookup;
        private Button btnLookup;
        private Button btnAllCars;
        private DataGridView gridOwners;
        private DataGridView gridCars;
        private TextBox txtOwner;
        private TextBox txtPhone;
        private TextBox txtAddress;
        private TextBox txtPlate;
        private NumericUpDown numYear;
        private TextBox txtBrand;
        private ComboBox cboOwner;
        private Button btnNewOwner;
        private Button btnSaveOwner;
        private Button btnDeleteOwner;
        private Button btnNewCar;
        private Button btnSaveCar;
        private Button btnDeleteCar;
        private Button btnReceive;
        private TabPage tabParts;
        private DataGridView gridParts;
        private TextBox txtPart;
        private NumericUpDown numPrice;
        private NumericUpDown numStock;
        private Button btnNewPart;
        private Button btnSavePart;
        private Button btnDeletePart;
        private TabPage tabRepair;
        private ComboBox cboCar;
        private TextBox txtWork;
        private ComboBox cboPart;
        private NumericUpDown numQty;
        private NumericUpDown numLabor;
        private Button btnAddLine;
        private DataGridView gridDraft;
        private Label lblEstimate;
        private Button btnRemoveLine;
        private Button btnFinish;
        private TabPage tabBills;
        private Label lblBillSummary;
        private Button btnRefreshBills;
        private DataGridView gridBills;
        private DataGridView gridBillLines;
        private Button btnPay;
        private Button btnExport;

        private void InitializeComponent()
        {
            this.tabs = new TabControl();
            this.lblMessage = new Label();
            this.tabReceive = new TabPage();
            var label1 = new Label();
            this.txtLookup = new TextBox();
            this.btnLookup = new Button();
            this.btnAllCars = new Button();
            var label2 = new Label();
            var label3 = new Label();
            this.gridOwners = new DataGridView();
            this.gridCars = new DataGridView();
            var label4 = new Label();
            this.txtOwner = new TextBox();
            var label5 = new Label();
            this.txtPhone = new TextBox();
            var label6 = new Label();
            this.txtAddress = new TextBox();
            var label7 = new Label();
            this.txtPlate = new TextBox();
            var label8 = new Label();
            this.numYear = new NumericUpDown();
            var label9 = new Label();
            this.txtBrand = new TextBox();
            var label10 = new Label();
            this.cboOwner = new ComboBox();
            this.btnNewOwner = new Button();
            this.btnSaveOwner = new Button();
            this.btnDeleteOwner = new Button();
            this.btnNewCar = new Button();
            this.btnSaveCar = new Button();
            this.btnDeleteCar = new Button();
            this.btnReceive = new Button();
            this.tabParts = new TabPage();
            this.gridParts = new DataGridView();
            var label11 = new Label();
            this.txtPart = new TextBox();
            var label12 = new Label();
            this.numPrice = new NumericUpDown();
            var label13 = new Label();
            this.numStock = new NumericUpDown();
            this.btnNewPart = new Button();
            this.btnSavePart = new Button();
            this.btnDeletePart = new Button();
            this.tabRepair = new TabPage();
            var label14 = new Label();
            this.cboCar = new ComboBox();
            var label15 = new Label();
            this.txtWork = new TextBox();
            var label16 = new Label();
            this.cboPart = new ComboBox();
            var label17 = new Label();
            this.numQty = new NumericUpDown();
            var label18 = new Label();
            this.numLabor = new NumericUpDown();
            this.btnAddLine = new Button();
            this.gridDraft = new DataGridView();
            this.lblEstimate = new Label();
            this.btnRemoveLine = new Button();
            this.btnFinish = new Button();
            this.tabBills = new TabPage();
            this.lblBillSummary = new Label();
            this.btnRefreshBills = new Button();
            this.gridBills = new DataGridView();
            var label19 = new Label();
            this.gridBillLines = new DataGridView();
            this.btnPay = new Button();
            this.btnExport = new Button();
            this.SuspendLayout();

            this.tabs.Name = "tabs";
            this.tabs.Location = new Point(12, 12);
            this.tabs.Size = new Size(1032, 578);
            this.tabReceive.Name = "tabReceive";
            this.tabReceive.Text = "Tiếp nhận xe";
            this.tabReceive.UseVisualStyleBackColor = true;
            this.tabs.Controls.Add(this.tabReceive);
            label1.Name = "label1";
            label1.Location = new Point(12, 15);
            label1.Size = new Size(75, 23);
            label1.Text = "Biển số";
            this.tabReceive.Controls.Add(label1);

            this.txtLookup.Name = "txtLookup";
            this.txtLookup.Location = new Point(90, 12);
            this.txtLookup.Size = new Size(220, 26);
            this.txtLookup.MaxLength = 20;
            this.tabReceive.Controls.Add(this.txtLookup);

            this.btnLookup.Name = "btnLookup";
            this.btnLookup.Location = new Point(321, 10);
            this.btnLookup.Size = new Size(95, 30);
            this.btnLookup.Text = "Tra cứu";
            this.btnLookup.UseVisualStyleBackColor = true;
            this.btnLookup.Click += this.LookupClick;
            this.tabReceive.Controls.Add(this.btnLookup);

            this.btnAllCars.Name = "btnAllCars";
            this.btnAllCars.Location = new Point(425, 10);
            this.btnAllCars.Size = new Size(97, 30);
            this.btnAllCars.Text = "Tất cả xe";
            this.btnAllCars.UseVisualStyleBackColor = true;
            this.btnAllCars.Click += this.AllCarsClick;
            this.tabReceive.Controls.Add(this.btnAllCars);

            label2.Name = "label2";
            label2.Location = new Point(12, 56);
            label2.Size = new Size(478, 23);
            label2.Text = "Danh sách chủ xe";
            this.tabReceive.Controls.Add(label2);

            label3.Name = "label3";
            label3.Location = new Point(518, 56);
            label3.Size = new Size(475, 23);
            label3.Text = "Danh sách xe";
            this.tabReceive.Controls.Add(label3);

            this.gridOwners.Name = "gridOwners";
            this.gridOwners.Location = new Point(12, 82);
            this.gridOwners.Size = new Size(490, 204);
            this.gridOwners.AllowUserToAddRows = false;
            this.gridOwners.AllowUserToDeleteRows = false;
            this.gridOwners.ReadOnly = true;
            this.gridOwners.MultiSelect = false;
            this.gridOwners.RowHeadersVisible = false;
            this.gridOwners.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.gridOwners.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.gridOwners.BackgroundColor = SystemColors.Window;
            this.gridOwners.CurrentCellChanged += this.OwnerSelected;
            this.tabReceive.Controls.Add(this.gridOwners);

            this.gridCars.Name = "gridCars";
            this.gridCars.Location = new Point(518, 82);
            this.gridCars.Size = new Size(492, 204);
            this.gridCars.AllowUserToAddRows = false;
            this.gridCars.AllowUserToDeleteRows = false;
            this.gridCars.ReadOnly = true;
            this.gridCars.MultiSelect = false;
            this.gridCars.RowHeadersVisible = false;
            this.gridCars.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.gridCars.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.gridCars.BackgroundColor = SystemColors.Window;
            this.gridCars.CurrentCellChanged += this.CarSelected;
            this.tabReceive.Controls.Add(this.gridCars);

            label4.Name = "label4";
            label4.Location = new Point(12, 309);
            label4.Size = new Size(75, 23);
            label4.Text = "Họ tên";
            this.tabReceive.Controls.Add(label4);

            this.txtOwner.Name = "txtOwner";
            this.txtOwner.Location = new Point(91, 306);
            this.txtOwner.Size = new Size(411, 26);
            this.txtOwner.MaxLength = 100;
            this.tabReceive.Controls.Add(this.txtOwner);

            label5.Name = "label5";
            label5.Location = new Point(12, 343);
            label5.Size = new Size(75, 23);
            label5.Text = "Điện thoại";
            this.tabReceive.Controls.Add(label5);

            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Location = new Point(91, 340);
            this.txtPhone.Size = new Size(411, 26);
            this.txtPhone.MaxLength = 15;
            this.tabReceive.Controls.Add(this.txtPhone);

            label6.Name = "label6";
            label6.Location = new Point(12, 377);
            label6.Size = new Size(75, 23);
            label6.Text = "Địa chỉ";
            this.tabReceive.Controls.Add(label6);

            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Location = new Point(91, 374);
            this.txtAddress.Size = new Size(411, 26);
            this.txtAddress.MaxLength = 200;
            this.tabReceive.Controls.Add(this.txtAddress);

            label7.Name = "label7";
            label7.Location = new Point(518, 309);
            label7.Size = new Size(84, 23);
            label7.Text = "Biển số xe";
            this.tabReceive.Controls.Add(label7);

            this.txtPlate.Name = "txtPlate";
            this.txtPlate.Location = new Point(607, 306);
            this.txtPlate.Size = new Size(152, 26);
            this.txtPlate.MaxLength = 20;
            this.tabReceive.Controls.Add(this.txtPlate);

            label8.Name = "label8";
            label8.Location = new Point(779, 309);
            label8.Size = new Size(44, 23);
            label8.Text = "Năm";
            this.tabReceive.Controls.Add(label8);

            this.numYear.Name = "numYear";
            this.numYear.Location = new Point(827, 306);
            this.numYear.Size = new Size(183, 26);
            this.numYear.Minimum = 1900;
            this.numYear.Maximum = 9999;
            this.numYear.Value = 2024;
            this.numYear.DecimalPlaces = 0;
            this.tabReceive.Controls.Add(this.numYear);

            label9.Name = "label9";
            label9.Location = new Point(518, 343);
            label9.Size = new Size(84, 23);
            label9.Text = "Hiệu xe";
            this.tabReceive.Controls.Add(label9);

            this.txtBrand.Name = "txtBrand";
            this.txtBrand.Location = new Point(607, 340);
            this.txtBrand.Size = new Size(403, 26);
            this.txtBrand.MaxLength = 50;
            this.tabReceive.Controls.Add(this.txtBrand);

            label10.Name = "label10";
            label10.Location = new Point(518, 377);
            label10.Size = new Size(84, 23);
            label10.Text = "Chủ xe";
            this.tabReceive.Controls.Add(label10);

            this.cboOwner.Name = "cboOwner";
            this.cboOwner.Location = new Point(607, 374);
            this.cboOwner.Size = new Size(403, 26);
            this.cboOwner.DropDownStyle = ComboBoxStyle.DropDownList;
            this.tabReceive.Controls.Add(this.cboOwner);

            this.btnNewOwner.Name = "btnNewOwner";
            this.btnNewOwner.Location = new Point(12, 419);
            this.btnNewOwner.Size = new Size(115, 30);
            this.btnNewOwner.Text = "Chủ xe mới";
            this.btnNewOwner.UseVisualStyleBackColor = true;
            this.btnNewOwner.Click += this.NewOwnerClick;
            this.tabReceive.Controls.Add(this.btnNewOwner);

            this.btnSaveOwner.Name = "btnSaveOwner";
            this.btnSaveOwner.Location = new Point(137, 419);
            this.btnSaveOwner.Size = new Size(115, 30);
            this.btnSaveOwner.Text = "Lưu chủ xe";
            this.btnSaveOwner.UseVisualStyleBackColor = true;
            this.btnSaveOwner.Click += this.SaveOwnerClick;
            this.tabReceive.Controls.Add(this.btnSaveOwner);

            this.btnDeleteOwner.Name = "btnDeleteOwner";
            this.btnDeleteOwner.Location = new Point(262, 419);
            this.btnDeleteOwner.Size = new Size(115, 30);
            this.btnDeleteOwner.Text = "Xóa chủ xe";
            this.btnDeleteOwner.UseVisualStyleBackColor = true;
            this.btnDeleteOwner.Click += this.DeleteOwnerClick;
            this.tabReceive.Controls.Add(this.btnDeleteOwner);

            this.btnNewCar.Name = "btnNewCar";
            this.btnNewCar.Location = new Point(518, 419);
            this.btnNewCar.Size = new Size(103, 30);
            this.btnNewCar.Text = "Xe mới";
            this.btnNewCar.UseVisualStyleBackColor = true;
            this.btnNewCar.Click += this.NewCarClick;
            this.tabReceive.Controls.Add(this.btnNewCar);

            this.btnSaveCar.Name = "btnSaveCar";
            this.btnSaveCar.Location = new Point(630, 419);
            this.btnSaveCar.Size = new Size(103, 30);
            this.btnSaveCar.Text = "Lưu xe";
            this.btnSaveCar.UseVisualStyleBackColor = true;
            this.btnSaveCar.Click += this.SaveCarClick;
            this.tabReceive.Controls.Add(this.btnSaveCar);

            this.btnDeleteCar.Name = "btnDeleteCar";
            this.btnDeleteCar.Location = new Point(742, 419);
            this.btnDeleteCar.Size = new Size(103, 30);
            this.btnDeleteCar.Text = "Xóa xe";
            this.btnDeleteCar.UseVisualStyleBackColor = true;
            this.btnDeleteCar.Click += this.DeleteCarClick;
            this.tabReceive.Controls.Add(this.btnDeleteCar);

            this.btnReceive.Name = "btnReceive";
            this.btnReceive.Location = new Point(518, 464);
            this.btnReceive.Size = new Size(296, 30);
            this.btnReceive.Text = "Tiếp nhận cả chủ mới và xe mới";
            this.btnReceive.UseVisualStyleBackColor = true;
            this.btnReceive.Click += this.ReceiveClick;
            this.tabReceive.Controls.Add(this.btnReceive);

            this.tabParts.Name = "tabParts";
            this.tabParts.Text = "Phụ tùng";
            this.tabParts.UseVisualStyleBackColor = true;
            this.tabs.Controls.Add(this.tabParts);
            this.gridParts.Name = "gridParts";
            this.gridParts.Location = new Point(12, 13);
            this.gridParts.Size = new Size(998, 305);
            this.gridParts.AllowUserToAddRows = false;
            this.gridParts.AllowUserToDeleteRows = false;
            this.gridParts.ReadOnly = true;
            this.gridParts.MultiSelect = false;
            this.gridParts.RowHeadersVisible = false;
            this.gridParts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.gridParts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.gridParts.BackgroundColor = SystemColors.Window;
            this.gridParts.CurrentCellChanged += this.PartSelected;
            this.tabParts.Controls.Add(this.gridParts);

            label11.Name = "label11";
            label11.Location = new Point(12, 345);
            label11.Size = new Size(108, 23);
            label11.Text = "Tên phụ tùng";
            this.tabParts.Controls.Add(label11);

            this.txtPart.Name = "txtPart";
            this.txtPart.Location = new Point(122, 342);
            this.txtPart.Size = new Size(464, 26);
            this.txtPart.MaxLength = 150;
            this.tabParts.Controls.Add(this.txtPart);

            label12.Name = "label12";
            label12.Location = new Point(12, 383);
            label12.Size = new Size(108, 23);
            label12.Text = "Đơn giá (đ)";
            this.tabParts.Controls.Add(label12);

            this.numPrice.Name = "numPrice";
            this.numPrice.Location = new Point(122, 380);
            this.numPrice.Size = new Size(156, 26);
            this.numPrice.Minimum = 0;
            this.numPrice.Maximum = 999999999;
            this.numPrice.Value = 0;
            this.numPrice.DecimalPlaces = 2;
            this.numPrice.ThousandsSeparator = true;
            this.tabParts.Controls.Add(this.numPrice);

            label13.Name = "label13";
            label13.Location = new Point(298, 383);
            label13.Size = new Size(83, 23);
            label13.Text = "Tồn kho";
            this.tabParts.Controls.Add(label13);

            this.numStock.Name = "numStock";
            this.numStock.Location = new Point(382, 380);
            this.numStock.Size = new Size(130, 26);
            this.numStock.Minimum = 0;
            this.numStock.Maximum = 1000000;
            this.numStock.Value = 0;
            this.numStock.DecimalPlaces = 0;
            this.numStock.ThousandsSeparator = true;
            this.tabParts.Controls.Add(this.numStock);

            this.btnNewPart.Name = "btnNewPart";
            this.btnNewPart.Location = new Point(12, 438);
            this.btnNewPart.Size = new Size(126, 30);
            this.btnNewPart.Text = "Phụ tùng mới";
            this.btnNewPart.UseVisualStyleBackColor = true;
            this.btnNewPart.Click += this.NewPartClick;
            this.tabParts.Controls.Add(this.btnNewPart);

            this.btnSavePart.Name = "btnSavePart";
            this.btnSavePart.Location = new Point(147, 438);
            this.btnSavePart.Size = new Size(125, 30);
            this.btnSavePart.Text = "Lưu phụ tùng";
            this.btnSavePart.UseVisualStyleBackColor = true;
            this.btnSavePart.Click += this.SavePartClick;
            this.tabParts.Controls.Add(this.btnSavePart);

            this.btnDeletePart.Name = "btnDeletePart";
            this.btnDeletePart.Location = new Point(281, 438);
            this.btnDeletePart.Size = new Size(130, 30);
            this.btnDeletePart.Text = "Xóa phụ tùng";
            this.btnDeletePart.UseVisualStyleBackColor = true;
            this.btnDeletePart.Click += this.DeletePartClick;
            this.tabParts.Controls.Add(this.btnDeletePart);

            this.tabRepair.Name = "tabRepair";
            this.tabRepair.Text = "Lập phiếu sửa chữa";
            this.tabRepair.UseVisualStyleBackColor = true;
            this.tabs.Controls.Add(this.tabRepair);
            label14.Name = "label14";
            label14.Location = new Point(12, 16);
            label14.Size = new Size(70, 23);
            label14.Text = "Xe";
            this.tabRepair.Controls.Add(label14);

            this.cboCar.Name = "cboCar";
            this.cboCar.Location = new Point(90, 13);
            this.cboCar.Size = new Size(342, 26);
            this.cboCar.DropDownStyle = ComboBoxStyle.DropDownList;
            this.tabRepair.Controls.Add(this.cboCar);

            label15.Name = "label15";
            label15.Location = new Point(12, 55);
            label15.Size = new Size(77, 23);
            label15.Text = "Nội dung sửa";
            this.tabRepair.Controls.Add(label15);

            this.txtWork.Name = "txtWork";
            this.txtWork.Location = new Point(90, 52);
            this.txtWork.Size = new Size(920, 60);
            this.txtWork.MaxLength = 1000;
            this.txtWork.Multiline = true;
            this.txtWork.ScrollBars = ScrollBars.Vertical;
            this.tabRepair.Controls.Add(this.txtWork);

            label16.Name = "label16";
            label16.Location = new Point(12, 135);
            label16.Size = new Size(75, 23);
            label16.Text = "Phụ tùng";
            this.tabRepair.Controls.Add(label16);

            this.cboPart.Name = "cboPart";
            this.cboPart.Location = new Point(90, 132);
            this.cboPart.Size = new Size(285, 26);
            this.cboPart.DropDownStyle = ComboBoxStyle.DropDownList;
            this.tabRepair.Controls.Add(this.cboPart);

            label17.Name = "label17";
            label17.Location = new Point(390, 135);
            label17.Size = new Size(65, 23);
            label17.Text = "Số lượng";
            this.tabRepair.Controls.Add(label17);

            this.numQty.Name = "numQty";
            this.numQty.Location = new Point(462, 132);
            this.numQty.Size = new Size(90, 26);
            this.numQty.Minimum = 1;
            this.numQty.Maximum = 1000000;
            this.numQty.Value = 1;
            this.numQty.DecimalPlaces = 0;
            this.numQty.ThousandsSeparator = true;
            this.tabRepair.Controls.Add(this.numQty);

            label18.Name = "label18";
            label18.Location = new Point(567, 135);
            label18.Size = new Size(91, 23);
            label18.Text = "Tiền công (đ)";
            this.tabRepair.Controls.Add(label18);

            this.numLabor.Name = "numLabor";
            this.numLabor.Location = new Point(665, 132);
            this.numLabor.Size = new Size(151, 26);
            this.numLabor.Minimum = 0;
            this.numLabor.Maximum = 999999999;
            this.numLabor.Value = 0;
            this.numLabor.DecimalPlaces = 2;
            this.numLabor.ThousandsSeparator = true;
            this.tabRepair.Controls.Add(this.numLabor);

            this.btnAddLine.Name = "btnAddLine";
            this.btnAddLine.Location = new Point(832, 130);
            this.btnAddLine.Size = new Size(178, 30);
            this.btnAddLine.Text = "Thêm dòng";
            this.btnAddLine.UseVisualStyleBackColor = true;
            this.btnAddLine.Click += this.AddLineClick;
            this.tabRepair.Controls.Add(this.btnAddLine);

            this.gridDraft.Name = "gridDraft";
            this.gridDraft.Location = new Point(12, 184);
            this.gridDraft.Size = new Size(998, 239);
            this.gridDraft.AllowUserToAddRows = false;
            this.gridDraft.AllowUserToDeleteRows = false;
            this.gridDraft.ReadOnly = true;
            this.gridDraft.MultiSelect = false;
            this.gridDraft.RowHeadersVisible = false;
            this.gridDraft.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.gridDraft.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.gridDraft.BackgroundColor = SystemColors.Window;
            this.tabRepair.Controls.Add(this.gridDraft);

            this.lblEstimate.Name = "lblEstimate";
            this.lblEstimate.Location = new Point(12, 438);
            this.lblEstimate.Size = new Size(520, 24);
            this.tabRepair.Controls.Add(this.lblEstimate);

            this.btnRemoveLine.Name = "btnRemoveLine";
            this.btnRemoveLine.Location = new Point(12, 474);
            this.btnRemoveLine.Size = new Size(158, 30);
            this.btnRemoveLine.Text = "Bỏ dòng đã chọn";
            this.btnRemoveLine.UseVisualStyleBackColor = true;
            this.btnRemoveLine.Click += this.RemoveLineClick;
            this.tabRepair.Controls.Add(this.btnRemoveLine);

            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Location = new Point(767, 474);
            this.btnFinish.Size = new Size(243, 30);
            this.btnFinish.Text = "Hoàn tất và lưu phiếu";
            this.btnFinish.UseVisualStyleBackColor = true;
            this.btnFinish.Click += this.FinishClick;
            this.tabRepair.Controls.Add(this.btnFinish);

            this.tabBills.Name = "tabBills";
            this.tabBills.Text = "Hóa đơn và báo cáo";
            this.tabBills.UseVisualStyleBackColor = true;
            this.tabs.Controls.Add(this.tabBills);
            this.lblBillSummary.Name = "lblBillSummary";
            this.lblBillSummary.Location = new Point(12, 13);
            this.lblBillSummary.Size = new Size(785, 27);
            this.tabBills.Controls.Add(this.lblBillSummary);

            this.btnRefreshBills.Name = "btnRefreshBills";
            this.btnRefreshBills.Location = new Point(899, 10);
            this.btnRefreshBills.Size = new Size(111, 30);
            this.btnRefreshBills.Text = "Tải lại";
            this.btnRefreshBills.UseVisualStyleBackColor = true;
            this.btnRefreshBills.Click += this.RefreshBillsClick;
            this.tabBills.Controls.Add(this.btnRefreshBills);

            this.gridBills.Name = "gridBills";
            this.gridBills.Location = new Point(12, 52);
            this.gridBills.Size = new Size(998, 207);
            this.gridBills.AllowUserToAddRows = false;
            this.gridBills.AllowUserToDeleteRows = false;
            this.gridBills.ReadOnly = true;
            this.gridBills.MultiSelect = false;
            this.gridBills.RowHeadersVisible = false;
            this.gridBills.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.gridBills.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.gridBills.BackgroundColor = SystemColors.Window;
            this.gridBills.CurrentCellChanged += this.BillSelected;
            this.tabBills.Controls.Add(this.gridBills);

            label19.Name = "label19";
            label19.Location = new Point(12, 275);
            label19.Size = new Size(460, 23);
            label19.Text = "Chi tiết phiếu đang chọn";
            this.tabBills.Controls.Add(label19);

            this.gridBillLines.Name = "gridBillLines";
            this.gridBillLines.Location = new Point(12, 301);
            this.gridBillLines.Size = new Size(998, 148);
            this.gridBillLines.AllowUserToAddRows = false;
            this.gridBillLines.AllowUserToDeleteRows = false;
            this.gridBillLines.ReadOnly = true;
            this.gridBillLines.MultiSelect = false;
            this.gridBillLines.RowHeadersVisible = false;
            this.gridBillLines.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.gridBillLines.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.gridBillLines.BackgroundColor = SystemColors.Window;
            this.tabBills.Controls.Add(this.gridBillLines);

            this.btnPay.Name = "btnPay";
            this.btnPay.Location = new Point(12, 476);
            this.btnPay.Size = new Size(128, 30);
            this.btnPay.Text = "Thanh toán";
            this.btnPay.UseVisualStyleBackColor = true;
            this.btnPay.Click += this.PayClick;
            this.tabBills.Controls.Add(this.btnPay);

            this.btnExport.Name = "btnExport";
            this.btnExport.Location = new Point(151, 476);
            this.btnExport.Size = new Size(162, 30);
            this.btnExport.Text = "Xuất hóa đơn TXT";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += this.ExportClick;
            this.tabBills.Controls.Add(this.btnExport);

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
            this.Text = "Bài 6.4 - Quản lý garage và bảo dưỡng ô tô";
            this.Load += this.MainFormLoad;
            this.ResumeLayout(false);
        }
    }
}
