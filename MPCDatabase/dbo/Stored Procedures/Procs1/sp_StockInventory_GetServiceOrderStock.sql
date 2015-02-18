
Create PROCEDURE [dbo].[sp_StockInventory_GetServiceOrderStock]
(@OrderID int)
	
AS
	SELECT   tbl_section_costcentres.SectionCostcentreID,tbl_Contacts.ContactID,tbl_Addresses.AddressID,tbl_items.ItemID as EstimateItem,
tbl_ContactCompanies.ContactCompanyID, tbl_ContactCompanies.Name, 

(case when tbl_items.jobSelectedQty=1 then
tbl_section_costcentres.Qty1EstimatedPlantCost + tbl_section_costcentres.Qty1EstimatedLabourCost + tbl_section_costcentres.Qty1EstimatedStockCost
when tbl_items.jobSelectedQty=2 then
tbl_section_costcentres.Qty2EstimatedLabourCost + tbl_section_costcentres.Qty2EstimatedStockCost + tbl_section_costcentres.Qty2EstimatedPlantCost 
when tbl_items.jobSelectedQty=3 then
tbl_section_costcentres.Qty3EstimatedLabourCost + tbl_section_costcentres.Qty3EstimatedStockCost  +tbl_section_costcentres.Qty3EstimatedPlantCost 
else
tbl_section_costcentres.Qty1EstimatedPlantCost + tbl_section_costcentres.Qty1EstimatedLabourCost + tbl_section_costcentres.Qty1EstimatedStockCost
end) as Cost,	 

(case when tbl_items.jobSelectedQty=1 then
tbl_costcentres.Description + ' ' + convert(varchar(4000),tbl_costcentres.ItemDescription)+ ' '   + convert(varchar(4000),tbl_section_costcentres.Qty1WorkInstructions)
when tbl_items.jobSelectedQty=2 then
tbl_costcentres.Description + ' ' + convert(varchar(4000),tbl_costcentres.ItemDescription) + ' '  + convert(varchar(4000),tbl_section_costcentres.Qty2WorkInstructions)
when tbl_items.jobSelectedQty=3 then
tbl_costcentres.Description + ' ' + convert(varchar(4000),tbl_costcentres.ItemDescription)+ ' '   + convert(varchar(4000), tbl_section_costcentres.Qty3WorkInstructions)
else
tbl_costcentres.Description + ' ' + convert(varchar(4000),tbl_costcentres.ItemDescription) + ' '  + convert(varchar(4000),tbl_section_costcentres.Qty1WorkInstructions)
end) as Description

FROM         tbl_section_costcentre_detail 
INNER JOIN tbl_section_costcentres ON tbl_section_costcentre_detail.SectionCostCentreID = tbl_section_costcentres.SectionCostcentreID and tbl_section_costcentres.IsPurchaseOrderRaised <> 0 
inner join tbl_costcentres on tbl_section_costcentres.CostCentreID=tbl_costcentres.CostCentreID
INNER JOIN tbl_ContactCompanies ON tbl_section_costcentre_detail.SupplierID = tbl_ContactCompanies.Contactcompanyid 
INNER JOIN   tbl_Addresses ON tbl_Addresses.Contactcompanyid = tbl_ContactCompanies.Contactcompanyid  and tbl_Addresses.IsDefaultAddress<>0
INNER JOIN   tbl_Contacts ON tbl_Contacts.Contactcompanyid = tbl_ContactCompanies.Contactcompanyid and tbl_Contacts.IsDefaultContact<>0
INNER JOIN tbl_item_sections ON tbl_section_costcentres.ItemSectionID = tbl_item_sections.ItemSectionID
INNER JOIN tbl_items ON tbl_items.ItemID = tbl_item_sections.ItemID 
where tbl_items.EstimateID=@OrderID and tbl_section_costcentre_detail.StockId = 0  
	RETURN