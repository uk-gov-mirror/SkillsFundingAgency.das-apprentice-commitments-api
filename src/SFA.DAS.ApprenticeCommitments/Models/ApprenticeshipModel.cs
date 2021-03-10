using System;

namespace SFA.DAS.ApprenticeCommitments.Models
{
    public class ApprenticeshipModel
    {
        public long? Id { get; set; }
        public long CommitmentsApprenticeshipId { get; set; }
        public string Organisation { get; set; }
    }
}
