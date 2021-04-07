CREATE TABLE [dbo].[Registration]
(
	[Id] UNIQUEIDENTIFIER NOT NULL, 
    [ApprenticeId] UNIQUEIDENTIFIER NULL,
    [ApprenticeshipId] BIGINT NOT NULL, 
    [Email] NVARCHAR(150) NOT NULL, 
    [UserIdentityId] UNIQUEIDENTIFIER NULL,
    [CreatedOn] DATETIME2 NOT NULL DEFAULT current_timestamp,
    [EmployerAccountLegalEntityId] BIGINT NOT NULL, 
    [EmployerName] NVARCHAR(100) NOT NULL , 
    [TrainingProviderId] BIGINT NOT NULL, 
    [TrainingProviderName] NVARCHAR(100) NOT NULL, 
    [CourseName] NVARCHAR(MAX) NOT NULL, 
    [CourseLevel] int NOT NULL, 
    [CourseOption] NVARCHAR(MAX) NULL, 
    [PlannedStartDate] datetime2 NOT NULL,
    [PlannedEndDate] datetime2 NOT NULL,
    CONSTRAINT PK_Registration_Id PRIMARY KEY CLUSTERED ([Id]),
)
