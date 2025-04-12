using Core.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Infrastructure.Data.Entities
{
    public class Course
    {
        public Course()
        {
            StudentCourses = new List<StudentCourse>();
        }
        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(TeacherId))]
        public Guid TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public Subjects Subject { get; set; }

        public decimal CourseFee { get; set; }

        public ICollection<StudentCourse> StudentCourses { get; set; }

    }
}
