
CREATE Procedure [dbo].[usp_copy_corporate_store]

 @oldStoreID int
As
Begin
 
Declare @newStoreID int  
Declare @defaultAddID int

----Set @oldStoreID = 1707

--copy store
insert into tbl_contactcompanies ( AccountNumber, Name, URL, CreditReference, CreditLimit, Terms, TypeID, DefaultNominalCode, DefaultTill, DefaultMarkUpID, AccountOpenDate, AccountManagerID, Status, IsCustomer, Notes, ISBN, NotesLastUpdatedDate, NotesLastUpdatedBy, AccountOnHandDesc, AccountStatusID, IsDisabled, LockedBy, AccountBalance, CreationDate, VATRegNumber, IsParaentCompany, ParaentCompanyID, SystemSiteID, VATRegReference, FlagID, IsEmailSubscription, IsMailSubscription, IsEmailFormat, HomeContact, AbountUs, ContactUs, IsGeneral, WebAccessAdminUserName, WebAccessAdminPassword, WebAccessAdminPasswordHint, IsShowFinishedGoodPrices, IsReed, DepartmentID, SalesPerson, Image, WebAccessCode, isArchived, CanCreateShoppingAddress, PayByPersonalCredeitCard, PONumberRequired, ShowPrices, CarrierWebPath, CarrierTrackingPath, CorporateOrderingPolicy, isDisplaySiteHeader, isDisplayMenuBar, isDisplayBanners, isDisplayFeaturedProducts, isDisplayPromotionalProducts, isDisplayChooseUsIcons, isDisplaySecondaryPages, isDisplaySiteFooter, RedirectWebstoreURL, defaultPalleteID, isDisplaylBrokerBanners, isBrokerCanLaminate, isBrokerCanRoundCorner, isBrokerCanDeliverSameDay, isBrokerCanAcceptPaymentOnline, BrokerContactCompanyID, isBrokerOrderApprovalRequired, isBrokerPaymentRequired, isWhiteLabel, TwitterURL, FacebookURL, LinkedinURL, WebMasterTag, WebAnalyticCode, isShowGoogleMap, isTextWatermark, WatermarkText, CoreCustomerID, StoreBackgroundImage, isDisplayBrokerSecondaryPages, PriceFlagID, isIncludeVAT, isAllowRegistrationFromWeb, MarketingBriefRecipient, isLoginFirstTime, facebookAppId, facebookAppKey, twitterAppId, twitterAppKey,CustomCSS)
select AccountNumber, Name + ' Copy', URL, CreditReference, CreditLimit, Terms, TypeID, DefaultNominalCode, DefaultTill, DefaultMarkUpID, AccountOpenDate, AccountManagerID, Status, IsCustomer, Notes, ISBN, NotesLastUpdatedDate, NotesLastUpdatedBy, AccountOnHandDesc, AccountStatusID, IsDisabled, LockedBy, AccountBalance, CreationDate, VATRegNumber, IsParaentCompany, ParaentCompanyID, SystemSiteID, VATRegReference, FlagID, IsEmailSubscription, IsMailSubscription, IsEmailFormat, HomeContact, AbountUs, ContactUs, IsGeneral, WebAccessAdminUserName, WebAccessAdminPassword, WebAccessAdminPasswordHint, IsShowFinishedGoodPrices, IsReed, DepartmentID, SalesPerson, Image, WebAccessCode+ '-copy', isArchived, CanCreateShoppingAddress, PayByPersonalCredeitCard, PONumberRequired, ShowPrices, CarrierWebPath, CarrierTrackingPath, CorporateOrderingPolicy, isDisplaySiteHeader, isDisplayMenuBar, isDisplayBanners, isDisplayFeaturedProducts, isDisplayPromotionalProducts, isDisplayChooseUsIcons, isDisplaySecondaryPages, isDisplaySiteFooter, RedirectWebstoreURL, defaultPalleteID, isDisplaylBrokerBanners, isBrokerCanLaminate, isBrokerCanRoundCorner, isBrokerCanDeliverSameDay, isBrokerCanAcceptPaymentOnline, BrokerContactCompanyID, isBrokerOrderApprovalRequired, isBrokerPaymentRequired, isWhiteLabel, TwitterURL, FacebookURL, LinkedinURL, WebMasterTag, WebAnalyticCode, isShowGoogleMap, isTextWatermark, WatermarkText, CoreCustomerID, StoreBackgroundImage, isDisplayBrokerSecondaryPages, PriceFlagID, isIncludeVAT, isAllowRegistrationFromWeb, MarketingBriefRecipient, isLoginFirstTime, facebookAppId, facebookAppKey, twitterAppId, twitterAppKey,CustomCSS
from tbl_contactcompanies where contactcompanyid = @oldStoreID and isarchived = 0

Set @newStoreID =(SELECT SCOPE_IDENTITY());

--Copy strore territories
insert into tbl_ContactCompanyTerritories ( TerritoryName, ContactCompanyID, TerritoryCode)
select  TerritoryName, @newStoreID, TerritoryCode
from tbl_ContactCompanyTerritories where contactcompanyid = @oldStoreID

DECLARE @TerritoryOld AS TABLE(
     ROWID  INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
     oldTerritoryID int
     );
     
insert into @TerritoryOld (oldTerritoryID)
(select TerritoryID from tbl_ContactCompanyTerritories where contactCompanyid = @oldStoreID)

