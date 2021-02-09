CREATE TABLE [dbo].[Apprentice]
(
	[Id] BIGINT IDENTITY(1000,1) NOT NULL PRIMARY KEY,
	[FirstName] NVARCHAR(100) NOT NULL,
	[LastName] NVARCHAR(100) NOT NULL,
	[UserIdentityId] UNIQUEIDENTIFIER NOT NULL,
	[Email] NVARCHAR(200) NOT NULL, 
    [DateOfBirth] DATETIME2 NOT NULL
)

GO

CREATE UNIQUE INDEX [IX_Apprentice_UserId] ON [dbo].[Apprentice]
(
	[UserIdentityId] ASC
)