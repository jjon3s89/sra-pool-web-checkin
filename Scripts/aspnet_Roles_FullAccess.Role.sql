USE [applicationdata]
GO
/****** Object:  DatabaseRole [aspnet_Roles_FullAccess]    Script Date: 7/17/2013 5:04:33 PM ******/
CREATE ROLE [aspnet_Roles_FullAccess]
GO
ALTER ROLE [aspnet_Roles_BasicAccess] ADD MEMBER [aspnet_Roles_FullAccess]
GO
ALTER ROLE [aspnet_Roles_ReportingAccess] ADD MEMBER [aspnet_Roles_FullAccess]
GO
