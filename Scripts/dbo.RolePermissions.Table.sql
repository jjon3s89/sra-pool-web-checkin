USE [applicationdata]
GO
/****** Object:  Table [dbo].[RolePermissions]    Script Date: 7/17/2013 5:04:34 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RolePermissions](
	[RoleName] [nvarchar](128) NOT NULL,
	[PermissionId] [nvarchar](322) NOT NULL,
 CONSTRAINT [PK_RolePermissions] PRIMARY KEY CLUSTERED 
(
	[RoleName] ASC,
	[PermissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[RolePermissions] ADD  CONSTRAINT [DF_RolePermissions_RoleName]  DEFAULT ('') FOR [RoleName]
GO
ALTER TABLE [dbo].[RolePermissions] ADD  CONSTRAINT [DF_RolePermissions_PermissionId]  DEFAULT ('') FOR [PermissionId]
GO
