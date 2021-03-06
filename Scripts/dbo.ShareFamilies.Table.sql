USE [applicationdata]
GO
/****** Object:  Table [dbo].[ShareFamilies]    Script Date: 7/17/2013 5:04:34 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShareFamilies](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Active] [bit] NOT NULL,
	[ShareFamily_Share] [int] NOT NULL,
	[ShareFamily_Family] [int] NOT NULL,
	[ShareFamily_ShareUserType] [int] NOT NULL,
 CONSTRAINT [PK_ShareFamilies] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ShareFamilies] ADD  CONSTRAINT [DF_ShareFamilies_Active]  DEFAULT ((0)) FOR [Active]
GO
ALTER TABLE [dbo].[ShareFamilies] ADD  CONSTRAINT [DF_ShareFamilies_ShareFamily_Share]  DEFAULT ((0)) FOR [ShareFamily_Share]
GO
ALTER TABLE [dbo].[ShareFamilies] ADD  CONSTRAINT [DF_ShareFamilies_ShareFamily_Family]  DEFAULT ((0)) FOR [ShareFamily_Family]
GO
ALTER TABLE [dbo].[ShareFamilies] ADD  CONSTRAINT [DF_ShareFamilies_ShareFamily_ShareUserType]  DEFAULT ((0)) FOR [ShareFamily_ShareUserType]
GO
ALTER TABLE [dbo].[ShareFamilies]  WITH NOCHECK ADD  CONSTRAINT [ShareFamily_Family] FOREIGN KEY([ShareFamily_Family])
REFERENCES [dbo].[Families] ([Id])
GO
ALTER TABLE [dbo].[ShareFamilies] CHECK CONSTRAINT [ShareFamily_Family]
GO
ALTER TABLE [dbo].[ShareFamilies]  WITH NOCHECK ADD  CONSTRAINT [ShareFamily_Share] FOREIGN KEY([ShareFamily_Share])
REFERENCES [dbo].[Shares] ([Id])
GO
ALTER TABLE [dbo].[ShareFamilies] CHECK CONSTRAINT [ShareFamily_Share]
GO
ALTER TABLE [dbo].[ShareFamilies]  WITH NOCHECK ADD  CONSTRAINT [ShareFamily_ShareUserType] FOREIGN KEY([ShareFamily_ShareUserType])
REFERENCES [dbo].[ShareUserTypes] ([Id])
GO
ALTER TABLE [dbo].[ShareFamilies] CHECK CONSTRAINT [ShareFamily_ShareUserType]
GO
