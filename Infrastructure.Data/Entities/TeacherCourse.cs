using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Entities
{
    public class TeacherCourse
    {
        public Guid TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public Guid CourseId { get; set; }
        public Course Course { get; set; }
    }
}
