CREATE PROCEDURE dbo.sp_machines_get_costcentresWithName
(@MachineID int)
AS
	SELECT tbl_costcentre_groups.GroupID, tbl_costcentre_groups.GroupName 
	FROM tbl_machine_costcentre_groups 
	INNER JOIN tbl_costcentre_groups ON (tbl_machine_costcentre_groups.CostCentreGroupID = tbl_costcentre_groups.GroupID)  
	where tbl_machine_costcentre_groups.MachineID=@MachineID order by tbl_costcentre_groups.GroupName 
	RETURN