CREATE TABLE [dbo].[ApprenticeEmailAddressHistory] (
    [ChangedOn]    DATETIME2 (7)  NOT NULL,
    [ApprenticeId] BIGINT         NOT NULL,
    [Id]           BIGINT            IDENTITY (2000, 1) NOT NULL,
    [EmailAddress] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_ApprenticeEmailAddressHistory] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ApprenticeEmailAddressHistory_Apprentice_ApprenticeId] FOREIGN KEY ([ApprenticeId]) REFERENCES [dbo].[Apprentice] ([Id]) ON DELETE CASCADE
);

GO
CREATE NONCLUSTERED INDEX [IX_ApprenticeEmailAddressHistory_ApprenticeId]
    ON [dbo].[ApprenticeEmailAddressHistory]([ApprenticeId] ASC);
