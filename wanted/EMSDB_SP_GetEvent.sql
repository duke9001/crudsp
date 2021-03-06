USE [EMSDB]
GO
/****** Object:  StoredProcedure [dbo].[GetPosition]    Script Date: 9/17/2017 5:54:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Thanuja
-- Create date: 20170911
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[GetEvent]( 
			@EventId int = 0
) AS

BEGIN
	SELECT [Event_Id]
      ,[Name]
      ,[Is_Active]
  FROM [dbo].[Event]

  WHERE (([Event_Id] = @EventId OR @EventId = 0)
		AND Is_Active = 1)

END
