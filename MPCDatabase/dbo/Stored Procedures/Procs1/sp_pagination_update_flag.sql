CREATE PROCEDURE dbo.sp_pagination_update_flag
(
@ID int,@FlagID int)
AS
	update tbl_pagination_profile set FlagID=@FlagID where ID=@ID
                RETURN