using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ApprenticeCommitments.Application.Commands.VerifyRegistrationCommand;
using SFA.DAS.ApprenticeCommitments.Application.Queries.RegistrationQuery;
using SFA.DAS.ApprenticeCommitments.Application.Queries.RegistrationRemindersQuery;

namespace SFA.DAS.ApprenticeCommitments.Api.Controllers
{
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RegistrationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("registrations/{apprenticeId}")]
        public async Task<IActionResult> GetRegistration(Guid apprenticeId)
        {
            var response = await _mediator.Send(new RegistrationQuery { ApprenticeId = apprenticeId });

            if (response == null)
            {
                return NotFound();
            }
            return new OkObjectResult(response);
        }


        [HttpGet("registrations/reminders")]
        public async Task<IActionResult> GetRegistrationsNeedingReminders(DateTime cutOffDateTime)
        {
            var response = await _mediator.Send(new RegistrationRemindersQuery { CutOffDateTime = cutOffDateTime });

            return new OkObjectResult(response);
        }

        [HttpPost("registrations")]
        public async Task<IActionResult> VerifiedRegistration(VerifyRegistrationCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }
    }
}
