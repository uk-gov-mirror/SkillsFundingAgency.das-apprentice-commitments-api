using System.IO;
using Microsoft.Data.Sqlite;

namespace SFA.DAS.ApprenticeCommitments.Api.AcceptanceTests
{
    public static class AcceptanceTestsData
    {
        public const string AcceptanceTestsDatabaseName = "AcceptanceTestsDb";

        public static void DropDatabase(string connectionString)
        {
            File.Delete(AcceptanceTestsDatabaseName);
        }

        public static void CreateTables(string connectionString)
        {

            using SqliteConnection connection = new SqliteConnection(connectionString);
            connection.Open();
            
            using SqliteCommand registrationCommand = new SqliteCommand(
                @"
                    CREATE TABLE Registration(
                        Id uniqueidentifier NOT NULL PRIMARY KEY, 
                        ApprenticeshipId INTEGER NOT NULL,
                        Email nvarchar(255) NOT NULL,
                        UserIdentityId uniqueidentifier NULL, 
                        ApprenticeId INTEGER NULL, 
                        CreatedOn TIMESTAMP DEFAULT CURRENT_TIMESTAMP
                );
            ", connection);
            registrationCommand.ExecuteNonQuery();

            using SqliteCommand apprenticeCommand = new SqliteCommand(
                @"
                    CREATE TABLE Apprentice(
                        Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, 
                        FirstName nvarchar(100) NOT NULL,
                        LastName nvarchar(100) NOT NULL,
                        UserIdentityId uniqueidentifier NOT NULL, 
                        Email nvarchar(255) NOT NULL,
                        DateOfBirth TIMESTAMP NOT NULL,
                        CreatedOn TIMESTAMP DEFAULT CURRENT_TIMESTAMP
                );
            ", connection);
            apprenticeCommand.ExecuteNonQuery();

            using SqliteCommand apprenticeshipCommand = new SqliteCommand(
                @"
                    CREATE TABLE Apprenticeship(
                        Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, 
                        ApprenticeId INTEGER NOT NULL,
                        [CommitmentsApprenticeshipId] INTEGER NOT NULL
                );
            ", connection);
            apprenticeshipCommand.ExecuteNonQuery();


            using SqliteCommand clientOutBoxDataCommand = new SqliteCommand(
                @"
                    CREATE TABLE ClientOutboxData(
                        MessageId uniqueidentifier NOT NULL PRIMARY KEY, 
                        EndpointName nvarchar(150) NOT NULL, 
                        CreatedAt TIMESTAMP NOT NULL, 
                        Dispatched INTEGER NOT NULL,
                        DispatchedAt TIMESTAMP NULL, 
                        Operations nvarchar(255) NOT NULL,
                        PersistenceVersion nvarchar(23) NOT NULL
                );
            ", connection);
            clientOutBoxDataCommand.ExecuteNonQuery();

            using SqliteCommand outBoxDataCommand = new SqliteCommand(
                @"
                    CREATE TABLE OutboxData(
                        MessageId uniqueidentifier NOT NULL PRIMARY KEY, 
                        Dispatched INTEGER NOT NULL,
                        DispatchedAt TIMESTAMP NULL, 
                        PersistenceVersion nvarchar(23) NOT NULL,
                        Operations nvarchar(255) NOT NULL
                );
            ", connection);
            outBoxDataCommand.ExecuteNonQuery();

            connection.Close();
        }
    }
}