﻿using Core.Domain;
using System.ComponentModel.DataAnnotations;

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

        public string FullName { get; set; }

        public DateTime Birthday { get; set; }

        public string CodeMelly { get; set; }

        public string PhoneNumber { get; set; }

        public Gender Gender { get; set; }

        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();

    }
}
