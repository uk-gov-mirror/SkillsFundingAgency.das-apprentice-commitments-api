using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.ApprenticeCommitments.Data;
using SFA.DAS.ApprenticeCommitments.Models;

namespace SFA.DAS.ApprenticeCommitments.Application.Commands.CreateRegistrationCommand
{
    public class CreateRegistrationCommandHandler : IRequestHandler<CreateRegistrationCommand>
    {
        private readonly IRegistrationRepository _registrationRepository;

        public CreateRegistrationCommandHandler(IRegistrationRepository registrationRepository)
        {
            _registrationRepository = registrationRepository;
        }
        public async Task<Unit> Handle(CreateRegistrationCommand command, CancellationToken cancellationToken)
        {
            await _registrationRepository.Add(new RegistrationModel {Id = command.RegistrationId, ApprenticeshipId = command.ApprenticeshipId, 
                Email = command.Email, Organisation = command.Organisation });

            return Unit.Value;
        }
    }
}
