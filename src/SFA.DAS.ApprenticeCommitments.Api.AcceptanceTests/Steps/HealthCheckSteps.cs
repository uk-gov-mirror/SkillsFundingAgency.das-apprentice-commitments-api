using System.Net;
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
            await _context.Api.Get("ping");
        }

        [Then(@"the result should be return okay")]
        public void ThenTheResultShouldBeReturnOkay()
        {
            _context.Api.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
