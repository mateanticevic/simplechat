-- =============================================
-- Author:		<MA>
-- Create date: <2016-10-08>
-- =============================================
CREATE PROCEDURE [dbo].[MessageGet]
	@Identifier nvarchar(32)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @MessageId int = dbo.GetMessageId(@Identifier)

	SELECT
		m.Message AS Content,
		m.CreatedOn AS Timestamp,
		m.Identifier AS Identifier,
		p.Nickname AS Nickname
	FROM Message m
	JOIN Profile p ON p.Id = m.FromProfileId
	WHERE m.Id = @MessageId
END
