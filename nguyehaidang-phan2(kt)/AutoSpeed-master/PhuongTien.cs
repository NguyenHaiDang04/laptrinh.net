using System;

namespace AutoSpeed
{
    public abstract class PhuongTien
    {
        // 1. Fields (Private)
        private string _maPT;
        private string _tenHang;
        private int _namSanXuat;
        private decimal _giaGoc;

        // 2. Properties (Validation Encapsulation)
        public string MaPT
        {
            get => _maPT;
            set
            {
                // Loại bỏ khoảng trắng, nếu rỗng gán mặc định PT000
                string processedValue = value?.Replace(" ", "");
                _maPT = string.IsNullOrEmpty(processedValue) ? "PT000" : processedValue;
            }
        }

        public string TenHang
        {
            get => _tenHang;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Tên hãng không được để trống!");
                _tenHang = value;
            }
        }

        public int NamSanXuat
        {
            get => _namSanXuat;
            set
            {
                // Bắt buộc >= 1900 và <= Năm hiện tại
                if (value < 1900 || value > DateTime.Now.Year)
                    throw new ArgumentException("Năm sản xuất không hợp lệ!");
                _namSanXuat = value;
            }
        }

        public decimal GiaGoc
        {
            get => _giaGoc;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Giá gốc bắt buộc phải lớn hơn 0!");
                _giaGoc = value;
            }
        }

        // 3. Constructor
        public PhuongTien(string maPT, string tenHang, int namSanXuat, decimal giaGoc)
        {
            MaPT = maPT;
            TenHang = tenHang;
            NamSanXuat = namSanXuat;
            GiaGoc = giaGoc;
        }

        // 4. Abstract Method (Tính đa hình - các lớp con bắt buộc phải ghi đè)
        public abstract decimal TinhGiaLanBanh();

        // 5. Virtual Method (Các lớp con có thể ghi đè để mở rộng)
        public virtual string GetInfo()
        {
            return $"Mã PT: {MaPT} | Hãng: {TenHang} | Năm SX: {NamSanXuat} | Giá gốc: {GiaGoc:N0} VNĐ";
        }
    }
}