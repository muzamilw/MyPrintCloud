CREATE PROCEDURE dbo.sp_regionalsettings_check_stateinsite
(@StateID int)
AS
	select State from tbl_company_sites where State=@StateID
	RETURN