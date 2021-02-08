namespace SFA.DAS.ApprenticeCommitments.Models
{
    public class ApprenticeshipModel
    {
        public long? Id { get; set; }
        public long ApprenticeId { get; set; }
        public long CommitmentsApprenticeshipId { get; set; }
    }
}
