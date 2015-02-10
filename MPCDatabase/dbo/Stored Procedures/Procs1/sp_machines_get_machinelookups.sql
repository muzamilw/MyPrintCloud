CREATE PROCEDURE dbo.sp_machines_get_machinelookups
(@MachineID int)
AS
	select * from tbl_machine_lookup_methods where MachineID = @MachineID
        RETURN