DECLARE @TerritoryNew AS TABLE(
     ROWID  INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
     newTerritoryID int
     );

insert into @TerritoryNew(newTerritoryID)
(select TerritoryID from tbl_ContactCompanyTerritories where contactCompanyid = @newStoreID)

DECLARE @TerritoryMapping AS TABLE(
     ROWID  INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
     oldTerritoryID int,
     newTerritoryID int
     );
     
Declare @tCount int = (select count(*) from @TerritoryOld);
Declare @tCounter int = 1

While (@tCounter <= @tCount)
Begin
	insert into @TerritoryMapping(oldTerritoryID,newTerritoryID)
	Values((select oldTerritoryID from @TerritoryOld where RowID = @tCounter),(select newTerritoryID from @TerritoryNew where RowID = @tCounter))
	set @tCounter = @tCounter + 1;
End

--copy store addresses with new ID
insert into tbl_addresses ( ContactCompanyID, AddressName, Address1, Address2, Address3, City, State, Country, PostCode, Fax, Email, URL, Tel1, Tel2, Extension1, Extension2, Reference, FAO, IsDefaultAddress, IsDefaultShippingAddress, isArchived, TerritoryID, GeoLatitude, GeoLongitude,isDefaultTerrorityBilling,isDefaultTerrorityShipping)
select  @newStoreID, AddressName, Address1, Address2, Address3, City, State, Country, PostCode, Fax, Email, URL, Tel1, Tel2, Extension1, Extension2, Reference, FAO, IsDefaultAddress, IsDefaultShippingAddress, isArchived, TerritoryID, GeoLatitude, GeoLongitude ,isDefaultTerrorityBilling,isDefaultTerrorityShipping
from tbl_addresses where contactcompanyid = @oldStoreID and isarchived = 0


--update territory tbl_addresses
Declare @updateCount int = (select count(*) from @territoryMapping)
Declare @updateCounter int = 1
While (@updateCounter <= @updateCount)
Begin
update tbl_addresses 
set territoryid = (select newTerritoryID from @TerritoryMapping where rowid = @updateCounter)
where contactcompanyid = @newStoreID and isarchived = 0 and TerritoryID = (select oldTerritoryID from @TerritoryMapping where rowid = @updateCounter)
set @updateCounter = @updateCounter + 1;
End


Set @defaultAddID = (select top 1 addressid from tbl_addresses where contactcompanyid = @newStoreID and isdefaultaddress = 1)

--address mapping
DECLARE @AddressesOld AS TABLE(
     ROWID  INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
     AddressID int
     );
     
insert into @AddressesOld (AddressID)
(select AddressID from tbl_addresses where contactCompanyid = @oldStoreID)

DECLARE @AddressesNew AS TABLE(
     ROWID  INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
     AddressID2 int
     );

insert into @AddressesNew(AddressID2)
(select AddressID from tbl_addresses where contactCompanyid = @newStoreID)

DECLARE @AddressMapping AS TABLE(
     ROWID  INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
     oldAddressID int,
     newAddressID int
     );
     
Declare @tCountAddress int = (select count(*) from @AddressesOld);
Declare @tCounterAddress int = 1

While (@tCounterAddress <= @tCountAddress)
Begin
	insert into @AddressMapping(oldAddressID,newAddressID)
	Values((select AddressID from @AddressesOld where RowID = @tCounterAddress),(select AddressID2 from @AddressesNew where RowID = @tCounterAddress))
	set @tCounterAddress = @tCounterAddress + 1;
End

--copy store contacts with new ID and Default AddressID
insert into tbl_contacts ( AddressID, ContactCompanyID, FirstName, MiddleName, LastName, Title, HomeTel1, HomeTel2, HomeExtension1, HomeExtension2, Mobile, Pager, Email, FAX, JobTitle, DOB, Notes, IsDefaultContact, HomeAddress1, HomeAddress2, HomeCity, HomeState, HomePostCode, HomeCountry, SecretQuestion, SecretAnswer, Password, URL, IsEmailSubscription, IsNewsLetterSubscription, image, quickFullName, quickTitle, quickCompanyName, quickAddress1, quickAddress2, quickAddress3, quickPhone, quickFax, quickEmail, quickWebsite, quickCompMessage, QuestionID, IsApprover, isWebAccess, isPlaceOrder, CreditLimit, isArchived, ContactRoleID, DepartmentID, TerritoryID, ClaimIdentifer, AuthentifiedBy, IsPayByPersonalCreditCard, IsPricingshown, SkypeID, LinkedinURL, FacebookURL, TwitterURL, authenticationToken, twitterScreenName,shippingaddressid)
select  AddressID, @newStoreID, FirstName, MiddleName, LastName, Title, HomeTel1, HomeTel2, HomeExtension1, HomeExtension2, Mobile, Pager, 
substring(Email,0,charindex('@',Email)) + '-copy' + substring(Email,charindex('@',Email),LEN(Email)) , FAX, JobTitle, DOB, Notes, IsDefaultContact, HomeAddress1, HomeAddress2, HomeCity, HomeState, HomePostCode, HomeCountry, SecretQuestion, SecretAnswer, Password, URL, IsEmailSubscription, IsNewsLetterSubscription, image, quickFullName, quickTitle, quickCompanyName, quickAddress1, quickAddress2, quickAddress3, quickPhone, quickFax, quickEmail, quickWebsite, quickCompMessage, QuestionID, IsApprover, isWebAccess, isPlaceOrder, CreditLimit, isArchived, ContactRoleID, DepartmentID, TerritoryID, ClaimIdentifer, AuthentifiedBy, IsPayByPersonalCreditCard, IsPricingshown, SkypeID, LinkedinURL, FacebookURL, TwitterURL, authenticationToken, twitterScreenName,shippingaddressid
from tbl_contacts where contactcompanyid = @oldStoreID and isarchived = 0

