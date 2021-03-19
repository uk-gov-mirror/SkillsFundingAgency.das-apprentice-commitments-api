﻿using MediatR;
using SFA.DAS.ApprenticeCommitments.Data;
using SFA.DAS.ApprenticeCommitments.Data.Models;
using SFA.DAS.ApprenticeCommitments.Exceptions;
using SFA.DAS.ApprenticeCommitments.Models;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

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
            var registration = await _registrationRepository.GetDb(command.RegistrationId);

            if (registration == null)
                throw new DomainException($"Registration {command.RegistrationId} not found");

            registration.Verify(
                command.FirstName, command.LastName,
                new MailAddress(command.Email), command.DateOfBirth,
                command.UserIdentityId);

            await _apprenticeRepository.AddApprenticeDb(registration.Apprentice);

            return Unit.Value;
        }

        private async Task AddApprentice(VerifyRegistrationCommand command, RegistrationDto registration)
        {
            var apprentice = new Apprentice(
                command.RegistrationId,
                command.FirstName,
                command.LastName,
                new MailAddress(command.Email),
                command.DateOfBirth);

            apprentice.AddApprenticeship(new Apprenticeship(
                registration.ApprenticeshipId,
                registration.EmployerAccountLegalEntityId,
                registration.EmployerName,
                registration.TrainingProviderId,
                registration.TrainingProviderName
                                                           ));

            await _apprenticeRepository.AddApprenticeDb(apprentice);
        }
    }
}