using MediatR;
using Microsoft.Extensions.Logging;
using SFA.DAS.ApprenticeCommitments.Data;
using SFA.DAS.ApprenticeCommitments.Data.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeCommitments.Application.Queries.GetRegistrationsByAccountDetails
{
    public class GetRegistrationByAccountDetailsQueryHandler : IRequestHandler<GetRegistrationByAccountDetailsQuery, List<Registration>>
    {
        private readonly IRegistrationContext _registrationContext;
        private readonly ILogger<GetRegistrationByAccountDetailsQueryHandler> _logger;

        public GetRegistrationByAccountDetailsQueryHandler(IRegistrationContext registrationContext, ILogger<GetRegistrationByAccountDetailsQueryHandler> logger)
        {
            _registrationContext = registrationContext;
            _logger = logger;
        }

        public async Task<List<Registration>> Handle(
            GetRegistrationByAccountDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await _registrationContext.FindByAccountDetails(
                request.FirstName,
                request.LastName,
                request.DateOfBirth,
                cancellationToken);

            if (entity == null || entity.Count == 0) 
            {
                _logger.LogInformation("No registrations found for FirstName: {FirstName}, LastName: {LastName}, DateOfBirth: {DateOfBirth}", request.FirstName, request.LastName, request.DateOfBirth);
                return new List<Registration>(); 
            }

            return entity;
        }
    }
}
