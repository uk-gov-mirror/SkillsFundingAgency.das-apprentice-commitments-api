using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ApprenticeCommitments.Application.Commands.ConfirmRolesAndResponsibilitiesCommand;
using System;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeCommitments.Api.Controllers
{
    public class ConfirmRolesAndResponsibilitiesRequest
    {
        public bool RolesAndResponsibilitiesCorrect { get; set; }
    }

    [ApiController]
    public class ConfirmRolesAndResponsibilitiesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConfirmRolesAndResponsibilitiesController(IMediator mediator) => _mediator = mediator;

        [HttpPost("apprentices/{apprenticeId}/apprenticeships/{apprenticeshipId}/RolesAndResponsibilitiesConfirmation")]
        public async Task<IActionResult> ConfirmTrainingProvider(
            Guid apprenticeId, long apprenticeshipId,
            [FromBody] ConfirmRolesAndResponsibilitiesRequest request)
        {
            var command = new ConfirmRolesAndResponsibilitiesCommand(apprenticeId, apprenticeshipId, request.RolesAndResponsibilitiesCorrect);
            await _mediator.Send(command);
            return Ok();
        }
    }
}
