CREATE PROCEDURE dbo.sp_machines_get_resourceswithnames
(@MachineID int)
AS
	select ResourceID,UserName from tbl_machine_resource,tbl_systemusers where MachineID=@MachineID and ResourceID=SystemUserID order by tbl_systemusers.UserName
	RETURN