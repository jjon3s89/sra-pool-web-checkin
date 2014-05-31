use applicationdata;

BEGIN TRANSACTION
GO
update dbo.People set Needs_Adult = Is_Minor | Needs_Adult;
GO

ALTER TABLE dbo.People
	DROP CONSTRAINT DF_People_Is_Minor
GO
ALTER TABLE dbo.People
	DROP COLUMN Is_Minor
GO
ALTER TABLE dbo.People SET (LOCK_ESCALATION = TABLE)
GO
COMMIT