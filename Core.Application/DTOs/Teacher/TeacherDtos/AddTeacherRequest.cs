using Core.Domain;

namespace Core.Application.DTOs.Teacher.TeacherDtos
{
    public class AddTeacherRequest
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

        //[JsonPropertyName("حقوق")]
        public decimal Salary { get; set; }

        public Subjects Subject { get; set; }
    }
}
