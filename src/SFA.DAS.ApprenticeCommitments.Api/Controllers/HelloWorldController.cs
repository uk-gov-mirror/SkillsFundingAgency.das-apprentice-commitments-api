using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SFA.DAS.ApprenticeCommitments.Exceptions;

namespace SFA.DAS.ApprenticeCommitments.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HelloWorldController : ControllerBase
    {
        private readonly ILogger<HelloWorldController> _logger;

        public HelloWorldController(ILogger<HelloWorldController> logger)
        {
            _logger = logger;
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



    }
}
