USE [applicationdata]
GO
/****** Object:  Table [dbo].[Messages]    Script Date: 7/17/2013 5:04:34 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Messages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Subject] [nvarchar](255) NULL,
	[Body] [nvarchar](255) NULL,
	[Sent] [datetime] NULL,
	[Read] [datetime] NULL,
	[From] [nvarchar](255) NULL,
	[ReplyPhone] [nvarchar](255) NULL,
	[ReplyEmail] [nvarchar](255) NULL,
	[Display] [nvarchar](255) NULL,
	[Message_Family] [int] NULL,
	[Message_Person] [int] NULL,
 CONSTRAINT [PK_Messages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Messages]  WITH NOCHECK ADD  CONSTRAINT [Message_Family] FOREIGN KEY([Message_Family])
REFERENCES [dbo].[Families] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [Message_Family]
GO
ALTER TABLE [dbo].[Messages]  WITH NOCHECK ADD  CONSTRAINT [Message_Person] FOREIGN KEY([Message_Person])
REFERENCES [dbo].[People] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [Message_Person]
GO