--update territory tbl_contacts
Declare @updateContactCount int = (select count(*) from @territoryMapping)
Declare @updateContactCounter int = 1
While (@updateContactCounter <= @updateContactCount)
Begin
update tbl_contacts 
set territoryid = (select newTerritoryID from @TerritoryMapping where rowid = @updateContactCounter)
where contactcompanyid = @newStoreID and isarchived = 0 and TerritoryID = (select oldTerritoryID from @TerritoryMapping where rowid = @updateContactCounter)
set @updateContactCounter = @updateContactCounter + 1;
End
--update addressids tbl_contacts
Declare @updateAddressCount int = (select count(*) from @AddressMapping)
Declare @updateAddressCounter int = 1
While (@updateAddressCounter <= @updateAddressCount)
Begin
update tbl_contacts 
set addressid = (select newaddressid from @AddressMapping where rowid = @updateAddressCounter)
where contactcompanyid = @newStoreID and isarchived = 0 and addressid = (select oldaddressid from @AddressMapping where rowid = @updateAddressCounter)
set @updateAddressCounter = @updateAddressCounter + 1;
End

--update shipping address for tbl_contacts
Declare @uc int = (select count(*) from @AddressMapping)
Declare @ucr int = 1
While (@ucr <= @uc)
Begin
update tbl_contacts 
set ShippingAddressID = (select newaddressid from @AddressMapping where rowid = @ucr)
where contactcompanyid = @newStoreID and isarchived = 0 and ShippingAddressID = (select oldaddressid from @AddressMapping where rowid = @ucr)
set @ucr = @ucr + 1;
End

--Copy store paymentgateways
insert into tbl_PaymentGateways( BusinessEmail, IdentityToken, isActive, BrokerContactCompanyID, PaymentMethodID, SecureHash)
select  BusinessEmail, IdentityToken, isActive, @newStoreID, PaymentMethodID, SecureHash 
from tbl_PaymentGateways where BrokerContactCompanyID = @oldStoreID

--Copy store cost centres

insert into tbl_ContactCompanyCostCenters(ContactCompanyID, CostCentreID, BrokerMarkup, ContactMarkup, isDisplayToUser)
select @newStoreID, CostCentreID, BrokerMarkup, ContactMarkup, isDisplayToUser
from tbl_ContactCompanyCostCenters where contactcompanyid = @oldStoreID

--Copy CMS PAGE BANNERS
insert into tbl_cmsPageBanners( PageID, ImageURL, Heading, Description, ItemURL, ButtonURL, isActive, CreatedBy, CreateDate, ModifyID, ModifyDate, ContactCompanyID)
select   PageID, ImageURL, Heading, Description, ItemURL, ButtonURL, isActive, CreatedBy, CreateDate, ModifyID, ModifyDate, @newStoreID
from tbl_cmsPageBanners where contactcompanyid = @oldStoreID

DECLARE @CATS AS TABLE(
     ROWID  INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
     ProCatID int     
     );
     
 DECLARE @NEWCATS AS TABLE(
	 old_ID  int,
	 new_ID int     
     );
     
Declare @iteration int = 1;
  
WITH CTE(ProductCategoryID, ParentCategoryID, CategoryName,level)  
AS (SELECT   p2.ProductCategoryID, p2.ParentCategoryID, p2.CategoryName, 0 as level                       
from tbl_ProductCategory p2
inner join (select  *
from tbl_productcategory parent
where parent.contactcompanyid = @oldStoreID  and  parent.isarchived = 0 ) maincat on maincat.productcategoryid = p2.productcategoryid
UNION ALL        
SELECT    PC.ProductCategoryID, pc.ParentCategoryID, pc.CategoryName, level - 1  
from tbl_ProductCategory PC  
Inner join CTE on CTE.ProductCategoryID = PC.ParentCategoryID
where PC.isarchived = 0
) 			 
insert into @CATS(ProCatID)
SELECT     distinct ProductCategoryID
FROM CTE AS CTE_1 

--select * from @CATS

Declare @Counter int = 1
Declare @CatCount int = (select count(*) from @CATS)
Declare @ItemCounter int = 1

Declare @ProductImagesPath varchar(50) = '/StoredImages/ProductImages/'
Declare @ProductCategoryImagesPath varchar(50) = '/StoredImages/ProductCategoryImages/'

