using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.Course.CourseDtos
{
    public class AddCourseRequest
    {
        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public Subjects Subject { get; set; }

        public decimal CourseFee { get; set; }

        public string TeacherId { get; set; }
    }
}
