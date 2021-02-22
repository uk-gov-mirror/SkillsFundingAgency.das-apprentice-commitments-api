using AutoFixture;
using AutoFixture.Dsl;
using FluentAssertions;
using SFA.DAS.ApprenticeCommitments.Application.Commands.CreateRegistrationCommand;
using SFA.DAS.ApprenticeCommitments.Data.Models;
using System.Linq;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SFA.DAS.ApprenticeCommitments.Api.AcceptanceTests.Steps
{
    [Binding]
    [Scope(Feature = "ChangeEmailAddress")]
    public class ChangeEmailAddressSteps
    {
        private readonly TestContext _context;
        private Fixture _fixture = new Fixture();
        private Apprentice _apprentice;
        private ChangeEmailAddressCommand _command;

        public ChangeEmailAddressSteps(TestContext context)
        {
            _context = context;
        }

        [Given(@"we have an existing apprentice")]
        public void GivenWeHaveAnExistingApprentice()
        {
            _apprentice = _fixture.Build<Apprentice>().Create();

            _context.DbContext.Apprentices.Add(_apprentice);
            _context.DbContext.SaveChanges();
        }

        [When(@"we change the apprentice's email address")]
        public async Task WhenWeChangeTheApprenticesEmailAddress()
        {
            _command = _fixture
                .Build<ChangeEmailAddressCommand>()
                .With(p => p.ApprenticeId, _apprentice.Id)
                .Create();

            await _context.Api.Post("apprentices", _command);
        }

        [Then(@"the apprentice record is created")]
        public void ThenTheApprenticeRecordIsCreated()
        {
            _context.DbContext.Apprentices.Should().ContainEquivalentOf(new
            {
                Id = _command.ApprenticeId,
                _command.Email,
            });

            _context.DbContext.Apprentices.Should().NotContain(_apprentice);
        }
    }
}