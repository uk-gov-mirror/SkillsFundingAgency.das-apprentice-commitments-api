using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.ApprenticeCommitments.Data;

namespace SFA.DAS.ApprenticeCommitments.Application.Commands.RegistrationFirstSeenCommand
{
    public class RegistrationFirstSeenCommandHandler : IRequestHandler<RegistrationFirstSeenCommand>
    {
        private readonly IRegistrationContext _registrations;

        public RegistrationFirstSeenCommandHandler(IRegistrationContext registrations)
        {
            _registrations = registrations;
        }

        public async Task<Unit> Handle(Commands.RegistrationFirstSeenCommand.RegistrationFirstSeenCommand command, CancellationToken _)
        {
            var registration = await _registrations.GetById(command.ApprenticeId);
            registration.ViewedByUser(command.SeenOn);
            return Unit.Value;
        }
    }
}