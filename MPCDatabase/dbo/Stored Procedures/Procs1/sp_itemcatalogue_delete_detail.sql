CREATE PROCEDURE dbo.sp_itemcatalogue_delete_detail
(@ID int)
                  AS
Delete from tbl_itemlibrary_catalogue_detail where tbl_itemlibrary_catalogue_detail.ItemCatalogueID=@ID
                     RETURN