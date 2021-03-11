CREATE TABLE [dbo].[Registration]
(
	[Id] UNIQUEIDENTIFIER NOT NULL, 
    [ApprenticeId] UNIQUEIDENTIFIER NULL,
    [ApprenticeshipId] BIGINT NOT NULL, 
    [Email] NVARCHAR(150) NOT NULL, 
    [UserIdentityId] UNIQUEIDENTIFIER NULL,
    [CreatedOn] DATETIME2 NOT NULL DEFAULT current_timestamp,
    [EmployerName] NVARCHAR(100) NOT NULL , 
    [EmployerAccountLegalEntityId] BIGINT NOT NULL, 
    CONSTRAINT PK_Registration_Id PRIMARY KEY CLUSTERED ([Id]),
)
