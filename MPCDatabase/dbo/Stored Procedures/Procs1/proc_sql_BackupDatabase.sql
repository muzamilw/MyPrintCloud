CREATE Procedure [dbo].[proc_sql_BackupDatabase]
As
Begin
	Declare @dbPathName	varchar(2000)

	Set @dbPathName = 'D:\Backups\MPC\MPC_' + Replace(Convert(varchar,getdate(),110),'-','') +
		'_' + Substring(Replace(Convert(varchar,getdate(),114),':',''),1,6) + '.BAK'

	-- Backup database.	
	BACKUP DATABASE MPC
	TO DISK = @dbPathName 
	WITH RETAINDAYS = 7, NOFORMAT, INIT,
		NAME = 'CLYDY FULL DATABASE BACKUP',
		SKIP, NOREWIND, NOUNLOAD,  STATS = 10

	-- Truncate the log by changing the database recovery model to SIMPLE. 
	ALTER DATABASE MPC	 
	SET RECOVERY SIMPLE	 
	 
	-- Shrink the truncated log file to 1 MB.	 
	DBCC SHRINKFILE (2, 1)  -- here 2 is the file ID for trasaction log file,you can also mention the log file name (dbname_log)
	--DBCC SHRINKFILE (N'Clydy_log', 1);  
	 
	-- Reset the database recovery model.	 
	ALTER DATABASE MPC	 
		SET RECOVERY FULL

End