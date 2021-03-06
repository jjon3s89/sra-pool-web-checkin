USE [applicationdata]
GO
/****** Object:  Table [dbo].[Addresses]    Script Date: 7/17/2013 5:04:34 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Addresses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Address2] [nvarchar](255) NULL,
	[City] [nvarchar](255) NULL,
	[State] [nvarchar](255) NULL,
	[Zip] [nvarchar](255) NULL,
	[HouseNumber] [smallint] NULL,
	[Address_Street] [int] NOT NULL,
 CONSTRAINT [PK_Addresses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Addresses] ADD  CONSTRAINT [DF_Addresses_Address_Street]  DEFAULT ((0)) FOR [Address_Street]
GO
ALTER TABLE [dbo].[Addresses]  WITH NOCHECK ADD  CONSTRAINT [Address_Street] FOREIGN KEY([Address_Street])
REFERENCES [dbo].[Streets] ([Id])
GO
ALTER TABLE [dbo].[Addresses] CHECK CONSTRAINT [Address_Street]
GO
