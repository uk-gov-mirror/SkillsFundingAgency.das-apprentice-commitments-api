﻿using System;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.ApprenticeCommitments.Infrastructure.Mediator;
using SFA.DAS.UnitOfWork.Managers;

namespace SFA.DAS.ApprenticeCommitments.UnitTests.Infrastructure.MediatorTests
{
    public class WhenInvokingUnitOfWorkInMediatorPipeline
    {
        private Mock<IUnitOfWorkManager> _unitOfWorkManager;
        private UnitOfWorkPipelineBehavior<SimpleUnitOfWorkRequest, SimpleResponse> _sut;

        [SetUp]
        public void Arrange()
        {
            _unitOfWorkManager = new Mock<IUnitOfWorkManager>();
            _sut = new UnitOfWorkPipelineBehavior<SimpleUnitOfWorkRequest, SimpleResponse>(_unitOfWorkManager.Object);
        }

        [Test, AutoData]
        public async Task Then_we_call_delegator(SimpleUnitOfWorkRequest request, SimpleResponse expectedResponse)
        {
            var response = await _sut.Handle(request, CancellationToken.None, () => Task.FromResult(expectedResponse));

            response.Should().Be(expectedResponse);
        }

        [Test, AutoData]
        public async Task Then_we_invoke_unit_of_work(SimpleUnitOfWorkRequest request, SimpleResponse expectedResponse)
        {
            await _sut.Handle(request, CancellationToken.None, () => Task.FromResult(expectedResponse));

            _unitOfWorkManager.Verify(x => x.BeginAsync(), Times.Once);
            _unitOfWorkManager.Verify(x => x.EndAsync(null), Times.Once);
        }

        [Test, AutoData]
        public async Task And_an_exception_occurs_in_handler_Then_we_start_unit_of_work_and_end_it_passing_exception(SimpleUnitOfWorkRequest request, SimpleResponse expectedResponse)
        {

            Func<Task> action = () => _sut.Handle(request, CancellationToken.None, () => throw new Exception("failed"));

            action.Should().Throw<Exception>().WithMessage("failed");
            _unitOfWorkManager.Verify(x => x.BeginAsync(), Times.Once);
            _unitOfWorkManager.Verify(x => x.EndAsync(It.Is<Exception>(e=>e.Message == "failed")), Times.Once);
        }

        public class SimpleUnitOfWorkRequest : IUnitOfWorkCommand
        {
        }

        public class SimpleResponse
        {
        }
    }
}
