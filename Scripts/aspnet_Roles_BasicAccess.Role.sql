USE [applicationdata]
GO

DECLARE @RoleName sysname
set @RoleName = N'aspnet_Roles_BasicAccess'

IF @RoleName <> N'public' and (select is_fixed_role from sys.database_principals where name = @RoleName) = 0
BEGIN
	DECLARE @RoleMemberName sysname
	DECLARE Member_Cursor CURSOR FOR
	select [name]
	from sys.database_principals 
	where principal_id in ( 
		select member_principal_id
		from sys.database_role_members
		where role_principal_id in (
			select principal_id
			FROM sys.database_principals where [name] = @RoleName AND type = 'R'))

	OPEN Member_Cursor;

	FETCH NEXT FROM Member_Cursor
	into @RoleMemberName
	
	DECLARE @SQL NVARCHAR(4000)

	WHILE @@FETCH_STATUS = 0
	BEGIN
		
		SET @SQL = 'ALTER ROLE '+ QUOTENAME(@RoleName,'[') +' DROP MEMBER '+ QUOTENAME(@RoleMemberName,'[')
		EXEC(@SQL)
		
		FETCH NEXT FROM Member_Cursor
		into @RoleMemberName
	END;

	CLOSE Member_Cursor;
	DEALLOCATE Member_Cursor;
END
/****** Object:  DatabaseRole [aspnet_Roles_BasicAccess]    Script Date: 5/24/2013 9:03:06 AM ******/
DROP ROLE [aspnet_Roles_BasicAccess]
GO
/****** Object:  DatabaseRole [aspnet_Roles_BasicAccess]    Script Date: 5/24/2013 9:03:06 AM ******/
CREATE ROLE [aspnet_Roles_BasicAccess]
GO
