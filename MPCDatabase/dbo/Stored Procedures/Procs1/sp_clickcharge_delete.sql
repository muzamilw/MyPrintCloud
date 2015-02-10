CREATE PROCEDURE dbo.sp_clickcharge_delete
(@ID int
)
AS
delete from tbl_machine_clickchargelookup where ID=@ID
	RETURN