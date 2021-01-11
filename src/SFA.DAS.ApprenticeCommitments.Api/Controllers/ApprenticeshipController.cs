using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ApprenticeCommitments.Api.Types;
using SFA.DAS.ApprenticeCommitments.Application.Commands.CreateRegistrationCommand;

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
        public async Task<IActionResult> CreateRegistration([FromBody] RegistrationRequest request)
        {
            await _mediator.Send(new CreateRegistrationCommand {RegistrationId = request.RegistrationId, ApprenticeshipId = request.ApprenticeshipId, Email = request.Email});
            return Accepted();
        }
    }
}
