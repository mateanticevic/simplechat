-- =============================================
-- Author:      MA
-- Create date: 2016-10-07
-- =============================================
CREATE PROCEDURE [dbo].[MessageInsert]
	@Nickname nvarchar(50),
	@ConversationIdentifier nvarchar(32),
	@Message nvarchar(MAX)
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
		DECLARE @ProfileId int = dbo.GetProfileId(@Nickname)
		DECLARE @Identifier nvarchar(32) = (SELECT REPLACE(NEWID(), '-', ''))
	
		INSERT INTO [Message] (CreatedOn, ConversationId, Message, FromProfileId, Identifier)
		VALUES (GETDATE(), @ConversationId, @Message, @ProfileId, @Identifier)
 
        IF @localTran = 1 AND XACT_STATE() = 1
            COMMIT TRAN LocalTran

		SELECT @Identifier AS Identifier
 
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