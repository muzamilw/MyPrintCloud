CREATE PROCEDURE dbo.sp_machines_get_machinetypes
--(@SystemSiteID int)
AS
	Select MachineCatID,MachineCategory from tbl_machine_categories  order by MachineCategory
        RETURN