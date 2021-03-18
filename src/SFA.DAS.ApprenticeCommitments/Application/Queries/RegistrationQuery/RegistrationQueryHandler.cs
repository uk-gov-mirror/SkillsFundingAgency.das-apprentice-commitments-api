using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.ApprenticeCommitments.Data;
using SFA.DAS.ApprenticeCommitments.Models;

namespace SFA.DAS.ApprenticeCommitments.Application.Queries.RegistrationQuery
{
    public class RegistrationQueryHandler : IRequestHandler<RegistrationQuery, RegistrationResponse>
    {
        private readonly RegistrationRepository _registrationRepository;

        public RegistrationQueryHandler(RegistrationRepository registrationRepository)
        {
            _registrationRepository = registrationRepository;
        }

        public async Task<RegistrationResponse> Handle(RegistrationQuery query, CancellationToken cancellationToken)
        {
            var model = await _registrationRepository.Get(query.RegistrationId);

            return Map(model); 
        }

        private RegistrationResponse Map(RegistrationDto model)
        {
            if (model == null)
            {
                return null;
            }

            return new RegistrationResponse
            {
                RegistrationId = model.Id,
                Email = model.Email
            };
        }
    }
}
