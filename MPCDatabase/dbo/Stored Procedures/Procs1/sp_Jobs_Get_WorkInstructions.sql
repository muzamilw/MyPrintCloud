
CREATE PROCEDURE [dbo].[sp_Jobs_Get_WorkInstructions]
(
	@ItemID int
)
AS
	SELECT distinct tbl_section_costcentres.SectionCostcentreID,IsNull(tbl_items.jobSelectedQty,1)as jobSelectedQty,
	IsnUll(tbl_item_sections.SectionNo,1) as SectionNo,
        (case tbl_section_costcentres.SystemCostCentreType 
		when 10 then tbl_stockitems.ItemName
		when 11 then tbl_contactcompanies.Name
		else tbl_costcentres.Name end) as CostCentreName, 
        (
        case IsNull(tbl_items.jobSelectedQty,1)
        when 1 then IsNull(convert(nvarchar(4000),tbl_section_costcentres.Qty1WorkInstructions),'')
        when 2 then IsNull(convert(nvarchar(4000),tbl_section_costcentres.Qty2WorkInstructions),'')
        when 3 then IsNull(convert(nvarchar(4000),tbl_section_costcentres.Qty3WorkInstructions),'')
        end) as WorkInstruction, IsNull(tbl_section_costcentres.IsPrintable,1) as IsPrintable,tbl_section_costcentres.[Order]
        FROM tbl_items 
        INNER JOIN tbl_item_sections ON (tbl_items.ItemID = tbl_item_sections.ItemID) 
        INNER JOIN tbl_section_costcentres ON (tbl_item_sections.ItemSectionID = tbl_section_costcentres.ItemSectionID) 
        LEFT OUTER JOIN tbl_section_costcentre_detail ON (tbl_section_costcentres.SectionCostcentreID = tbl_section_costcentre_detail.SectionCostCentreID) 
        LEFT OUTER JOIN tbl_stockitems ON (tbl_section_costcentre_detail.StockId = tbl_stockitems.stockItemID) 
        LEFT OUTER JOIN tbl_contactcompanies ON (tbl_section_costcentre_detail.SupplierID = tbl_contactcompanies.ContactCompanyID) 
        LEFT OUTER JOIN tbl_costcentres ON (tbl_section_costcentres.CostCentreID = tbl_costcentres.costcentreID) 
        where tbl_items.ItemID=@ItemID 
        /*  group by tbl_section_costcentres.SectionCostCentreID */
	RETURN