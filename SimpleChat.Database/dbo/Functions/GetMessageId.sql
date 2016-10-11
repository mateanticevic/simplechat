-- =============================================
-- Author:		MA
-- Create date: 2016-10-07
-- =============================================
CREATE FUNCTION [dbo].[GetMessageId]
(
	@Identifier nvarchar(50)
)
RETURNS int
AS
BEGIN

	RETURN (SELECT TOP 1 Id FROM Message WHERE Identifier = @Identifier)

END
