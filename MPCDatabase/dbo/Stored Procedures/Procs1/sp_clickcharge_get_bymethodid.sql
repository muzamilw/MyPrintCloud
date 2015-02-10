CREATE PROCEDURE dbo.sp_clickcharge_get_bymethodid
(@MethodID int
)
AS
select * from tbl_machine_clickchargelookup where MethodID=@MethodID
	RETURN