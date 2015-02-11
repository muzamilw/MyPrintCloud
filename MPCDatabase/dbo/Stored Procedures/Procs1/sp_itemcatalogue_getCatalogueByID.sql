CREATE PROCEDURE dbo.sp_itemcatalogue_getCatalogueByID
(
@ID int)
                  AS
select * from tbl_finishedgoods_catalogue where ID=@ID
                     RETURN