USE [applicationdata]
GO
/****** Object:  View [dbo].[vw_aspnet_Applications]    Script Date: 5/24/2013 9:03:06 AM ******/
DROP VIEW [dbo].[vw_aspnet_Applications]
GO
/****** Object:  View [dbo].[vw_aspnet_Applications]    Script Date: 5/24/2013 9:03:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

  CREATE VIEW [dbo].[vw_aspnet_Applications]
  AS SELECT [dbo].[aspnet_Applications].[ApplicationName], [dbo].[aspnet_Applications].[LoweredApplicationName], [dbo].[aspnet_Applications].[ApplicationId], [dbo].[aspnet_Applications].[Description]
  FROM [dbo].[aspnet_Applications]
  
GO
