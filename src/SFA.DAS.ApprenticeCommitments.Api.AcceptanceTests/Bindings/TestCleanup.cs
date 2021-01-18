using TechTalk.SpecFlow;

namespace SFA.DAS.ApprenticeCommitments.Api.AcceptanceTests.Bindings
{
    [Binding]
    public class TestCleanUp
    {
        private readonly TestContext _context;
        public TestCleanUp(TestContext context)
        {
            _context = context;
        }

        [AfterFeature()]
        public static void CleanUpFeature()
        {
            Api.Client?.Dispose();
            Api.Client = null;
        }
    }
}