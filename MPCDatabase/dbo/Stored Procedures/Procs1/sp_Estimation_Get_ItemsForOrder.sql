CREATE PROCEDURE [dbo].[sp_Estimation_Get_ItemsForOrder]
(@EstimateID int)
AS
	SELECT tbl_items.ItemID,tbl_items.Title,tbl_items.EstimateDescription,
     tbl_items.Qty1,tbl_items.Qty2,tbl_items.Qty3,tbl_items.IsMultipleQty,tbl_items.Qty1GrossTotal,tbl_items.Qty2grossTotal,
     tbl_items.Qty3GrossTotal, tbl_items.jobSelectedQty,tbl_items.Status FROM tbl_items WHERE ((tbl_items.EstimateID = @EstimateID))-- and (tbl_items.status = 1))
	RETURN