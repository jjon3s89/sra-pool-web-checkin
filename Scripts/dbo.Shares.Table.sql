USE [applicationdata]
GO
ALTER TABLE [dbo].[Shares] DROP CONSTRAINT [Share_Address]
GO
/****** Object:  Table [dbo].[Shares]    Script Date: 5/24/2013 9:03:06 AM ******/
DROP TABLE [dbo].[Shares]
GO
/****** Object:  Table [dbo].[Shares]    Script Date: 5/24/2013 9:03:06 AM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shares](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Notes] [nvarchar](255) NULL,
	[Message] [nvarchar](255) NULL,
	[Active] [bit] NULL,
	[Share_Address] [int] NULL,
 CONSTRAINT [PK_Shares] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Shares]  WITH NOCHECK ADD  CONSTRAINT [Share_Address] FOREIGN KEY([Share_Address])
REFERENCES [dbo].[Addresses] ([Id])
GO
ALTER TABLE [dbo].[Shares] CHECK CONSTRAINT [Share_Address]
GO
