using System;
using System.Data.SQLite;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.ApprenticeCommitments.Data.Models;
using TechTalk.SpecFlow;

namespace SFA.DAS.ApprenticeCommitments.Api.AcceptanceTests.Bindings
{
    [Binding]
    [Scope(Tag = "database")]
    public class InMemoryDatabase
    {
        private readonly TestContext _context;

        public InMemoryDatabase(TestContext context)
        {
            _context = context;
        }

        [BeforeScenario()]
        public void Initialise()
        {

            //using var con = new SQLiteConnection(_context.DatabaseConnectionString);
            //string sql = "CREATE TABLE [dbo].[Registration]\r\n(\r\n\t[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, \r\n    [ApprenticeshipId] BIGINT NOT NULL, \r\n    [Email] NVARCHAR(150) NOT NULL, \r\n    [CreatedOn] DATETIME2 NOT NULL DEFAULT GETDATE()\r\n)\r\n";

            //var command = new SQLiteCommand(sql, con);
            //command.ExecuteNonQuery();



            var optionsBuilder = new DbContextOptionsBuilder<ApprenticeCommitmentsDbContext>().UseSqlite(_context.DatabaseConnectionString);
            _context.DataContext = new ApprenticeCommitmentsDbContext(optionsBuilder.Options);
            var context = _context.DataContext;

            context.Database.OpenConnection();

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();



            //_context.DataContext.Registrations.Add(new Registration
            //{ ApprenticeshipId = 100, Email = "paul@1222", Id = Guid.NewGuid(), CreatedOn = new DateTime(2020, 01, 02)});
            //_context.DataContext.SaveChanges();
        }
    }

}
