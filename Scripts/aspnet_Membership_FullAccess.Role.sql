USE [applicationdata]
GO
/****** Object:  DatabaseRole [aspnet_Membership_FullAccess]    Script Date: 7/17/2013 5:04:33 PM ******/
CREATE ROLE [aspnet_Membership_FullAccess]
GO
ALTER ROLE [aspnet_Membership_BasicAccess] ADD MEMBER [aspnet_Membership_FullAccess]
GO
ALTER ROLE [aspnet_Membership_ReportingAccess] ADD MEMBER [aspnet_Membership_FullAccess]
GO
