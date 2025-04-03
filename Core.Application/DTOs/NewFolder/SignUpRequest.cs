using System.Text.Json.Serialization;
using System;
using Core.Domain;
namespace Core.Application.DTOs.NewFolder
{
    public class SignUpRequest
    {
        [JsonPropertyName("اسم کامل")]
        public string FullName { get; set; }

        [JsonPropertyName("کد ملی")]
        public string CodeMelly { get; set; }

        [JsonPropertyName("رمز ورورد")]
        public string Password { get; set; }

        [JsonPropertyName("تاریخ تولد")]
        public DateTime Birthday { get; set; }

        [JsonPropertyName("جنیست")]
        public Gender Gender { get; set; }

        [JsonPropertyName("نقش")]
        public string[] Roles { get; set; }

        public AdminData? AdminInfo { get; set; }
        public TeacherData? TeacherInfo { get; set; }
        public StudentData? StudentInfo { get; set; }
    }

    public class AdminData
    {
        public decimal Salary { get; set; }
    }

    public class TeacherData
    {
        public string Subject { get; set; }
        public decimal Salary { get; set; }
    }

    public class StudentData
    {
        public int Grade { get; set; }
    }
}