using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Models
{
    [Index(nameof(CodeMelly), IsUnique = true)]
    public class ApplicationUser : IdentityUser
    {
        public DateTime Birthday { get; set; }

        public string FullName { get; set; }
        
        public string CodeMelly { get; set; }

        public Gender Gender { get; set; }
    }

    public  enum Gender
    {
        Male ,
        Female
    }

   
}