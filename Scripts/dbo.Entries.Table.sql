USE [applicationdata]
GO
/****** Object:  Table [dbo].[Entries]    Script Date: 7/17/2013 5:04:34 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Entries](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Time] [datetime] NOT NULL,
	[Entry_Person] [int] NOT NULL,
	[Is_exit] [bit] NULL,
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
ALTER TABLE [dbo].[Entries]  WITH NOCHECK ADD  CONSTRAINT [Entry_Person] FOREIGN KEY([Entry_Person])
REFERENCES [dbo].[People] ([Id])
GO
ALTER TABLE [dbo].[Entries] CHECK CONSTRAINT [Entry_Person]
GO
