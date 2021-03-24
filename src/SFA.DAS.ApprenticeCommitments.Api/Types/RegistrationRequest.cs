using System;

namespace SFA.DAS.ApprenticeCommitments.Api.Types
{
    public class RegistrationRequest
    {
        public Guid ApprenticeId { get; set; }
        public long ApprenticeshipId { get; set; }
        public string Email { get; set; }
    }
}
