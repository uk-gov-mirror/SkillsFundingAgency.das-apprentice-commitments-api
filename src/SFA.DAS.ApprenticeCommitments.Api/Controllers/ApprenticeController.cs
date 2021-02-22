using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ApprenticeCommitments.Application.Commands.CreateRegistrationCommand;

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

        [HttpPost("apprentices")]
        public async Task<IActionResult> CreateRegistration([FromBody] ChangeEmailAddressCommand request)
        {
            await _mediator.Send(request);
            return Accepted();
        }
    }
}
