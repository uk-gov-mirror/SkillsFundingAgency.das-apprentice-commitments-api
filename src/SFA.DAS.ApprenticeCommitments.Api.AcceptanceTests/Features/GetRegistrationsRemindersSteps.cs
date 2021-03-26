using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Newtonsoft.Json;
using SFA.DAS.ApprenticeCommitments.Api.Extensions;
using SFA.DAS.ApprenticeCommitments.Application.Queries.RegistrationQuery;
using SFA.DAS.ApprenticeCommitments.Application.Queries.RegistrationRemindersQuery;
using SFA.DAS.ApprenticeCommitments.Data.Models;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SFA.DAS.ApprenticeCommitments.Api.AcceptanceTests.Features
{
    [Binding]
    [Scope(Feature = "GetRegistrationsReminders")]
    public class GetRegistrationsRemindersSteps
    {
        private readonly TestContext _context;
        private Fixture _fixture;
        private List<Registration> _registrations;
        private List<RegistrationTest> _testData;

        public GetRegistrationsRemindersSteps(TestContext context)
        {
            _fixture = new Fixture();
            _context = context;
            _registrations = new List<Registration>();
        }

        [Given(@"the following registration details exist")]
        public async Task GivenTheFollowingRegistrationDetailsExist(Table table)
        {
            _testData = table.CreateSet<RegistrationTest>().ToList();

            foreach (var reg in _testData)
            {
                var registration = _fixture.Create<Registration>();
                
                registration.SetProperty(x=>x.CreatedOn, reg.CreatedOn);
                registration.SetProperty(x=>x.Email, reg.Email);
                registration.SetProperty(x=>x.FirstViewedOn, reg.FirstViewedOn);
                registration.SetProperty(x=>x.UserIdentityId, reg.UserIdentityId);
                registration.SetProperty(x=>x.SignUpReminderSentOn, reg.SignUpReminderSentOn);
                
                _registrations.Add(registration);
            }

            _context.DbContext.Registrations.AddRange(_registrations);
            await _context.DbContext.SaveChangesAsync();
        }


        [When(@"we get reminders before cut off date (.*)")]
        public async Task WhenWeGetRemindersBeforeCutOffDate(DateTime cutOffTime)
        {
            await _context.Api.Get($"registrations/reminders/?cutOffDateTime={cutOffTime.ToString("yyy-MM-dd")}");
        }

        [Then(@"the result should return (.*) matching registration")]
        public async Task ThenTheResultShouldReturnMatchingRegistration(int count)
        {
            var content = await _context.Api.Response.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<RegistrationRemindersResponse>(content);
            content.Should().NotBeNull();
            response.Registrations.Count.Should().Be(count);
        }

        [Then(@"that should be has a registration with the email (.*) and it's expected values")]
        public async Task ThenThatShouldBeHasARegistrationWithTheEmailAndItSExpectedValues(string email)
        {
            var content = await _context.Api.Response.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<RegistrationRemindersResponse>(content);
            content.Should().NotBeNull();

            var expected = _testData.FirstOrDefault(x => x.Email == email);
            var match = response.Registrations.FirstOrDefault(x=>x.Email == email);
            match.Should().NotBeNull();
            match.CreatedOn.Should().Be(expected.CreatedOn);
            match.UserIdentityId.Should().Be(expected.UserIdentityId);
        }

        public class RegistrationTest
        {
            public string Email;
            public DateTime? CreatedOn;
            public DateTime? FirstViewedOn;
            public DateTime? SignUpReminderSentOn;
            public Guid? UserIdentityId;
        }

        //[Given(@"there is no registration")]
        //public void GivenThereIsNoRegistration()
        //{
        //}

        //[Given(@"there is a registration")]
        //public Task GivenThereIsARegistration()
        //{
        //    _context.DbContext.Registrations.Add(_registration);
        //    return _context.DbContext.SaveChangesAsync();
        //}

        //[When(@"we try to retrieve the registration")]
        //public Task WhenWeTryToRetrieveTheRegistration()
        //{
        //    return _context.Api.Get($"registrations/{_registration.ApprenticeId}");
        //}

        //[Given(@"there is an empty apprentice id")]
        //public void GivenThereIsAnEmptyRegistration()
        //{
        //    _fixture.Inject(Guid.Empty);
        //    _registration = _fixture.Create<Registration>();
        //}

        //[When(@"we try to retrieve the registration using a bad request format")]
        //public Task WhenWeTryToRetrieveTheRegistrationUsingABadRequestFormat()
        //{
        //    return _context.Api.Get($"registrations/1234-1234");
        //}

        //[Then(@"the result should return ok")]
        //public void ThenTheResultShouldReturnOk()
        //{
        //    _context.Api.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        //}

        //[Then(@"the response should match the registration in the database")]
        //public async Task ThenTheResponseShouldMatchTheRegistrationInTheDatabase()
        //{
        //    var content = await _context.Api.Response.Content.ReadAsStringAsync();
        //    content.Should().NotBeNull();
        //    var response  = JsonConvert.DeserializeObject<RegistrationResponse>(content);
        //    response.Email.Should().Be(_registration.Email);
        //    response.ApprenticeId.Should().Be(_registration.ApprenticeId);
        //}

        //[Then(@"the result should return not found")]
        //public void ThenTheResultShouldReturnNotFound()
        //{
        //    _context.Api.Response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        //}

        //[Then(@"the result should return bad request")]
        //public void ThenTheResultShouldReturnBadRequest()
        //{
        //    _context.Api.Response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        //}

        //[Then(@"the error must be say apprentice id must be valid")]
        //public async Task ThenTheErrorMustBeSayRegistrationMustBeValid()
        //{
        //    var content = await _context.Api.Response.Content.ReadAsStringAsync();
        //    content.Should().NotBeNull();
        //    var response = JsonConvert.DeserializeObject<List<ErrorItem>>(content);
        //    response.Count.Should().Be(1);
        //    response[0].PropertyName.Should().Be("ApprenticeId");
        //    response[0].ErrorMessage.Should().Be("The Apprentice Id must be valid");
        //}
    }
}
