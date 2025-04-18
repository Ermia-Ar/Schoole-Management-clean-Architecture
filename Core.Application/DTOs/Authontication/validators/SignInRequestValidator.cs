﻿using FluentValidation;


namespace Core.Application.DTOs.Authentication.validators
{
    public class SignInRequestValidator : AbstractValidator<LoginInRequest>
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
