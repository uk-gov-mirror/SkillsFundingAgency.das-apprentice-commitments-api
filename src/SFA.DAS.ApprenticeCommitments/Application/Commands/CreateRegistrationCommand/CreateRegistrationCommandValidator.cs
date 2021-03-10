using FluentValidation;

namespace SFA.DAS.ApprenticeCommitments.Application.Commands.CreateRegistrationCommand
{
    public class CreateRegistrationCommandValidator : AbstractValidator<CreateRegistrationCommand>
    {
        public CreateRegistrationCommandValidator()
        {
            RuleFor(model => model.RegistrationId).Must(id => id != default).WithMessage("The Registration Id must be valid");
            RuleFor(model => model.ApprenticeshipId).Must(id => id > 0).WithMessage("The ApprenticeshipId must be positive");
            RuleFor(model => model.AccountLegalEntityId).Must(id => id > 0).WithMessage("The AccountLegalEntityId must be positive");
            RuleFor(model => model.Email).NotNull().EmailAddress().WithMessage("Email must be a valid email address");
            RuleFor(model => model.EmployerName).NotEmpty().WithMessage("The Employer Name is required");
        }
    }
}
