CREATE PROCEDURE dbo.sp_machines_check_cylinder
(@CylinderName varchar(50),
@CylinderID int)
AS
	select ID from tbl_machine_cylinders where ID<>@CylinderID and Name=@CylinderName
	RETURN