USE [applicationdata]
GO
/****** Object:  DatabaseRole [aspnet_Profile_FullAccess]    Script Date: 5/30/2013 7:02:15 PM ******/
CREATE ROLE [aspnet_Profile_FullAccess]
GO
ALTER ROLE [aspnet_Profile_BasicAccess] ADD MEMBER [aspnet_Profile_FullAccess]
GO
ALTER ROLE [aspnet_Profile_ReportingAccess] ADD MEMBER [aspnet_Profile_FullAccess]
GO
