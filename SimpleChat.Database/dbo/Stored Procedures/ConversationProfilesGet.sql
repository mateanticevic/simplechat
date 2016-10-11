-- =============================================
-- Author:		<MA>
-- Create date: <2016-10-06>
-- =============================================
CREATE PROCEDURE [dbo].[ConversationProfilesGet]
	@Identifier nvarchar(32)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @ConversationId int = dbo.GetConversationId(@Identifier)

	SELECT
		p.Nickname
	FROM ConversationProfile cp
	JOIN Profile p ON p.Id = cp.ProfileId
	WHERE cp.ConversationId = @ConversationId

END
