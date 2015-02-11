CREATE PROCEDURE dbo.sp_clickcharge_check
(@ID int
)
AS
select ID from tbl_machine_clickchargelookup where ID=@ID
	RETURN