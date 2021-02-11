CREATE TABLE [dbo].[Registration]
(
	[Id] UNIQUEIDENTIFIER NOT NULL, 
    [ApprenticeshipId] BIGINT NOT NULL, 
    [Email] NVARCHAR(150) NOT NULL, 
    [UserIdentityId] UNIQUEIDENTIFIER NULL,
    [ApprenticeId] BIGINT NULL,
    [CreatedOn] DATETIME2 NOT NULL DEFAULT current_timestamp,
    CONSTRAINT PK_Registration_Id PRIMARY KEY CLUSTERED ([Id]),
    CONSTRAINT FK_Registration_ApprenticeId FOREIGN KEY ([ApprenticeId]) REFERENCES [dbo].[Apprentice] ([Id])

)
