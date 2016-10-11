-- =============================================
-- Author:		<MA>
-- Create date: <2016-10-05>
-- =============================================
CREATE PROCEDURE [dbo].[ProfileUpdate]
	@Id int,
	@Nickname nvarchar(50),
	@Email nvarchar(200)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @ErrorCode  int
    SELECT @ErrorCode = @@ERROR

    BEGIN TRY

		BEGIN TRAN
			
			UPDATE Profile
			SET
				Nickname = @Nickname,
				Email = @Email
			WHERE Id = @Id

		COMMIT TRAN

		SELECT  @ErrorCode  = 0
		RETURN @ErrorCode

	END TRY

	BEGIN CATCH

		IF @@TRANCOUNT > 0 ROLLBACK

		SELECT @ErrorCode = ERROR_NUMBER()
		RETURN @ErrorCode 

	END CATCH


END
