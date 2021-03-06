USE [EMSDB]
GO
/****** Object:  StoredProcedure [dbo].[DeleteEvent]    Script Date: 9/28/2017 9:26:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Thanuja
-- Create date: 20170910
-- Description:	
-- =============================================
ALTER PROCEDURE [dbo].[DeleteEvent]( 
	
		   @EventId int,
		    @Success bit output,
			@Message VARCHAR(MAX) OUTPUT
	)
AS
BEGIN

		UPDATE [dbo].[Event]
			SET [Is_Active] = 0
		WHERE [Event_Id] = @EventId
		SET @Success = 1
		SET @Message = 'Success'
	IF @@ROWCOUNT > 0
		BEGIN 
			SET @Success = 1
			SET @Message = 'Success'
		END
		ELSE
		BEGIN
			SET @Success = 0
			SET @Message = ERROR_MESSAGE()
		END
END
