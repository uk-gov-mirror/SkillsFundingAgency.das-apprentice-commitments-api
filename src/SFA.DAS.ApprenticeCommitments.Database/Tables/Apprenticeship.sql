CREATE TABLE [dbo].[Apprenticeship]
(
	[Id] BIGINT IDENTITY(10000,1) NOT NULL,
	[ApprenticeId] BIGINT NOT NULL, 
    [CommitmentsApprenticeshipId] BIGINT NOT NULL,
	CONSTRAINT PK_Apprenticeship_Id PRIMARY KEY CLUSTERED ([Id]),
	CONSTRAINT FK_Apprenticeship_ApprenticeId FOREIGN KEY ([ApprenticeId]) REFERENCES [dbo].[Apprentice] ([Id])
)

GO

CREATE NONCLUSTERED INDEX [IX_Apprenticeship_ApprenticeId] ON [dbo].[Apprenticeship]
(
	[ApprenticeId] ASC
)