USE [applicationdata]
GO
/****** Object:  DatabaseRole [aspnet_Membership_FullAccess]    Script Date: 5/30/2013 7:02:15 PM ******/
CREATE ROLE [aspnet_Membership_FullAccess]
GO
ALTER ROLE [aspnet_Membership_BasicAccess] ADD MEMBER [aspnet_Membership_FullAccess]
GO
ALTER ROLE [aspnet_Membership_ReportingAccess] ADD MEMBER [aspnet_Membership_FullAccess]
GO
