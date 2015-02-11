
create PROCEDURE [dbo].[sp_Enquiries_Insert_Enquiry]
		@EnquiryTitle varchar (200) , 
        @ReceivedDate datetime ,@CustomerID int,@ContactID int,@RequiredByDate datetime,@DeliveryByDate datetime ,@Status int ,@Origination int ,@PreviousQuoteNO varchar (50), 
        @PreviousOrderNO varchar (50) ,@QuoteDestinition int,@SendUsing int,@SalesPersonID int ,@ProcessID int ,@ArtworkOriginationTypeID int,
        @ProofRequiredID int,@ProductTypeID int ,@ProductCode int,@FrequencyID int,@DataFormat varchar (100),@DataAvailable varchar (100),@EnquiryNotes ntext,@ItemNotes ntext, 
        @CoverSide1Colors int,@CoverSide2Colors int,@CoverInkCoveragePercentage float,@CoverSpecialColorsID int,@TextSide1Colors int,@TextSide2Colors int,
        @TextInkCoveragePercentage float,@TextSpecialColorsID int,@OtherSide1Colors int,@OtherSide2Colors int,@OtherInkCoveragePercentage float, 
        @OtherSpecialColorsID int,@ISCoverPaperSupplied int,@ISTextPaperSupplied int,@ISOtherPaperSupplied int ,@CoverPaperTypeID int,@TextPaperTypeID int, 
        @OtherPaperTypeID int ,@PaperNotes ntext ,@Quantity1 int,@Quantity2 int,@Quantity3 int,@FinishingStyleID int ,@CoverStyleID int ,@CoverFinishingID int, 
        @PackingStyleID int ,@InsertStyleID int ,@NoOfInserts int ,@FinishingNotes ntext ,@DeliveryAddressID int ,@BillingAddressID int,@SystemSiteID int,@CompanyName varchar (255),@PrintingNotes ntext ,@EnquiryCode varchar (100),@SalesPersonRead int,@EstimatorRead int,@FlagID int,
        @CreatedBy int,@CreatedByCustomer bit,@CreationDateTime datetime ,
		@PaperSizeID int,@CustomeSize bit,@PaperWeight int,@CoverSpecialColorSide2ID int,@TextSpecialColorSide2ID int,@OtherSpecialColorSide2ID int,@NCRSet int
AS
insert into tbl_enquiries (EnquiryTitle, 
        ReceivedDate,ContactCompanyID,ContactID,RequiredByDate,DeliveryByDate,Status,Origination,PreviousQuoteNO, 
        PreviousOrderNO,QuoteDestinition,SendUsing,SalesPersonID,ProcessID,ArtworkOriginationTypeID,
        ProofRequiredID,ProductTypeID,ProductCode,FrequencyID,DataFormat,DataAvailable,EnquiryNotes,ItemNotes, 
        CoverSide1Colors,CoverSide2Colors,CoverInkCoveragePercentage,CoverSpecialColorsID,TextSide1Colors,TextSide2Colors,
        TextInkCoveragePercentage,TextSpecialColorsID,OtherSide1Colors,OtherSide2Colors,OtherInkCoveragePercentage, 
        OtherSpecialColorsID,ISCoverPaperSupplied,ISTextPaperSupplied,ISOtherPaperSupplied,CoverPaperTypeID,TextPaperTypeID, 
        OtherPaperTypeID,PaperNotes,Quantity1,Quantity2,Quantity3,FinishingStyleID,CoverStyleID,CoverFinishingID, 
        PackingStyleID,InsertStyleID,NoOfInserts,FinishingNotes,DeliveryAddressID,BillingAddressID,SystemSiteID,CompanyName,PrintingNotes,EnquiryCode,SalesPersonRead,EstimatorRead,FlagID,CreatedBy ,CreatedByCustomer ,CreationDateTime,
		PaperSizeID,CustomeSize,PaperWeight,CoverSpecialColorSide2ID,TextSpecialColorSide2ID,OtherSpecialColorSide2ID,NCRSet  ) VALUES ( 
        @EnquiryTitle, 
        @ReceivedDate,@CustomerID,@ContactID,@RequiredByDate,@DeliveryByDate,@Status,@Origination,@PreviousQuoteNO, 
        @PreviousOrderNO,@QuoteDestinition,@SendUsing,@SalesPersonID,@ProcessID,@ArtworkOriginationTypeID,
        @ProofRequiredID,@ProductTypeID,@ProductCode,@FrequencyID,@DataFormat,@DataAvailable,@EnquiryNotes,@ItemNotes, 
        @CoverSide1Colors,@CoverSide2Colors,@CoverInkCoveragePercentage,@CoverSpecialColorsID,@TextSide1Colors,@TextSide2Colors,
        @TextInkCoveragePercentage,@TextSpecialColorsID,@OtherSide1Colors,@OtherSide2Colors,@OtherInkCoveragePercentage, 
        @OtherSpecialColorsID,@ISCoverPaperSupplied,@ISTextPaperSupplied,@ISOtherPaperSupplied,@CoverPaperTypeID,@TextPaperTypeID, 
        @OtherPaperTypeID,@PaperNotes,@Quantity1,@Quantity2,@Quantity3,@FinishingStyleID,@CoverStyleID,@CoverFinishingID, 
        @PackingStyleID,@InsertStyleID,@NoOfInserts,@FinishingNotes,@DeliveryAddressID,@BillingAddressID,@SystemSiteID,@CompanyName,@PrintingNotes,@EnquiryCode,@SalesPersonRead,@EstimatorRead,@FlagID,@CreatedBy ,@CreatedByCustomer ,@CreationDateTime ,
		@PaperSizeID,@CustomeSize,@PaperWeight,@CoverSpecialColorSide2ID,@TextSpecialColorSide2ID,@OtherSpecialColorSide2ID,@NCRSet);select @@Identity as EnquiryID
	RETURN