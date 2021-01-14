using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace SFA.DAS.ApprenticeCommitments.Api.AcceptanceTests.Steps
{
    [Binding]
    [Scope(Feature = "HealthCheck")]
    public class HealthCheckSteps
    {
        private readonly TestContext _context;
        private HttpResponseMessage _response;

        public HealthCheckSteps(TestContext context)
        {
            _context = context;
        }


        [Given(@"the api has started")]
        public void GivenTheApiHasStarted()
        {
        }

        [When(@"the ping endpoint is called")]
        public async Task WhenThePingEndpointIsCalled()
        {
            _response = await _context.Api.GetAsync("ping");
        }

        [Then(@"the result should be return okay")]
        public void ThenTheResultShouldBeReturnOkay()
        {
            _response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
