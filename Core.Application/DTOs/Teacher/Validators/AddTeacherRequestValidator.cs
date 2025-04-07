using Core.Application.DTOs.Teacher.TeacherDtos;
using FluentValidation;

namespace Core.Application.DTOs.Teacher.Validators
{
    public class AddTeacherRequestValidator : AbstractValidator<AddTeacherRequest>
    {
        public AddTeacherRequestValidator()
        {
            RuleFor(user => user.FullName)
                .MinimumLength(10);

            RuleFor(x => x.Password)
                .MinimumLength(8)
                .WithMessage("حداقل طول رمز عبور 8 کاراکتر است"); 


            RuleFor(user => user.PhoneNumber)
                .Matches("/((0?9)|(\\+?989))\\d{2}\\W?\\d{3}\\W?\\d{4}/g")
                .NotNull();

            RuleFor(user => user.CodeMelly)
                .MinimumLength(10)
                .MaximumLength(10);

            RuleFor(user => user.Birthday)
                .NotNull();
        }
    }
}
