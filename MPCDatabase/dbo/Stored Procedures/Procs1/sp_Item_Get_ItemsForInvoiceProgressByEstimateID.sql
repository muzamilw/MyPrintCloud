
CREATE PROCEDURE [dbo].[sp_Item_Get_ItemsForInvoiceProgressByEstimateID]
(
	@EstimateID int
)
AS
	SELECT tbl_items.ItemID,tbl_items.ItemCode,tbl_items.NominalCodeID as NominalCode,
        tbl_items.Qty1,tbl_items.Qty2,tbl_items.Qty3,
        (tbl_items.Qty1Tax1Value + COALESCE(tbl_items.Qty1Tax2Value ,0) +  COALESCE(tbl_items.Qty1Tax3Value,0)) as Qty1TaxValue,
        (tbl_items.Qty2Tax1Value + COALESCE(tbl_items.Qty2Tax2Value ,0) +  COALESCE(tbl_items.Qty2Tax3Value,0)) as Qty2TaxValue,
        (tbl_items.Qty3Tax1Value + COALESCE(tbl_items.Qty3Tax2Value,0) +  COALESCE(tbl_items.Qty3Tax3Value,0)) as Qty3TaxValue,
        tbl_items.Qty1NetTotal,tbl_items.Qty2NetTotal,tbl_items.Qty3NetTotal,
        tbl_items.EstimateDescription,tbl_items.JobDescription,
        EstimateDescriptionTitle1,EstimateDescriptionTitle2,EstimateDescriptionTitle3,EstimateDescriptionTitle4,EstimateDescriptionTitle5,EstimateDescriptionTitle6,EstimateDescriptionTitle7,EstimateDescriptionTitle8,EstimateDescriptionTitle9,EstimateDescriptionTitle10,
        EstimateDescription1,EstimateDescription2,EstimateDescription3,EstimateDescription4,EstimateDescription5,EstimateDescription6,EstimateDescription7,EstimateDescription8,EstimateDescription9,EstimateDescription10,
        JobDescriptionTitle1,JobDescriptionTitle2,JobDescriptionTitle3,JobDescriptionTitle4,JobDescriptionTitle5,JobDescriptionTitle6,JobDescriptionTitle7,JobDescriptionTitle8,JobDescriptionTitle9,JobDescriptionTitle10,
        JobDescription1,JobDescription2,JobDescription3,JobDescription4,JobDescription5,JobDescription6,JobDescription7,JobDescription8,JobDescription9,JobDescription10,
        tbl_items.Status,tbl_items.InvoiceDescription,tbl_items.jobSelectedQty,tbl_items.ProductName as Title,tbl_items.IsMultipleQty,tbl_items.IsGroupItem,isnull(tbl_items.invoiceid,0) as InvoiceID
        
        FROM tbl_items WHERE (tbl_items.EstimateID = @EstimateID)
	RETURN