using Core.Domain;
using Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Data.Entities
{
    public class Teacher
    {
        Teacher()
        {
            Courses = new List<Course>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(450)]
        public string ApplicationUserId { get; set; }

        public decimal Salary { get; set; }

        public Subjects Subject { get; set; }

        public string FullName { get; set; }

        public DateTime Birthday { get; set; }

        public string CodeMelly { get; set; }

        public string PhoneNumber { get; set; }

        public Gender Gender { get; set; }

        public ICollection<Course> Courses { get; set; }

    }
}
