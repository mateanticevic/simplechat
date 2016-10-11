-- =============================================
-- Author:		<MA>
-- Create date: <2016-10-05>
-- =============================================
CREATE PROCEDURE [dbo].[ProfileGet]
	@Nickname nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		Nickname,
		Email,
		PasswordHash
	FROM Profile
	WHERE Nickname = @Nickname
END
