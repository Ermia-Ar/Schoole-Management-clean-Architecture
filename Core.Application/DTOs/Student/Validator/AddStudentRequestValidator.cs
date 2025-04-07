using Core.Application.DTOs.Student.StudentDtos;
using FluentValidation;

namespace Core.Application.DTOs.Student.Validator
{
    public class AddStudentRequestValidator : AbstractValidator<AddStudentRequest>
    {
        public AddStudentRequestValidator()
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
