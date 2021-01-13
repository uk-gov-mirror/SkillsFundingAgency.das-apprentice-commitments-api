using System;
using System.Linq.Expressions;
using FluentValidation.TestHelper;
using NUnit.Framework;
using SFA.DAS.ApprenticeCommitments.Application.Commands.CreateRegistrationCommand;

namespace SFA.DAS.ApprenticeCommitments.UnitTests.Application.Commands.CreateRegistrationCommandTests
{
    [TestFixture]
    public class WhenValidatingCreateRegistrationCommand
    {
        [TestCase(0, false)]
        [TestCase(-10, false)]
        [TestCase(10, true)]
        public void When_validating_ApprenticeshipId(long id, bool expectValid)
        {
            AssertValidationResult(request => request.ApprenticeshipId, id, expectValid);
        }

        [TestCase(null, false)]
        [TestCase("", false)]
        [TestCase("bob@domain.com", true)]
        [TestCase("bob@domain", true)]
        public void When_validating_Email(string email, bool expectValid)
        {
            AssertValidationResult(request => request.Email, email, expectValid);
        }

        [Test]
        public void When_empty_RegistrationId_it_fails()
        {
            AssertValidationResult(request => request.RegistrationId, Guid.Empty, false);
        }

        [Test]
        public void When_non_empty_RegistrationId_it_passes()
        {
            AssertValidationResult(request => request.RegistrationId, Guid.NewGuid(), true);
        }

        private void AssertValidationResult<T>(Expression<Func<CreateRegistrationCommand, T>> property, T value, bool expectedValid)
        {
            // Arrange
            var validator = new CreateRegistrationCommandValidator();

            // Act
            if (expectedValid)
            {
                validator.ShouldNotHaveValidationErrorFor(property, value);
            }
            else
            {
                validator.ShouldHaveValidationErrorFor(property, value);
            }
        }


    }
}
