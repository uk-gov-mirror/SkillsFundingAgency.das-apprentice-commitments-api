using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SFA.DAS.ApprenticeCommitments.Data.Models
{
    [Table("Apprentice")]
    public class Apprentice
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid UserIdentityId { get; set; }
        public string Email { get; set; }
        public ICollection<ApprenticeEmailHistory> PreviousEmails { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime CreatedOn { get; private set; } = DateTime.UtcNow;

        internal void UpdateEmail(string email)
        {
            Email = email;
            PreviousEmails.Add(new ApprenticeEmailHistory(email));
        }
    }

    public class ApprenticeEmailHistory
    {
        private ApprenticeEmailHistory()
        {
        }

        public ApprenticeEmailHistory(string emailAddress)
            => EmailAddress = emailAddress;

        public string EmailAddress { get; private set; }
        public DateTime ChangedOn { get; private set; } = DateTime.UtcNow;
    }
}