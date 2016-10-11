CREATE TABLE [dbo].[Message] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [Identifier]     NVARCHAR (32)  NOT NULL,
    [ConversationId] INT            NOT NULL,
    [FromProfileId]  INT            NULL,
    [Message]        NVARCHAR (MAX) NOT NULL,
    [CreatedOn]      DATETIME2 (3)  NOT NULL,
    CONSTRAINT [PK_Message] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Message_Conversation] FOREIGN KEY ([ConversationId]) REFERENCES [dbo].[Conversation] ([Id]),
    CONSTRAINT [FK_Message_Profile] FOREIGN KEY ([FromProfileId]) REFERENCES [dbo].[Profile] ([Id]),
    CONSTRAINT [IX_Message] UNIQUE NONCLUSTERED ([Identifier] ASC)
);

