using Core.Application.DTOs.Authontication;
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
                .MaximumLength(10)
                .WithMessage("تعداد رقم وارد شده بیش از 10 است ")
                .MinimumLength(10)
                .WithMessage("تعداد رقم وارد شده کمتر از 10 است");

            RuleFor(x => x.Password)
                .MinimumLength(8)
                .WithMessage("حداقل طول رمز عبور 8 کاراکتر است");
        }
    }
} 
