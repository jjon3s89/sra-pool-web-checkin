USE [applicationdata]
GO
/****** Object:  StoredProcedure [dbo].[aspnet_UnRegisterSchemaVersion]    Script Date: 7/17/2013 5:04:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[aspnet_UnRegisterSchemaVersion]
    @Feature                   nvarchar(128),
    @CompatibleSchemaVersion   nvarchar(128)
AS
BEGIN
    DELETE FROM dbo.aspnet_SchemaVersions
        WHERE   Feature = LOWER(@Feature) AND @CompatibleSchemaVersion = CompatibleSchemaVersion
END
GO
