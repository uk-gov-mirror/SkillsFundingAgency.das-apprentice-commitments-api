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

        [HttpPost("apprentices/{id}/email")]
        public async Task<IActionResult> CreateRegistration(long id, ChangeEmailAddressCommand request)
        {
            request.ApprenticeId = id;
            await _mediator.Send(request);
            return Accepted();
        }
    }
}
