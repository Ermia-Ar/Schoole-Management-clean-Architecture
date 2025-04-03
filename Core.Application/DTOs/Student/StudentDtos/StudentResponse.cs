using Core.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
