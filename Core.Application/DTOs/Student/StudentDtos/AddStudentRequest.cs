using Core.Domain;
using System.Text.Json.Serialization;

namespace Core.Application.DTOs.Student.StudentDtos
{
    public class AddStudentRequest
    {
        public string UserName { get; set; }
        
        //[JsonPropertyName("نام و نام خانوادگی")]
        public string FullName { get; set; }

        //[JsonPropertyName("کد ملی")]
        public string CodeMelly { get; set; }

        //[JsonPropertyName("رمز ورورد")]
        public string Password { get; set; }

        //[JsonPropertyName("شماره تماس")]
        public string PhoneNumber { get; set; }

        //[JsonPropertyName("تاریخ تولد")]
        public DateTime Birthday { get; set; }

        //[JsonPropertyName("جنسیت")]
        public Gender Gender { get; set; }

        //[JsonPropertyName("پایه ")]
        public Grade Grade { get; set; }

    }
}
