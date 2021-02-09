using FluentValidation;

namespace SFA.DAS.ApprenticeCommitments.Application.Commands.VerifyRegistrationCommand
{
    public class VerifyRegistrationCommandValidator : AbstractValidator<VerifyRegistrationCommand>
    {
        public VerifyRegistrationCommandValidator()
        {
            RuleFor(model => model.RegistrationId).Must(id => id != default).WithMessage("The Registration Id must be valid");
            RuleFor(model => model.UserId).Must(id => id != default).WithMessage("The User Id must be valid");
            RuleFor(model => model.FirstName).NotEmpty().WithMessage("The First name is required");
            RuleFor(model => model.LastName).NotEmpty().WithMessage("The Last name is required");
            RuleFor(model => model.DateOfBirth).Must(dob => dob != default).WithMessage("Date of birth is required");
            RuleFor(model => model.Email).NotEmpty().EmailAddress().WithMessage("A valid email address is required");
        }
    }
}
