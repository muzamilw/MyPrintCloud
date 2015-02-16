CREATE PROCEDURE dbo.sp_regionalsetttings_check_stateincompany
(@StateID int)
AS
	select State from tbl_company where State=@StateID
	RETURN