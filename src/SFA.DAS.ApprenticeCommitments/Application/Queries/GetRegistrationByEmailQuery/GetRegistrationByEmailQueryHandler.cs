using MediatR;
using Microsoft.Extensions.Logging;
using SFA.DAS.ApprenticeCommitments.Data;
using SFA.DAS.ApprenticeCommitments.Data.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeCommitments.Application.Queries.GetRegistrationByEmailQuery
{
    public class GetRegistrationByEmailQueryHandler : IRequestHandler<GetRegistrationByEmailQuery, List<Registration>>
    {
        private readonly IRegistrationContext _registrationContext;
        private readonly ILogger<GetRegistrationByEmailQueryHandler> _logger;

        public GetRegistrationByEmailQueryHandler(IRegistrationContext registrationContext, ILogger<GetRegistrationByEmailQueryHandler> logger)
        {
            _registrationContext = registrationContext;
            _logger = logger;
        }

        public async Task<List<Registration>> Handle(GetRegistrationByEmailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _registrationContext.FindByEmail(request.Email, cancellationToken);

            if (entity == null || entity.Count == 0) 
            {
                _logger.LogInformation("No registration found for email: {Email}", request.Email);
                return new List<Registration>();
            }            

            return entity;
        }
    }
}
