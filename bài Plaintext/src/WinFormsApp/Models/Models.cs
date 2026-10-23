using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WinFormsApp.Models
{
    public abstract class ObservableModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void Set<T>(ref T field, T value, [CallerMemberName] string property = null)
        {
            if (Equals(field, value)) return;
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }

    public sealed class MemberModel : ObservableModel
    {
        private string id = "", name = "", phone = "", package = "Theo tháng";
        private DateTime startDate = DateTime.Today;
        private decimal fee;
        public string MemberId { get => id; set => Set(ref id, value); }
        public string FullName { get => name; set => Set(ref name, value); }
        public string Phone { get => phone; set => Set(ref phone, value); }
        public string Package { get => package; set => Set(ref package, value); }
        public DateTime StartDate { get => startDate; set => Set(ref startDate, value); }
        public decimal Fee { get => fee; set => Set(ref fee, value); }
    }

    public sealed class BookModel
    {
        public string BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string CoverPath { get; set; }
        public string CoverFile => System.IO.Path.GetFileName(CoverPath ?? "");
        public decimal TotalValue => UnitPrice * Quantity;
    }

    public sealed class PatientModel
    {
        public string PatientId { get; set; }
        public string FullName { get; set; }
        public int BirthYear { get; set; }
        public string Diagnosis { get; set; }
    }
    public sealed class DoctorModel
    {
        public string DoctorId { get; set; }
        public string FullName { get; set; }
        public string Department { get; set; }
        public string Shift { get; set; }
    }
    public sealed class DepartmentModel
    {
        public string DepartmentId { get; set; }
        public string Name { get; set; }
        public string Room { get; set; }
    }
    public sealed class RevenueModel
    {
        public string TransactionId { get; set; }
        public DateTime Date { get; set; }
        public string Branch { get; set; }
        public decimal Amount { get; set; }
    }
    public sealed class PriceLogModel
    {
        public int Sequence { get; set; }
        public DateTime UpdatedAt { get; set; }
        public decimal Price { get; set; }
        public decimal Change { get; set; }
        public string Direction => Change > 0 ? "Tăng" : Change < 0 ? "Giảm" : "Không đổi";
    }
}
