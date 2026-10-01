using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoSpeed
{
    public class QuanLyPhuongTien
    {
        // Danh sách chứa phương tiện (Áp dụng Đa hình: List chứa lớp cha nhưng có thể add đối tượng lớp con)
        private List<PhuongTien> _danhSach = new List<PhuongTien>();

        // 1. Thêm phương tiện mới
        public void AddPhuongTien(PhuongTien pt)
        {
            _danhSach.Add(pt);
            Console.WriteLine($"Đã thêm thành công: {pt.TenHang}");
        }

        // 2. In danh sách toàn bộ phương tiện kèm Giá lăn bánh
        public void DisplayAll()
        {
            Console.WriteLine("\n--- DANH SÁCH PHƯƠNG TIỆN ---");
            if (_danhSach.Count == 0)
            {
                Console.WriteLine("Danh sách trống!");
                return;
            }

            foreach (var pt in _danhSach)
            {
                // Gọi pt.TinhGiaLanBanh() ở đây chính là ĐA HÌNH
                Console.WriteLine($"{pt.GetInfo()} | Giá lăn bánh: {pt.TinhGiaLanBanh():N0} VNĐ");
            }
        }

        // 3. Tìm phương tiện có Giá lăn bánh cao nhất
        public PhuongTien FindMaxGiaLanBanh()
        {
            if (_danhSach.Count == 0) return null;

            // Dùng LINQ để tìm giá trị max nhanh gọn
            decimal maxGia = _danhSach.Max(pt => pt.TinhGiaLanBanh());
            return _danhSach.First(pt => pt.TinhGiaLanBanh() == maxGia);
        }

        // 4. Tìm kiếm theo tên hãng
        public void SearchByName(string keyword)
        {
            Console.WriteLine($"\n--- KẾT QUẢ TÌM KIẾM CHO: '{keyword}' ---");

            // Dùng LINQ để lọc danh sách bỏ qua phân biệt hoa thường
            var ketQua = _danhSach.Where(pt => pt.TenHang.ToLower().Contains(keyword.ToLower())).ToList();

            if (ketQua.Count == 0)
            {
                Console.WriteLine("Không tìm thấy phương tiện nào!");
            }
            else
            {
                foreach (var pt in ketQua)
                {
                    Console.WriteLine($"{pt.GetInfo()} | Giá lăn bánh: {pt.TinhGiaLanBanh():N0} VNĐ");
                }
            }
        }
    }
}