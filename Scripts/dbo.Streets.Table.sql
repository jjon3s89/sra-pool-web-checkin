USE [applicationdata]
GO
ALTER TABLE [dbo].[Streets] DROP CONSTRAINT [DF_Streets_Name]
GO
/****** Object:  Table [dbo].[Streets]    Script Date: 5/24/2013 9:03:06 AM ******/
DROP TABLE [dbo].[Streets]
GO
/****** Object:  Table [dbo].[Streets]    Script Date: 5/24/2013 9:03:06 AM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Streets](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Streets] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Streets] ADD  CONSTRAINT [DF_Streets_Name]  DEFAULT ('') FOR [Name]
GO
