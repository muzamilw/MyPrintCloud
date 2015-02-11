CREATE PROCEDURE dbo.sp_SystemLog_Delete_SystemRecord
(
	@SectionID int,
	@RecordID int	
)
AS
	delete from tbl_system_log where SectionID=@SectionID and ID=@RecordID	
	RETURN