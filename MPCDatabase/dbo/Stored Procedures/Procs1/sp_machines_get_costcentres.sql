CREATE PROCEDURE dbo.sp_machines_get_costcentres
(@MachineID int)
AS
	select * from tbl_machine_costcentre_groups where MachineID=@MachineID
	RETURN