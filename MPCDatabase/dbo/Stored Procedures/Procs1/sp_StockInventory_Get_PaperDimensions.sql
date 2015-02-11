
create PROCEDURE [dbo].[sp_StockInventory_Get_PaperDimensions]
(
	@ItemID int
)
AS
	SELECT  (case tbl_stockitems.ItemSizeCustom when 0 then tbl_papersize.Height else tbl_stockitems.ItemSizeHeight end )       as Height,
 (case tbl_stockitems.ItemSizeCustom when 0 then tbl_papersize.Width else tbl_stockitems.ItemSizeWidth end)       as Width       
FROM tbl_papersize 
RIGHT OUTER JOIN tbl_stockitems ON (tbl_papersize.PaperSizeID = tbl_stockitems.ItemSizeID) where tbl_stockitems.Stockitemid=1

	RETURN