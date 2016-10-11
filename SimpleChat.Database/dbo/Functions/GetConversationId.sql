-- =============================================
-- Author:		MA
-- Create date: 2016-10-07
-- =============================================
CREATE FUNCTION [dbo].[GetConversationId]
(
	@Identifier nvarchar(50)
)
RETURNS int
AS
BEGIN

	RETURN (SELECT TOP 1 Id FROM Conversation WHERE Identifier = @Identifier)

END
