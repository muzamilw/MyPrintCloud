CREATE PROCEDURE dbo.sp_FInishedGoods_Get_GoodsByCustomerID_All

	(
		@CustomerID int,
		@GenCustomerID int		
	)

AS
	/* SET NOCOUNT ON */
	SELECT DISTINCT 
                      tbl_items.ItemID, tbl_finishedgoodpricematrix.CategoryID, tbl_items.Title, CONVERT(nvarchar, tbl_items.EstimateDescription) AS EstimateDescription, 
                      CONVERT(nvarchar, tbl_finishedgoods.Description1) AS Description1, CONVERT(nvarchar, tbl_finishedgoods.Description2) AS Description2, 
                      CONVERT(nvarchar, tbl_finishedgoods.Description3) AS Description3, tbl_items.Qty1, tbl_items.Qty1NetTotal, tbl_finishedgoods.ID, tbl_items.Tax1, 
                      tbl_items.Tax2, tbl_items.Tax3, tbl_finishedgoodpricematrix.CustomerID, tbl_items.ItemCode, tbl_items.NominalCode
						FROM tbl_finishedgoodpricematrix INNER JOIN
                      tbl_items ON tbl_finishedgoodpricematrix.ItemID = tbl_items.ItemID INNER JOIN
                      tbl_finishedgoods ON tbl_items.ItemID = tbl_finishedgoods.ItemID
						WHERE (tbl_items.IsItemLibraray <> 0) and (tbl_finishedgoodpricematrix.CustomerID=@CustomerID or tbl_finishedgoodpricematrix.CustomerID=@GenCustomerID)
	RETURN