﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.ApprenticeCommitments.Data;
using SFA.DAS.ApprenticeCommitments.Models;

namespace SFA.DAS.ApprenticeCommitments.Application.Queries.RegistrationQuery
{
    public class RegistrationQueryHandler : IRequestHandler<RegistrationQuery, RegistrationResponse>
    {
        private readonly IRegistrationRepository _registrationRepository;

        public RegistrationQueryHandler(IRegistrationRepository registrationRepository)
        {
            _registrationRepository = registrationRepository;
        }

        public async Task<RegistrationResponse> Handle(RegistrationQuery query, CancellationToken cancellationToken)
        {
            var model = await _registrationRepository.Get(query.RegistrationId);

            return Map(model); 
        }

        private RegistrationResponse Map(RegistrationModel model)
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
