CREATE TABLE [dbo].[Registration]
(
    [ApprenticeId] UNIQUEIDENTIFIER NOT NULL,
    [ApprenticeshipId] BIGINT NOT NULL, 
    [Email] NVARCHAR(150) NOT NULL, 
    [UserIdentityId] UNIQUEIDENTIFIER NULL,
    [CreatedOn] DATETIME2 NOT NULL DEFAULT current_timestamp,
    [EmployerName] NVARCHAR(100) NOT NULL , 
    [EmployerAccountLegalEntityId] BIGINT NOT NULL, 
    [TrainingProviderId] BIGINT NOT NULL, 
    [TrainingProviderName] NVARCHAR(100) NOT NULL, 
    CONSTRAINT PK_Registration_ApprenticeId PRIMARY KEY CLUSTERED ([ApprenticeId]),
)
