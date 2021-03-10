using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ApprenticeCommitments.Application.Commands.CreateRegistrationCommand;
using SFA.DAS.ApprenticeCommitments.Application.Queries.ApprenticeQuery;
using System;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeCommitments.Api.Controllers
{
    [ApiController]
    public class ApprenticesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ApprenticesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("apprentices/{id}/apprenticeships")]
        public async Task<IActionResult> GetApprentice(Guid id)
        {
            var result = await _mediator.Send(new ApprenticeshipsQuery(id));
            return Ok(result);
        }

        [HttpPost("apprentices/{id}/email")]
        public async Task<IActionResult> CreateRegistration(Guid id, ChangeEmailAddressCommand request)
        {
            request.ApprenticeId = id;
            await _mediator.Send(request);
            return Accepted();
        }
    }
}