while (@Counter <= @CatCount)
Begin

	Declare @oldCatID int = (select ProCatID from @CATS where RowID = @Counter)
	
	Declare @newCatID int = 0
	Declare @isParentCat int = (select isnull(ParentCategoryID,0) from tbl_productcategory where ProductCategoryID = @oldCatID)
	Declare @url varchar(100) = ''
	Declare @extention varchar(10) = ''
	if (@isParentCat = 0)
	Begin
		--Copy Category
		insert into tbl_productcategory (CategoryName, ContentType, Description1, Description2, LockedBy, ContactCompanyID, ParentCategoryID, DisplayOrder, ImagePath, ThumbnailPath, isEnabled, isMarketPlace, TemplateDesignerMappedCategoryName, isArchived, isPublished, TrimmedWidth, TrimmedHeight, isColorImposition, isOrderImposition, isLinkToTemplates, Sides, ApplySizeRestrictions, ApplyFoldLines, WidthRestriction, HeightRestriction, CategoryTypeID, RegionID, ZoomFactor, ScaleFactor, isShelfProductCategory, MetaKeywords, MetaDescription, MetaTitle)
		select  CategoryName, ContentType, Description1, Description2, LockedBy, @newStoreID, ParentCategoryID, DisplayOrder, ImagePath, ThumbnailPath, isEnabled, isMarketPlace, TemplateDesignerMappedCategoryName, isArchived, isPublished, TrimmedWidth, TrimmedHeight, isColorImposition, isOrderImposition, isLinkToTemplates, Sides, ApplySizeRestrictions, ApplyFoldLines, WidthRestriction, HeightRestriction, CategoryTypeID, RegionID, ZoomFactor, ScaleFactor, isShelfProductCategory, MetaKeywords, MetaDescription, MetaTitle
		from tbl_productcategory where ProductCategoryID = @oldCatID	
		Set @newCatID = (SELECT SCOPE_IDENTITY());
		
		
		
		insert into @NEWCATS (old_ID,new_ID)
		Values(@oldCatID,@newCatID)
		
	End
	Else
	Begin
		insert into tbl_productcategory (CategoryName, ContentType, Description1, Description2, LockedBy, ContactCompanyID, ParentCategoryID, DisplayOrder, ImagePath, ThumbnailPath, isEnabled, isMarketPlace, TemplateDesignerMappedCategoryName, isArchived, isPublished, TrimmedWidth, TrimmedHeight, isColorImposition, isOrderImposition, isLinkToTemplates, Sides, ApplySizeRestrictions, ApplyFoldLines, WidthRestriction, HeightRestriction, CategoryTypeID, RegionID, ZoomFactor, ScaleFactor, isShelfProductCategory, MetaKeywords, MetaDescription, MetaTitle)
		select  CategoryName, ContentType, Description1, Description2, LockedBy, NULL, ParentCategoryID, DisplayOrder, ImagePath, ThumbnailPath, isEnabled, isMarketPlace, TemplateDesignerMappedCategoryName, isArchived, isPublished, TrimmedWidth, TrimmedHeight, isColorImposition, isOrderImposition, isLinkToTemplates, Sides, ApplySizeRestrictions, ApplyFoldLines, WidthRestriction, HeightRestriction, CategoryTypeID, RegionID, ZoomFactor, ScaleFactor, isShelfProductCategory, MetaKeywords, MetaDescription, MetaTitle
		from tbl_productcategory where ProductCategoryID = @oldCatID	
		Set @newCatID = (SELECT SCOPE_IDENTITY());
		insert into @NEWCATS (old_ID,new_ID)
		Values(@oldCatID,@newCatID)
		
		Declare @oldParentID int = (select parentcategoryid from tbl_productcategory where productcategoryid = @newCatID)
		Declare @newParentID int = (select new_id from @NEWCATS where old_ID = @oldParentID)
		update tbl_productCategory set parentcategoryid = @newParentID where productcategoryid = @newCatID
	End
	--Update category image and thumbnail	
	Set @url = (select IsNull(imagePath,'') from tbl_productcategory where productcategoryid = @oldCatID and imagePath is not null and imagepath <> '')
	if @url <> ''
	Begin
		Set @extention = (select top 1 items from fb_Split_String(@url,'.') order by 1 desc)
		update tbl_productcategory set ImagePath = @ProductCategoryImagesPath + CategoryName + '_' + Convert(Varchar(max),ProductCategoryID) + '_catDetail.' + @extention
		where productcategoryid = @newCatID
	End
	
	set @url = (select IsNull(ThumbnailPath,'') from tbl_productcategory where productcategoryid = @oldCatID and ThumbnailPath is not null and ThumbnailPath <> '')
	if @url <> ''
	Begin
		Set @extention  = (select top 1 items from fb_Split_String(@url,'.') order by 1 desc)
		update tbl_productcategory set ThumbnailPath = @ProductCategoryImagesPath + CategoryName + '_' + Convert(Varchar(max),ProductCategoryID) + '_catThumb.' + @extention
		where productcategoryid = @newCatID
	End
	------------	
