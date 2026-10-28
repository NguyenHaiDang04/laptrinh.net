using System;

namespace BaiTap4.Shared
{
    public sealed class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
    }

    public sealed class RegistrationInput
    {
        public string FullName { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public int CourseId { get; set; }
        public bool ReceiveNews { get; set; }
    }

    public sealed class RegistrationRow
    {
        public int RegistrationId { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public string CourseName { get; set; }
        public bool ReceiveNews { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public sealed class LoginResult
    {
        public bool Success { get; set; }
        public string FullName { get; set; }
    }
}
