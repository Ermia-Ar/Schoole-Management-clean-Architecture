using Core.Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Validators
{
    public class SignInRequestValidator : AbstractValidator<SignInRequest>
    {
        public SignInRequestValidator()
        {
            RuleFor(x => x.CodeMelly)
                .MinimumLength(4)
                 .NotNull();

            RuleFor(x => x.Password)
                .MinimumLength(8);
        }
    }
} 
