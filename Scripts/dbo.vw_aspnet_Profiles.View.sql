USE [applicationdata]
GO
/****** Object:  View [dbo].[vw_aspnet_Profiles]    Script Date: 5/24/2013 9:03:06 AM ******/
DROP VIEW [dbo].[vw_aspnet_Profiles]
GO
/****** Object:  View [dbo].[vw_aspnet_Profiles]    Script Date: 5/24/2013 9:03:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

  CREATE VIEW [dbo].[vw_aspnet_Profiles]
  AS SELECT [dbo].[aspnet_Profile].[UserId], [dbo].[aspnet_Profile].[LastUpdatedDate],
      [DataSize]=  DATALENGTH([dbo].[aspnet_Profile].[PropertyNames])
                 + DATALENGTH([dbo].[aspnet_Profile].[PropertyValuesString])
                 + DATALENGTH([dbo].[aspnet_Profile].[PropertyValuesBinary])
  FROM [dbo].[aspnet_Profile]
  
GO
