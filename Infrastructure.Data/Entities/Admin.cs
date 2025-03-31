using Infrastructure.Identity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Entities
{
    public class Admin
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(450)]
        public string ApplicationUserId { get; set; }

        public decimal Salary { get; set; }
    }
}
