CREATE TABLE [dbo].[ApprenticeEmailHistory] (
    [ChangedOn]    DATETIME2 (7)  NOT NULL,
    [ApprenticeId] BIGINT         NOT NULL,
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [EmailAddress] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_ApprenticeEmailHistory] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ApprenticeEmailHistory_Apprentice_ApprenticeId] FOREIGN KEY ([ApprenticeId]) REFERENCES [dbo].[Apprentice] ([Id]) ON DELETE CASCADE
);




GO
CREATE NONCLUSTERED INDEX [IX_ApprenticeEmailHistory_ApprenticeId]
    ON [dbo].[ApprenticeEmailHistory]([ApprenticeId] ASC);

