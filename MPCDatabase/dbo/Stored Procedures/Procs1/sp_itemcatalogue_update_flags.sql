CREATE PROCEDURE dbo.sp_itemcatalogue_update_flags
(@ID int,@FlagID int)
                  AS
update tbl_finishedgoods_catalogue set FlagID=@FlagID where ID=@ID                     
RETURN