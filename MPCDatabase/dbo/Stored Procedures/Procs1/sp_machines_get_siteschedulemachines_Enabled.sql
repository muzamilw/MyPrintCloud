CREATE PROCEDURE [dbo].[sp_machines_get_siteschedulemachines_Enabled]
(@SystemSiteID int)
AS
	select * from tbl_machines where SystemSiteID=@SystemSiteID and
	 IsScheduleable <> 0 and (IsDisabled = 0 or IsDisabled is null ) order by  MachineCatID
        RETURN