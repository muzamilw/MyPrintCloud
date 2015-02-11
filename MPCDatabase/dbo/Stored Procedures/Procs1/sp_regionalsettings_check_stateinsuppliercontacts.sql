CREATE PROCEDURE dbo.sp_regionalsettings_check_stateinsuppliercontacts
(@StateID int)
AS
	select HomeStateID from tbl_suppliercontacts where HomeStateID=@StateID
	RETURN