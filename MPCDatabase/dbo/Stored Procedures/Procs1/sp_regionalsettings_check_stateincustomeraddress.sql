CREATE PROCEDURE dbo.sp_regionalsettings_check_stateincustomeraddress
(@StateID int)
AS
	select StateID from tbl_customeraddresses where StateID=@StateID
	RETURN