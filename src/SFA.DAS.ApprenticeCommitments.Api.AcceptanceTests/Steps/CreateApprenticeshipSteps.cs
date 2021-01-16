using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using SFA.DAS.ApprenticeCommitments.Application.Commands.CreateRegistrationCommand;
using TechTalk.SpecFlow;

namespace SFA.DAS.ApprenticeCommitments.Api.AcceptanceTests.Steps
{
    [Binding]
    [Scope(Feature = "CreateApprenticeship")]
    public class CreateApprenticeshipSteps
    {
        private readonly TestContext _context;
        private CreateRegistrationCommand _createApprenticeshipRequest; 
        private HttpResponseMessage _response;

        public CreateApprenticeshipSteps(TestContext context)
        {
            _context = context;
        }

        [Given(@"we have an invalid apprenticeship request")]
        public void GivenWeHaveAnInvalidApprenticeshipRequest()
        {
            _createApprenticeshipRequest = new CreateRegistrationCommand();
        }

        [Given(@"we have a valid apprenticeship request")]
        public void GivenWeHaveAValidApprenticeshipRequest()
        {
            _createApprenticeshipRequest = new CreateRegistrationCommand
            {
                RegistrationId = Guid.NewGuid(),
                ApprenticeshipId = 1233,
                Email = "paul@fff.com"
            };
        }


        [When(@"the apprenticeship is posted")]
        public async Task WhenTheApprenticeshipIsPosted()
        {
            _response = await _context.Api.PostValueAsync("apprenticeships", _createApprenticeshipRequest);
        }

        [Then(@"the result should be return bad request")]
        public void ThenTheResultShouldBeReturnBadRequest()
        {
            _response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Then(@"the result should be return accepted")]
        public void ThenTheResultShouldBeReturnAccepted()
        {
            _response.StatusCode.Should().Be(HttpStatusCode.Accepted);
        }
    }
}
