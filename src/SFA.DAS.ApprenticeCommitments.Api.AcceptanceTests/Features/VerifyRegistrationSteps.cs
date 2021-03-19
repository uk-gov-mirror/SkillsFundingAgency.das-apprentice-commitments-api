using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SFA.DAS.ApprenticeCommitments.Api.Extensions;
using SFA.DAS.ApprenticeCommitments.Application.Commands.VerifyRegistrationCommand;
using SFA.DAS.ApprenticeCommitments.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SFA.DAS.ApprenticeCommitments.Api.AcceptanceTests.Steps
{
    [Binding]
    [Scope(Feature = "VerifyRegistration")]
    public class VerifyRegistrationSteps
    {
        private readonly TestContext _context;
        private VerifyRegistrationCommand _command;
        private Fixture _f;
        private Registration _registration;
        private Guid _missingRegistrationId;
        private string _validEmail;
        private Guid _apprenticeId;

        public VerifyRegistrationSteps(TestContext context)
        {
            _context = context;
            _f = new Fixture();
            _validEmail = _f.Create<MailAddress>().Address;
            _missingRegistrationId = _f.Create<Guid>();
        }

        [Given(@"we have an existing registration")]
        public void GivenWeHaveAnExistingRegistration()
        {
            _registration = _f.Build<Registration>()
                .Without(p => p.ApprenticeId)
                .Without(p => p.UserIdentityId)
                .With(p => p.Email, _validEmail).Create();

            _context.DbContext.Registrations.Add(_registration);
            _context.DbContext.SaveChanges();
        }

        [Given(@"the request matches registration details")]
        public void GivenTheRequestMatchesRegistrationDetails()
        {
            _command = _f.Build<VerifyRegistrationCommand>()
                .With(p => p.Email, _validEmail)
                .With(p => p.RegistrationId, _registration.Id)
                .Create();
        }

        [Given(@"the request email does not match")]
        public void GivenTheRequestEmailDoesNotMatch()
        {
            _command = _f.Build<VerifyRegistrationCommand>()
                .With(p => p.Email, "another@email.com")
                .With(p => p.RegistrationId, _registration.Id)
                .Create();
        }

        [Given(@"we have an existing already verified registration")]
        public void GivenWeHaveAnExistingAlreadyVerifiedRegistration()
        {
            _registration = _f.Build<Registration>()
                .Without(p => p.ApprenticeId) // There is no proper relationship yet
                .With(p => p.Email, _validEmail).Create();

            _context.DbContext.Registrations.Add(_registration);
            _context.DbContext.SaveChanges();
        }

        [Given(@"the verify registration request is invalid")]
        public void GivenTheVerifyRegistrationRequestIsInvalid()
        {
            _command = new VerifyRegistrationCommand();
        }

        [Given(@"we do NOT have an existing registration")]
        public void GivenWeDoNOTHaveAnExistingRegistration()
        {
        }

        [Given(@"a valid registration request is submitted")]
        public void GivenAValidRegistrationRequestIsSubmitted()
        {
            _command = _f.Build<VerifyRegistrationCommand>()
                .With(p => p.Email, "another@email.com")
                .With(p => p.RegistrationId, _missingRegistrationId)
                .Create();
        }

        [Given(@"the verify registration request is valid except email address")]
        public void GivenTheVerifyRegistrationRequestIsValidExceptEmailAddress()
        {
            _command = _f.Build<VerifyRegistrationCommand>()
                .With(p => p.Email, "missingemail.com")
                .Create();
        }

        [When(@"we verify that registration")]
        public async Task WhenWeVerifyThatRegistration()
        {
            await _context.Api.Post("registrations", _command);
        }

        [Then(@"the apprentice record is created")]
        public void ThenTheApprenticeRecordIsCreated()
        {
            var apprentice = _context.DbContext.Apprentices.FirstOrDefault(x => x.Id == _command.RegistrationId);
            apprentice.Should().NotBeNull();
            apprentice.FirstName.Should().Be(_command.FirstName);
            apprentice.LastName.Should().Be(_command.LastName);
            apprentice.Email.Should().Be(_validEmail);
            apprentice.DateOfBirth.Should().Be(_command.DateOfBirth);
            apprentice.Id.Should().Be(_command.RegistrationId);
            _apprenticeId = apprentice.Id;
        }

        [Then(@"an apprenticeship record is created")]
        public void ThenAnApprenticeshipRecordIsCreated()
        {
            var apprentice = _context.DbContext
                .Apprentices.Include(x => x.Apprenticeships)
                .FirstOrDefault(x => x.Id == _command.RegistrationId);

            apprentice.Apprenticeships.Should().ContainEquivalentOf(new
            {
                CommitmentsApprenticeshipId = _registration.ApprenticeshipId,
                _registration.EmployerName,
                _registration.EmployerAccountLegalEntityId,
                _registration.TrainingProviderId,
                _registration.TrainingProviderName,
            });
        }

        [Then(@"the registration has been marked as completed")]
        public void ThenTheRegistrationHasBeenMarkedAsCompleted()
        {
            var registration = _context.DbContext.Registrations.FirstOrDefault(x => x.Id == _registration.Id);
            registration.UserIdentityId.Should().Be(_command.UserIdentityId);
            registration.ApprenticeId.Should().Be(_apprenticeId);
        }

        [Then(@"the registration CreatedOn field is unchanged")]
        public void ThenTheRegistrationCreatedOnFieldIsUnchanged()
        {
            _context.DbContext.Registrations.Should().ContainEquivalentOf(new
            {
                _registration.Id,
                _registration.CreatedOn
            });
        }

        [Then(@"a bad request is returned")]
        public void ThenABadRequestIsReturned()
        {
            _context.Api.Response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Then(@"a email domain error is returned")]
        public async Task ThenAEmailDomainErrorIsReturned()
        {
            var content = await _context.Api.Response.Content.ReadAsStringAsync();
            var errors = JsonConvert.DeserializeObject<List<ErrorItem>>(content);
            errors.Count.Should().Be(1);
            errors[0].PropertyName.Should().BeNull();
            errors[0].ErrorMessage.Should().Be("Email from Verifying user doesn't match registered user");
        }

        [Then(@"an 'already verified' domain error is returned")]
        public async Task ThenAnAlreadyVerifiedDomainErrorIsReturned()
        {
            var content = await _context.Api.Response.Content.ReadAsStringAsync();
            var errors = JsonConvert.DeserializeObject<List<ErrorItem>>(content);
            errors.Should().ContainEquivalentOf(new ErrorItem
            {
                PropertyName = null,
                ErrorMessage = "Already verified",
            });
        }

        [Then(@"response contains the expected error messages")]
        public async Task ThenResponseContainsTheExpectedErrorMessages()
        {
            var content = await _context.Api.Response.Content.ReadAsStringAsync();
            var errors = JsonConvert.DeserializeObject<List<ErrorItem>>(content);

            errors.Should().ContainEquivalentOf(new { PropertyName = "RegistrationId" });
            errors.Should().ContainEquivalentOf(new { PropertyName = "UserIdentityId" });
            errors.Should().ContainEquivalentOf(new { PropertyName = "FirstName" });
            errors.Should().ContainEquivalentOf(new { PropertyName = "LastName" });
            errors.Should().ContainEquivalentOf(new { PropertyName = "DateOfBirth" });
            errors.Should().ContainEquivalentOf(new { PropertyName = "Email" });
        }

        [Then(@"response contains the expected email error message")]
        public async Task ThenResponseContainsTheExpectedEmailErrorMessage()
        {
            var content = await _context.Api.Response.Content.ReadAsStringAsync();
            var errors = JsonConvert.DeserializeObject<List<ErrorItem>>(content);

            errors.Count(x => x.PropertyName == "Email").Should().Be(1);
        }

        [Then(@"response contains the not found error message")]
        public async Task ThenResponseContainsTheNotFoundErrorMessage()
        {
            var content = await _context.Api.Response.Content.ReadAsStringAsync();
            var errors = JsonConvert.DeserializeObject<List<ErrorItem>>(content);

            errors.Count().Should().Be(1);
            errors[0].ErrorMessage.Should().Be($"Registration {_missingRegistrationId} not found");
        }

        [Then("a record of the apprentice email address is kept")]
        public void ThenTheChangeHistoryIsRecorded()
        {
            var modified = _context.DbContext
                .Apprentices.Include(x => x.PreviousEmailAddresses)
                .Single(x => x.Id == _apprenticeId);

            modified.PreviousEmailAddresses.Should().ContainEquivalentOf(new
            {
                EmailAddress = new MailAddress(_command.Email),
            });
        }
    }
}