CREATE PROCEDURE dbo.sp_machines_update_changeFlag
(@MachineID int,
@FlagID int)
AS
	Update tbl_machines set FlagID=@FlagID where MachineID=@MachineID
	RETURN