using AutoFixture;
using FluentAssertions;
using SFA.DAS.ApprenticeCommitments.Api.Controllers;
using SFA.DAS.ApprenticeCommitments.Data.Models;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SFA.DAS.ApprenticeCommitments.Api.AcceptanceTests.Features
{
    [Binding]
    [Scope(Feature = "ConfirmTrainingProvider")]
    public class ConfirmTrainingProviderSteps
    {
        private readonly Fixture _fixture = new Fixture();
        private readonly TestContext _context;
        private readonly Apprentice _apprentice;
        private readonly Apprenticeship _apprenticeship;
        private bool _trainingProviderCorrectness;

        public ConfirmTrainingProviderSteps(TestContext context)
        {
            _context = context;

            _apprentice = _fixture.Create<Apprentice>();
            _apprenticeship = _fixture.Build<Apprenticeship>()
                .With(a => a.TrainingProviderCorrect, () => null)
                .Create();
            _apprentice.AddApprenticeship(_apprenticeship);
        }

        [Given("we have an apprenticeship waiting to be confirmed")]
        public async Task GivenWeHaveAnApprenticeshipWaitingToBeConfirmed()
        {
            _context.DbContext.Apprentices.Add(_apprentice);
            await _context.DbContext.SaveChangesAsync();
        }

        [Given("a ConfirmTrainingProviderRequest stating the training provider is correct")]
        public void GivenAConfirmTrainingProviderRequestStatingTheTrainingProviderIsCorrect()
        {
            _trainingProviderCorrectness = true;
        }

        [Given(@"a ConfirmTrainingProviderRequest stating the training provider is incorrect")]
        public void GivenAConfirmTrainingProviderRequestStatingTheTrainingProviderIsIncorrect()
        {
            _trainingProviderCorrectness = false;
        }

        [When("we send the confirmation")]
        public async Task WhenWeSendTheConfirmation()
        {
            var command = new ConfirmTrainingProviderRequest
            {
                TrainingProviderCorrect = _trainingProviderCorrectness,
            };

            await _context.Api.Post(
                $"apprentices/{_apprentice.Id}/apprenticeships/{_apprenticeship.Id}/TrainingProviderConfirmation",
                command);
        }

        [Then("the response is OK")]
        public void ThenTheResponseIsOK()
        {
            _context.Api.Response.EnsureSuccessStatusCode();
        }

        [Then("the apprenticeship record is updated")]
        public void ThenTheApprenticeshipRecordIsUpdated()
        {
            _context.DbContext.Apprenticeships.Should().ContainEquivalentOf(new
            {
                _apprenticeship.Id,
                TrainingProviderCorrect = _trainingProviderCorrectness,
            });
        }
    }
}