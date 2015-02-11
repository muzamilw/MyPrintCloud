CREATE PROCEDURE dbo.sp_SystemLog_Insert_LogRecord
(
	@SectionID int,
	@RecordID int,
	@Notes text,
	@LogDate datetime,
	@LogTime datetime,
	@SystemUserID int
)
AS
	insert into tbl_system_log (SectionID,ID,Notes,LogDate,LogTime,SystemUserID) 
	VALUES (@SectionID,@RecordID,@Notes,@LogDate,@LogTime,@SystemUserID)
	RETURN