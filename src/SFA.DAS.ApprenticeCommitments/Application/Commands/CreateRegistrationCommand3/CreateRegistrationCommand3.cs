using System;
using MediatR;
using SFA.DAS.ApprenticeCommitments.Infrastructure.Mediator;

namespace SFA.DAS.ApprenticeCommitments.Application.Commands.CreateRegistrationCommand3
{
    public class CreateRegistrationCommand3 : IRequest
    {
        public Guid RegistrationId { get; set; }
        public long ApprenticeshipId { get; set; }
        public string Email { get; set; }
    }
}
