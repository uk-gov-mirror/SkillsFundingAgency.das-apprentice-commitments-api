using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SFA.DAS.ApprenticeCommitments.Data.Models
{
    [Table("Registration")]
    public class Registration
    {
        public Guid Id { get; set; }
        public long ApprenticeshipId { get; set; }
        public string Email { get; set; }
        public Guid? UserId { get; set; }
        public long? ApprenticeId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedOn { get; set; }
    }
}
