CREATE PROCEDURE dbo.sp_RecordLocking_Get_CheckFinishedGoodCatalogLockByID
( 
	@RecordID int
)
AS
	SELECT tbl_finishedgoods_catalogue.LockedBy, tbl_systemusers.UserName, tbl_systemusers.FullName, tbl_systemusers.CurrentMachineName, tbl_systemusers.CurrentMachineIP FROM tbl_systemusers right Outer Join tbl_finishedgoods_catalogue ON (tbl_systemusers.SystemUserID = tbl_finishedgoods_catalogue.LockedBy) WHERE tbl_finishedgoods_catalogue.ID = @RecordID
	RETURN