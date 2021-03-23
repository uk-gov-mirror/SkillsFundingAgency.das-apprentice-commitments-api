using System;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.ApprenticeCommitments.Data;
using SFA.DAS.ApprenticeCommitments.Exceptions;
using SFA.DAS.ApprenticeCommitments.Models;

namespace SFA.DAS.ApprenticeCommitments.Application.Commands.VerifyRegistrationCommand
{
    public class VerifyRegistrationCommandHandler : IRequestHandler<VerifyRegistrationCommand>
    {
        private readonly RegistrationRepository _registrationRepository;
        private readonly ApprenticeRepository _apprenticeRepository;
        private readonly ApprenticeshipRepository _apprenticeshipRepository;

        public VerifyRegistrationCommandHandler(RegistrationRepository registrationRepository, ApprenticeRepository apprenticeRepository, ApprenticeshipRepository apprenticeshipRepository)
        {
            _registrationRepository = registrationRepository;
            _apprenticeRepository = apprenticeRepository;
            _apprenticeshipRepository = apprenticeshipRepository;
        }
        public async Task<Unit> Handle(VerifyRegistrationCommand command, CancellationToken cancellationToken)
        {
            
            var registration = await _registrationRepository.Get(command.RegistrationId);

            if (registration == null)
            {
                throw new DomainException($"Registration {command.RegistrationId} not found");
            }

            if (registration.HasBeenCompleted)
            {
                throw new DomainException("Already verified");
            }

            // Verify Email matches incoming email
            if (!command.Email.Equals(registration.Email, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new DomainException("Email from Verifying user doesn't match registered user");
            }

            var apprentice = await AddApprentice(command, registration);

            await _registrationRepository.CompleteRegistration(registration.Id, command.UserIdentityId);

            return Unit.Value;
        }

        private async Task<ApprenticeModel> AddApprentice(VerifyRegistrationCommand command, RegistrationModel registration)
        {
            var apprentice = new ApprenticeModel
            {
                UserIdentityId = command.RegistrationId,
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = new MailAddress(command.Email),
                DateOfBirth = command.DateOfBirth
            };

            var apprenticeship = new ApprenticeshipModel
            {
                CommitmentsApprenticeshipId = registration.ApprenticeshipId,
                EmployerName = registration.EmployerName,
                EmployerAccountLegalEntityId = registration.EmployerAccountLegalEntityId,
                TrainingProviderId = registration.TrainingProviderId,
                TrainingProviderName = registration.TrainingProviderName,
            };

            return await _apprenticeRepository.AddApprentice(apprentice, apprenticeship);
        }
    }
}
