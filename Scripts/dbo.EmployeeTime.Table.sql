USE [applicationdata]
GO
/****** Object:  Table [dbo].[EmployeeTime]    Script Date: 5/30/2013 7:02:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeTime](
	[EmployeeId] [int] NOT NULL,
	[Entry] [bit] NOT NULL,
	[Time] [datetime] NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_EmployeeTime] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[EmployeeTime]  WITH CHECK ADD  CONSTRAINT [FK_Employee_EmployeeTime] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[EmployeeTime] CHECK CONSTRAINT [FK_Employee_EmployeeTime]
GO
