using MediatR;
using SFA.DAS.ApprenticeCommitments.Data;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeCommitments.Application.Commands.VerifyRegistrationCommand
{
    public class VerifyRegistrationCommandHandler : IRequestHandler<VerifyRegistrationCommand>
    {
        private readonly IRegistrationContext _registrations;
        private readonly ApprenticeRepository _apprenticeRepository;

        public VerifyRegistrationCommandHandler(IRegistrationContext registrations, ApprenticeRepository apprenticeRepository)
        {
            _registrations = registrations;
            _apprenticeRepository = apprenticeRepository;
        }

        public async Task<Unit> Handle(VerifyRegistrationCommand command, CancellationToken cancellationToken)
        {
            var registration = await _registrations.GetById(command.RegistrationId);

            var apprentice = registration.ConvertToApprentice(
                command.FirstName, command.LastName,
                new MailAddress(command.Email), command.DateOfBirth,
                command.UserIdentityId);

            await _apprenticeRepository.AddApprenticeDb(apprentice);

            return Unit.Value;
        }
    }
}