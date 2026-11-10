using System;
using System.Data;
using System.Linq;
using ProjectTask.DAL;
using ProjectTask.DTO;

namespace ProjectTask.BUS
{
    internal sealed class Service
    {
        public static readonly string[] TrangThaiDuAn = { "Đang chạy", "Hoàn thành", "Tạm dừng" };
        public static readonly string[] TrangThaiTask = { "To Do", "In Progress", "Review", "Done" };
        public static readonly string[] MucUuTien = { "Cao", "Trung bình", "Thấp" };
        private readonly Repository repository = new Repository();

        public DataTable DuAn() => repository.DuAn();
        public DataTable ThanhVien() => repository.ThanhVien();
        public DataTable CongViec(int duAn, string trangThai = "") => repository.CongViec(duAn, trangThai);
        public DataTable PhanCong(int task) => repository.PhanCong(task);
        public DataTable TreHan(int duAn) => repository.TreHan(duAn);
        public DataTable BaoCao(int duAn) => repository.BaoCao(duAn);

        public void LuuDuAn(DuAn duAn)
        {
            duAn.TenDuAn = (duAn.TenDuAn ?? "").Trim();
            if (duAn.TenDuAn.Length == 0 || duAn.TenDuAn.Length > 150)
                throw new ArgumentException("Nhập tên dự án từ 1 đến 150 ký tự.");
            if (duAn.NgayKetThuc < duAn.NgayBatDau)
                throw new ArgumentException("Ngày kết thúc phải từ ngày bắt đầu trở đi.");
            if (!TrangThaiDuAn.Contains(duAn.TrangThai))
                throw new ArgumentException("Chọn trạng thái dự án.");
            CheckUpdated(repository.LuuDuAn(duAn));
        }

        public void XoaDuAn(int id)
        {
            if (id <= 0) throw new ArgumentException("Chọn dự án cần xóa.");
            if (repository.CongViec(id, "").Rows.Count > 0)
                throw new InvalidOperationException("Dự án còn công việc. Hãy xóa công việc trước.");
            CheckUpdated(repository.XoaDuAn(id));
        }

        public int LuuCongViec(CongViec viec)
        {
            if (viec.MaDuAn <= 0) throw new ArgumentException("Chọn dự án cho công việc.");
            viec.TenTask = (viec.TenTask ?? "").Trim();
            if (viec.TenTask.Length == 0 || viec.TenTask.Length > 200)
                throw new ArgumentException("Nhập tên công việc từ 1 đến 200 ký tự.");
            if (!TrangThaiTask.Contains(viec.TrangThai) || !MucUuTien.Contains(viec.DoUuTien))
                throw new ArgumentException("Chọn trạng thái và độ ưu tiên.");
            viec.ThanhVien = (viec.ThanhVien ?? new int[0]).Distinct().ToArray();
            return repository.LuuCongViec(viec);
        }

        public void XoaCongViec(int id)
        {
            if (id <= 0) throw new ArgumentException("Chọn công việc cần xóa.");
            CheckUpdated(repository.XoaCongViec(id));
        }

        public void ChuyenTrangThai(int id, string trangThai)
        {
            if (id <= 0 || !TrangThaiTask.Contains(trangThai))
                throw new ArgumentException("Chọn công việc và trạng thái mới.");
            CheckUpdated(repository.ChuyenTrangThai(id, trangThai));
        }

        private static void CheckUpdated(int rows)
        {
            if (rows != 1) throw new InvalidOperationException("Bản ghi không còn tồn tại. Hãy tải lại danh sách.");
        }
    }
}
