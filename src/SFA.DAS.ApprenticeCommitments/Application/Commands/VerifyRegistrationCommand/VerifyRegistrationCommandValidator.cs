using FluentValidation;

namespace SFA.DAS.ApprenticeCommitments.Application.Commands.VerifyRegistrationCommand
{
    public class VerifyRegistrationCommandValidator : AbstractValidator<VerifyRegistrationCommand>
    {
        public VerifyRegistrationCommandValidator()
        {
            RuleFor(model => model.RegistrationId).Must(id => id != default).WithMessage("The Registration Id must be valid");
            RuleFor(model => model.UserId).Must(id => id != default).WithMessage("The User Id must be valid");
            RuleFor(model => model.FirstName).NotNull().NotEmpty().WithMessage("The First name is required");
            RuleFor(model => model.LastName).NotNull().NotEmpty().WithMessage("The Last name is required");
            RuleFor(model => model.DateOfBirth).NotNull().WithMessage("Date of birth is required");
            RuleFor(model => model.Email).NotNull().NotEmpty().EmailAddress().WithMessage("A valid email address is required");
        }
    }
}
