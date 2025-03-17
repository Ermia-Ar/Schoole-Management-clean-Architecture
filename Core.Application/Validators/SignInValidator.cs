using Core.Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Validators
{
    public class SignInValidator : AbstractValidator<SignInRequest>
    {
        public SignInValidator()
        {
            RuleFor(x => x.Email_UserName)
                .MinimumLength(4)
                 .NotNull();

            RuleFor(x => x.Password)
                .MinimumLength(8);
        }
    }
} 
