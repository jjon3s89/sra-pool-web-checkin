USE [applicationdata]
GO
ALTER TABLE [dbo].[ShareUserTypes] DROP CONSTRAINT [DF_ShareUserTypes_Name]
GO
/****** Object:  Table [dbo].[ShareUserTypes]    Script Date: 5/24/2013 9:03:06 AM ******/
DROP TABLE [dbo].[ShareUserTypes]
GO
/****** Object:  Table [dbo].[ShareUserTypes]    Script Date: 5/24/2013 9:03:06 AM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShareUserTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](255) NULL,
 CONSTRAINT [PK_ShareUserTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ShareUserTypes] ADD  CONSTRAINT [DF_ShareUserTypes_Name]  DEFAULT ('') FOR [Name]
GO
