using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp.Forms
{
    internal static class Ui
    {
        public static void Setup(Form form, string title)
        {
            form.Text = title;
            form.Font = new Font("Segoe UI", 10F);
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ClientSize = new Size(1080, 650);
            form.MinimumSize = new Size(940, 620);
        }
        public static TableLayoutPanel Page(Form form, params float[] heights)
        {
            var panel = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = heights.Length, Padding = new Padding(12) };
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            foreach (var h in heights) panel.RowStyles.Add(new RowStyle(h < 0 ? SizeType.Percent : SizeType.Absolute, h < 0 ? 100 : h));
            form.Controls.Add(panel);
            return panel;
        }
        public static Label Heading(string text) => new Label { Text = text, Dock = DockStyle.Fill, Font = new Font("Segoe UI", 15F, FontStyle.Bold), TextAlign = ContentAlignment.MiddleLeft };
        public static TextBox Text(string name, bool readOnly = false) => new TextBox { Name = name, Dock = DockStyle.Fill, ReadOnly = readOnly, Margin = new Padding(3, 6, 20, 3) };
        public static ComboBox Combo(string name, params string[] items)
        {
            var c = new ComboBox { Name = name, Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList, Margin = new Padding(3, 6, 20, 3) };
            c.Items.AddRange(items); c.SelectedIndex = 0; return c;
        }
        public static DateTimePicker Date(string name) => new DateTimePicker { Name = name, Format = DateTimePickerFormat.Custom, CustomFormat = "dd/MM/yyyy", Dock = DockStyle.Fill, Margin = new Padding(3, 6, 20, 3) };
        public static TableLayoutPanel Fields(params object[] pairs)
        {
            var p = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 4, RowCount = (pairs.Length / 2 + 1) / 2 };
            p.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 125)); p.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            p.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 125)); p.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            for (int i = 0; i < pairs.Length / 2; i++)
            {
                int row = i / 2, col = i % 2 * 2;
                if (col == 0) p.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / p.RowCount));
                p.Controls.Add(new Label { Text = (string)pairs[i * 2], Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft }, col, row);
                p.Controls.Add((Control)pairs[i * 2 + 1], col + 1, row);
            }
            return p;
        }
        public static Button Button(string name, string text, EventHandler click)
        {
            var b = new Button { Name = name, Text = text, AutoSize = true, Height = 34, MinimumSize = new Size(115, 34), Margin = new Padding(0, 5, 10, 5) };
            b.Click += click; return b;
        }
        public static FlowLayoutPanel Buttons(params Control[] buttons)
        {
            var p = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = false };
            p.Controls.AddRange(buttons); return p;
        }
        public static DataGridView Grid(string name)
        {
            return new DataGridView { Name = name, Dock = DockStyle.Fill, AutoGenerateColumns = false, ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect, MultiSelect = false, AllowUserToAddRows = false,
                AllowUserToDeleteRows = false, RowHeadersVisible = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White, BorderStyle = BorderStyle.FixedSingle, ColumnHeadersHeight = 36,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
                RowTemplate = { Height = 29 }, AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(245, 247, 250) } };
        }
        public static void Column(DataGridView grid, string property, string title, string format = null)
        {
            var c = new DataGridViewTextBoxColumn { Name = property, DataPropertyName = property, HeaderText = title, SortMode = DataGridViewColumnSortMode.NotSortable };
            if (format != null) c.DefaultCellStyle.Format = format;
            grid.Columns.Add(c);
        }
        public static void Error(Form owner, string message) => MessageBox.Show(owner, message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }
}
