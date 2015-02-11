
create PROCEDURE [dbo].[sp_Enquiries_Update_Enquiry]
@EnquiryTitle varchar(200) ,@CustomerID int ,@ContactID int,@RequiredByDate datetime,@DeliveryByDate datetime,@PreviousQuoteNO varchar(50),@PreviousOrderNO varchar(50),
@QuoteDestinition varchar,@SendUsing int,@SalesPersonID int,@ProcessID int,@ArtworkOriginationTypeID int ,@ProofRequiredID int,
@ProductTypeID int ,@ProductCode int,@FrequencyID int,@DataFormat varchar(100),@DataAvailable varchar(100),@EnquiryNotes ntext,@ItemNotes ntext,
@CoverSide1Colors int,@CoverSide2Colors int,@CoverInkCoveragePercentage float,@CoverSpecialColorsID int,@TextSide1Colors int,
@TextSide2Colors int,@TextInkCoveragePercentage float,@TextSpecialColorsID int,@OtherSide1Colors int,@OtherSide2Colors int,
@OtherInkCoveragePercentage float,@OtherSpecialColorsID int,@ISCoverPaperSupplied int,@ISTextPaperSupplied int,
@ISOtherPaperSupplied int ,@CoverPaperTypeID int,@TextPaperTypeID int,@OtherPaperTypeID int ,@PaperNotes ntext,@Quantity1 int,
@Quantity2 int,@Quantity3 int,@FinishingStyleID int,@CoverStyleID int,@CoverFinishingID int,@PackingStyleID int ,@InsertStyleID int,
@NoOfInserts int ,@FinishingNotes ntext,@DeliveryAddressID int,@BillingAddressID int,@SystemSiteID int,@CompanyName varchar,
@PrintingNotes ntext,@FlagID int,@EnquiryID int,
@PaperSizeID int,@CustomeSize bit,@PaperWeight int,@CoverSpecialColorSide2ID int,@TextSpecialColorSide2ID int,@OtherSpecialColorSide2ID int,@NCRSet int
AS
update tbl_enquiries set EnquiryTitle=@EnquiryTitle,ContactCompanyID=@CustomerID,ContactID=@ContactID,
RequiredByDate=@RequiredByDate,DeliveryByDate=@DeliveryByDate,PreviousQuoteNO=@PreviousQuoteNO,
PreviousOrderNO=@PreviousOrderNO,QuoteDestinition=@QuoteDestinition,SendUsing=@SendUsing,
SalesPersonID=@SalesPersonID,ProcessID=@ProcessID,ArtworkOriginationTypeID=@ArtworkOriginationTypeID,
ProofRequiredID=@ProofRequiredID,ProductTypeID=@ProductTypeID,ProductCode=@ProductCode,FrequencyID=@FrequencyID,
DataFormat=@DataFormat,DataAvailable=@DataAvailable,EnquiryNotes=@EnquiryNotes,ItemNotes=@ItemNotes,
CoverSide1Colors=@CoverSide1Colors,CoverSide2Colors=@CoverSide2Colors,CoverInkCoveragePercentage=@CoverInkCoveragePercentage,
CoverSpecialColorsID=@CoverSpecialColorsID,TextSide1Colors=@TextSide1Colors,TextSide2Colors=@TextSide2Colors,
TextInkCoveragePercentage=@TextInkCoveragePercentage,TextSpecialColorsID=@TextSpecialColorsID,
OtherSide1Colors=@OtherSide1Colors,OtherSide2Colors=@OtherSide2Colors,OtherInkCoveragePercentage=@OtherInkCoveragePercentage,
OtherSpecialColorsID=@OtherSpecialColorsID,ISCoverPaperSupplied=@ISCoverPaperSupplied,
ISTextPaperSupplied=@ISTextPaperSupplied,ISOtherPaperSupplied=@ISOtherPaperSupplied,CoverPaperTypeID=@CoverPaperTypeID,
TextPaperTypeID=@TextPaperTypeID,OtherPaperTypeID=@OtherPaperTypeID,PaperNotes=@PaperNotes,
Quantity1=@Quantity1,Quantity2=@Quantity2,Quantity3=@Quantity3,FinishingStyleID=@FinishingStyleID,
CoverStyleID=@CoverStyleID,CoverFinishingID=@CoverFinishingID,PackingStyleID=@PackingStyleID,
InsertStyleID=@InsertStyleID,NoOfInserts=@NoOfInserts,FinishingNotes=@FinishingNotes,DeliveryAddressID=@DeliveryAddressID,
BillingAddressID=@BillingAddressID,SystemSiteID=@SystemSiteID,CompanyName=@CompanyName,PrintingNotes=@PrintingNotes,FlagID=@FlagID ,
		    PaperSizeID = @PaperSizeID, CustomeSize =@CustomeSize ,PaperWeight = @PaperWeight ,
		     CoverSpecialColorSide2ID = @CoverSpecialColorSide2ID ,TextSpecialColorSide2ID =@TextSpecialColorSide2ID , 
		     OtherSpecialColorSide2ID= @OtherSpecialColorSide2ID , NCRSet = @NCRSet
    where (EnquiryID=@EnquiryID)
	RETURN