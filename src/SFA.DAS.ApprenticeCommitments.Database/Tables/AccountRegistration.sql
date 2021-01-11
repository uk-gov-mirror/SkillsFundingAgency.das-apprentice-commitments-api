CREATE TABLE [dbo].[AccountRegistration]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [ApprenticeshipId] BIGINT NOT NULL, 
    [Email] NCHAR(150) NOT NULL 
)
