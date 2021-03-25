using System;

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
        public bool? EmployerCorrect { get; set; }
        public string CourseName { get; set; }
        public int CourseLevel { get; set; }
        public string CourseOption { get; set; }
        public DateTime PlannedStartDate { get; set; }
        public DateTime PlannedEndDate { get; set; }
        public int Duration { get; set; }
    }
}
