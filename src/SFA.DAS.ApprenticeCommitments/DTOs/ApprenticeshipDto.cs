namespace SFA.DAS.ApprenticeCommitments.DTOs
{
    public class ApprenticeshipDto
    {
        public long? Id { get; set; }
        public long CommitmentsApprenticeshipId { get; set; }
        public string EmployerName { get; set; }
        public long EmployerAccountLegalEntityId { get; set; }
        public long TrainingProviderId { get; internal set; }
        public string TrainingProviderName { get; set; }
        public bool? TrainingProviderCorrect { get; set; }
    }
}
