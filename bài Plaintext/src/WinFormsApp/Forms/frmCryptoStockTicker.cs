using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp.Models;

namespace WinFormsApp.Forms
{
    public sealed class frmCryptoStockTicker : Form
    {
        internal readonly BindingList<PriceLogModel> History = new BindingList<PriceLogModel>();
        private readonly Label lblBTCPrice = new Label { Name = "lblBTCPrice", Dock = DockStyle.Fill, Font = new Font("Segoe UI", 24F, FontStyle.Bold), Text = "BTC/USD: chờ cập nhật" };
        private readonly Label status = new Label { Name = "lblStatus", Dock = DockStyle.Fill, Text = "Dữ liệu mô phỏng ngẫu nhiên, không phải tỷ giá thực tế." };
        private readonly ToolStripStatusLabel lastUpdate = new ToolStripStatusLabel("Lần cập nhật cuối: --:--:--"), count = new ToolStripStatusLabel("Số lượt đồng bộ: 0");
        private readonly Button start, pause;
        private readonly DataGridView dgvHistoryLog = Ui.Grid("dgvHistoryLog");
        private CancellationTokenSource cancellation;
        private bool closing;
        private decimal previousPrice = 65000M;
        internal Task CurrentTask { get; private set; } = Task.CompletedTask;
        public frmCryptoStockTicker()
        {
            Ui.Setup(this, "Bài 5 - Giám sát tỷ giá BTC/USD");
            var p = Ui.Page(this, 48, 48, 65, 30, -1, 28);
            p.Controls.Add(Ui.Heading("GIÁM SÁT TỶ GIÁ BTC/USD"), 0, 0);
            start = Ui.Button("btnStart", "Bắt đầu Giám sát", (s, e) => StartMonitoring()); pause = Ui.Button("btnPause", "Tạm dừng", (s, e) => PauseMonitoring()); pause.Enabled = false;
            p.Controls.Add(Ui.Buttons(start, pause), 0, 1); p.Controls.Add(lblBTCPrice, 0, 2); p.Controls.Add(status, 0, 3); p.Controls.Add(dgvHistoryLog, 0, 4);
            Ui.Column(dgvHistoryLog, "Sequence", "Lượt"); Ui.Column(dgvHistoryLog, "UpdatedAt", "Thời gian", "HH:mm:ss"); Ui.Column(dgvHistoryLog, "Price", "BTC/USD", "N2"); Ui.Column(dgvHistoryLog, "Change", "Chênh lệch", "N2"); Ui.Column(dgvHistoryLog, "Direction", "Biến động");
            dgvHistoryLog.DataSource = History;
            var strip = new StatusStrip(); strip.Items.Add(lastUpdate); strip.Items.Add(new ToolStripStatusLabel { Spring = true }); strip.Items.Add(count); p.Controls.Add(strip, 0, 5);
        }
        internal void StartMonitoring()
        {
            if (cancellation != null || closing) return;
            var cts = new CancellationTokenSource(); cancellation = cts; start.Enabled = false; pause.Enabled = true;
            status.Text = "Đang giám sát dữ liệu mô phỏng, cập nhật mỗi 2 giây.";
            decimal initialPrice = previousPrice;
            CurrentTask = RunMonitoringAsync(cts, initialPrice);
        }
        private async Task RunMonitoringAsync(CancellationTokenSource cts, decimal initialPrice)
        {
            try
            {
                await Task.Run(async () =>
                {
                    var random = new Random(); decimal price = initialPrice;
                    while (true)
                    {
                        await Task.Delay(2000, cts.Token).ConfigureAwait(false); cts.Token.ThrowIfCancellationRequested();
                        price = Math.Max(1M, price + random.Next(-10000, 10001) / 100M);
                        PublishPrice(price, DateTime.Now, cts);
                    }
                }, cts.Token);
            }
            catch (OperationCanceledException) { if (!closing) status.Text = "Đã tạm dừng. Bấm Bắt đầu Giám sát để tiếp tục, lịch sử được giữ lại."; }
            catch (Exception ex) { if (!closing && !IsDisposed) status.Text = "Lỗi giám sát: " + ex.Message; }
            finally { cancellation = null; cts.Dispose(); if (!closing && !IsDisposed) { start.Enabled = true; pause.Enabled = false; } }
        }
        private void PublishPrice(decimal price, DateTime time, CancellationTokenSource cts)
        {
            if (cts.IsCancellationRequested) return;
            try
            {
                // Tất cả thay đổi control và BindingList của grid đều về UI thread.
                if (InvokeRequired) { Invoke(new Action(() => ApplyPrice(price, time, cts))); }
                else ApplyPrice(price, time, cts);
            }
            catch (ObjectDisposedException) when (cts.IsCancellationRequested) { }
            catch (InvalidOperationException) when (cts.IsCancellationRequested) { }
        }
        private void ApplyPrice(decimal price, DateTime time, CancellationTokenSource cts)
        {
            if (closing || IsDisposed || cts.IsCancellationRequested || cancellation != cts) return;
            decimal change = price - previousPrice; previousPrice = price;
            lblBTCPrice.Text = "BTC/USD: " + price.ToString("N2"); lblBTCPrice.ForeColor = change > 0 ? Color.ForestGreen : change < 0 ? Color.Firebrick : Color.DimGray;
            History.Add(new PriceLogModel { Sequence = History.Count + 1, UpdatedAt = time, Price = price, Change = change });
            lastUpdate.Text = "Lần cập nhật cuối: " + time.ToString("HH:mm:ss"); count.Text = "Số lượt đồng bộ: " + History.Count;
            dgvHistoryLog.FirstDisplayedScrollingRowIndex = dgvHistoryLog.Rows.Count - 1;
        }
        internal void PauseMonitoring() { cancellation?.Cancel(); pause.Enabled = false; }
        protected override void OnFormClosing(FormClosingEventArgs e) { base.OnFormClosing(e); if (!e.Cancel) { closing = true; cancellation?.Cancel(); } }
    }
}
