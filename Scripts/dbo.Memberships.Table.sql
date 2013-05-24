USE [applicationdata]
GO
ALTER TABLE [dbo].[Memberships] DROP CONSTRAINT [DF_Memberships_Id]
GO
/****** Object:  Table [dbo].[Memberships]    Script Date: 5/24/2013 9:03:06 AM ******/
DROP TABLE [dbo].[Memberships]
GO
/****** Object:  Table [dbo].[Memberships]    Script Date: 5/24/2013 9:03:06 AM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Memberships](
	[Id] [int] NOT NULL,
 CONSTRAINT [PK_Memberships] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Memberships] ADD  CONSTRAINT [DF_Memberships_Id]  DEFAULT ((0)) FOR [Id]
GO
