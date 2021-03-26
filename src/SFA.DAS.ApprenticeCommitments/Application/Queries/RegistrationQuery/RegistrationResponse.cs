using System;

namespace SFA.DAS.ApprenticeCommitments.Application.Queries.RegistrationQuery
{
    public class RegistrationResponse
    {
        public Guid ApprenticeId { get; set; }
        public string Email { get; set; }
        public bool HasViewedVerification { get; set; }
    }
}
