using Core.Application.DTOs;
using FluentValidation;
using FluentValidation.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Validators
{
    public class SignUpRequestValidator : AbstractValidator<SignUpRequest>
    {
        public SignUpRequestValidator()
        {
            RuleFor(x => x.UserName)
                .NotNull()
                .MinimumLength(4);

            RuleFor(x => x.CodeMelly)
                .NotNull()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotNull()
                .MinimumLength(8);
        }
    }
}
