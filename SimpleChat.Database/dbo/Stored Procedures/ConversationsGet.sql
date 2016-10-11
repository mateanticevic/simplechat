-- =============================================
-- Author:		<MA>
-- Create date: <2016-10-06>
-- =============================================
CREATE PROCEDURE [dbo].[ConversationsGet]
	@Nickname nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @ProfileId int = (SELECT TOP 1 Id FROM Profile WHERE Nickname = @Nickname)

	SELECT
		c.Identifier,
		MAX(m.CreatedOn) AS 'LastMessage',
		CASE WHEN EXISTS (SELECT * FROM ConversationSeen WHERE ProfileId = @ProfileId AND ConversationId = c.Id AND Timestamp > MAX(m.CreatedOn))
			THEN 0
			ELSE 
			(
				SELECT COUNT(*)
				FROM Message m1 WHERE m1.ConversationId = c.Id
				AND m1.FromProfileId != @ProfileId
				AND m1.CreatedOn > (
										SELECT
											CASE WHEN MAX(Timestamp) IS NULL THEN '1900-1-1' ELSE MAX(Timestamp) END AS LastSeen
										FROM ConversationSeen
										WHERE ProfileId = @ProfileId AND ConversationId = c.Id
									)
			)
		END AS 'NewMessages'
	FROM Conversation c
	JOIN ConversationProfile cp ON cp.ConversationId = c.Id AND cp.ProfileId = @ProfileId
	LEFT JOIN Message m ON m.ConversationId = c.Id
	GROUP BY c.Id, c.Identifier
	ORDER BY 'LastMessage' DESC

END
