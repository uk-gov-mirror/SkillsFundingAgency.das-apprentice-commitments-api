using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SFA.DAS.ApprenticeCommitments.Data.Models
{
    [Table("Registration")]
    public class Registration
    {
        public Guid Id { get; set; }
        public Guid? ApprenticeId { get; set; }
        public long ApprenticeshipId { get; set; }
        public string Email { get; set; }
        public string EmployerName { get; set; }
        public long EmployerAccountLegalEntityId { get; set; }
        public Guid? UserIdentityId { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string TrainingProviderName { get; set; }
    }
}