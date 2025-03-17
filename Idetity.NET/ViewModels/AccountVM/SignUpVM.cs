using System.ComponentModel.DataAnnotations;

namespace Identity.NET.ViewModels.AccountVM
{
    public class SignUpVM
    {
        public string UserName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string RePassword { get; set; }
    }
}
