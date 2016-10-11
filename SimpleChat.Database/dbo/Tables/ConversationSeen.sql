CREATE TABLE [dbo].[ConversationSeen] (
    [ConversationId] INT           NOT NULL,
    [ProfileId]      INT           NOT NULL,
    [Timestamp]      DATETIME2 (3) NOT NULL,
    CONSTRAINT [FK_MessageSeen_Conversation] FOREIGN KEY ([ConversationId]) REFERENCES [dbo].[Conversation] ([Id]),
    CONSTRAINT [FK_MessageSeen_Profile] FOREIGN KEY ([ProfileId]) REFERENCES [dbo].[Profile] ([Id])
);

