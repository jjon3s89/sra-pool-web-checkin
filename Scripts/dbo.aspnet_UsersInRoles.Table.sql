USE [applicationdata]
GO
ALTER TABLE [dbo].[aspnet_UsersInRoles] DROP CONSTRAINT [FK__aspnet_Us__UserI__49C3F6B7]
GO
ALTER TABLE [dbo].[aspnet_UsersInRoles] DROP CONSTRAINT [FK__aspnet_Us__RoleI__4AB81AF0]
GO
/****** Object:  Index [aspnet_UsersInRoles_index]    Script Date: 5/24/2013 9:03:06 AM ******/
DROP INDEX [aspnet_UsersInRoles_index] ON [dbo].[aspnet_UsersInRoles]
GO
/****** Object:  Table [dbo].[aspnet_UsersInRoles]    Script Date: 5/24/2013 9:03:06 AM ******/
DROP TABLE [dbo].[aspnet_UsersInRoles]
GO
/****** Object:  Table [dbo].[aspnet_UsersInRoles]    Script Date: 5/24/2013 9:03:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_UsersInRoles](
	[UserId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Index [aspnet_UsersInRoles_index]    Script Date: 5/24/2013 9:03:06 AM ******/
CREATE NONCLUSTERED INDEX [aspnet_UsersInRoles_index] ON [dbo].[aspnet_UsersInRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO
