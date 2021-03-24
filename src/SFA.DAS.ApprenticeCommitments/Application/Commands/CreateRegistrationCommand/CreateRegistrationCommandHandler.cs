using MediatR;
using SFA.DAS.ApprenticeCommitments.Data;
using SFA.DAS.ApprenticeCommitments.Data.Models;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeCommitments.Application.Commands.CreateRegistrationCommand
{
    public class CreateRegistrationCommandHandler : IRequestHandler<CreateRegistrationCommand>
    {
        private readonly IRegistrationContext _registrations;

        public CreateRegistrationCommandHandler(IRegistrationContext registrations)
            => _registrations = registrations;

        public async Task<Unit> Handle(CreateRegistrationCommand command, CancellationToken cancellationToken)
        {
            await _registrations.AddAsync(new Registration(
                command.ApprenticeId,
                command.ApprenticeshipId,
                new MailAddress(command.Email),
                command.EmployerName,
                command.EmployerAccountLegalEntityId,
                command.TrainingProviderId,
                command.TrainingProviderName));

            return Unit.Value;
        }
    }
}