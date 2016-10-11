CREATE TABLE [dbo].[Profile] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [Nickname]     NVARCHAR (50)  NOT NULL,
    [PasswordHash] NVARCHAR (60)  NOT NULL,
    [Email]        NVARCHAR (200) NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [IX_Profile] UNIQUE NONCLUSTERED ([Nickname] ASC)
);

