using FluentValidation;

namespace Core.Application.DTOs.Email
{
    public class EmailValidator : AbstractValidator<EmailRequest>
    {
        public EmailValidator()
        {
            RuleFor(x => x.To)
                .EmailAddress();

            RuleFor(x => x.Body)
                .NotNull();

            RuleFor(x => x.Subject)
                .NotNull();
        }
    }
}
