using System;

namespace SFA.DAS.ApprenticeCommitments.Models
{
    public class ApprenticeshipModel
    {
        public long? Id { get; set; }
        public long CommitmentsApprenticeshipId { get; set; }
        public string EmployerName { get; set; }
        public long AccountLegalEntityId { get; set; }
    }
}
