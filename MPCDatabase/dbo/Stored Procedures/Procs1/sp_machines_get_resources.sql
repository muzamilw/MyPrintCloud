CREATE PROCEDURE dbo.sp_machines_get_resources
(@MachineID int)
AS
	select * from tbl_machine_resource where MachineID=@MachineID
	RETURN