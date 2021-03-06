USE [applicationdata]
GO
/****** Object:  Table [dbo].[Families]    Script Date: 7/17/2013 5:04:34 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Families](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FamilyName] [nvarchar](255) NOT NULL,
	[EmergencyContact] [nvarchar](255) NULL,
	[EmergencyContactPhone] [nvarchar](255) NULL,
	[Email] [nvarchar](255) NULL,
	[PrimaryPhone] [nvarchar](255) NULL,
	[Family_Address] [int] NULL,
	[Family_ShareUserType] [int] NULL,
 CONSTRAINT [PK_Families] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Families] ADD  CONSTRAINT [DF_Families_FamilyName]  DEFAULT ('') FOR [FamilyName]
GO
ALTER TABLE [dbo].[Families]  WITH NOCHECK ADD  CONSTRAINT [Family_Address] FOREIGN KEY([Family_Address])
REFERENCES [dbo].[Addresses] ([Id])
GO
ALTER TABLE [dbo].[Families] CHECK CONSTRAINT [Family_Address]
GO
ALTER TABLE [dbo].[Families]  WITH NOCHECK ADD  CONSTRAINT [Family_ShareUserType] FOREIGN KEY([Family_ShareUserType])
REFERENCES [dbo].[ShareUserTypes] ([Id])
GO
ALTER TABLE [dbo].[Families] CHECK CONSTRAINT [Family_ShareUserType]
GO
