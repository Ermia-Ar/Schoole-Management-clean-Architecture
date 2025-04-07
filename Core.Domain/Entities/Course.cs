namespace Core.Domain.Entities
{
    public class Course
    {

        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public Subjects Subject { get; set; } 

        public decimal CourseFee { get; set; }

        public TeacherCourse TeacherCourse { get; set; }
    }
}
