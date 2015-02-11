
CREATE PROCEDURE [dbo].[usp_DeleteContactByID]
	@ContactID int
AS
	BEGIN
		

				delete  scd
				from dbo.tbl_section_costcentre_detail scd
				inner join dbo.tbl_section_costcentres sc on sc.SectionCostcentreID = scd.SectionCostCentreID
				inner join tbl_item_sections iss on iss.ItemSectionID = sc.ItemSectionID
				INNER JOIN tbl_items i ON i.ItemID = iss.ItemID
				inner join tbl_estimates E on E.EstimateID = i.EstimateID
				inner join tbl_contacts CC on CC.ContactID = E.ContactID
				where CC.ContactID = @ContactID



				--deletinng the item section cost center resources
				delete  scr
				from dbo.tbl_section_costcentre_resources scr
				inner join dbo.tbl_section_costcentres sc on sc.SectionCostcentreID = scr.SectionCostCentreID
				inner join tbl_item_sections iss on iss.ItemSectionID = sc.ItemSectionID
				INNER JOIN tbl_items i ON i.ItemID = iss.ItemID
				inner join tbl_estimates E on E.EstimateID = i.EstimateID
				inner join tbl_contacts CC on CC.ContactID = E.ContactID
				where CC.ContactID = @ContactID


				--deletinng the item section cost center
				delete  sc
				from dbo.tbl_section_costcentres sc
				inner join tbl_item_sections iss on iss.ItemSectionID = sc.ItemSectionID
				INNER JOIN tbl_items i ON i.ItemID = iss.ItemID
				inner join tbl_estimates E on E.EstimateID = i.EstimateID
				inner join tbl_contacts CC on CC.ContactID = E.ContactID
				where CC.ContactID = @ContactID




				--deletinng the item section ink coverage
				delete  sink
				from dbo.tbl_section_inkcoverage sink
				inner join tbl_item_sections iss on iss.ItemSectionID = sink.SectionID
				INNER JOIN tbl_items i ON i.ItemID = iss.ItemID
				inner join tbl_estimates E on E.EstimateID = i.EstimateID
				inner join tbl_contacts CC on CC.ContactID = E.ContactID
				where CC.ContactID = @ContactID

				--deletinng the item section
				delete  isec
				from dbo.tbl_item_sections isec
				INNER JOIN tbl_items i ON i.ItemID = isec.ItemID
				inner join tbl_estimates E on E.EstimateID = i.EstimateID
			    inner join tbl_contacts CC on CC.ContactID = E.ContactID
				where CC.ContactID = @ContactID
				
				--deletinng the item attachments
				delete  isa
				from dbo.tbl_item_attachments isa
				INNER JOIN tbl_items i ON i.ItemID = isa.ItemID
				inner join tbl_estimates E on E.EstimateID = i.EstimateID
				inner join tbl_contacts CC on CC.ContactID = E.ContactID
				where CC.ContactID = @ContactID




				--deletinng the item addon Cost Centres
				delete  iacc
				from dbo.tbl_Items_AddonCostCentres iacc
				INNER JOIN tbl_items i ON i.ItemID = iacc.ItemID
				inner join tbl_estimates E on E.EstimateID = i.EstimateID
				inner join tbl_contacts CC on CC.ContactID = E.ContactID
				where CC.ContactID = @ContactID


				--deletinng the item Stock Options
				delete  ipm
				from dbo.tbl_itemStockOptions ipm
				INNER JOIN tbl_items i ON i.ItemID = ipm.ItemID
				inner join tbl_estimates E on E.EstimateID = i.EstimateID
				inner join tbl_contacts CC on CC.ContactID = E.ContactID
				where CC.ContactID = @ContactID


				--deletinng the item Price Matrix
				delete  ipm
				from dbo.tbl_items_PriceMatrix ipm
				INNER JOIN tbl_items i ON i.ItemID = ipm.ItemID
				inner join tbl_estimates E on E.EstimateID = i.EstimateID
				inner join tbl_contacts CC on CC.ContactID = E.ContactID
				where CC.ContactID = @ContactID


				--deletinng the item related items
				delete  iri
				from dbo.tbl_items_RelatedItems iri
				INNER JOIN tbl_items i ON i.ItemID = iri.ItemID
				inner join tbl_estimates E on E.EstimateID = i.EstimateID
				inner join tbl_contacts CC on CC.contactid = E.contactid
				where CC.contactid = @ContactID
				
				
				
				--deletinng the items
				delete  i
				from dbo.tbl_items i
				inner join tbl_estimates E on E.EstimateID = i.EstimateID
				inner join tbl_contacts CC on CC.contactid = E.contactid
				where CC.contactid = @ContactID


				--deletinng the prepayments against estimates/orders
				delete  pp
				from dbo.tbl_PrePayments pp
				inner join tbl_estimates E on E.EstimateID = pp.OrderID
				inner join tbl_contacts CC on CC.ContactID = E.ContactID
				where CC.ContactID = @ContactID


				--deletinng the estimates/orders
				delete  E
				from dbo.tbl_estimates E
				inner join tbl_contacts CC on CC.ContactID = E.ContactID
				where CC.ContactID = @ContactID

--- here final.....................
		
		
		-- deleting the Purchase Detail
		       delete pd
		       from tbl_purchasedetail pd
		       inner join tbl_purchase p on pd.PurchaseID = p.PurchaseID
		       where p.ContactID = @ContactID
		  
		-- deleting the purchase     
		       delete p 
		       from tbl_purchase p 
		       inner join tbl_contacts cc on cc.contactid = p.contactid
		       where p.ContactID = @ContactID
		
		--deleting the invoice detail
				delete id
				from tbl_invoicedetails id
				inner join tbl_invoices i on id.InvoiceID = i.InvoiceID
				where i.ContactID = @ContactID
				
				
		--deletinng the invoices
				delete  E
				from dbo.tbl_invoices E
				inner join tbl_contacts CC on CC.ContactID = E.ContactID
				where CC.ContactID = @ContactID

	
	   	--deletinng the tbl_Inquiry Attachments
				delete  IA
			    from tbl_Inquiry_Attachments IA
				inner join dbo.tbl_Inquiry I on IA.InquiryID = I.InquiryID
				inner join tbl_contacts CC on CC.ContactID = I.ContactID
				where CC.ContactID = @ContactID


				--deletinng the tbl_Inquiry_Items
				delete  II
				from tbl_Inquiry_Items II
				inner join dbo.tbl_Inquiry I on II.InquiryID = I.InquiryID
				inner join tbl_contacts CC on CC.ContactID = I.ContactID
				where CC.ContactID = @ContactID

				--deletinng the Inquiries
				delete  I
				from dbo.tbl_Inquiry I
				inner join tbl_contacts CC on CC.ContactID = I.ContactID
				where CC.ContactID = @ContactID
	
	           delete c
	           from tbl_contacts c
	           where c.contactid = @ContactID

	END