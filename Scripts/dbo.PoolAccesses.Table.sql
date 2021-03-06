USE [applicationdata]
GO
/****** Object:  Table [dbo].[PoolAccesses]    Script Date: 7/17/2013 5:04:34 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PoolAccesses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PoolAccess_Share] [int] NOT NULL,
	[PoolAccess_ShareOwnerType] [int] NULL,
	[PoolAccess_ShareUserType] [int] NULL,
	[PoolAccess_Family] [int] NOT NULL,
 CONSTRAINT [PK_PoolAccesses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[PoolAccesses] ADD  CONSTRAINT [DF_PoolAccesses_PoolAccess_Share]  DEFAULT ((0)) FOR [PoolAccess_Share]
GO
ALTER TABLE [dbo].[PoolAccesses] ADD  CONSTRAINT [DF_PoolAccesses_PoolAccess_Family]  DEFAULT ((0)) FOR [PoolAccess_Family]
GO
ALTER TABLE [dbo].[PoolAccesses]  WITH NOCHECK ADD  CONSTRAINT [PoolAccess_Family] FOREIGN KEY([PoolAccess_Family])
REFERENCES [dbo].[Families] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PoolAccesses] CHECK CONSTRAINT [PoolAccess_Family]
GO
ALTER TABLE [dbo].[PoolAccesses]  WITH NOCHECK ADD  CONSTRAINT [PoolAccess_ShareOwnerType] FOREIGN KEY([PoolAccess_ShareOwnerType])
REFERENCES [dbo].[ShareOwnerTypes] ([Id])
GO
ALTER TABLE [dbo].[PoolAccesses] CHECK CONSTRAINT [PoolAccess_ShareOwnerType]
GO
ALTER TABLE [dbo].[PoolAccesses]  WITH NOCHECK ADD  CONSTRAINT [PoolAccess_ShareUserType] FOREIGN KEY([PoolAccess_ShareUserType])
REFERENCES [dbo].[ShareUserTypes] ([Id])
GO
ALTER TABLE [dbo].[PoolAccesses] CHECK CONSTRAINT [PoolAccess_ShareUserType]
GO
