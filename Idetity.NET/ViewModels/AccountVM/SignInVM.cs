using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Identity.NET.ViewModels.AccountVM
{
    public class SignInVM
    {
        public string Email_UserName;

        [DataType(DataType.Password)]
        public string Password;

        [DataType(DataType.Password)]
        public bool RemeberMe;


    }
}
