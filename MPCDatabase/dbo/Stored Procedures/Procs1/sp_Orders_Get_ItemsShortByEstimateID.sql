CREATE PROCEDURE [dbo].[sp_Orders_Get_ItemsShortByEstimateID]
(
	@EstimateID int
)
AS
	Select Isnull(tbl_items.IsGroupItem,0) as IsGroupItem ,Isnull(tbl_items.ItemType,0) as ItemType, tbl_items.ItemID,tbl_items.ItemCode,
	tbl_items.ProductName as Title,
	 (case tbl_items.jobselectedQty
		when 1 then tbl_items.Qty1Tax1Value
		when 2 then tbl_items.Qty2Tax1Value
		when 3 then tbl_items.Qty3Tax1Value
		End ) as VAT
	,
	(CASe tbl_items.jobselectedQty 
		when 1 then tbl_items.Qty1NetTotal
		when 2 then tbl_items.Qty2NetTotal
		when 3 then tbl_items.Qty3NetTotal
		end ) as Total 
	,tbl_items.status,
	tbl_statuses.statusname AS JobStatus
		,tbl_items.JobStatusID,tbl_items.FlagID,
	( case tbl_items.jobselectedQty 
		when 1 then tbl_items.Qty1MarkUp1Value
		when 2 then tbl_items.Qty2MarkUp2Value
		when 3 then tbl_items.Qty3MarkUp3Value
		end ) as ProfitMargin,
	--tbl_invoicedetails.InvoiceID,
	tbl_items.IsScheduled,tbl_items.IsJobCardPrinted,
	(Select Count(ID) from tbl_item_attachments where tbl_item_attachments.ItemID=tbl_items.ItemID) as AttachmentCount,(select count(*) from tbl_item_sections where ItemID = tbl_items.ItemID) as TotalSections
    FROM tbl_items 
    Left outer JOIN tbl_statuses  ON (tbl_statuses.StatusID = tbl_items.Status)
  --  LEFT OUTER JOIN tbl_invoicedetails ON tbl_items.ItemID = tbl_invoicedetails.ItemID
	where tbl_items.EstimateID = @EstimateID 
	--and (tbl_items.status=2 or tbl_items.status=3 or tbl_items.status=4)
	 order by tbl_items.ItemID
	RETURN