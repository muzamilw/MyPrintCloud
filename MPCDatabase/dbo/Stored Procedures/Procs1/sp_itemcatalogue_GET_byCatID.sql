﻿CREATE PROCEDURE dbo.sp_itemcatalogue_GET_byCatID

(@ID int)

                  AS
select * from tbl_itemlibrary_catalogue_detail where ItemCatalogueID=@ID 

                     RETURN