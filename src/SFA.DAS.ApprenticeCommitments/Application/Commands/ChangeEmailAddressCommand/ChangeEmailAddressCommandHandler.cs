using MediatR;
using SFA.DAS.ApprenticeCommitments.Data;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeCommitments.Application.Commands.CreateRegistrationCommand
{
    public class ChangeEmailAddressCommandHandler : IRequestHandler<ChangeEmailAddressCommand>
    {
        private readonly ApprenticeRepository _apprenticeRepository;

        public ChangeEmailAddressCommandHandler(ApprenticeRepository apprenticeRepository)
            => _apprenticeRepository = apprenticeRepository;

        public async Task<Unit> Handle(ChangeEmailAddressCommand command, CancellationToken cancellationToken)
        {
            var apprentice = await _apprenticeRepository.GetById(command.ApprenticeId);
            apprentice.UpdateEmail(new MailAddress(command.Email));
            return Unit.Value;
        }
    }
}