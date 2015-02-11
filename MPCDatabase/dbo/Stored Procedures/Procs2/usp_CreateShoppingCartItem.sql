CREATE PROCEDURE [dbo].[usp_CreateShoppingCartItem]
		
		@ItemID		numeric,
		@CustomerID	numeric,
		@ContactID	numeric,
		@Quantity  numeric,
		@QtyCost	float,
		@QtyCostCentreProfit float,
		@TaxID	int = 2,
		@MarkupID	int = 30,
		@MarkupValue	float,
		@TaxValue		float
				
AS
BEGIN

	declare @EstimateID numeric, 
			@ProductName	varchar(255),
			@NewItemID numeric, 
			@SectionID numeric,
			@NetTotal	float,
			@GrossTotal	float
	Set @NetTotal = @QtyCost + @QtyCostCentreProfit + @MarkupValue
	set @GrossTotal = @NetTotal + @TaxValue
	select @ProductName = ProductName from tbl_items where ItemID = @ItemID
	
			insert into tbl_estimates	
					(Estimate_Name, ContactCompanyID, ContactID, CompanyName, StatusID)
			values( cast(@Quantity as varchar)+ ' ' + @ProductName , @CustomerID, @ContactID, 'Web User', 3)
				select @EstimateID = SCOPE_IDENTITY()

			insert into tbl_items
					(ItemCode, EstimateID, InvoiceID, Title, Tax1, Tax2, Tax3, 
				CreatedBy, Status, ItemCreationDateTime, ItemLastUpdateDateTime, 
				IsMultipleQty, RunOnQty, RunonCostCentreProfit, RunonBaseCharge, 
				RunOnMarkUpID, RunonPercentageValue, RunOnMarkUpValue, RunOnNetTotal, 
				Qty1, Qty2, Qty3, Qty1CostCentreProfit, Qty2CostCentreProfit, 
				Qty3CostCentreProfit, Qty1BaseCharge1, Qty2BaseCharge2, Qty3BaseCharge3, 
				Qty1MarkUpID1, Qty2MarkUpID2, Qty3MarkUpID3, Qty1MarkUpPercentageValue, 
				Qty2MarkUpPercentageValue, Qty3MarkUpPercentageValue, Qty1MarkUp1Value, 
				Qty2MarkUp2Value, Qty3MarkUp3Value, Qty1NetTotal, Qty2NetTotal, Qty3NetTotal, 
				Qty1Tax1Value, Qty1Tax2Value, Qty1Tax3Value, Qty1GrossTotal, Qty2Tax1Value, 
				Qty2Tax2Value, Qty2Tax3Value, Qty2grossTotal, Qty3Tax1Value, Qty3Tax2Value, 
				Qty3Tax3Value, Qty3GrossTotal, IsDescriptionLocked, qty1title, qty2title, qty3Title, 
				RunonTitle, AdditionalInformation, qty2Description, qty3Description, RunonDescription, 
				EstimateDescriptionTitle1, EstimateDescriptionTitle2, EstimateDescriptionTitle3, 
				EstimateDescriptionTitle4, EstimateDescriptionTitle5, EstimateDescriptionTitle6, 
				EstimateDescriptionTitle7, EstimateDescriptionTitle8, EstimateDescriptionTitle9, 
				EstimateDescriptionTitle10, EstimateDescription1, EstimateDescription2, EstimateDescription3,
				EstimateDescription4, EstimateDescription5, EstimateDescription6, EstimateDescription7, 
				EstimateDescription8, EstimateDescription9, EstimateDescription10, JobDescriptionTitle1, 
				JobDescriptionTitle2, JobDescriptionTitle3, JobDescriptionTitle4, JobDescriptionTitle5, 
				JobDescriptionTitle6, JobDescriptionTitle7, JobDescriptionTitle8, JobDescriptionTitle9, 
				JobDescriptionTitle10, JobDescription1, JobDescription2, JobDescription3, JobDescription4,
				JobDescription5, JobDescription6, JobDescription7, JobDescription8, JobDescription9, 
				JobDescription10, IsParagraphDescription, EstimateDescription, JobDescription, InvoiceDescription, 
				JobCode, JobManagerID, JobEstimatedStartDateTime, JobEstimatedCompletionDateTime, 
				JobCreationDateTime, JobProgressedBy, jobSelectedQty, JobStatusID, IsJobCardPrinted, 
				IsItemLibraray, ItemLibrarayGroupID,  	    
				 PayInFullInvoiceID, IsGroupItem, ItemType, 
				IsIncludedInPipeLine, IsRunOnQty, CanCopyToEstimate, FlagID, CostCenterDescriptions, 
				IsRead, IsScheduled, IsPaperStatusChanged, IsJobCardCreated, IsAttachmentAdded, 
				IsItemValueChanged, DepartmentID, ItemNotes, UpdatedBy, LastUpdate, JobActualStartDateTime, 
				JobActualCompletionDateTime, IsJobCostingDone, ProductName, ProductCategoryID, ImagePath, 
				ThumbnailPath, ProductSpecification, CompleteSpecification, DesignGuideLines, ProductCode, IsPublished)

			select ItemCode, @EstimateID, InvoiceID, Title, @TaxID, Tax2, Tax3, 
				CreatedBy, Status, GETDATE(), ItemLastUpdateDateTime, 
				IsMultipleQty, RunOnQty, RunonCostCentreProfit, RunonBaseCharge, 
				RunOnMarkUpID, RunonPercentageValue, RunOnMarkUpValue, RunOnNetTotal, 
				@Quantity, Qty2, Qty3, Qty1CostCentreProfit, Qty2CostCentreProfit, 
				Qty3CostCentreProfit, @QtyCost, Qty2BaseCharge2, Qty3BaseCharge3, 
				@MarkupID, Qty2MarkUpID2, Qty3MarkUpID3, 0, 
				Qty2MarkUpPercentageValue, Qty3MarkUpPercentageValue,
				@MarkupValue, 
				Qty2MarkUp2Value, Qty3MarkUp3Value, @NetTotal, Qty2NetTotal, Qty3NetTotal, 
				@TaxValue, Qty1Tax2Value, Qty1Tax3Value, @GrossTotal, Qty2Tax1Value, 
				Qty2Tax2Value, Qty2Tax3Value, Qty2grossTotal, Qty3Tax1Value, Qty3Tax2Value, 
				Qty3Tax3Value, Qty3GrossTotal, IsDescriptionLocked, qty1title, qty2title, qty3Title, 
				RunonTitle, AdditionalInformation, qty2Description, qty3Description, RunonDescription, 
				EstimateDescriptionTitle1, EstimateDescriptionTitle2, EstimateDescriptionTitle3, 
				EstimateDescriptionTitle4, EstimateDescriptionTitle5, EstimateDescriptionTitle6, 
				EstimateDescriptionTitle7, EstimateDescriptionTitle8, EstimateDescriptionTitle9, 
				EstimateDescriptionTitle10, EstimateDescription1, EstimateDescription2, EstimateDescription3,
				EstimateDescription4, EstimateDescription5, EstimateDescription6, EstimateDescription7, 
				EstimateDescription8, EstimateDescription9, EstimateDescription10, JobDescriptionTitle1, 
				JobDescriptionTitle2, JobDescriptionTitle3, JobDescriptionTitle4, JobDescriptionTitle5, 
				JobDescriptionTitle6, JobDescriptionTitle7, JobDescriptionTitle8, JobDescriptionTitle9, 
				JobDescriptionTitle10, JobDescription1, JobDescription2, JobDescription3, JobDescription4,
				JobDescription5, JobDescription6, JobDescription7, JobDescription8, JobDescription9, 
				JobDescription10, IsParagraphDescription, EstimateDescription, JobDescription, InvoiceDescription, 
				JobCode, JobManagerID, JobEstimatedStartDateTime, JobEstimatedCompletionDateTime, 
				JobCreationDateTime, JobProgressedBy, jobSelectedQty, JobStatusID, IsJobCardPrinted, 
				IsItemLibraray, ItemLibrarayGroupID, PayInFullInvoiceID, IsGroupItem, ItemType, 
				IsIncludedInPipeLine, IsRunOnQty, CanCopyToEstimate, FlagID, CostCenterDescriptions, 
				IsRead, IsScheduled, IsPaperStatusChanged, IsJobCardCreated, IsAttachmentAdded, 
				IsItemValueChanged, DepartmentID, ItemNotes, UpdatedBy, LastUpdate, JobActualStartDateTime, 
				JobActualCompletionDateTime, IsJobCostingDone, ProductName, ProductCategoryID, ImagePath, 
				ThumbnailPath, ProductSpecification, CompleteSpecification, DesignGuideLines, ProductCode, 0
			from tbl_items
			where ItemID = @ItemID

			select @NewItemID = SCOPE_IDENTITY()

			insert into tbl_item_sections
			(ItemID, Qty1, Qty1Profit, BaseCharge1)
			values (@NewItemID, @Quantity, 0, @QtyCost)
			select @SectionID = SCOPE_IDENTITY()
			
			insert into tbl_section_costcentres
			(CostCentreID, Qty1Charge, Qty1MarkUpID, Qty1MarkUpValue, 
			Qty1NetTotal, Locked, ItemSectionID)
			values (202, 1, @MarkupID, @MarkupValue, 1 + @MarkupValue, 0, @SectionID)
			
			select @SectionID

END