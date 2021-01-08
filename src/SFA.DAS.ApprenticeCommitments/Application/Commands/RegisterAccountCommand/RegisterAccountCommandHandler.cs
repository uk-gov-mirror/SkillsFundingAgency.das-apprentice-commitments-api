using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.ApprenticeCommitments.Models;

namespace SFA.DAS.ApprenticeCommitments.Application.Commands.RegisterAccountCommand
{
    public class RegisterAccountCommandHandler : IRequestHandler<RegisterAccountCommand>
    {
        private readonly Lazy<ApprenticeCommitmentsDbContext> _dbContext;

        public RegisterAccountCommandHandler(Lazy<ApprenticeCommitmentsDbContext> dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Unit> Handle(RegisterAccountCommand request, CancellationToken cancellationToken)
        {
            var db = _dbContext.Value;

            // TODO add record to table if not already there

            // TODO publish NSB event or command to send email invitation

            return await Task.FromResult(Unit.Value);
        }
    }
}
