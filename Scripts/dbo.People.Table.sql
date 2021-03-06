USE [applicationdata]
GO
/****** Object:  Table [dbo].[People]    Script Date: 7/17/2013 5:04:34 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[People](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](255) NOT NULL,
	[LastName] [nvarchar](255) NULL,
	[Picture] [varbinary](max) NULL,
	[MiddleName] [nvarchar](255) NULL,
	[Birthday] [datetime] NULL,
	[Gender] [nvarchar](255) NULL,
	[Email] [nvarchar](255) NULL,
	[HomePhone] [nvarchar](255) NULL,
	[MobilePhone] [nvarchar](255) NULL,
	[WorkPhone] [nvarchar](255) NULL,
	[EmergencyContact] [nvarchar](255) NULL,
	[EmergencyContactPhone] [nvarchar](255) NULL,
	[InsuranceCompany] [nvarchar](255) NULL,
	[InsurancePolicyNumber] [nvarchar](255) NULL,
	[Family_Person] [int] NULL,
	[FamilyMemberType_Person] [int] NULL,
	[Is_Guest] [bit] NULL,
 CONSTRAINT [PK_People] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING ON
GO
ALTER TABLE [dbo].[People] ADD  CONSTRAINT [DF_People_FirstName]  DEFAULT ('') FOR [FirstName]
GO
ALTER TABLE [dbo].[People]  WITH NOCHECK ADD  CONSTRAINT [Family_Person] FOREIGN KEY([Family_Person])
REFERENCES [dbo].[Families] ([Id])
GO
ALTER TABLE [dbo].[People] CHECK CONSTRAINT [Family_Person]
GO
ALTER TABLE [dbo].[People]  WITH NOCHECK ADD  CONSTRAINT [FamilyMemberType_Person] FOREIGN KEY([FamilyMemberType_Person])
REFERENCES [dbo].[FamilyMemberTypes] ([Id])
GO
ALTER TABLE [dbo].[People] CHECK CONSTRAINT [FamilyMemberType_Person]
GO
