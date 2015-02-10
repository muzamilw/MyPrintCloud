CREATE PROCEDURE dbo.sp_products_changeflag
(@FlagID int,
@ID int)
AS
	update tbl_profile set FlagID=@FlagID where ID=@ID
	RETURN