﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SFA.DAS.ApprenticeCommitments.Application.Queries.TestQuery;
using SFA.DAS.ApprenticeCommitments.Exceptions;

namespace SFA.DAS.ApprenticeCommitments.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HelloWorldController : ControllerBase
    {
        private readonly ILogger<HelloWorldController> _logger;
        private readonly IMediator _mediator;


        public HelloWorldController(ILogger<HelloWorldController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Returning 'Hello World' to caller");
            return Ok("Hello World");
        }

        [HttpGet]
        [Route("test-error")]
        public IActionResult TestError()
        {
            _logger.LogInformation("Throwing and exception");

            var errors = new Dictionary<string, string>();
            errors.Add("Error 1", "My first error");
            errors.Add("Error 2", "My second error");

            throw new InvalidRequestException(errors);
        }

        [HttpGet]
        [Route("test-query")]
        public async Task<IActionResult> TestQuery()
        {
            _logger.LogInformation("TestQuery");

            await _mediator.Send(new TestQuery { ApplicationId = Guid.NewGuid() });

            return Ok();
        }
    }
}
