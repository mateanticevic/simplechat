-- =============================================
-- Author:		<MA>
-- Create date: <2016-10-06>
-- =============================================
CREATE PROCEDURE [dbo].[ConversationMessagesGet]
	@Identifier nvarchar(32)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @ConversationId int = (SELECT TOP 1 Id FROM Conversation WHERE Identifier = @Identifier)

	SELECT
		m.CreatedOn,
		m.Message,
		m.Identifier,
		p.Nickname
	FROM Message m
	JOIN Profile p ON p.Id = m.FromProfileId
	WHERE m.ConversationId = @ConversationId
	ORDER BY m.CreatedOn DESC

END
