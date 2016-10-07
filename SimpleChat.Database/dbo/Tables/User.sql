CREATE TABLE [dbo].[User] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Nickname]  NVARCHAR (50)  NOT NULL,
    [Email]     NVARCHAR (200) NOT NULL,
    [FirstName] NVARCHAR (100) NOT NULL,
    [LastName]  NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC)
);

