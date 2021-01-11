using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SFA.DAS.ApprenticeCommitments.Models
{
    public class RegistrationModel
    {
        public Guid Id { get; set; }
        public long ApprenticeshipId { get; set; }
        public string Email { get; set; }
        public DateTime? CreatedOn { get; private set; }
    }
}
