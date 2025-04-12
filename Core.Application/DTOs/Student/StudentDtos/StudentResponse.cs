using Core.Domain;

namespace Core.Application.DTOs.Student.StudentDtos
{
    public class StudentResponse
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public string CodeMelly { get; set; }

        public Gender Gender { get; set; }

        public DateTime Birthday { get; set; }

        public string PhoneNumber { get; set; }
    }
}
