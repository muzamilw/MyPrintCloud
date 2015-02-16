CREATE PROCEDURE dbo.sp_SystemLog_Get_LogBySectionAndRecordID
(
	@RecordID int,
	@SectionID int	
)
AS
	SELECT  tbl_system_log.SectionID, tbl_system_log.LogID, tbl_system_log.ID, tbl_system_log.Notes,tbl_systemusers.FullName as [User], tbl_system_log.LogDate as [Date], tbl_system_log.LogTime as [Time], tbl_system_log.SystemUserID,  tbl_systemusers.UserName FROM tbl_system_log INNER JOIN tbl_systemusers ON (tbl_system_log.SystemUserID = tbl_systemusers.SystemUserID) where SectionID=@SectionID and ID=@RecordID order by tbl_system_log.LogDate desc , tbl_system_log.LogTime desc
	RETURN