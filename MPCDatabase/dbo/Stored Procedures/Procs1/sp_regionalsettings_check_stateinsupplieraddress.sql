CREATE PROCEDURE dbo.sp_regionalsettings_check_stateinsupplieraddress
(@StateID int)
AS
select StateID from tbl_supplieraddresses where StateID=@StateID
	RETURN