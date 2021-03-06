USE [EMSDB]
GO
/****** Object:  StoredProcedure [dbo].[InsertPosition]    Script Date: 9/17/2017 5:59:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Thanuja
-- Create date: 20170910
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[InsertEvent]( 
	@Name varchar(max),
           
		   
		   @Success BIT OUTPUT,
		   @Message VARCHAR(MAX) OUTPUT
	)
AS
BEGIN

	BEGIN TRY
		INSERT INTO [dbo].[Event]
           ([Name])

		VALUES (@Name)

		SET @Success = 1
		SET @Message = 'Success'

	END TRY

	BEGIN CATCH
		SET @Success = 0
		SET @Message = ERROR_MESSAGE()
	END CATCH


END
