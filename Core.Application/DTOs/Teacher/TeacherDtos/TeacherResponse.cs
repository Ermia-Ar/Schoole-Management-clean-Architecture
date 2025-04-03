using Core.Domain;

namespace Core.Application.DTOs.Teacher.TeacherDtos
{
    public class TeacherResponse
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public string CodeMelly { get; set; }

        public Gender Gender { get; set; }

        public DateTime Birthday { get; set; }

        public string PhoneNumber { get; set; }

        public decimal Salary { get; set; }

        public Subjects Subject { get; set; }
    }
}
