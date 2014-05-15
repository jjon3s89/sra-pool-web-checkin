USE [applicationdata]
GO

alter table dbo.Shares add Paid_Dues bit not null default 1;

alter table dbo.People add Is_Minor bit not null default 0;

alter table dbo.People add Needs_Adult bit not null default 1;

alter table dbo.Families add guest_ticket_count int not null default 0;

alter table dbo.Entries add Entry_Type varchar(8) not null default 'MEMBER';


/****** Object:  Table [dbo].[AuditLog]    Script Date: 5/14/2014 6:17:50 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AuditLog](
	[date] [datetime2] NOT NULL,
	[message] [ntext] NOT NULL,
	[personId] [int] NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_AuditLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[AuditLog] ADD  CONSTRAINT [DF_AuditLog_date]  DEFAULT (getdate()) FOR [date]
GO


/****** Object:  View [dbo].[rpt_employee_time]    Script Date: 5/15/2014 12:55:20 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[rpt_employee_time]
AS
SELECT        TOP (100) PERCENT e.FirstName, e.LastName, e.EmployeeType, CASE et.entry WHEN 0 THEN 'Out' ELSE 'In' END AS in_out, et.Time
                         AS date_time
FROM            dbo.Employee AS e INNER JOIN
                         dbo.EmployeeTime AS et ON e.Id = et.EmployeeId
ORDER BY et.Time DESC

GO

