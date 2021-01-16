using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.ApprenticeCommitments.Data;
using SFA.DAS.ApprenticeCommitments.Models;

namespace SFA.DAS.ApprenticeCommitments.Application.Commands.CreateRegistrationCommand3
{
    public class CreateRegistrationCommand3Handler : IRequestHandler<CreateRegistrationCommand3>
    {
        private readonly IRegistrationRepository _registrationRepository;

        public CreateRegistrationCommand3Handler(IRegistrationRepository registrationRepository)
        {
            _registrationRepository = registrationRepository;
        }
        public async Task<Unit> Handle(CreateRegistrationCommand3 command, CancellationToken cancellationToken)
        {
            await _registrationRepository.Add(new RegistrationModel {Id = command.RegistrationId, ApprenticeshipId = command.ApprenticeshipId, Email = command.Email});

            // TODO publish NSB event or command to send email invitation

            return Unit.Value;
        }
    }
}
