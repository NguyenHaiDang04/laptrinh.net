using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp.Models;

namespace WinFormsApp.Forms
{
    public sealed class frmRevenueAnalytics : Form
    {
        private readonly DateTimePicker dtpFromDate = Ui.Date("dtpFromDate"), dtpToDate = Ui.Date("dtpToDate");
        private readonly ComboBox cboBranch = Ui.Combo("cboBranch", "Tất cả", "Hà Nội", "Đà Nẵng", "TP. Hồ Chí Minh");
        private readonly TextBox txtNote = Ui.Text("txtNote");
        private readonly ProgressBar pgbLoading = new ProgressBar { Name = "pgbLoading", Dock = DockStyle.Fill, Minimum = 0, Maximum = 100 };
        private readonly Label lblProcessingStatus = new Label { Name = "lblProcessingStatus", Dock = DockStyle.Fill };
        private readonly DataGridView dgvRevenueData = Ui.Grid("dgvRevenueData");
        private readonly Button start, cancel;
        private CancellationTokenSource cancellation;
        private bool closing;
        internal Task CurrentTask { get; private set; } = Task.CompletedTask;
        internal string ProcessingStatus => lblProcessingStatus.Text;
        internal int ResultCount => dgvRevenueData.Rows.Count;
        public frmRevenueAnalytics()
        {
            Ui.Setup(this, "Bài 4 - Thống kê doanh thu bất đồng bộ"); dtpFromDate.Value = DateTime.Today.AddDays(-7);
            var p = Ui.Page(this, 48, 92, 48, 25, 35, -1);
            p.Controls.Add(Ui.Heading("THỐNG KÊ DOANH THU"), 0, 0);
            p.Controls.Add(Ui.Fields("Từ ngày", dtpFromDate, "Đến ngày", dtpToDate, "Chi nhánh", cboBranch, "Ghi chú thử UI", txtNote), 0, 1);
            start = Ui.Button("btnAnalyze", "Thống Kê Doanh Thu", (s, e) => { CurrentTask = AnalyzeAsync(); });
            cancel = Ui.Button("btnCancel", "Hủy Thao Tác", (s, e) => CancelAnalysis()); cancel.Enabled = false;
            p.Controls.Add(Ui.Buttons(start, cancel), 0, 2); p.Controls.Add(pgbLoading, 0, 3); p.Controls.Add(lblProcessingStatus, 0, 4); p.Controls.Add(dgvRevenueData, 0, 5);
            Ui.Column(dgvRevenueData, "TransactionId", "Mã giao dịch"); Ui.Column(dgvRevenueData, "Date", "Ngày", "dd/MM/yyyy"); Ui.Column(dgvRevenueData, "Branch", "Chi nhánh"); Ui.Column(dgvRevenueData, "Amount", "Doanh thu (đ)", "N0");
            lblProcessingStatus.Text = "Dữ liệu giả lập. Có thể nhập ghi chú, di chuyển hoặc thu/phóng form khi xử lý.";
        }
        internal async Task AnalyzeAsync()
        {
            if (cancellation != null) return;
            DateTime from = dtpFromDate.Value.Date, to = dtpToDate.Value.Date; string branch = cboBranch.Text;
            if (from > to) { lblProcessingStatus.Text = "Từ ngày phải nhỏ hơn hoặc bằng Đến ngày."; return; }
            var cts = new CancellationTokenSource(); cancellation = cts;
            start.Enabled = false; cancel.Enabled = true; pgbLoading.Value = 0;
            lblProcessingStatus.Text = "Đang tổng hợp dữ liệu... 0%";
            var progress = new Progress<int>(value =>
            {
                if (closing || IsDisposed || cancellation != cts || cts.IsCancellationRequested) return;
                pgbLoading.Value = value; lblProcessingStatus.Text = "Đang tổng hợp dữ liệu... " + value + "%";
            });
            try
            {
                // Dữ liệu control được chụp trước Task.Run. Worker không truy cập control.
                var rows = await Task.Run(async () =>
                {
                    for (int i = 1; i <= 20; i++)
                    {
                        await Task.Delay(250, cts.Token).ConfigureAwait(false);
                        cts.Token.ThrowIfCancellationRequested(); ((IProgress<int>)progress).Report(i * 5);
                    }
                    var result = new List<RevenueModel>(); var random = new Random(); string[] branches = { "Hà Nội", "Đà Nẵng", "TP. Hồ Chí Minh" };
                    int days = (to - from).Days + 1;
                    for (int i = 0; i < 30; i++)
                    {
                        cts.Token.ThrowIfCancellationRequested();
                        result.Add(new RevenueModel { TransactionId = "GD" + (i + 1).ToString("D4"), Date = from.AddDays(random.Next(days)), Branch = branch == "Tất cả" ? branches[i % 3] : branch, Amount = random.Next(1, 100) * 10000M });
                    }
                    return result.OrderBy(r => r.Date).ToList();
                }, cts.Token);
                cts.Token.ThrowIfCancellationRequested();
                if (!closing) { dgvRevenueData.DataSource = new BindingList<RevenueModel>(rows); pgbLoading.Value = 100; lblProcessingStatus.Text = "Hoàn thành " + rows.Count + " giao dịch. Tổng doanh thu: " + rows.Sum(r => r.Amount).ToString("N0") + " đ"; }
            }
            catch (OperationCanceledException) { if (!closing) { pgbLoading.Value = 0; lblProcessingStatus.Text = "Đã hủy thao tác!"; } }
            catch (Exception ex) { if (!closing) { pgbLoading.Value = 0; lblProcessingStatus.Text = "Lỗi thống kê: " + ex.Message; } }
            finally { cancellation = null; cts.Dispose(); if (!closing && !IsDisposed) { start.Enabled = true; cancel.Enabled = false; } }
        }
        internal void CancelAnalysis() { cancellation?.Cancel(); }
        protected override void OnFormClosing(FormClosingEventArgs e) { base.OnFormClosing(e); if (!e.Cancel) { closing = true; cancellation?.Cancel(); } }
    }
}
