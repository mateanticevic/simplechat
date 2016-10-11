-- =============================================
-- Author:		<MA>
-- Create date: <2016-10-05>
-- =============================================
CREATE PROCEDURE [dbo].[ProfilesSearch]
	@SearchQuery nvarchar(200)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT TOP 5
		Nickname,
		Email
	FROM Profile
	WHERE Nickname LIKE '%' + @SearchQuery + '%'
		OR Email LIKE '%' + @SearchQuery + '%'
		
END
