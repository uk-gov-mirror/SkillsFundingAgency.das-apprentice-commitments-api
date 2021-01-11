using System;

namespace SFA.DAS.ApprenticeCommitments.Api.Types
{
    public class RegistrationRequest
    {
        public Guid RegistrationId { get; set; }
        public long ApprenticeshipId { get; set; }
        public string Email { get; set; }
    }
}
