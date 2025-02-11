﻿using System;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SFA.DAS.ApprenticeCommitments.Infrastructure.Mediator;

namespace SFA.DAS.ApprenticeCommitments.UnitTests.Infrastructure.MediatorTests
{
    public class WhenLoggingTheMediatorPipeline
    {
        private Mock<ILogger<SimpleRequest>> _loggerMock;
        private LoggingPipelineBehavior<SimpleRequest, SimpleResponse> _sut;

        [SetUp]
        public void Arrange()
        {
            _loggerMock = new Mock<ILogger<SimpleRequest>>();
            _sut = new LoggingPipelineBehavior<SimpleRequest, SimpleResponse>(_loggerMock.Object);
        }

        [Test, AutoData]
        public async Task Then_we_call_delegator_and_return_response(SimpleRequest request, SimpleResponse expectedResponse)
        {
            var response = await _sut.Handle(request, CancellationToken.None, () => Task.FromResult(expectedResponse));

            response.Should().Be(expectedResponse);
        }

        [Test, AutoData]
        public async Task Then_we_log_the_handler_is_starting(SimpleRequest request, SimpleResponse expectedResponse)
        {
            await _sut.Handle(request, CancellationToken.None, () => Task.FromResult(expectedResponse));

            _loggerMock.VerifyLog(LogLevel.Information, Times.Once(), $"Start handling '{typeof(SimpleRequest)}'");
        }

        [Test, AutoData]
        public async Task Then_we_log_the_handler_has_finished(SimpleRequest request, SimpleResponse expectedResponse)
        {
            await _sut.Handle(request, CancellationToken.None, () => Task.FromResult(expectedResponse));

            _loggerMock.VerifyLog(LogLevel.Information, Times.Once(), $"End handling '{typeof(SimpleRequest)}'");
        }

        [Test, AutoData]
        public async Task Then_we_log_the_handler_has_errored(SimpleRequest request, SimpleResponse expectedResponse)
        {
            Func<Task> action = () => _sut.Handle(request, CancellationToken.None, () => throw new Exception("failed"));

            action.Should().Throw<Exception>().WithMessage("failed");

            _loggerMock.VerifyLog(LogLevel.Error, Times.Once(), $"Error handling '{typeof(SimpleRequest)}'");
        }

        public class SimpleRequest
        {
        }

        public class SimpleResponse
        {
        }
    }
}
