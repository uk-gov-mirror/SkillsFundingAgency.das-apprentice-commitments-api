using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ApprenticeCommitments.Application.Commands.ConfirmApprenticeshipCommand;
using System;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeCommitments.Api.Controllers
{
    public class ConfirmApprenticeshipRequest
    {
        public bool ApprenticeshipCorrect { get; set; }
    }

    public class ConfirmApprenticeshipController : Controller
    {
        private readonly IMediator _mediator;

        public ConfirmApprenticeshipController(IMediator mediator) => _mediator = mediator;

        [HttpPost("apprentices/{apprenticeId}/apprenticeships/{apprenticeshipId}/ApprenticeshipConfirmation")]
        public async Task<IActionResult> ConfirmTrainingProvider(
            Guid apprenticeId, long apprenticeshipId,
            [FromBody] ConfirmApprenticeshipRequest request)
        {
            var command = new ConfirmApprenticeshipCommand(apprenticeId, apprenticeshipId, request.ApprenticeshipCorrect);
            await _mediator.Send(command);
            return Ok();
        }
    }
}
