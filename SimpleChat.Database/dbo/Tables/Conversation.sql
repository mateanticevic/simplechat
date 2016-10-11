CREATE TABLE [dbo].[Conversation] (
    [Id]                 INT           IDENTITY (1, 1) NOT NULL,
    [Identifier]         NVARCHAR (32) NOT NULL,
    [InitiatedProfileId] INT           NOT NULL,
    [Created]            DATETIME2 (7) NOT NULL,
    CONSTRAINT [PK_Conversation] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [IX_Conversation] UNIQUE NONCLUSTERED ([Identifier] ASC)
);

