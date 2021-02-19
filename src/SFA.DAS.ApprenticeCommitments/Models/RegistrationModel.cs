using System;

namespace SFA.DAS.ApprenticeCommitments.Models
{
    public class RegistrationModel
    {
        public Guid Id { get; set; }
        public long ApprenticeshipId { get; set; }
        public string Email { get; set; }
        public DateTime? CreatedOn { get; private set; }
        public Guid? UserIdentityId { get; set; }
        public long? ApprenticeId { get; set; }

        public bool HasBeenCompleted => UserIdentityId != null;
    }
}
