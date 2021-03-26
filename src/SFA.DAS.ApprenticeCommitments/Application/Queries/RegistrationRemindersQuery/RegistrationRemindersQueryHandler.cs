using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.ApprenticeCommitments.Data;
using SFA.DAS.ApprenticeCommitments.DTOs;

namespace SFA.DAS.ApprenticeCommitments.Application.Queries.RegistrationRemindersQuery
{
    public class RegistrationRemindersQueryHandler : IRequestHandler<RegistrationRemindersQuery, RegistrationRemindersResponse>
    {
        private readonly IRegistrationContext _registrations;

        public RegistrationRemindersQueryHandler(IRegistrationContext registrations)
            => _registrations = registrations;

        public async Task<RegistrationRemindersResponse> Handle(RegistrationRemindersQuery query, CancellationToken _)
        {
            var reminders = await _registrations.RegistrationsNeedingSignUpReminders(query.CutOffDateTime);
            return new RegistrationRemindersResponse(reminders.Select(x=>x.MapToRegistrationDto()));
        }
    }
}