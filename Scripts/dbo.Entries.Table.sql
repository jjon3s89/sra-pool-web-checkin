USE [applicationdata]
GO
ALTER TABLE [dbo].[Entries] DROP CONSTRAINT [Entry_Person]
GO
ALTER TABLE [dbo].[Entries] DROP CONSTRAINT [DF_Entries_Is_exit]
GO
ALTER TABLE [dbo].[Entries] DROP CONSTRAINT [DF_Entries_Entry_Person]
GO
ALTER TABLE [dbo].[Entries] DROP CONSTRAINT [DF_Entries_Time]
GO
/****** Object:  Table [dbo].[Entries]    Script Date: 5/24/2013 9:03:06 AM ******/
DROP TABLE [dbo].[Entries]
GO
/****** Object:  Table [dbo].[Entries]    Script Date: 5/24/2013 9:03:06 AM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Entries](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Time] [datetime] NOT NULL,
	[Entry_Person] [int] NOT NULL,
	[Is_exit] [bit] NOT NULL,
 CONSTRAINT [PK_Entries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Entries] ADD  CONSTRAINT [DF_Entries_Time]  DEFAULT ((0)) FOR [Time]
GO
ALTER TABLE [dbo].[Entries] ADD  CONSTRAINT [DF_Entries_Entry_Person]  DEFAULT ((0)) FOR [Entry_Person]
GO
ALTER TABLE [dbo].[Entries] ADD  CONSTRAINT [DF_Entries_Is_exit]  DEFAULT ((0)) FOR [Is_exit]
GO
ALTER TABLE [dbo].[Entries]  WITH NOCHECK ADD  CONSTRAINT [Entry_Person] FOREIGN KEY([Entry_Person])
REFERENCES [dbo].[People] ([Id])
GO
ALTER TABLE [dbo].[Entries] CHECK CONSTRAINT [Entry_Person]
GO
