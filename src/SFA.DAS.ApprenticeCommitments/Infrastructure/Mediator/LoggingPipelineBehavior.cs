using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace SFA.DAS.ApprenticeCommitments.Infrastructure.MediatorExtensions
{
    public class LoggingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        public LoggingPipelineBehavior(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                _logger.LogInformation($"Start handling '{typeof(TRequest)}' command");
                var response = await next();
                _logger.LogInformation($"End handling '{typeof(TRequest)}' command");
                return response;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error handling '{typeof(TRequest)}' command");
                throw;
            }
        }
    }
}