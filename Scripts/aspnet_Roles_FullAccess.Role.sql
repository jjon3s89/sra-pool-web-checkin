USE [applicationdata]
GO
/****** Object:  DatabaseRole [aspnet_Roles_FullAccess]    Script Date: 5/30/2013 7:02:15 PM ******/
CREATE ROLE [aspnet_Roles_FullAccess]
GO
ALTER ROLE [aspnet_Roles_BasicAccess] ADD MEMBER [aspnet_Roles_FullAccess]
GO
ALTER ROLE [aspnet_Roles_ReportingAccess] ADD MEMBER [aspnet_Roles_FullAccess]
GO
