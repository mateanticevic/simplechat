CREATE TABLE [dbo].[ConversationProfile] (
    [ConversationId]   INT           NOT NULL,
    [ProfileId]        INT           NOT NULL,
    [AddedByProfileId] INT           NOT NULL,
    [Timestamp]        DATETIME2 (3) NOT NULL,
    CONSTRAINT [FK_ConversationProfile_Conversation] FOREIGN KEY ([ConversationId]) REFERENCES [dbo].[Conversation] ([Id]),
    CONSTRAINT [FK_ConversationProfile_Profile] FOREIGN KEY ([ProfileId]) REFERENCES [dbo].[Profile] ([Id]),
    CONSTRAINT [FK_ConversationProfile_Profile_AddedBy] FOREIGN KEY ([AddedByProfileId]) REFERENCES [dbo].[Profile] ([Id]),
    CONSTRAINT [IX_ConversationProfile] UNIQUE NONCLUSTERED ([ConversationId] ASC, [ProfileId] ASC)
);

