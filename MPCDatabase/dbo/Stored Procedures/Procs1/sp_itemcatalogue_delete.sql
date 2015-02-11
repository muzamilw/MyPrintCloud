CREATE PROCEDURE dbo.sp_itemcatalogue_delete
(@ID int)
                  AS
Delete from tbl_finishedgoods_catalogue where tbl_finishedgoods_catalogue.ID=@ID
                     RETURN