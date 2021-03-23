using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ApprenticeCommitments.Application.Commands.ConfirmTrainingProviderCommand;
using System;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeCommitments.Api.Controllers
{
    public class ConfirmTrainingProviderRequest
    {
        public bool TrainingProviderCorrect { get; set; }
    }

    [ApiController]
    public class ConfirmTrainingProviderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConfirmTrainingProviderController(IMediator mediator) => _mediator = mediator;

        [HttpPost("apprentices/{apprenticeId}/apprenticeships/{apprenticeshipId}/TrainingProviderConfirmation")]
        public async Task<IActionResult> ConfirmTrainingProvider(
            Guid apprenticeId, long apprenticeshipId,
            [FromBody] ConfirmTrainingProviderRequest request)
        {
            var command = new ConfirmTrainingProviderCommand(apprenticeId, apprenticeshipId, request.TrainingProviderCorrect);
            await _mediator.Send(command);
            return Ok();
        }
    }
}