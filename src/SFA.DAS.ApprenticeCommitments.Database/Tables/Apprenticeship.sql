CREATE TABLE [dbo].[Apprenticeship]
(
	[Id] BIGINT IDENTITY(10000,1) NOT NULL PRIMARY KEY,
	[ApprenticeId] BIGINT NOT NULL, 
    [CommitmentsApprenticeshipId] BIGINT NOT NULL
)

GO

CREATE NONCLUSTERED INDEX [IX_Apprenticeship_ApprenticeId] ON [dbo].[Apprenticeship]
(
	[ApprenticeId] ASC
)