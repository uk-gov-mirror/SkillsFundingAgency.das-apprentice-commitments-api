using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Newtonsoft.Json;
using SFA.DAS.ApprenticeCommitments.Api.Extensions;
using SFA.DAS.ApprenticeCommitments.Application.Queries.RegistrationQuery;
using SFA.DAS.ApprenticeCommitments.Data.Models;
using TechTalk.SpecFlow;

namespace SFA.DAS.ApprenticeCommitments.Api.AcceptanceTests.Steps
{
    [Binding]
    [Scope(Feature = "GetRegistration")]
    public class GetRegistrationSteps
    {
        private readonly TestContext _context;
        private Fixture _fixture;
        private Registration _registration;

        public GetRegistrationSteps(TestContext context)
        {
            _fixture = new Fixture();
            _registration = _fixture.Create<Registration>();
            _context = context;
        }

        [Given(@"there is no registration")]
        public void GivenThereIsNoRegistration()
        {
        }

        [Given(@"there is a registration")]
        public Task GivenThereIsARegistration()
        {
            _context.DbContext.Registrations.Add(_registration);
            return _context.DbContext.SaveChangesAsync();
        }

        [When(@"we try to retrieve the registration")]
        public Task WhenWeTryToRetrieveTheRegistration()
        {
            return _context.Api.Get($"registrations/{_registration.Id}");
        }

        [Given(@"there is an empty registration")]
        public void GivenThereIsAnEmptyRegistration()
        {
            _fixture.Inject(Guid.Empty);
            _registration = _fixture.Create<Registration>();
        }

        [When(@"we try to retrieve the registration using a bad request format")]
        public Task WhenWeTryToRetrieveTheRegistrationUsingABadRequestFormat()
        {
            return _context.Api.Get($"registrations/1234-1234");
        }

        [Then(@"the result should return ok")]
        public void ThenTheResultShouldReturnOk()
        {
            _context.Api.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Then(@"the response should match the registration in the database")]
        public async Task ThenTheResponseShouldMatchTheRegistrationInTheDatabase()
        {
            var content = await _context.Api.Response.Content.ReadAsStringAsync();
            content.Should().NotBeNull();
            var response  = JsonConvert.DeserializeObject<RegistrationResponse>(content);
            response.Email.Should().Be(_registration.Email.ToString());
            response.RegistrationId.Should().Be(_registration.Id);
        }

        [Then(@"the result should return not found")]
        public void ThenTheResultShouldReturnNotFound()
        {
            _context.Api.Response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Then(@"the result should return bad request")]
        public void ThenTheResultShouldReturnBadRequest()
        {
            _context.Api.Response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Then(@"the error must be say registration must be valid")]
        public async Task ThenTheErrorMustBeSayRegistrationMustBeValid()
        {
            var content = await _context.Api.Response.Content.ReadAsStringAsync();
            content.Should().NotBeNull();
            var response = JsonConvert.DeserializeObject<List<ErrorItem>>(content);
            response.Count.Should().Be(1);
            response[0].PropertyName.Should().Be("RegistrationId");
            response[0].ErrorMessage.Should().Be("The Registration Id must be valid");
        }
    }
}
