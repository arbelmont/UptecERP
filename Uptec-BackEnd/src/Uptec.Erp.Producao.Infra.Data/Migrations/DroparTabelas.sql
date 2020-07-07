DECLARE @cmd varchar(4000)
DECLARE cmds CURSOR FOR
	SELECT 'DROP TABLE [' + name + ']' AS cmd
	FROM	sys.objects 
	WHERE	type_desc = 'USER_TABLE'
		AND LEFT(NAME,3) <> 'ASP'
		AND NAME <> '__EFMigrationsHistory' 

OPEN cmds
WHILE 1 = 1
BEGIN
    FETCH cmds INTO @cmd
    
	IF @@fetch_status != 0 BREAK

	print 'exluindo tabelas [' + @cmd + ']'
    EXEC(@cmd)
END
CLOSE cmds;
DEALLOCATE cmds
GO

DROP SEQUENCE [LoteSequence]
GO

DROP SEQUENCE [OrdemSequence]
GO


