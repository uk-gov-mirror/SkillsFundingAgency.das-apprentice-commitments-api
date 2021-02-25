using FluentValidation;

namespace SFA.DAS.ApprenticeCommitments.Application.Commands.CreateRegistrationCommand
{
    public class ChangeEmailAddressCommandValidator : AbstractValidator<ChangeEmailAddressCommand>
    {
        public ChangeEmailAddressCommandValidator()
        {
            RuleFor(model => model.ApprenticeId).Must(id => id > 0).WithMessage("The ApprenticeId must be positive");
            RuleFor(model => model.Email).NotNull().EmailAddress().WithMessage("Email must be a valid email address");
        }
    }
}
