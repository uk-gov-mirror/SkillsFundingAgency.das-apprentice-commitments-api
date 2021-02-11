using System;
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
        public DateTime DateOfBirth { get; set; }
    }
}
