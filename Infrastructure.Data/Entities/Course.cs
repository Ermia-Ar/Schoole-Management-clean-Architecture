using Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Entities
{
    public class Course
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public Subjects Subject { get; set; } // مثلا Math, Chemistry, Physics

        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
        public ICollection<TeacherCourse> TeacherCourses { get; set; } = new List<TeacherCourse>();

    }


}
