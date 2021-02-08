CREATE TABLE [dbo].[Registration]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [ApprenticeshipId] BIGINT NOT NULL, 
    [Email] NVARCHAR(150) NOT NULL, 
    [UserId] UNIQUEIDENTIFIER NULL,
    [ApprenticeId] BIGINT NULL,
    [CreatedOn] DATETIME2 NOT NULL DEFAULT current_timestamp
)
