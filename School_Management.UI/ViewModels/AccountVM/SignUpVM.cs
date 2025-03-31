using System.ComponentModel.DataAnnotations;

namespace School_Management.UI.ViewModels.AccountVM
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

        //public string returnUrl { get; set; }   
        public bool IsConfirmEmailSend { get; set; }   
    }
}
