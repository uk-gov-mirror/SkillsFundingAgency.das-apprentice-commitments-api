using System;
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
        private readonly IRegistrationRepository _registrationRepository;
        private readonly IApprenticeRepository _apprenticeRepository;
        private readonly IApprenticeshipRepository _apprenticeshipRepository;

        public VerifyRegistrationCommandHandler(IRegistrationRepository registrationRepository, IApprenticeRepository apprenticeRepository, IApprenticeshipRepository apprenticeshipRepository)
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

            var apprentice = await AddApprentice(command);
            await AddApprenticeship(apprentice.Id.Value, registration);

            await _registrationRepository.CompleteRegistration(registration.Id, apprentice.Id.Value, command.UserIdentityId);

            return Unit.Value;
        }

        private async Task AddApprenticeship(long apprenticeId, RegistrationModel registration)
        {
            await _apprenticeshipRepository.Add(new ApprenticeshipModel
            {
                ApprenticeId = apprenticeId,
                CommitmentsApprenticeshipId = registration.ApprenticeshipId
            });
        }

        private async Task<ApprenticeModel> AddApprentice(VerifyRegistrationCommand command)
        {
            var apprentice = new ApprenticeModel
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = command.Email,
                UserIdentityId = command.UserIdentityId,
                DateOfBirth = command.DateOfBirth
            };

            return await _apprenticeRepository.Add(apprentice);
        }
    }
}