-------------Item Section----
		
		DECLARE @ITEMS AS TABLE(
		 ROWID  INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
		 ItemID int     
		 );
     
		insert into @ITEMS (ItemID)
		select ItemID from tbl_items where productcategoryid = @oldCatID and isarchived = 0 and estimateid is NULL and invoiceid is null
		
		Declare @ItemCount int = (select count(*) from @ITEMS)
		
		while (@ItemCounter <= @ItemCount)
		Begin	
			set @iteration = @iteration + 1;
			
			Declare @oldItemID int = (select ItemID from @ITEMS where ROWID = @ItemCounter)
			Declare @newItemID int = 0
			--Copy Item
			insert into tbl_items (ItemCode, EstimateID, InvoiceID, Title, Tax1, Tax2, Tax3, CreatedBy, Status, ItemCreationDateTime, ItemLastUpdateDateTime, IsMultipleQty, RunOnQty, RunonCostCentreProfit, RunonBaseCharge, RunOnMarkUpID, RunonPercentageValue, RunOnMarkUpValue, RunOnNetTotal, Qty1, Qty2, Qty3, Qty1CostCentreProfit, Qty2CostCentreProfit, Qty3CostCentreProfit, Qty1BaseCharge1, Qty2BaseCharge2, Qty3BaseCharge3, Qty1MarkUpID1, Qty2MarkUpID2, Qty3MarkUpID3, Qty1MarkUpPercentageValue, Qty2MarkUpPercentageValue, Qty3MarkUpPercentageValue, Qty1MarkUp1Value, Qty2MarkUp2Value, Qty3MarkUp3Value, Qty1NetTotal, Qty2NetTotal, Qty3NetTotal, Qty1Tax1Value, Qty1Tax2Value, Qty1Tax3Value, Qty1GrossTotal, Qty2Tax1Value, Qty2Tax2Value, Qty2Tax3Value, Qty2grossTotal, Qty3Tax1Value, Qty3Tax2Value, Qty3Tax3Value, Qty3GrossTotal, IsDescriptionLocked, qty1title, qty2title, qty3Title, RunonTitle, AdditionalInformation, qty2Description, qty3Description, RunonDescription, EstimateDescriptionTitle1, EstimateDescriptionTitle2, EstimateDescriptionTitle3, EstimateDescriptionTitle4, EstimateDescriptionTitle5, EstimateDescriptionTitle6, EstimateDescriptionTitle7, EstimateDescriptionTitle8, EstimateDescriptionTitle9, EstimateDescriptionTitle10, EstimateDescription1, EstimateDescription2, EstimateDescription3, EstimateDescription4, EstimateDescription5, EstimateDescription6, EstimateDescription7, EstimateDescription8, EstimateDescription9, EstimateDescription10, JobDescriptionTitle1, JobDescriptionTitle2, JobDescriptionTitle3, JobDescriptionTitle4, JobDescriptionTitle5, JobDescriptionTitle6, JobDescriptionTitle7, JobDescriptionTitle8, JobDescriptionTitle9, JobDescriptionTitle10, JobDescription1, JobDescription2, JobDescription3, JobDescription4, JobDescription5, JobDescription6, JobDescription7, JobDescription8, JobDescription9, JobDescription10, IsParagraphDescription, EstimateDescription, JobDescription, InvoiceDescription, JobCode, JobManagerID, JobEstimatedStartDateTime, JobEstimatedCompletionDateTime, JobCreationDateTime, JobProgressedBy, jobSelectedQty, JobStatusID, IsJobCardPrinted, IsItemLibraray, ItemLibrarayGroupID, PayInFullInvoiceID, IsGroupItem, ItemType, IsIncludedInPipeLine, IsRunOnQty, CanCopyToEstimate, FlagID, CostCenterDescriptions, IsRead, IsScheduled, IsPaperStatusChanged, IsJobCardCreated, IsAttachmentAdded, IsItemValueChanged, DepartmentID, ItemNotes, UpdatedBy, LastUpdate, JobActualStartDateTime, JobActualCompletionDateTime, IsJobCostingDone, ProductName, ProductCategoryID, ImagePath, ThumbnailPath, ProductSpecification, CompleteSpecification, DesignGuideLines, ProductCode, IsPublished, ContactCompanyID, PriceDiscountPercentage, IsEnabled, IsSpecialItem, IconPath, IsPopular, IsFeatured, IsPromotional, TipsAndHints, FactSheetFileName, IsArchived, NominalCodeID, RefItemID, TemplateID, WebDescription, ItemTypeID, IsOrderedItem, JobCardPrintedBy, JobCardLastPrintedDate, EstimateProductionTime, SortOrder, IsFinishedGoods, LayoutGridContent, HowToVideoContent, file1, file2, file3, file4, file5, GridImage, isQtyRanged, CostCentreProfitBroker, BaseChargeBroker, MarkUpValueBroker, NetTotalBroker, TaxValueBroker, GrossTotalBroker, isCMYK, SupplierID, isStockControl, isUploadImage, isMarketingBrief, SupplierID2, FinishedGoodID, IsFinishedGoodPrivate, MetaKeywords, MetaDescription, MetaTitle)
			select ItemCode, EstimateID, InvoiceID, Title, Tax1, Tax2, Tax3, CreatedBy, Status, ItemCreationDateTime, ItemLastUpdateDateTime, IsMultipleQty, RunOnQty, RunonCostCentreProfit, RunonBaseCharge, RunOnMarkUpID, RunonPercentageValue, RunOnMarkUpValue, RunOnNetTotal, Qty1, Qty2, Qty3, Qty1CostCentreProfit, Qty2CostCentreProfit, Qty3CostCentreProfit, Qty1BaseCharge1, Qty2BaseCharge2, Qty3BaseCharge3, Qty1MarkUpID1, Qty2MarkUpID2, Qty3MarkUpID3, Qty1MarkUpPercentageValue, Qty2MarkUpPercentageValue, Qty3MarkUpPercentageValue, Qty1MarkUp1Value, Qty2MarkUp2Value, Qty3MarkUp3Value, Qty1NetTotal, Qty2NetTotal, Qty3NetTotal, Qty1Tax1Value, Qty1Tax2Value, Qty1Tax3Value, Qty1GrossTotal, Qty2Tax1Value, Qty2Tax2Value, Qty2Tax3Value, Qty2grossTotal, Qty3Tax1Value, Qty3Tax2Value, Qty3Tax3Value, Qty3GrossTotal, IsDescriptionLocked, qty1title, qty2title, qty3Title, RunonTitle, AdditionalInformation, qty2Description, qty3Description, RunonDescription, EstimateDescriptionTitle1, EstimateDescriptionTitle2, EstimateDescriptionTitle3, EstimateDescriptionTitle4, EstimateDescriptionTitle5, EstimateDescriptionTitle6, EstimateDescriptionTitle7, EstimateDescriptionTitle8, EstimateDescriptionTitle9, EstimateDescriptionTitle10, EstimateDescription1, EstimateDescription2, EstimateDescription3, EstimateDescription4, EstimateDescription5, EstimateDescription6, EstimateDescription7, EstimateDescription8, EstimateDescription9, EstimateDescription10, JobDescriptionTitle1, JobDescriptionTitle2, JobDescriptionTitle3, JobDescriptionTitle4, JobDescriptionTitle5, JobDescriptionTitle6, JobDescriptionTitle7, JobDescriptionTitle8, JobDescriptionTitle9, JobDescriptionTitle10, JobDescription1, JobDescription2, JobDescription3, JobDescription4, JobDescription5, JobDescription6, JobDescription7, JobDescription8, JobDescription9, JobDescription10, IsParagraphDescription, EstimateDescription, JobDescription, InvoiceDescription, JobCode, JobManagerID, JobEstimatedStartDateTime, JobEstimatedCompletionDateTime, JobCreationDateTime, JobProgressedBy, jobSelectedQty, JobStatusID, IsJobCardPrinted, IsItemLibraray, ItemLibrarayGroupID, PayInFullInvoiceID, IsGroupItem, ItemType, IsIncludedInPipeLine, IsRunOnQty, CanCopyToEstimate, FlagID, CostCenterDescriptions, IsRead, IsScheduled, IsPaperStatusChanged, IsJobCardCreated, IsAttachmentAdded, IsItemValueChanged, DepartmentID, ItemNotes, UpdatedBy, LastUpdate, JobActualStartDateTime, JobActualCompletionDateTime, IsJobCostingDone, ProductName, @newCatID, ImagePath, ThumbnailPath, ProductSpecification, CompleteSpecification, DesignGuideLines, ProductCode, IsPublished, ContactCompanyID, PriceDiscountPercentage, IsEnabled, IsSpecialItem, IconPath, IsPopular, IsFeatured, IsPromotional, TipsAndHints, FactSheetFileName, IsArchived, NominalCodeID, RefItemID, TemplateID, WebDescription, ItemTypeID, IsOrderedItem, JobCardPrintedBy, JobCardLastPrintedDate, EstimateProductionTime, SortOrder, IsFinishedGoods, LayoutGridContent, HowToVideoContent, file1, file2, file3, file4, file5, GridImage, isQtyRanged, CostCentreProfitBroker, BaseChargeBroker, MarkUpValueBroker, NetTotalBroker, TaxValueBroker, GrossTotalBroker, isCMYK, SupplierID, isStockControl, isUploadImage, isMarketingBrief, SupplierID2, FinishedGoodID, IsFinishedGoodPrivate, MetaKeywords, MetaDescription, MetaTitle
			from tbl_items where itemid = @oldItemID
			Set @newItemID = (SELECT SCOPE_IDENTITY());
			
			--Update ImagePaths
			--Image
			Set @url = (select IsNull(imagePath,'') from tbl_items where itemid = @oldItemID and imagePath is not null and imagepath <> '')
			if @url <> ''
			Begin
				Set @extention = (select top 1 items from fb_Split_String(@url,'.') order by 1 desc)
				update tbl_items set ImagePath = @ProductImagesPath + ProductName + '_' + Convert(Varchar(max),ItemID) + '_Detail.' + @extention
				where itemid = @newItemID
			End
			--Thumbnail
			Set @url = (select IsNull(ThumbnailPath,'') from tbl_items where itemid = @oldItemID and ThumbnailPath is not null and ThumbnailPath <> '')
			if @url <> ''
			Begin
				Set @extention = (select top 1 items from fb_Split_String(@url,'.') order by 1 desc)
				update tbl_items set ThumbnailPath = @ProductImagesPath + ProductName + '_' + Convert(Varchar(max),ItemID) + '_Thumbnail.' + @extention
				where itemid = @newItemID
			End
			--Grid Image
			Set @url = (select IsNull(GridImage,'') from tbl_items where itemid = @oldItemID and GridImage is not null and GridImage <> '')
			if @url <> ''
			Begin
				Set @extention = (select top 1 items from fb_Split_String(@url,'.') order by 1 desc)
				update tbl_items set GridImage = @ProductImagesPath + ProductName + '_' + Convert(Varchar(max),ItemID) + '_GridImage.' + @extention
				where itemid = @newItemID
			End
			
			-------------------
			
			--Copy Item Price Matrix
			insert into tbl_items_pricematrix(Quantity, Price, ItemID, PricePaperType1, PricePaperType2, PricePaperType3, IsDiscounted, QtyRangeFrom, QtyRangeTo, SupplierID, PriceStockType4, PriceStockType5, PriceStockType6, PriceStockType7, PriceStockType8, PriceStockType9, PriceStockType10, PriceStockType11, FlagID, SupplierSequence, ContactCompanyID)
			select  Quantity, Price, @newItemID, PricePaperType1, PricePaperType2, PricePaperType3, IsDiscounted, QtyRangeFrom, QtyRangeTo, SupplierID, PriceStockType4, PriceStockType5, PriceStockType6, PriceStockType7, PriceStockType8, PriceStockType9, PriceStockType10, PriceStockType11, FlagID, SupplierSequence, ContactCompanyID
			from tbl_items_pricematrix where ItemID = @oldItemID
			
			--Copy Item Stock Options
			insert into tbl_ItemStockOptions ( ItemID, OptionSequence, StockID, StockLabel)
			select @newItemID, OptionSequence, StockID, StockLabel
			from tbl_ItemStockOptions where ItemID = @oldItemID
			
			--Copy Item's RelatedItems
			insert into tbl_items_RelatedItems (ItemID,RelatedItemID)
			select @newItemID,RelatedItemID
			from tbl_items_RelatedItems where ItemID = @oldItemID
			
			--Copy Product Details
			insert into tbl_items_ProductDetails ( ItemID, isInternalActivity, isAutoCreateSupplierPO, isQtyLimit, QtyLimit, DeliveryTimeSupplier1, DeliveryTimeSupplier2, isPrintItem, isAllowMarketBriefAttachment, MarketBriefSuccessMessage)
			select  @newItemID, isInternalActivity, isAutoCreateSupplierPO, isQtyLimit, QtyLimit, DeliveryTimeSupplier1, DeliveryTimeSupplier2, isPrintItem, isAllowMarketBriefAttachment, MarketBriefSuccessMessage
			from tbl_items_ProductDetails where ItemID = @oldItemID
			
			--Copy Items Addons
			insert into tbl_Items_AddonCostCentres ( ItemID, CostCentreID, DiscountPercentage, IsDiscounted)
			select @newItemID, CostCentreID, DiscountPercentage, IsDiscounted
			from tbl_Items_AddonCostCentres where ItemID = @oldItemID
			
			--Copy Items Sections
			insert into tbl_item_sections (SectionNo, SectionName, IsMainSection, IsMultipleQty, IsRunOnQty, ItemID, Qty1, Qty2, Qty3, Qty4, Qty5, Qty1Profit, Qty2Profit, Qty3Profit, Qty4Profit, Qty5Profit, BaseCharge1, BaseCharge2, Basecharge3, BaseCharge4, BaseCharge5, RunOnQty, RunOnBaseCharge, RunonProfit, SectionSizeID, IsSectionSizeCustom, SectionSizeHeight, SectionSizeWidth, ItemSizeID, IsItemSizeCustom, ItemSizeHeight, ItemSizeWidth, GuillotineID, IncludeGutter, PressID, FilmID, PlateID, ItemGutterHorizontal, ItemGutterVertical, IsPressrestrictionApplied, IsDoubleSided, IsWashup, PrintViewLayoutLandScape, PrintViewLayoutPortrait, PrintViewLayout, SetupSpoilage, RunningSpoilage, RunningSpoilageValue, EstimateForWholePacks, IsFirstTrim, IsSecondTrim, PaperQty, ImpressionQty1, ImpressionQty2, ImpressionQty3, ImpressionQty4, ImpressionQty5, FilmQty, IsFilmSupplied, IsPlateSupplied, IsPaperSupplied, WashupQty, MakeReadyQty, IsPaperCoated, GuillotineFirstCut, GuillotineSecondCut, GuillotineCutTime, GuillotineQty1BundlesFirstTrim, GuillotineQty2BundlesFirstTrim, GuillotineQty3BundlesFirstTrim, GuillotineQty1BundlesSecondTrim, GuillotineQty2BundlesSecondTrim, GuillotineQty3BundlesSecondTrim, GuillotineQty1FirstTrimCuts, GuillotineQty2FirstTrimCuts, GuillotineQty3FirstTrimCuts, GuillotineQty1SecondTrimCuts, GuillotineQty2SecondTrimCuts, GuillotineQty3SecondTrimCuts, GuillotineQty1TotalsCuts, GuillotineQty2TotalsCuts, GuillotineQty3TotalsCuts, AdditionalFilmUsed, AdditionalPlateUsed, IsFilmUsed, IsPlateUsed, NoofUniqueInks, WizardRunMode, OverAllPTV, ItemPTV, Side1Inks, Side2Inks, IsSwingApplied, SectionType, IsMakeReadyUsed, isWorknTurn, isWorkntumble, QuestionQueue, StockQueue, InputQueue, CostCentreQueue, PressSpeed1, PressSpeed2, PressSpeed3, PressSpeed4, PressSpeed5, PrintSheetQty1, PrintSheetQty2, PrintSheetQty3, PrintSheetQty4, PrintSheetQty5, PressHourlyCharge, PrintChargeExMakeReady1, PrintChargeExMakeReady2, PrintChargeExMakeReady3, PrintChargeExMakeReady4, PrintChargeExMakeReady5, PaperGsm, PaperPackPrice, PTVRows, PTVColoumns, PaperWeight1, PaperWeight2, PaperWeight3, PaperWeight4, PaperWeight5, FinishedItemQty1, FinishedItemQty2, FinishedItemQty3, FinishedItemQty4, FinishedItemQty5, ProfileID, SelectedPressCalculationMethodID, SectionNotes, IsScheduled, ImageType, WebClylinderHeight, WebCylinderWidth, WebCylinderId, WebPaperLengthWithSp, WebPaperLengthWoSp, WebReelMakereadyQty, WebStockPaperCost, WebSpoilageType, PressPassesQty, PrintingType, PadsLeafQty, PadsQuantity, LastUpdateDate, LastUpdatedBy, Qty1MarkUpID, Qty2MarkUpID, Qty3MarkUpID, StockItemID1, StockItemID2, StockItemID3, Side1PlateQty, IsPortrait, Side2PlateQty, InkColorType, BaseCharge1Broker, PlateInkID, SimilarSections)
			select SectionNo, SectionName, IsMainSection, IsMultipleQty, IsRunOnQty, @newItemID, Qty1, Qty2, Qty3, Qty4, Qty5, Qty1Profit, Qty2Profit, Qty3Profit, Qty4Profit, Qty5Profit, BaseCharge1, BaseCharge2, Basecharge3, BaseCharge4, BaseCharge5, RunOnQty, RunOnBaseCharge, RunonProfit, SectionSizeID, IsSectionSizeCustom, SectionSizeHeight, SectionSizeWidth, ItemSizeID, IsItemSizeCustom, ItemSizeHeight, ItemSizeWidth, GuillotineID, IncludeGutter, PressID, FilmID, PlateID, ItemGutterHorizontal, ItemGutterVertical, IsPressrestrictionApplied, IsDoubleSided, IsWashup, PrintViewLayoutLandScape, PrintViewLayoutPortrait, PrintViewLayout, SetupSpoilage, RunningSpoilage, RunningSpoilageValue, EstimateForWholePacks, IsFirstTrim, IsSecondTrim, PaperQty, ImpressionQty1, ImpressionQty2, ImpressionQty3, ImpressionQty4, ImpressionQty5, FilmQty, IsFilmSupplied, IsPlateSupplied, IsPaperSupplied, WashupQty, MakeReadyQty, IsPaperCoated, GuillotineFirstCut, GuillotineSecondCut, GuillotineCutTime, GuillotineQty1BundlesFirstTrim, GuillotineQty2BundlesFirstTrim, GuillotineQty3BundlesFirstTrim, GuillotineQty1BundlesSecondTrim, GuillotineQty2BundlesSecondTrim, GuillotineQty3BundlesSecondTrim, GuillotineQty1FirstTrimCuts, GuillotineQty2FirstTrimCuts, GuillotineQty3FirstTrimCuts, GuillotineQty1SecondTrimCuts, GuillotineQty2SecondTrimCuts, GuillotineQty3SecondTrimCuts, GuillotineQty1TotalsCuts, GuillotineQty2TotalsCuts, GuillotineQty3TotalsCuts, AdditionalFilmUsed, AdditionalPlateUsed, IsFilmUsed, IsPlateUsed, NoofUniqueInks, WizardRunMode, OverAllPTV, ItemPTV, Side1Inks, Side2Inks, IsSwingApplied, SectionType, IsMakeReadyUsed, isWorknTurn, isWorkntumble, QuestionQueue, StockQueue, InputQueue, CostCentreQueue, PressSpeed1, PressSpeed2, PressSpeed3, PressSpeed4, PressSpeed5, PrintSheetQty1, PrintSheetQty2, PrintSheetQty3, PrintSheetQty4, PrintSheetQty5, PressHourlyCharge, PrintChargeExMakeReady1, PrintChargeExMakeReady2, PrintChargeExMakeReady3, PrintChargeExMakeReady4, PrintChargeExMakeReady5, PaperGsm, PaperPackPrice, PTVRows, PTVColoumns, PaperWeight1, PaperWeight2, PaperWeight3, PaperWeight4, PaperWeight5, FinishedItemQty1, FinishedItemQty2, FinishedItemQty3, FinishedItemQty4, FinishedItemQty5, ProfileID, SelectedPressCalculationMethodID, SectionNotes, IsScheduled, ImageType, WebClylinderHeight, WebCylinderWidth, WebCylinderId, WebPaperLengthWithSp, WebPaperLengthWoSp, WebReelMakereadyQty, WebStockPaperCost, WebSpoilageType, PressPassesQty, PrintingType, PadsLeafQty, PadsQuantity, LastUpdateDate, LastUpdatedBy, Qty1MarkUpID, Qty2MarkUpID, Qty3MarkUpID, StockItemID1, StockItemID2, StockItemID3, Side1PlateQty, IsPortrait, Side2PlateQty, InkColorType, BaseCharge1Broker, PlateInkID, SimilarSections
			from tbl_item_sections where ItemID = @oldItemID
			
			--Copy Product Images
			insert into tbl_itemImages ( ItemID, ImageTitle, ImageURL, ImageType, ImageName, UploadDate)
			select @newItemID,ImageTitle, ImageURL, ImageType, ImageName, UploadDate
			from tbl_itemImages where ItemID = @oldItemID 
			
			--update product image paths
			
			update tbl_itemimages set ImageURL = @ProductImagesPath + Convert(Varchar(max),ItemID) + '/' + ImageName
			where ItemID = @newItemID
			Set @ItemCounter = @ItemCounter + 1;
		End
		
		------- End Item Section-----
	Set @Counter = @Counter + 1;
End	


select @newStoreID as NewStoreID

End