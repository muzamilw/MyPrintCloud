CREATE PROCEDURE dbo.sp_regionalsettings_get_statename_by_id
(@StateId int)
AS
	Select StateName  from tbl_state where StateId= @StateId
	RETURN