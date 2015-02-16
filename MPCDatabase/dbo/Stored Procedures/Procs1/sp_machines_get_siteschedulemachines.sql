CREATE PROCEDURE dbo.sp_machines_get_siteschedulemachines
(@SystemSiteID int)
AS
	select * from tbl_machines where SystemSiteID=@SystemSiteID and IsScheduleable <> 0 order by  MachineCatID
        RETURN