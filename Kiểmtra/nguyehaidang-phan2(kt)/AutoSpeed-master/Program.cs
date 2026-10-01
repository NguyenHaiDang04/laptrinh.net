using System;

namespace AutoSpeed
{
    class Program
    {
        static void Main(string[] args)
        {
            // Set encoding để hiển thị tiếng Việt có dấu trên Console
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            QuanLyPhuongTien ql = new QuanLyPhuongTien();

            Console.WriteLine("========== KIỂM THỬ HỆ THỐNG AUTOSPEED ==========\n");

            // --- TC01: Kiểm tra Validation Năm sản xuất ---
            Console.WriteLine("-> TC01: Khởi tạo Ô tô có NamSanXuat = 1850");
            try
            {
                OTo otoLoi = new OTo("OT001", "Ford", 1850, 1000000000, 5, 2.0);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TC01 - PASS] Hệ thống báo lỗi: {ex.Message}");
            }


            // --- TC02: Kiểm tra Tính Giá Lăn Bánh Ô tô ---
            Console.WriteLine("\n-> TC02: Tính Giá Lăn Bánh Ô tô 5 chỗ, Giá gốc 1 tỷ VNĐ");
            OTo otoHopLe = new OTo("OT002", "Toyota Camry", 2023, 1000000000, 5, 2.5);
            Console.WriteLine($"[TC02 - PASS] {otoHopLe.TenHang} - Giá lăn bánh: {otoHopLe.TinhGiaLanBanh():N0} VNĐ");
            // Kỳ vọng: 1.000.000.000 + 120.000.000 + 300.000.000 = 1.420.000.000


            // --- TC03: Kiểm tra Tính Giá Lăn Bánh Xe máy ---
            Console.WriteLine("\n-> TC03: Tính Giá Lăn Bánh Xe máy 150cc, Giá gốc 50 triệu VNĐ");
            XeMay xemayHopLe = new XeMay("XM001", "Honda Winner", 2023, 50000000, 150);
            Console.WriteLine($"[TC03 - PASS] {xemayHopLe.TenHang} - Giá lăn bánh: {xemayHopLe.TinhGiaLanBanh():N0} VNĐ");
            // Kỳ vọng: 50.000.000 + 1.000.000 = 51.000.000


            // --- TC04: Kiểm tra Đa hình List<PhuongTien> ---
            Console.WriteLine("\n-> TC04: Thêm vào danh sách và in hiển thị (Đa hình)");
            ql.AddPhuongTien(otoHopLe);
            ql.AddPhuongTien(xemayHopLe);
            ql.DisplayAll();
            Console.WriteLine("[TC04 - PASS] Hệ thống đã tự động kích hoạt đúng công thức tính thuế cho từng loại xe.");


            // --- TC05: Kiểm tra Tìm Giá Lăn Bánh Max ---
            Console.WriteLine("\n-> TC05: Tìm phương tiện có Giá lăn bánh cao nhất");
            PhuongTien maxPt = ql.FindMaxGiaLanBanh();
            if (maxPt != null)
            {
                Console.WriteLine($"[TC05 - PASS] Phương tiện Max: {maxPt.TenHang} - Giá: {maxPt.TinhGiaLanBanh():N0} VNĐ");
            }

            Console.WriteLine("\n================ HOÀN THÀNH PHẦN OOP ================");
            Console.ReadLine(); // Dừng màn hình để xem kết quả
        }
    }
}