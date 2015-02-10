-- =============================================
-- Author:		Muzzammil
-- Create date: 19-nov-2012
-- Description:	
-- =============================================
----select * from tbl_contactcompanies
----where name like '%usa%'

----DECLARE @TVP AS ContactCompaniesToBeDeleted;

----/* Add data to the table variable. */
----INSERT INTO @TVP (ContactCompanyID)
----select    ContactCompanyID from tbl_contactcompanies
----where IsAllowWebAccess = 0 and ContactcompanyID not in  (940, 996, 1115,903)


----/* Pass the table variable data to a stored procedure. */
----EXEC [usp_Delete_ContactCompany] @TVP;


--select * from @tvp





CREATE PROCEDURE [dbo].[usp_Delete_ContactCompany] 
	-- Add the parameters for the stored procedure here
	
	@DeleteList ContactCompaniesToBeDeleted READONLY

AS
BEGIN
	
	
	 declare @ContactCompanyID int = 0
	 

 declare @Totalrec int
 select @Totalrec = COUNT(*) from @DeleteList
 
 declare @currentrec int
 
 set @currentrec = 1
 
 WHILE (@currentrec <=@Totalrec)
 BEGIN

 select @ContactCompanyID = ContactCompanyID from @DeleteList
 where ID = @currentrec


				--deleting the template objects
				DELETE tob
				FROM TemplateObjects tob
				inner join TemplatePages topg on topg.ProductPageId = tob.ProductPageID
				inner join Templates t on t.ProductID = topg.productid
				INNER JOIN tbl_items i ON i.TemplateID = t.ProductID
				inner join tbl_estimates E on E.EstimateID = i.EstimateID
				inner join tbl_contactcompanies CC on CC.ContactCompanyID = E.ContactCompanyID
				where CC.ContactCompanyID = @ContactCompanyID

				--deleting the template pages
				DELETE topg
				FROM TemplatePages topg
				inner join Templates t on t.ProductID = topg.productid
				INNER JOIN tbl_items i ON i.TemplateID = t.ProductID
				inner join tbl_estimates E on E.EstimateID = i.EstimateID
				inner join tbl_contactcompanies CC on CC.ContactCompanyID = E.ContactCompanyID
				where CC.ContactCompanyID = @ContactCompanyID

				--deleting the template images
				DELETE topg
				FROM TemplateBackgroundImages topg
				inner join Templates t on t.ProductID = topg.productid
				INNER JOIN tbl_items i ON i.TemplateID = t.ProductID
				inner join tbl_estimates E on E.EstimateID = i.EstimateID
				inner join tbl_contactcompanies CC on CC.ContactCompanyID = E.ContactCompanyID
				where CC.ContactCompanyID = @ContactCompanyID

		        -- deleting the temp fonts
				DELETE tf
				FROM TemplateFonts tf
				where tf.CustomerID = @ContactCompanyID
              
				--deleting the templates
				DELETE t
				FROM templates t
				INNER JOIN tbl_items i ON i.TemplateID = t.ProductID
				inner join tbl_estimates E on E.EstimateID = i.EstimateID
				inner join tbl_contactcompanies CC on CC.ContactCompanyID = E.ContactCompanyID
				where CC.ContactCompanyID = @ContactCompanyID

				--deletinng the item section cost center detail
				delete  scd
				from dbo.tbl_section_costcentre_detail scd
				inner join dbo.tbl_section_costcentres sc on sc.SectionCostcentreID = scd.SectionCostCentreID
				inner join tbl_item_sections iss on iss.ItemSectionID = sc.ItemSectionID
				INNER JOIN tbl_items i ON i.ItemID = iss.ItemID
				inner join tbl_estimates E on E.EstimateID = i.EstimateID
				inner join tbl_contactcompanies CC on CC.ContactCompanyID = E.ContactCompanyID
				where CC.ContactCompanyID = @ContactCompanyID





				--deletinng the item section cost center resources
				delete  scr
				from dbo.tbl_section_costcentre_resources scr
				inner join dbo.tbl_section_costcentres sc on sc.SectionCostcentreID = scr.SectionCostCentreID
				inner join tbl_item_sections iss on iss.ItemSectionID = sc.ItemSectionID
				INNER JOIN tbl_items i ON i.ItemID = iss.ItemID
				inner join tbl_estimates E on E.EstimateID = i.EstimateID
				inner join tbl_contactcompanies CC on CC.ContactCompanyID = E.ContactCompanyID
				where CC.ContactCompanyID = @ContactCompanyID


				--deletinng the item section cost center
				delete  sc
				from dbo.tbl_section_costcentres sc
				inner join tbl_item_sections iss on iss.ItemSectionID = sc.ItemSectionID
				INNER JOIN tbl_items i ON i.ItemID = iss.ItemID
				inner join tbl_estimates E on E.EstimateID = i.EstimateID
				inner join tbl_contactcompanies CC on CC.ContactCompanyID = E.ContactCompanyID
				where CC.ContactCompanyID = @ContactCompanyID




				--deletinng the item section ink coverage
				delete  sink
				from dbo.tbl_section_inkcoverage sink
				inner join tbl_item_sections iss on iss.ItemSectionID = sink.SectionID
				INNER JOIN tbl_items i ON i.ItemID = iss.ItemID
				inner join tbl_estimates E on E.EstimateID = i.EstimateID
				inner join tbl_contactcompanies CC on CC.ContactCompanyID = E.ContactCompanyID
				where CC.ContactCompanyID = @ContactCompanyID

				--deletinng the item section
				delete  isec
				from dbo.tbl_item_sections isec
				INNER JOIN tbl_items i ON i.ItemID = isec.ItemID
				inner join tbl_estimates E on E.EstimateID = i.EstimateID
				inner join tbl_contactcompanies CC on CC.ContactCompanyID = E.ContactCompanyID
				where CC.ContactCompanyID = @ContactCompanyID

				--deletinng the item attachments
				delete  isa
				from dbo.tbl_item_attachments isa
				INNER JOIN tbl_items i ON i.ItemID = isa.ItemID
				inner join tbl_estimates E on E.EstimateID = i.EstimateID
				inner join tbl_contactcompanies CC on CC.ContactCompanyID = E.ContactCompanyID
				where CC.ContactCompanyID = @ContactCompanyID




				--deletinng the item addon Cost Centres
				delete  iacc
				from dbo.tbl_Items_AddonCostCentres iacc
				INNER JOIN tbl_items i ON i.ItemID = iacc.ItemID
				inner join tbl_estimates E on E.EstimateID = i.EstimateID
				inner join tbl_contactcompanies CC on CC.ContactCompanyID = E.ContactCompanyID
				where CC.ContactCompanyID = @ContactCompanyID


				--deletinng the item Stock Options
				delete  ipm
				from dbo.tbl_itemStockOptions ipm
				INNER JOIN tbl_items i ON i.ItemID = ipm.ItemID
				inner join tbl_estimates E on E.EstimateID = i.EstimateID
				inner join tbl_contactcompanies CC on CC.ContactCompanyID = E.ContactCompanyID
				where CC.ContactCompanyID = @ContactCompanyID


				--deletinng the item Price Matrix
				delete  ipm
				from dbo.tbl_items_PriceMatrix ipm
				INNER JOIN tbl_items i ON i.ItemID = ipm.ItemID
				inner join tbl_estimates E on E.EstimateID = i.EstimateID
				inner join tbl_contactcompanies CC on CC.ContactCompanyID = E.ContactCompanyID
				where CC.ContactCompanyID = @ContactCompanyID


				--deletinng the item related items
				delete  iri
				from dbo.tbl_items_RelatedItems iri
				INNER JOIN tbl_items i ON i.ItemID = iri.ItemID
				inner join tbl_estimates E on E.EstimateID = i.EstimateID
				inner join tbl_contactcompanies CC on CC.ContactCompanyID = E.ContactCompanyID
				where CC.ContactCompanyID = @ContactCompanyID

				--deleting the invoice detail
				delete id
				from tbl_invoicedetails id
				inner join tbl_invoices i on id.InvoiceID = i.InvoiceID
				where i.ContactCompanyID = @ContactCompanyID


				--deletinng the items
				delete  i
				from dbo.tbl_items i
				inner join tbl_estimates E on E.EstimateID = i.EstimateID
				inner join tbl_contactcompanies CC on CC.ContactCompanyID = E.ContactCompanyID
				where CC.ContactCompanyID = @ContactCompanyID


				--deletinng the prepayments against estimates/orders
				delete  pp
				from dbo.tbl_PrePayments pp
				inner join tbl_estimates E on E.EstimateID = pp.OrderID
				inner join tbl_contactcompanies CC on CC.ContactCompanyID = E.ContactCompanyID
				where CC.ContactCompanyID = @ContactCompanyID


				--deletinng the estimates/orders
				delete  E
				from dbo.tbl_estimates E
				inner join tbl_contactcompanies CC on CC.ContactCompanyID = E.ContactCompanyID
				where CC.ContactCompanyID = @ContactCompanyID


				--deletinng the invoices
				delete  E
				from dbo.tbl_invoices E
				inner join tbl_contactcompanies CC on CC.ContactCompanyID = E.ContactCompanyID
				where CC.ContactCompanyID = @ContactCompanyID


				--deletinng the tbl_Inquiry Attachments
				delete  IA
				from tbl_Inquiry_Attachments IA
				inner join dbo.tbl_Inquiry I on IA.InquiryID = I.InquiryID
				inner join tbl_contactcompanies CC on CC.ContactCompanyID = I.ContactCompanyID
				where CC.ContactCompanyID = @ContactCompanyID


				--deletinng the tbl_Inquiry_Items
				delete  II
				from tbl_Inquiry_Items II
				inner join dbo.tbl_Inquiry I on II.InquiryID = I.InquiryID
				inner join tbl_contactcompanies CC on CC.ContactCompanyID = I.ContactCompanyID
				where CC.ContactCompanyID = @ContactCompanyID

				--deletinng the Inquiries
				delete  I
				from dbo.tbl_Inquiry I
				inner join tbl_contactcompanies CC on CC.ContactCompanyID = I.ContactCompanyID
				where CC.ContactCompanyID = @ContactCompanyID


				--retreiving all corporate categories for the specifeid customer
				DECLARE @cats TABLE (
				ProductCategoryID int,
				ParentCategoryID int,
				CategoryName nvarchar(100)
				 );


				WITH Emp_CTE AS (
				 SELECT ProductCategoryID, ParentCategoryID, CategoryName
				 FROM dbo.tbl_ProductCategory
				 WHERE (ParentCategoryID IS NULL or ParentCategoryID = 0) and ContactCompanyID = @ContactCompanyID
				 UNION ALL
				 SELECT c.ProductCategoryID, c.ParentCategoryID, c.CategoryName
				 FROM dbo.tbl_ProductCategory c
				 INNER JOIN Emp_CTE ecte ON ecte.ProductCategoryID = c.ParentCategoryID
				 )
				 insert into @cats
				 SELECT *
				 FROM Emp_CTE
				 order by ProductCategoryID DESC




				--deleting the corporate template objects
				DELETE tob
				FROM TemplateObjects tob
				inner join TemplatePages topg on topg.ProductPageId = tob.ProductPageID
				inner join Templates t on t.ProductID = topg.productid
				INNER JOIN tbl_items i ON i.TemplateID = t.ProductID
				inner join @cats cat on cat.ProductCategoryID = i.ProductCategoryID

				--deleting the corproate template pages
				DELETE topg
				FROM TemplatePages topg
				inner join Templates t on t.ProductID = topg.productid
				INNER JOIN tbl_items i ON i.TemplateID = t.ProductID
				inner join @cats cat on cat.ProductCategoryID = i.ProductCategoryID

				--deleting the corproate template pages
				delete from ImagePermissions
				where ImageID in
				(select topg.ID FROM TemplateBackgroundImages topg
				inner join Templates t on t.ProductID = topg.productid
				INNER JOIN tbl_items i ON i.TemplateID = t.ProductID
				inner join @cats cat on cat.ProductCategoryID = i.ProductCategoryID)
				
				DELETE topg
				FROM TemplateBackgroundImages topg
				inner join Templates t on t.ProductID = topg.productid
				INNER JOIN tbl_items i ON i.TemplateID = t.ProductID
				inner join @cats cat on cat.ProductCategoryID = i.ProductCategoryID
				
		        -- deleting the temp fonts
				DELETE tf
				FROM TemplateFonts tf
				where tf.CustomerID = @ContactCompanyID

				--deleting the corproate templates
				DELETE t
				FROM templates t
				INNER JOIN tbl_items i ON i.TemplateID = t.ProductID
				inner join @cats cat on cat.ProductCategoryID = i.ProductCategoryID


				-----------------------------------------deleting the corproate products

				--deletinng the item section cost center detail
				delete  scd
				from dbo.tbl_section_costcentre_detail scd
				inner join dbo.tbl_section_costcentres sc on sc.SectionCostcentreID = scd.SectionCostCentreID
				inner join tbl_item_sections iss on iss.ItemSectionID = sc.ItemSectionID
				INNER JOIN tbl_items i ON i.ItemID = iss.ItemID
				inner join @cats cat on cat.ProductCategoryID = i.ProductCategoryID




				--deletinng the item section cost center resources
				delete  scr
				from dbo.tbl_section_costcentre_resources scr
				inner join dbo.tbl_section_costcentres sc on sc.SectionCostcentreID = scr.SectionCostCentreID
				inner join tbl_item_sections iss on iss.ItemSectionID = sc.ItemSectionID
				INNER JOIN tbl_items i ON i.ItemID = iss.ItemID
				inner join @cats cat on cat.ProductCategoryID = i.ProductCategoryID


				--deletinng the item section cost center
				delete  sc
				from dbo.tbl_section_costcentres sc
				inner join tbl_item_sections iss on iss.ItemSectionID = sc.ItemSectionID
				INNER JOIN tbl_items i ON i.ItemID = iss.ItemID
				inner join @cats cat on cat.ProductCategoryID = i.ProductCategoryID



				--deletinng the item section ink coverage
				delete  sink
				from dbo.tbl_section_inkcoverage sink
				inner join tbl_item_sections iss on iss.ItemSectionID = sink.SectionID
				INNER JOIN tbl_items i ON i.ItemID = iss.ItemID
				inner join @cats cat on cat.ProductCategoryID = i.ProductCategoryID

				--deletinng the item section
				delete  isec
				from dbo.tbl_item_sections isec
				INNER JOIN tbl_items i ON i.ItemID = isec.ItemID
				inner join @cats cat on cat.ProductCategoryID = i.ProductCategoryID

				--deletinng the item attachments
				delete  isa
				from dbo.tbl_item_attachments isa
				INNER JOIN tbl_items i ON i.ItemID = isa.ItemID
				inner join @cats cat on cat.ProductCategoryID = i.ProductCategoryID




				--deletinng the item addon Cost Centres
				delete  iacc
				from dbo.tbl_Items_AddonCostCentres iacc
				INNER JOIN tbl_items i ON i.ItemID = iacc.ItemID
				inner join @cats cat on cat.ProductCategoryID = i.ProductCategoryID


				--deletinng the item Price Matrix
				delete  ipm
				from dbo.tbl_items_PriceMatrix ipm
				INNER JOIN tbl_items i ON i.ItemID = ipm.ItemID
				inner join @cats cat on cat.ProductCategoryID = i.ProductCategoryID



				
				--deletinng the item Stock Options
				delete  ipm
				from dbo.tbl_itemStockOptions ipm
				INNER JOIN tbl_items i ON i.ItemID = ipm.ItemID
				inner join @cats cat on cat.ProductCategoryID = i.ProductCategoryID





				--deletinng the item related items
				delete  iri
				from dbo.tbl_items_RelatedItems iri
				INNER JOIN tbl_items i ON i.ItemID = iri.ItemID
				inner join @cats cat on cat.ProductCategoryID = i.ProductCategoryID


				--delete favorite designs
				delete fv 
				from tbl_FavoriteDesign fv
				inner join dbo.tbl_contacts  c on c.ContactID = fv.ContactUserID
				where ContactCompanyID = @ContactCompanyID

				--delete item images
				delete ii from tbl_itemImages ii
				inner join tbl_items i on i.ItemID = ii.ItemID
				inner join @cats cat on cat.ProductCategoryID = i.ProductCategoryID

				--deletinng the items
				delete  i
				from dbo.tbl_items i
				inner join @cats cat on cat.ProductCategoryID = i.ProductCategoryID


				-----------------------

				delete nls
				from tbl_NewsLetterSubscribers nls
				inner join dbo.tbl_contacts c on c.ContactID = nls.ContactID
				where c.ContactCompanyID = @ContactCompanyID
				
				
				--deleteing subscribers for contacts
				
				

				--delete dbo.tbl_contacts
				delete from dbo.tbl_contacts 
				where ContactCompanyID = @ContactCompanyID


				delete cat
				from tbl_ProductCategory cat 
				inner join @cats caty on cat.ProductCategoryID = caty.ProductCategoryID


				--delete dbo.tbl_ContactDepartments
				delete cd
				from tbl_ContactDepartments cd
				inner join dbo.tbl_ContactCompanyTerritories cct on cct.TerritoryID = cd.TerritoryID
				where ContactCompanyID = @ContactCompanyID

              
				--delete Territory Images
				delete from ImagePermissions
				where TerritoryID in(select TerritoryID from tbl_ContactCompanyTerritories where ContactCompanyID = @ContactCompanyID)


				--delete dbo.tbl_ContactCompanyTerritories
				--delete from dbo.tbl_ContactCompanyTerritories 
				--where ContactCompanyID = @ContactCompanyID

				


				-- delete customer page banners
				delete from tbl_cmsPageBanners
				where ContactCompanyID = @ContactCompanyID

				--deleting the broker customer items/products
				delete from tbl_ContactCompanyItems
				where ContactCompanyID = @ContactCompanyID
				
				
				--deleting the broker customer items/products
				delete from tbl_cmsColorPalletes
				where ContactCompanyID = @ContactCompanyID
				
				--deleting the customer vouchers
				delete from tbl_DiscountVouchers
				where ContactCompanyID = @ContactCompanyID


				--postcodes
				delete from tbl_PC_PostCodesBrokers
				where ContactCompanyID = @ContactCompanyID
				
				

				--delete dbo.tbl_contact addresses
				delete from dbo.tbl_addresses 
				where ContactCompanyID = @ContactCompanyID
				
				-- deleting company territories
				delete from tbl_ContactCompanyTerritories
				where ContactCompanyID = @ContactCompanyID
				
				--delete contact company
				delete from dbo.tbl_contactcompanies
				where ContactCompanyID = @ContactCompanyID
				 
				 
				 
				 ------------------------- deleting any orphan templates
				 

				DECLARE @cats2 TABLE (
				ProductCategoryID int,
				ParentCategoryID int,
				CategoryName nvarchar(100)
				 );



				WITH Emp_CTE AS (
				 SELECT ProductCategoryID, ParentCategoryID, CategoryName
				 FROM dbo.tbl_ProductCategory
				 WHERE (ParentCategoryID IS NULL or ParentCategoryID = 0) and ContactCompanyID = @ContactCompanyID
				 UNION ALL
				 SELECT c.ProductCategoryID, c.ParentCategoryID, c.CategoryName
				 FROM dbo.tbl_ProductCategory c
				 INNER JOIN Emp_CTE ecte ON ecte.ProductCategoryID = c.ParentCategoryID
				 )

				 insert into @cats2
				 SELECT *
				 FROM Emp_CTE
				 order by ProductCategoryID DESC


				DELETE tob
				FROM TemplateObjects tob
				inner join TemplatePages topg on topg.ProductPageId = tob.ProductPageID
				inner join Templates t on t.ProductID = topg.productid
				 inner join @cats2 cats on t.ProductCategoryID = cats.ProductCategoryID


				--deleting the template pages
				DELETE topg
				FROM TemplatePages topg
				inner join Templates t on t.ProductID = topg.productid
				 inner join @cats2 cats  on t.ProductCategoryID = cats.ProductCategoryID

				--deleting the template pages
				DELETE topg
				FROM TemplateBackgroundImages topg
				inner join Templates t on t.ProductID = topg.productid
				 inner join @cats2 cats  on t.ProductCategoryID = cats.ProductCategoryID
				
				-- deleting the temp fonts
				DELETE tf
				FROM TemplateFonts tf
				where tf.CustomerID = @ContactCompanyID
				
				
				--deleting the templates
				DELETE t
				FROM templates t
				inner join @cats2 cats  on t.ProductCategoryID = cats.ProductCategoryID


				 --SELECT distinct t.productid
				 --FROM @cats2 cats 
				 --inner join Templates t on t.ProductCategoryID = cats.ProductCategoryID

SET @currentrec = @currentrec + 1
 END
 
END