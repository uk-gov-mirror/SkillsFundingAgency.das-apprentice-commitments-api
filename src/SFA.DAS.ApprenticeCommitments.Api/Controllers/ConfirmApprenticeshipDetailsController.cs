using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ApprenticeCommitments.Application.Commands.ConfirmApprenticeshipDetailsCommand;
using System;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeCommitments.Api.Controllers
{
    public class ConfirmApprenticeshipDetailsRequest
    {
        public bool ApprenticeshipDetailsCorrect { get; set; }
    }

    [ApiController]
    public class ConfirmApprenticeshipDetailsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConfirmApprenticeshipDetailsController(IMediator mediator) => _mediator = mediator;

        [HttpPost("apprentices/{apprenticeId}/apprenticeships/{apprenticeshipId}/apprenticeshipdetailsconfirmation")]
        public async Task<IActionResult> ConfirmTrainingProvider(
            Guid apprenticeId, long apprenticeshipId,
            [FromBody] ConfirmApprenticeshipDetailsRequest request)
        {
            var command = new ConfirmApprenticeshipDetailsCommand(apprenticeId, apprenticeshipId, request.ApprenticeshipDetailsCorrect);
            await _mediator.Send(command);
            return Ok();
        }
    }
}