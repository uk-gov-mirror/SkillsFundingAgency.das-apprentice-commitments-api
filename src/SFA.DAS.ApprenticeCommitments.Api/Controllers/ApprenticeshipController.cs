﻿using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ApprenticeCommitments.Application.Commands.CreateRegistrationCommand;
using SFA.DAS.ApprenticeCommitments.Application.Queries.ApprenticeshipQuery;

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
            await _mediator.Send(request);
            return Accepted();
        }

        [HttpGet("apprentices/{apprenticeId}/apprenticeships/{apprenticeshipId}")]
        public async Task<IActionResult> GetApprenticeship(Guid apprenticeId, long apprenticeshipId)
        {
            var result = await _mediator.Send(new ApprenticeshipQuery(apprenticeId, apprenticeshipId));
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
