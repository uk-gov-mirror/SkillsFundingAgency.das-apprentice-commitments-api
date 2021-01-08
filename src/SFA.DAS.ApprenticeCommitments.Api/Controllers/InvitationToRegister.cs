using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ApprenticeCommitments.Api.Types;
using SFA.DAS.ApprenticeCommitments.Application.Commands.RegisterAccountCommand;

namespace SFA.DAS.ApprenticeCommitments.Api.Controllers
{
    [ApiController]
    public class InvitationToRegisterController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InvitationToRegisterController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("/register-account")]
        public async Task<IActionResult> RegisterAccount([FromBody] RegisterAccountRequest request)
        {
            await _mediator.Send(new RegisterAccountCommand {ApprenticeshipId = request.ApprenticeshipId, Email = request.Email});
            return Accepted();
        }

    }
}
