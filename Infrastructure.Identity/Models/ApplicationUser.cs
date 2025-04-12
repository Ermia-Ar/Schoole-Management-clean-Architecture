using Core.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity.Models
{
    [Index(nameof(CodeMelly), IsUnique = true)]
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            UserRefreshTokens = new HashSet<UserRefreshToken>();
        }
        public override string? UserName { get; set; }

        public override string? Email { get; set; }

        public DateTime Birthday { get; set; }

        public string FullName { get; set; }

        public string CodeMelly { get; set; }

        public Gender Gender { get; set; }

        public ICollection<UserRefreshToken> UserRefreshTokens { get; set; }

    }
}



