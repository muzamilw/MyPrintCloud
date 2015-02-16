CREATE PROCEDURE [dbo].[sp_Estimation_Get_EstimateItemsFromDelivery]
(@EstimateID int)
AS
	SELECT tbl_items.ItemID,tbl_items.ItemCode,tbl_items.ProductName as Title,
     (CASE when tbl_items.JobSelectedQty =1 then tbl_items.Qty1 when tbl_items.JobSelectedQty=2 then tbl_items.Qty2	when tbl_items.JobSelectedQty=3 then tbl_items.Qty3	eLSE tbl_items.Qty1 end)  as Quantity,     
     (case when tbl_items.JobSelectedQty=1 then tbl_items.Qty1GrossTotal when tbl_items.JobSelectedQty=2 then tbl_items.Qty2grossTotal when tbl_items.JobSelectedQty=3 then tbl_items.Qty3GrossTotal else tbl_items.Qty1GrossTotal end ) as TotalPrice, 
     dbo.Fnc_Items_getEstimateDescription(tbl_items.ItemID) as  EstimateDescription 
     FROM tbl_items 
    WHERE  tbl_items.EstimateID = @EstimateID
	RETURN