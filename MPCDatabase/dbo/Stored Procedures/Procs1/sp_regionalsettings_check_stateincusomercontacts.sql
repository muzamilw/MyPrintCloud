CREATE PROCEDURE dbo.sp_regionalsettings_check_stateincusomercontacts
(@StateID int)
AS
	select HomeStateID from tbl_customercontacts where HomeStateID=@StateID
	RETURN