using MediatR;
using SFA.DAS.ApprenticeCommitments.Data;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeCommitments.Application.Commands.CreateRegistrationCommand
{
    public class ChangeEmailAddressCommandHandler : IRequestHandler<ChangeEmailAddressCommand>
    {
        private readonly IApprenticeRepository _apprenticeRepository;

        public ChangeEmailAddressCommandHandler(IApprenticeRepository apprenticeRepository)
            => _apprenticeRepository = apprenticeRepository;

        public async Task<Unit> Handle(ChangeEmailAddressCommand command, CancellationToken cancellationToken)
        {
            await _apprenticeRepository.ChangeEmailAddress(command.ApprenticeId, command.Email);
            return Unit.Value;
        }
    }
}
