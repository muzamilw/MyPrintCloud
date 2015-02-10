
CREATE PROCEDURE [dbo].[sp_StockInventory_GetInvoiceStock]
(@InvoiceID int)
	
AS
	SELECT   tbl_section_costcentres.SectionCostcentreID,tbl_ContactDepartments.DepartmentName As Department,tbl_contacts.ContactID,tbl_addresses.AddressID,tbl_items.ItemID as EstimateItem,
	tbl_section_costcentre_detail.SectionCostCentreDetailId,tbl_stockitems.StockItemID,    tbl_stockitems.ItemCode,tbl_contactcompanies.Name, tbl_stockitems.ItemName, 
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
                      tbl_stockitems.inStock, tbl_stockitems.Allocated, tbl_stockitems.ReorderQty, tbl_stockitems.LastOrderQty, tbl_contactcompanies.contactcompanyid, tbl_stockitems.PackageQty,tbl_stockitems.TaxID
				FROM         tbl_items 
				INNER JOIN tbl_item_sections ON tbl_item_sections.ItemID = tbl_items.ItemID 
				INNER JOIN tbl_section_costcentres ON tbl_item_sections.ItemSectionID = tbl_section_costcentres.ItemSectionID AND tbl_section_costcentres.IsPurchaseOrderRaised <> 0 
				INNER JOIN tbl_section_costcentre_detail ON tbl_section_costcentre_detail.SectionCostCentreID = tbl_section_costcentres.SectionCostcentreID and tbl_section_costcentre_detail.StockId <> 0 
				INNER JOIN tbl_stockitems ON tbl_section_costcentre_detail.StockId = tbl_stockitems.StockItemID 
				INNER JOIN tbl_contactcompanies ON tbl_section_costcentre_detail.SupplierID = tbl_contactcompanies.contactcompanyid 
				INNER JOIN tbl_invoicedetails ON tbl_items.ItemID = tbl_invoicedetails.ItemID
				INNER JOIN   tbl_ContactDepartments ON tbl_ContactDepartments.DepartmentID = tbl_contactcompanies.departmentid
				INNER JOIN   tbl_addresses ON tbl_addresses.contactcompanyid = tbl_contactcompanies.contactcompanyid  and tbl_addresses.IsDefaultAddress<>0
				INNER JOIN   tbl_contacts ON tbl_contacts.contactcompanyid = tbl_contactcompanies.contactcompanyid and tbl_contacts.IsDefaultContact<>0
				WHERE   tbl_contactcompanies.iscustomer = 2 and  (tbl_invoicedetails.InvoiceID = @InvoiceID)
	RETURN