CREATE PROCEDURE dbo.sp_machines_get_machinenamebyid
(@MachineID int)
AS
	select MachineName from tbl_machines where MachineID=@MachineID
        RETURN