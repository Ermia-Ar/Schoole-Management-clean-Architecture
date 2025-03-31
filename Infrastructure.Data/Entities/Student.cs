using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Identity.Models;

namespace Infrastructure.Data.Entities
{
    public class Student
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(450)]
        public string ApplicationUserId { get; set; }

        public Grade Grade { get; set; }

        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();

    }

    public enum Grade
    {
        First,
        Second,
        Third,
    }
}
