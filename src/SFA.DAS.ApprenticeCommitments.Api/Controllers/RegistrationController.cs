using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ApprenticeCommitments.Application.Queries.RegistrationQuery;

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

        [HttpGet("registrations/{registrationId}")]
        public async Task<IActionResult> GetRegistration(Guid registrationId)
        {
            var response = await _mediator.Send(new RegistrationQuery { RegistrationId = registrationId });

            if (response == null)
            {
                return NotFound();
            }
            return new OkObjectResult(response);
        }
    }
}
