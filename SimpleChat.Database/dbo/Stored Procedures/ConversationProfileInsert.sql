-- =============================================
-- Author:      MA
-- Create date: 2016-10-07
-- =============================================
CREATE PROCEDURE [dbo].[ConversationProfileInsert]
	@NicknameAdder nvarchar(50),
	@NicknameAdded nvarchar(50),
	@ConversationIdentifier nvarchar(32)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT,
        QUOTED_IDENTIFIER,
        ANSI_NULLS,
        ANSI_PADDING,
        ANSI_WARNINGS,
        ARITHABORT,
        CONCAT_NULL_YIELDS_NULL ON;
    SET NUMERIC_ROUNDABORT OFF;
 
    DECLARE @localTran bit
    IF @@TRANCOUNT = 0
    BEGIN
        SET @localTran = 1
        BEGIN TRANSACTION LocalTran
    END
 
    BEGIN TRY

		DECLARE @ConversationId int = dbo.GetConversationId(@ConversationIdentifier)
		DECLARE @ProfileAdderId int = dbo.GetProfileId(@NicknameAdder)
		DECLARE @ProfileAddedId int = dbo.GetProfileId(@NicknameAdded)
	
		INSERT INTO ConversationProfile (AddedByProfileId, ConversationId, ProfileId, Timestamp)
		VALUES (@ProfileAdderId, @ConversationId, @ProfileAddedId, GETDATE())

		SELECT 1
 
        IF @localTran = 1 AND XACT_STATE() = 1
            COMMIT TRAN LocalTran
 
    END TRY
    BEGIN CATCH
 
        DECLARE @ErrorMessage NVARCHAR(4000)
        DECLARE @ErrorSeverity INT
        DECLARE @ErrorState INT
 
        SELECT  @ErrorMessage = ERROR_MESSAGE(),
                @ErrorSeverity = ERROR_SEVERITY(),
                @ErrorState = ERROR_STATE()
 
        IF @localTran = 1 AND XACT_STATE() <> 0
            ROLLBACK TRAN
 
        RAISERROR ( @ErrorMessage, @ErrorSeverity, @ErrorState)
 
    END CATCH
 
END