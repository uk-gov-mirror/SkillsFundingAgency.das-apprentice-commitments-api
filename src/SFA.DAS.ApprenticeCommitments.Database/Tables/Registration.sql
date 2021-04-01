CREATE TABLE [dbo].[Registration]
(
	[Id] UNIQUEIDENTIFIER NOT NULL, 
    [ApprenticeId] UNIQUEIDENTIFIER NULL,
    [ApprenticeshipId] BIGINT NOT NULL, 
    [Email] NVARCHAR(150) NOT NULL, 
    [UserIdentityId] UNIQUEIDENTIFIER NULL,
    [CreatedOn] DATETIME2 NOT NULL DEFAULT current_timestamp,
    [Apprenticeship_EmployerAccountLegalEntityId] BIGINT NOT NULL, 
    [Apprenticeship_EmployerName] NVARCHAR(100) NOT NULL , 
    [Apprenticeship_TrainingProviderId] BIGINT NOT NULL, 
    [Apprenticeship_TrainingProviderName] NVARCHAR(100) NOT NULL, 
    [Apprenticeship_Course_Name] NVARCHAR(MAX) NOT NULL, 
    [Apprenticeship_Course_Level] int NOT NULL, 
    [Apprenticeship_Course_Option] NVARCHAR(MAX) NOT NULL, 
    [Apprenticeship_Course_PlannedStartDate] datetime2 NOT NULL,
    [Apprenticeship_Course_PlannedEndDate] datetime2 NOT NULL,
    CONSTRAINT PK_Registration_Id PRIMARY KEY CLUSTERED ([Id]),
)
