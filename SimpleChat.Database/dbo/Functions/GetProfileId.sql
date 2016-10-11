-- =============================================
-- Author:		MA
-- Create date: 2016-10-07
-- =============================================
CREATE FUNCTION GetProfileId
(
	@Nickname nvarchar(50)
)
RETURNS int
AS
BEGIN

	RETURN (SELECT TOP 1 Id FROM Profile WHERE Nickname = @Nickname)

END
