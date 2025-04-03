using Core.Application.DTOs.NewFolder;
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
            RuleFor(x => x.FullName)
                .NotNull()
                .MinimumLength(10)
                .WithMessage("نام وارد شده کوتاه تر از حد مجاز است");

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
