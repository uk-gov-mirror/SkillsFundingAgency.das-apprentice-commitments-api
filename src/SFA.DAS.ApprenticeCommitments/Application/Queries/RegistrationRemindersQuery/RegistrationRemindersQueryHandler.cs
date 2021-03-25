using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.ApprenticeCommitments.Data;
using SFA.DAS.ApprenticeCommitments.DTOs;

namespace SFA.DAS.ApprenticeCommitments.Application.Queries.RegistrationRemindersQuery
{
    public class RegistrationRemindersQueryHandler : IRequestHandler<RegistrationRemindersQuery, List<RegistrationDto>>
    {
        private readonly IRegistrationContext _registrations;

        public RegistrationRemindersQueryHandler(IRegistrationContext registrations)
            => _registrations = registrations;

        public async Task<List<RegistrationDto>> Handle(RegistrationRemindersQuery query, CancellationToken _)
        {
            var reminders = await _registrations.RegistrationsNeedingSignUpReminders(query.CutOffDateTime);
            return reminders.Select(x=>x.MapToRegistrationDto()).ToList();
        }
    }
}