USE [applicationdata]
GO
/****** Object:  DatabaseRole [aspnet_Profile_FullAccess]    Script Date: 7/17/2013 5:04:33 PM ******/
CREATE ROLE [aspnet_Profile_FullAccess]
GO
ALTER ROLE [aspnet_Profile_BasicAccess] ADD MEMBER [aspnet_Profile_FullAccess]
GO
ALTER ROLE [aspnet_Profile_ReportingAccess] ADD MEMBER [aspnet_Profile_FullAccess]
GO
