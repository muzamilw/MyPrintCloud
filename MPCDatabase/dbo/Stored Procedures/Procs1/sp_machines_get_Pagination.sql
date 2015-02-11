CREATE PROCEDURE dbo.sp_machines_get_Pagination
(@MachineID int)
AS
	select * from tbl_machine_pagination_profile where MachineID=@MachineID
	RETURN