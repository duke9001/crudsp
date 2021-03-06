USE [EMSDB]
GO
/****** Object:  StoredProcedure [dbo].[UpdateEvent]    Script Date: 9/28/2017 9:33:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Thanuja
-- Create date: 20170911
-- Description:	
-- =============================================
ALTER PROCEDURE [dbo].[UpdateEvent]( 
			@Name varchar(max),
			@EventId INT,
		   @Success BIT OUTPUT,
		   @Message VARCHAR(MAX) OUTPUT
	)
AS
BEGIN

		UPDATE [dbo].[Event]
			SET [Name] = @Name
				
				
				
		WHERE [Event_Id] = @EventId

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
