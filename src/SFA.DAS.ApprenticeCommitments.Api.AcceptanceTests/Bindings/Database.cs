﻿using Microsoft.EntityFrameworkCore;
using SFA.DAS.ApprenticeCommitments.Data.Models;
using TechTalk.SpecFlow;

namespace SFA.DAS.ApprenticeCommitments.Api.AcceptanceTests.Bindings
{
    [Binding]
    [Scope(Tag = "database")]
    public class Database
    {
        private readonly TestContext _context;

        public Database(TestContext context)
        {
            _context = context;
        }

        [BeforeScenario()]
        public void Initialise()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApprenticeCommitmentsDbContext>();
            var options = new TestsDbConnectionFactory()
                .AddConnection(optionsBuilder)
                .Options;
            _context.DbContext = new UntrackedApprenticeCommitmentsDbContext(options);

            _context.DbContext.Database.EnsureDeleted();
            _context.DbContext.Database.EnsureCreated();
        }

        [AfterScenario()]
        public void Cleanup()
        {
            _context.DbContext.Database.EnsureDeleted();
            _context?.DbContext?.Dispose();
        }
    }
}