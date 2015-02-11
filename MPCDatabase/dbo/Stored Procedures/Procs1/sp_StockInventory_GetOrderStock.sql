
CREATE PROCEDURE [dbo].[sp_StockInventory_GetOrderStock]
(@OrderID int)
	
AS
	SELECT   tbl_section_costcentres.SectionCostcentreID,tbl_Contacts.ContactID,tbl_Addresses.AddressID,tbl_items.ItemID as EstimateItem,
	tbl_section_costcentre_detail.SectionCostCentreDetailId,tbl_stockitems.StockItemID,    tbl_stockitems.ItemCode, tbl_ContactCompanies.Name, tbl_stockitems.ItemName, 
		(case  
		when tbl_items.jobSelectedQty =1 
		   then	 CEILING(tbl_section_costcentre_detail.Qty1 / tbl_stockitems.PackageQty)
		when tbl_items.jobSelectedQty = 2 
		   then	 CEILING(tbl_section_costcentre_detail.Qty2 / tbl_stockitems.PackageQty)
 		when tbl_items.jobSelectedQty = 3
		   then	CEILING(tbl_section_costcentre_detail.Qty3 / tbl_stockitems.PackageQty)
		else
 			 CEILING(tbl_section_costcentre_detail.Qty1 / tbl_stockitems.PackageQty)
		end ) as RequireQty, 
                      tbl_stockitems.inStock, tbl_stockitems.Allocated, tbl_stockitems.ReorderQty, tbl_stockitems.LastOrderQty, tbl_ContactCompanies.ContactCompanyID,tbl_stockitems.PackageQty,tbl_stockitems.TaxID
				FROM         tbl_items
				INNER JOIN   tbl_item_sections  ON tbl_item_sections.ItemID = tbl_items.ItemID 
				INNER JOIN   tbl_section_costcentres ON  tbl_item_sections.ItemSectionID = tbl_section_costcentres.ItemSectionID and tbl_section_costcentres.IsPurchaseOrderRaised <> 0 
				INNER JOIN   tbl_section_costcentre_detail ON  tbl_section_costcentre_detail.SectionCostCentreID = tbl_section_costcentres.SectionCostcentreID and tbl_section_costcentre_detail.StockId <> 0
				INNER JOIN   tbl_stockitems ON tbl_section_costcentre_detail.StockId = tbl_stockitems.StockItemID 
				INNER JOIN   tbl_ContactCompanies ON tbl_section_costcentre_detail.SupplierID = tbl_ContactCompanies.ContactCompanyID 
				INNER JOIN   tbl_Addresses ON tbl_Addresses.ContactCompanyID = tbl_ContactCompanies.ContactCompanyID  and tbl_Addresses.IsDefaultAddress<>0
				INNER JOIN   tbl_Contacts ON tbl_Contacts.ContactCompanyID = tbl_ContactCompanies.ContactCompanyID and tbl_Contacts.IsDefaultContact<>0

				WHERE     (tbl_items.EstimateID = @OrderID)
	RETURN