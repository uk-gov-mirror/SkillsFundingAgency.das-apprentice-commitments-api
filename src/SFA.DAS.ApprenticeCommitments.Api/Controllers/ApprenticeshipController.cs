using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ApprenticeCommitments.Api.Types;
using SFA.DAS.ApprenticeCommitments.Application.Commands.CreateRegistrationCommand;
using SFA.DAS.ApprenticeCommitments.Application.Commands.CreateRegistrationCommand3;

namespace SFA.DAS.ApprenticeCommitments.Api.Controllers
{
    [ApiController]
    public class ApprenticeshipController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ApprenticeshipController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("apprenticeships")]
        public async Task<IActionResult> CreateRegistration([FromBody] CreateRegistrationCommand request)
        {
            await _mediator.Send(new CreateRegistrationCommand3 { RegistrationId = Guid.NewGuid(), ApprenticeshipId = 100, Email = "command3@command.com"});
            await _mediator.Send(request);
            return Accepted();
        }
    }
}
