CREATE PROCEDURE dbo.sp_machines_get_machineinkcoverage
(@MachineID int)
AS
	select * from tbl_machine_ink_coverage where MachineID=@MachineID
	RETURN