CREATE PROCEDURE [dbo].[sp_Estimation_Get_ItemsByEstimateID]
/*Stored procedure will not check the item status. what ever the item stage is. */
(@EstimateID int)
AS
	SELECT tbl_items.EstimateID,tbl_items.ItemID,tbl_items.ItemCode,tbl_items.ProductName as Title,tbl_items.Qty1Tax1Value,tbl_items.Qty1Tax2Value,
         tbl_items.Qty1Tax3Value,
         tbl_items.Qty3Tax1Value,
         tbl_items.Qty1NetTotal,tbl_items.Qty2NetTotal,tbl_items.Qty3NetTotal,tbl_items.FlagID FROM tbl_items WHERE tbl_items.EstimateID = @EstimateID order by tbl_items.ItemID
	RETURN