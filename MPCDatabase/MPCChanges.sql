/* Created By: Khurram Shehzad */

Use MPC

GO

/* Execution Date: 10/02/2015 */
exec sp_rename 'LookupMethods.CompanyID', 'OrganisationID', 'Column'

ALTER TABLE PaperSize
ADD OrganisationId bigint null

ALTER TABLE Machine
ADD OrganisationId bigint null

ALTER TABLE PhraseField
ADD OrganisationId bigint null

GO

/* Execution Date: 11/02/2015 */
GO

exec sp_rename 'Activity.ContactCompanyId', 'CompanyId', 'Column'

drop index activity.SupplierContactID

alter table activity
drop column suppliercontactid, prospectcontactid

alter table Activity
alter column CompanyId bigint

alter table Activity
add foreign key (CompanyId)
references Company(CompanyId)

drop index Activity.SystemUserID

alter table activity
drop constraint DF__tbl_activ__Syste__0DAF0CB0

alter table activity
alter column SystemUserId uniqueidentifier

exec sp_rename 'Invoice.ContactCompanyId', 'CompanyId', 'Column'

alter table Invoice
drop column contactcompany

alter table Invoice
alter column CompanyId bigint

alter table Invoice
alter column ContactId bigint

alter table Invoice
alter column EstimateId bigint

alter table Estimate
drop column companyname

alter table company
drop constraint DF__tbl_custo__Accou__30C33EC3

alter table company
alter column accountmanagerid uniqueidentifier null

drop table ArtworkFileTable

drop table AttachmentFileTable

alter table ProductCategory
drop constraint FK__ProductCa__Image__1B36525C

alter table ProductCategory
drop constraint FK__ProductCa__Thumb__1C2A7695

alter table ProductCategory
drop column ImageStreamId

alter table ProductCategory
drop column ThumbnailStreamId

drop table CategoryFileTable

drop table CompanyBannerFileTable

drop table CostCentreFileTable

drop table MediaFileTable

drop table MPCFileTable

alter table Organisation
drop constraint FK_OrganisationFileTable_Organisation

alter table Organisation
drop column MISLogoStreamId

drop table OrganisationFileTable

drop table ProductFileTable

drop table SecondaryPageFileTable

drop table StoreFileTable

drop table TemplateFileTable

drop function GetMPCFileTableNewPathLocator

drop procedure GetNewPathLocator

drop procedure MPCFileTable_Add

drop procedure MPCFileTable_Del

GO

/* Execution Date: 16/02/2015 */
GO

ALTER TABLE ReportNote
ADD OrganisationId bigint

GO

/* Execution Date: 18/02/2015 */

Go
alter table CostCentre add CarrierId bigint

/* Object:  Table [dbo].[DeliveryCarrier]    Script Date: 2/18/2015 5:04:43 PM */
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[DeliveryCarrier](
 [CarrierId] [bigint] IDENTITY(1,1) NOT NULL,
 [CarrierName] [varchar](200) NULL,
 [Url] [varchar](200) NULL,
 [ApiKey] [varchar](150) NULL,
 [ApiPassword] [varchar](150) NULL,
 [isEnable] [bit] NULL,
 CONSTRAINT [PK_DeliveryCarrier] PRIMARY KEY CLUSTERED 
(
 [CarrierId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER table items add printCropMarks bit null 
ALTER table items add drawBleedArea bit null 
ALTER table items add isMultipagePDF bit null 
ALTER table items add drawWaterMarkTxt bit null 
ALTER table items add allowPdfDownload bit null 
ALTER table items add allowImageDownload bit null

alter table company
drop column DeliveryPickUpAddressId

GO

/* Execution Date: 19/02/2015 */
GO

alter table Currency add CurrencySymbol varchar(5)
alter table paymentGateway add CancelPurchaseUrl varchar(255)
alter table paymentGateway add ReturnUrl varchar(255)
alter table paymentGateway add NotifyUrl varchar(255)
alter table paymentGateway add SendToReturnURL bit
alter table paymentGateway add UseSandbox bit
alter table paymentGateway add LiveApiUrl varchar(255)
alter table paymentGateway add TestApiUrl varchar(255)
alter table Organisation add TaxServiceUrl varchar(255)
alter table Organisation add TaxServiceKey varchar(255)

alter table ItemSection
drop column QuestionQueue

alter table ItemSection
drop DF_tbl_item_sections_InputQueue

alter table ItemSection
drop column InputQueue

alter table ItemSection
drop column StockQueue

alter table ItemSection
drop column CostCentreQueue

alter table ItemSection
add QuestionQueue ntext null

alter table ItemSection
add InputQueue ntext  null

alter table ItemSection
add StockQueue ntext null

alter table ItemSection
add CostCentreQueue ntext null

GO

/* Execution Date: 23/02/2015 */
GO

exec sp_rename 'CostCentreMatrix.CompanyId', 'OrganisationId'

alter table FieldVariable
add CompanyId bigint null

alter table FieldVariable
add Scope int null

alter table FieldVariable
add WaterMark varchar(200) null

alter table FieldVariable
add DefaultValue varchar(200) null

alter table FieldVariable
add InputMask varchar(100) null

alter table FieldVariable
add OrganisationId bigint null

alter table FieldVariable
add IsSystem bit null

alter table FieldVariable
add VariableTitle varchar(100) null

GO

GO

/****** Object:  Table [dbo].[CompanyContactVariable]    Script Date: 2/23/2015 11:36:42 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CompanyContactVariable](
	[ContactVariableId] [bigint] IDENTITY(1,1) NOT NULL,
	[ContactId] [bigint] NOT NULL,
	[VariableId] [bigint] NOT NULL,
	[Value] [varchar](200) NULL,
 CONSTRAINT [PK_CompanyContactVariable] PRIMARY KEY CLUSTERED 
(
	[ContactVariableId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CompanyContactVariable]  WITH CHECK ADD  CONSTRAINT [FK_CompanyContactVariable_CompanyContact] FOREIGN KEY([ContactId])
REFERENCES [dbo].[CompanyContact] ([ContactId])
GO

ALTER TABLE [dbo].[CompanyContactVariable] CHECK CONSTRAINT [FK_CompanyContactVariable_CompanyContact]
GO

ALTER TABLE [dbo].[CompanyContactVariable]  WITH CHECK ADD  CONSTRAINT [FK_CompanyContactVariable_FieldVariable] FOREIGN KEY([VariableId])
REFERENCES [dbo].[FieldVariable] ([VariableId])
GO

ALTER TABLE [dbo].[CompanyContactVariable] CHECK CONSTRAINT [FK_CompanyContactVariable_FieldVariable]
GO

GO

/****** Object:  Table [dbo].[VariableOption]    Script Date: 2/23/2015 11:53:35 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[VariableOption](
	[VariableOptionId] [bigint] IDENTITY(1,1) NOT NULL,
	[VariableId] [bigint] NULL,
	[Value] [varchar](200) NULL,
	[SortOrder] [int] NULL,
 CONSTRAINT [PK_VariableOption] PRIMARY KEY CLUSTERED 
(
	[VariableOptionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[VariableOption]  WITH CHECK ADD  CONSTRAINT [FK_VariableOption_FieldVariable] FOREIGN KEY([VariableId])
REFERENCES [dbo].[FieldVariable] ([VariableId])
GO

ALTER TABLE [dbo].[VariableOption] CHECK CONSTRAINT [FK_VariableOption_FieldVariable]
GO

GO

/****** Object:  Table [dbo].[SmartForm]    Script Date: 2/23/2015 12:05:49 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[SmartForm](
	[SmartFormId] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](200) NULL,
	[CompanyId] [bigint] NULL,
	[OrganisationId] [bigint] NULL,
 CONSTRAINT [PK_SmartForm] PRIMARY KEY CLUSTERED 
(
	[SmartFormId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[SmartForm]  WITH CHECK ADD  CONSTRAINT [FK_SmartForm_Company] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Company] ([CompanyId])
GO

ALTER TABLE [dbo].[SmartForm] CHECK CONSTRAINT [FK_SmartForm_Company]
GO

GO

/****** Object:  Table [dbo].[SmartFormDetail]    Script Date: 2/23/2015 12:06:29 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SmartFormDetail](
	[SmartFormDetailId] [bigint] IDENTITY(1,1) NOT NULL,
	[SmartFormId] [bigint] NOT NULL,
	[ObjectType] [int] NULL,
	[IsRequired] [bit] NULL,
	[SortOrder] [int] NULL,
 CONSTRAINT [PK_SmartFormDetail] PRIMARY KEY CLUSTERED 
(
	[SmartFormDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SmartFormDetail]  WITH CHECK ADD  CONSTRAINT [FK_SmartFormDetail_SmartForm] FOREIGN KEY([SmartFormId])
REFERENCES [dbo].[SmartForm] ([SmartFormId])
GO

ALTER TABLE [dbo].[SmartFormDetail] CHECK CONSTRAINT [FK_SmartFormDetail_SmartForm]
GO

GO

Alter table Invoice
alter column InvoiceStatus smallint null

Alter table Invoice
add foreign key (InvoiceStatus)
references Status(StatusId)

Alter table Invoice
add foreign key (ContactId)
references CompanyContact(ContactId)

GO

/* Execution Date: 24/02/2015 */

GO

alter table FieldVariable
alter column VariableSectionId bigint

alter table SmartFormDetail
add VariableId bigint null

alter table SmartFormDetail
add CaptionValue varchar(200) null

alter table items
drop DF__tbl_items__JobMa__6D6238AF

alter table Items
alter column JobManagerId uniqueidentifier null

alter table Items
alter column JobProgressedBy uniqueidentifier null

alter table Items
alter column JobCardPrintedBy uniqueidentifier null

alter table SmartForm
add Heading varchar(100) null

GO

/* Execution Date: 25/02/2015 */

GO

exec sp_rename 'dbo.CostCentreType.CompanyId', 'OrganisationId'

alter table CostCentreType
alter Column OrganisationId bigint null

alter table TemplateVariable
alter column VariableId bigint null

alter table TemplateVariable
alter column TemplateId bigint null

GO

/* Execution Date: 26/02/2015 */

GO

alter table CompanyContactVariable
alter column Value varchar(1000) null

alter table Items
add SmartFormId bigint null

GO

/* Execution Date: 27/02/2015 */

GO

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.NABTransaction
                (
                Id bigint NOT NULL IDENTITY (1, 1),
                EstimateId int NULL,
                Request nvarchar(MAX) NULL,
                Response nvarchar(MAX) NULL,
                [datetime] datetime null
                )  ON [PRIMARY]
                TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.NABTransaction ADD CONSTRAINT
                PK_NABTransaction PRIMARY KEY CLUSTERED 
                (
                Id
                ) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.NABTransaction SET (LOCK_ESCALATION = TABLE)
GO
COMMIT

GO

/* Execution Date: 28/02/2015 */

GO

sp_rename 'CompanyContactVariable', 'ScopeVariable'
GO

GO
sp_rename 'ScopeVariable.ContactId', 'Id'

GO

GO
sp_rename 'ScopeVariable.ContactVariableId', 'ScopeVariableId'

alter table ScopeVariable
add Scope int null

alter table ScopeVariable
DROP constraint FK_CompanyContactVariable_CompanyContact

GO

/* Execution Date: 02/03/2015 */

GO

alter table SmartFormDetail
add foreign key (VariableId)
references FieldVariable(VariableId)

GO
/* Execution Date: 04/03/2015 */

GO

alter table dbo.Items
add IsDigitalDownload bit null

alter table Items
add IsRealStateProduct bit null

alter table Items
add ProductDisplayOptions int null

GO

/* alter for saved designs */
ALTER VIEW [dbo].[vw_SaveDesign]
AS
 SELECT  item.ItemID, ItemAttach.ItemID AS AttachmentItemId,ItemAttach.FileName AS AttachmentFileName, 
 (case when est.StatusID = 6 or est.StatusID = 7 or est.StatusID = 10 or est.StatusID = 36 then 'MPC_Content/Attachments/' else ItemAttach.FolderPath  end) AS AttachmentFolderPath, 
item.EstimateID, item.ProductName, PCat.CategoryName AS ProductCategoryName, PCat.ProductCategoryID, PCat.ParentCategoryID, 
                      ISNULL(dbo.funGetMiniumProductValue(item.RefItemID), 0.0) AS MinPrice,  
                      item.IsEnabled, item.IsPublished,item.IsArchived, item.InvoiceID, contact.ContactID, contact.CompanyId, company.IsCustomer ,item.RefItemID, status_Check.StatusID, status_Check.StatusName,
                       item.IsOrderedItem, item.ItemCreationDateTime,item.TemplateID
FROM         dbo.items AS item Inner JOIN
				      dbo.ItemAttachment AS ItemAttach ON ItemAttach.ItemID = item.ItemID INNER JOIN
                      dbo.Template AS temp ON temp.ProductID = item.TemplateID INNER JOIN
                      dbo.Estimate AS est ON item.EstimateID = est.EstimateID INNER JOIN
                      dbo.Status AS status_Check ON item.Status = status_Check.StatusID INNER JOIN
                      dbo.CompanyContact AS contact ON contact.ContactID = est.ContactID INNER JOIN
                      dbo.Company As company ON contact.CompanyId = company.CompanyId
					  inner join dbo.ProductCategoryItem pc2 on item.ItemId = pc2.ItemId
					  inner join dbo.ProductCategory PCat on pc2.CategoryId = pcat.ProductCategoryId

GO

/* INSERT into cmsskinpagewidgets */


update CmsPage set PageName = 'SavedDesigns', Companyid = NULL where pageid = 4

insert into widgets values (80,'SavedDesigns','SavedDesigns')

insert into CmsSkinPageWidget values (4,45,6,0,2205,1)
insert into CmsSkinPageWidget values (4,27,6,0,2205,1)
insert into CmsSkinPageWidget values (4,41,6,0,2205,1)
insert into CmsSkinPageWidget values (4,50100,6,0,2205,1)
insert into CmsSkinPageWidget values (4,28,6,0,2205,1)
insert into CmsSkinPageWidget values (4,78,6,0,2205,1)

/* Execution Date: 05/03/2015 */

GO

exec sp_rename 'Company.isBrokerCanLaminate', 'isLaminate'
exec sp_rename 'Company.isBrokerCanRoundCorner', 'isRoundCorner'
exec sp_rename 'Company.isBrokerCanAcceptPaymentOnline', 'isAcceptPaymentOnline'
exec sp_rename 'Company.isBrokerOrderApprovalRequired', 'isOrderApprovalRequired'
exec sp_rename 'Company.isBrokerPaymentRequired', 'isPaymentRequired'
exec sp_rename 'Company.includeEmailBrokerArtworkOrderReport', 'includeEmailArtworkOrderReport'
exec sp_rename 'Company.includeEmailBrokerArtworkOrderXML', 'includeEmailArtworkOrderXML'
exec sp_rename 'Company.includeEmailBrokerArtworkOrderJobCard', 'includeEmailArtworkOrderJobCard'
exec sp_rename 'Company.makeEmailBrokerArtworkOrderProductionReady', 'makeEmailArtworkOrderProductionReady'

alter table Company
drop column isDisplaylBrokerBanners

alter table Company
drop column isDisplayBrokerSecondaryPages

GO


/* Execution Date: 06/03/2015 */

Insert into PaymentMethod (PaymentMethodId, MethodName, IsActive) values (6, 'NAB', 'True')


/* Execution Date: 09/03/2015 */

GO

alter table cmsskinpagewidget
drop constraint FK__CmsSkinPa__PageI__194E09EA

alter table cmsskinpagewidget
add constraint FK_CmsSkinPageWidget_CmsPage
Foreign Key (PageId) references
CmsPage(PageId) on delete cascade

alter table cmsskinpagewidgetparam
drop constraint FK_tbl_cmsSkinPageWidgetParams_tbl_cmsSkinPageWidgets

alter table cmsskinpagewidgetparam
add constraint FK_cmsSkinPageWidgetParam_cmsSkinPageWidget
Foreign Key (PageWidgetId) references
CmsSkinPageWidget(PageWidgetId) on delete cascade

GO

/* Execution Date: 10/03/2015 */

GO

alter table templateObject
add originalTextStyles nvarchar(MAX) null

alter table templateObject
add originalContentString  nvarchar(MAX) null

GO


/* Execution Date: 12/03/2015 */
Delete FROM [MPCLive].[dbo].[InkCoverageGroup] where SystemSiteId != 1

Delete FROM [MPCLive].[dbo].[InkCoverageGroup] where GroupName = 'Medium' OR GroupName = 'Very High' OR GroupName = 'Much High'
/* Execution Date: 12/02/2015 */

GO

exec sp_rename 'InkCoverageGroup.SystemSiteId', 'OrganisationId'

alter table SystemUser
add Email varchar(200) null

GO

/* Execution Date: 13/03/2015 */


GO

alter table company
add ActiveBannerSetId bigint null

GO


/* Execution Date: 16/03/2015 */

GO

ALTER VIEW [dbo].[GetItemsListView]
AS
SELECT        p.ItemId, p.ItemCode, p.isQtyRanged, p.EstimateId, p.ProductName, p.ProductCode, 
	(select 
STUFF((select ', ' + pc.CategoryName
from dbo.ProductCategoryItem pci 
join dbo.ProductCategory pc on pc.ProductCategoryId = pci.CategoryId
where pci.ItemId = p.ItemId
FOR XML PATH(''), TYPE
            ).value('.', 'NVARCHAR(MAX)') 
        ,1,2,'')) ProductCategoryName, (select 
STUFF((select ', ' + CAST(pc.ProductCategoryId AS NVARCHAR) 
from dbo.ProductCategoryItem pci 
join dbo.ProductCategory pc on pc.ProductCategoryId = pci.CategoryId
where pci.ItemId = p.ItemId
FOR XML PATH(''), TYPE
            ).value('.', 'NVARCHAR(MAX)') 
        ,1,2,'')) ProductCategoryIds,
		 
                         ISNULL(dbo.funGetMiniumProductValue(p.ItemId), 0.0) AS MinPrice, p.ImagePath, p.ThumbnailPath, p.IconPath, p.IsEnabled, p.IsSpecialItem, p.IsPopular, 
                         p.IsFeatured, p.IsPromotional, p.IsPublished, p.ProductType, p.ProductSpecification, p.CompleteSpecification, p.IsArchived, p.SortOrder,
						 p.OrganisationId, p.WebDescription, p.PriceDiscountPercentage, p.DefaultItemTax, p.isUploadImage, p.CompanyId, p.TemplateId,
						 p.printCropMarks, p.drawWaterMarkTxt, p.TemplateType
FROM            dbo.Items p

GO

/* Execution Date: 18/03/2015 */

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

----------------------------------------- Alter Store Procedure sp_cloneTemplate ----------------------
ALTER PROCEDURE [dbo].[sp_cloneTemplate] 
	-- Add the parameters for the stored procedure here
	@TemplateID bigint,
	@submittedBy bigint,
    @submittedByName nvarchar(100)
AS
BEGIN


      
	declare @NewTemplateID bigint
	declare @NewCode nvarchar(10)
	--DECLARE  @WaterMarkTxt as [dbo].[tbl_company_sites]
	
	set @NewCode = ''
	
	--INSERT INTO @WaterMarkTxt (CompanySiteName)
	--Select Top 1 CompanySiteName from tbl_company_sites

	
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	INSERT INTO [dbo].[Template]
           ([Code]
           ,[ProductName]
           ,[Description]
           ,[ProductCategoryId]
           ,[Thumbnail]
           ,[Image]
           ,[IsDisabled]

           ,[PDFTemplateWidth]
           ,[PDFTemplateHeight]

           ,[CuttingMargin]
           ,[MultiPageCount]
           ,[Orientation]
           ,[MatchingSetTheme]
           ,[BaseColorId]
           ,[SubmittedBy]
           ,[SubmittedByName]
           ,[SubmitDate]
           ,[Status]
           ,[ApprovedBy]
           ,[ApprovedByName]
           ,[UserRating]
           ,[UsedCount]
           ,[MPCRating]
           ,[RejectionReason]
           ,[ApprovalDate]
           ,[IsCorporateEditable]
           ,[MatchingSetId]
           ,[TempString],[TemplateType],[isSpotTemplate]
          )
     



   
	SELECT 
      @NewCode
      ,[ProductName] + ' Copy'
      ,[Description]
      ,[ProductCategoryID]
      
      ,[Thumbnail]
      ,[Image]
      ,[IsDisabled]

      ,[PDFTemplateWidth]
      ,[PDFTemplateHeight]


      ,[CuttingMargin]
      ,[MultiPageCount]
      ,[Orientation]
      ,[MatchingSetTheme]
      ,[BaseColorId]
      ,@submittedBy
      ,@submittedByName
      ,NULL
      ,1
      ,NULL
      ,NULL
      ,0
      ,0
      ,0
      ,''
      ,NULL,IsCorporateEditable,MatchingSetId,'',[TemplateType],isSpotTemplate
      
  FROM [dbo].[Template] where productid = @TemplateID
	
	
	set @NewTemplateID = SCOPE_IDENTITY() 
	
	-- updating water mark text 
	UPDATE [dbo].[Template]
	SET TempString= (Select Top 1 OrganisationName from organisation)
	WHERE productid = @NewTemplateID
	
	--copying the pages
	INSERT INTO [dbo].[TemplatePage]
           ([ProductId]
           ,[PageNo]
           ,[PageType]
           ,[Orientation]
           ,[BackGroundType]
           ,[BackgroundFileName]
      ,[ColorC]
      ,[ColorM]
      ,[ColorY]
      ,[ColorK]
      ,[IsPrintable]
           ,[PageName],[hasOverlayObjects])

SELECT 
      @NewTemplateID
      ,[PageNo]
      ,[PageType]
      ,[Orientation]
      ,[BackGroundType]
      ,[BackgroundFileName]
      ,[ColorC]
      ,[ColorM]
      ,[ColorY]
      ,[ColorK]
      ,[IsPrintable]
      
      ,[PageName],[hasOverlayObjects]
  FROM [dbo].[TemplatePage]
where productid = @TemplateID


	--copying the objects
	INSERT INTO [dbo].[TemplateObject]
           ([ObjectType]
           ,[Name]
           ,[IsEditable]
           ,[IsHidden]
           ,[IsMandatory]
           
           ,[PositionX]
           ,[PositionY]
           ,[MaxHeight]
           ,[MaxWidth]
           ,[MaxCharacters]
           ,[RotationAngle]
           ,[IsFontCustom]
           ,[IsFontNamePrivate]
           ,[FontName]
           ,[FontSize]
           ,[IsBold]
           ,[IsItalic]
           ,[Allignment]
           ,[VAllignment]
           ,[Indent]
           ,[IsUnderlinedText]
           ,[ColorType]
 
           ,[ColorName]
           ,[ColorC]
           ,[ColorM]
           ,[ColorY]
           ,[ColorK]
           ,[Tint]
           ,[IsSpotColor]
           ,[SpotColorName]
           ,[ContentString]
           ,[ContentCaseType]
           ,[ProductID]
           ,[DisplayOrderPdf]
           ,[DisplayOrderTxtControl]
           ,[RColor]
           ,[GColor]
           ,[BColor]
           ,[LineSpacing]
           ,[ProductPageId]
           ,[ParentId]
           ,CircleRadiusX
           ,Opacity
           ,[ExField1],
           [IsTextEditable],
           [IsPositionLocked],
           [CircleRadiusY]
          ,[ExField2],
           ColorHex
           ,[IsQuickText]
           ,[QuickTextOrder],
		   [watermarkText],
		   [textStyles],[charspacing],[AutoShrinkText],
		   [IsOverlayObject],[ClippedInfo],[originalContentString],[originalTextStyles]
           )
	SELECT 
      O.[ObjectType]
      ,O.[Name]
      ,O.[IsEditable]
      ,O.[IsHidden]
      ,O.[IsMandatory]
      
      ,O.[PositionX]
      ,O.[PositionY]
      ,O.[MaxHeight]
      ,O.[MaxWidth]
      ,O.[MaxCharacters]
      ,O.[RotationAngle]
      ,O.[IsFontCustom]
      ,O.[IsFontNamePrivate]
      ,O.[FontName]
      ,O.[FontSize]
      ,O.[IsBold]
      ,O.[IsItalic]
      ,O.[Allignment]
      ,O.[VAllignment]
      ,O.[Indent]
      ,O.[IsUnderlinedText]
      ,O.[ColorType]
      ,O.[ColorName]
      ,O.[ColorC]
      ,O.[ColorM]
      ,O.[ColorY]
      ,O.[ColorK]
      ,O.[Tint]
      ,O.[IsSpotColor]
      ,O.[SpotColorName]
      ,O.[ContentString]
      ,O.[ContentCaseType]
      ,@NewTemplateID
      ,O.[DisplayOrderPdf]
      ,O.[DisplayOrderTxtControl]
      ,O.[RColor]
      ,O.[GColor]
      ,O.[BColor]
      ,O.[LineSpacing]
      ,NP.[ProductPageId]
      ,O.[ParentId]
      ,O.CircleRadiusX
      ,O.Opacity
      ,O.[ExField1],
       O.[IsTextEditable],
       O.[IsPositionLocked],
       O.[CircleRadiusY]
      ,O.[ExField2],
       O.ColorHex
       ,[IsQuickText]
        ,[QuickTextOrder],[watermarkText],O.[textStyles],
		O.[charspacing],O.[AutoShrinkText],O.[IsOverlayObject]
		,O.[ClippedInfo]
		,O.[originalContentString],O.[originalTextStyles]
  FROM [dbo].[TemplateObject] O
  inner join [dbo].[TemplatePage]  P on o.ProductPageId = p.ProductPageId and o.ProductId = @TemplateID
  inner join [dbo].[TemplatePage] NP on P.PageName = NP.PageName and P.PageNo = NP.PageNo and NP.ProductId = @NewTemplateID
  
	--theme tags
	--insert into dbo.TemplateThemeTags   ([TagID],[ProductID])
	--select [TagID] ,@NewTemplateID from dbo.TemplateThemeTags where ProductID = @TemplateID
	
	---- industry tags
	--insert into dbo.TemplateIndustryTags   ([TagID],[ProductID])
	--select [TagID] ,@NewTemplateID from dbo.TemplateIndustryTags where ProductID = @TemplateID

	INSERT INTO [dbo].[TemplateBackgroundImage]
			   ([ProductId]
			   ,[ImageName]
			   ,[Name]
			   ,[flgPhotobook]
			   ,[flgCover]
			   ,[BackgroundImageAbsolutePath]
			   ,[BackgroundImageRelativePath],
			   ImageType,
			   ImageWidth,
			   ImageHeight
			   
			   )
	SELECT 
		  @NewTemplateID
		  ,[ImageName]
		  ,[Name]
		  ,[flgPhotobook]
		  ,[flgCover]
		  ,[BackgroundImageAbsolutePath]
		  ,[BackgroundImageRelativePath]
		  ,ImageType,
			   ImageWidth,
			   ImageHeight
	  FROM [dbo].[TemplateBackgroundImage] where ProductId = @TemplateID




select @NewTemplateID
	
END


GO

ALTER view [dbo].[GetCategoryProducts] as
SELECT        p.CompanyId,p.ItemId, p.ItemCode, p.isQtyRanged, p.EstimateId, p.ProductName, p.ProductCode, 
 (select 
STUFF((select ', ' + pc.CategoryName
from dbo.ProductCategoryItem pci 
join dbo.ProductCategory pc on pc.ProductCategoryId = pci.CategoryId
where pci.ItemId = p.ItemId
FOR XML PATH(''), TYPE
            ).value('.', 'NVARCHAR(MAX)') 

        ,1,2,'')) as ProductCategoryName,
   
                         ISNULL(dbo.funGetMiniumProductValue(p.ItemId), 0.0) AS MinPrice, p.ImagePath, p.ThumbnailPath, p.IconPath, p.IsEnabled, p.IsSpecialItem, p.IsPopular, 
                         p.IsFeatured, p.IsPromotional, p.IsPublished, p.ProductType, p.ProductSpecification, p.CompleteSpecification, p.IsArchived, p.SortOrder,
       p.OrganisationId, p.WebDescription, p.PriceDiscountPercentage,CAST ( p.isTemplateDesignMode as int) as isTemplateDesignMode, p.DefaultItemTax, p.isUploadImage,p.isMarketingBrief,pcat.ProductCategoryId,p.TemplateId,p.DesignerCategoryId
FROM            dbo.Items p
inner join dbo.ProductCategoryItem pc2 on p.ItemId = pc2.ItemId
inner join dbo.ProductCategory pcat on pc2.CategoryId = pcat.ProductCategoryId


GO


/* Execution Date: 19/03/2015 */

GO

delete from categoryterritory
where productcategoryid not in (select productcategoryid from ProductCategory)

alter table categoryterritory
add constraint FK_CategoryTerritory_ProductCategory
foreign key (ProductCategoryId)
references ProductCategory (ProductCategoryId)

alter table company
add CurrentThemeId bigint null

GO
--Executed on 24-03-2015
insert into DeliveryCarrier(CarrierName, URL, APiKey, APIPassword)
values('Fedex','fedex.com', null,null)
insert into DeliveryCarrier(CarrierName, URL, APiKey, APIPassword)
values('UPS','ups.com', null,null)
insert into DeliveryCarrier(CarrierName, URL, APiKey, APIPassword)
values('Other',null, null,null)
GO

/* Execution Date: 25/03/2015 */

GO

Create PROCEDURE [dbo].[usp_DeleteProduct]
	-- Add the parameters for the stored procedure here
	
	@itemid bigint = 0

AS
BEGIN
	
--BEGIN TRANSACTION;
--IF @@TRANCOUNT = 0
--BEGIN
declare @TemplateID bigint

select @TemplateID = templateid from items where itemid = @itemid
--57647
-- delete section cost centre details
delete  scd
				from dbo.SectionCostCentreDetail scd
				inner join dbo.SectionCostcentre sc on sc.SectionCostcentreID = scd.SectionCostCentreID
				inner join ItemSection iss on iss.ItemSectionID = sc.ItemSectionID
				INNER JOIN items i ON i.ItemID = iss.ItemID
					where i.ItemId = @itemid



	--deletinng the item section cost center resources
				delete  scr
				from dbo.SectionCostCentreResource scr
				inner join dbo.SectionCostcentre sc on sc.SectionCostcentreID = scr.SectionCostCentreID
				inner join ItemSection iss on iss.ItemSectionID = sc.ItemSectionID
				INNER JOIN items i ON i.ItemID = iss.ItemID
				where i.ItemId = @itemid


	--deletinng the item section cost center
				delete sc
				from dbo.SectionCostcentre sc
				inner join ItemSection iss on iss.ItemSectionID = sc.ItemSectionID
				INNER JOIN Items i ON i.ItemID = iss.ItemID
				where i.ItemId = @itemid

--deletinng the item section ink coverage
delete  sink
				from dbo.SectionInkCoverage sink
				inner join ItemSection iss on iss.ItemSectionID = sink.SectionID
				INNER JOIN Items i ON i.ItemID = iss.ItemID
				where i.ItemId = @itemid


	--deletinng the item section
				delete  isec
				from dbo.ItemSection isec
				INNER JOIN Items i ON i.ItemID = isec.ItemID
				where i.ItemId = @itemid


	--deletinng the item attachments
				delete ia
				from ItemAttachment ia
				INNER JOIN items i ON i.ItemID = ia.ItemID
				where i.ItemId = @itemid

    --deletinng the item addon Cost Centres xxxxxxxx
				delete  iacc
				from dbo.ItemAddonCostCentre iacc
				INNER JOIN ItemStockOption iss ON iss.ItemStockOptionId  = iacc.ItemStockOptionId
				INNER JOIN Items i ON i.ItemID = iss.ItemId
				where i.ItemId = @itemid

	--deletinng the item Stock Options
				delete isoo
				from dbo.ItemStockOption isoo
				INNER JOIN Items i ON i.ItemID = isoo.ItemID
				where i.ItemId = @itemid

   --deletinng the item Price Matrix
				delete ipms
				from dbo.ItemPriceMatrix ipms
				INNER JOIN Items i ON i.ItemID = ipms.ItemID
				where i.ItemId = @itemid

   	--deletinng the item related items
				delete  iri
				from dbo.ItemRelatedItem iri
				INNER JOIN Items i ON i.ItemID = iri.ItemID
				where i.ItemId = @itemid

	-- deleting item state tax
			    delete  ist
				from dbo.ItemStateTax ist
				INNER JOIN Items i ON i.ItemID = ist.ItemID
				where i.ItemId = @itemid

   -- deleting item VDP Price
			    delete  vdp
				from dbo.ItemVDPPrice vdp
				INNER JOIN Items i ON i.ItemID = vdp.ItemID
				where i.ItemId = @itemid

	  -- deleting item ProductDetail
			    delete  pd
				from dbo.ItemProductDetail pd
				INNER JOIN Items i ON i.ItemID = pd.ItemID
				where i.ItemId = @itemid
				 
    -- deleting item video Price
			    delete  vid
				from dbo.ItemVideo vid
				INNER JOIN Items i ON i.ItemID = vid.ItemID
				where i.ItemId = @itemid

   -- deleting item video Price
			    delete  img
				from dbo.ItemImage img
				INNER JOIN Items i ON i.ItemID = img.ItemID
				where i.ItemId = @itemid


 -- deleting ProductCategoryItem
			    delete PCI
				from dbo.ProductCategoryItem pci
				INNER JOIN Items i ON i.ItemID = pci.ItemID
				where i.ItemId = @itemid

	--deletinng the items
				delete  i
				from dbo.items i
				where i.ItemId = @itemid

 -- delete template objects

delete from TemplateObject where productid = @TemplateID

-- DELETE tob
--FROM TemplateObject tob
--inner join TemplatePage topg on topg.ProductPageId = tob.ProductPageID
--inner join Template t on t.ProductID = topg.productid
--INNER JOIN Items i ON i.TemplateID = t.ProductID
--where i.ItemId = @itemid


 -- delete template pages
 delete from TemplatePage where productid = @TemplateID
-- DELETE topg
--FROM TemplatePage topg
--				inner join Template t on t.ProductID = topg.productid
--				INNER JOIN Items i ON i.TemplateID = t.ProductID
--				where i.ItemId = @itemid
  -- delete image permisssions

 DELETE imgPer
				FROM ImagePermissions imgPer
				inner join TemplateBackgroundImage tbi on tbi.Id = imgPer.ImageID
				where tbi.ProductID = @TemplateID


 -- delete template background images
  delete from TemplateBackgroundImage where productid = @TemplateID
 --DELETE topg
	--			FROM TemplateBackgroundImage topg
	--			inner join Template t on t.ProductID = topg.productid
	--			INNER JOIN Items i ON i.TemplateID = t.ProductID
	--			where i.ItemId = @itemid


-- delete template 
DELETE from template where ProductId = @TemplateID

--END;
--ROLLBACK TRANSACTION;
end

--GO
/*
Rolled back the transaction.
*/



GO


truncate table CostCentreVariabletype
set  identity_insert CostCentreVariabletype ON
INSERT INTO CostCentreVariabletype ([CategoryID],[Name])VALUES(1,'Booklet Variables')
INSERT INTO CostCentreVariabletype ([CategoryID],[Name])VALUES(2,'Charge Variables')
INSERT INTO CostCentreVariabletype ([CategoryID],[Name])VALUES(3,'Colors')
INSERT INTO CostCentreVariabletype ([CategoryID],[Name])VALUES(4,'Guillotine')
INSERT INTO CostCentreVariabletype ([CategoryID],[Name])VALUES(5,'Imposition Variables')
INSERT INTO CostCentreVariabletype ([CategoryID],[Name])VALUES(6,'Item Quantities')
INSERT INTO CostCentreVariabletype ([CategoryID],[Name])VALUES(7,'Plate / Films')
INSERT INTO CostCentreVariabletype ([CategoryID],[Name])VALUES(8,'Press Makereadies')
INSERT INTO CostCentreVariabletype ([CategoryID],[Name])VALUES(9,'Press Variables')
INSERT INTO CostCentreVariabletype ([CategoryID],[Name])VALUES(10,'Section Variables')
INSERT INTO CostCentreVariabletype ([CategoryID],[Name])VALUES(11,'Stock')
INSERT INTO CostCentreVariabletype ([CategoryID],[Name])VALUES(12,'Weight')
INSERT INTO CostCentreVariabletype ([CategoryID],[Name])VALUES(13,'CostCentre Variables')
INSERT INTO CostCentreVariabletype ([CategoryID],[Name])VALUES(14,'Global Variables')
set  identity_insert CostCentreVariabletype OFF
Go


/* Execution Date: 26/03/2015 */


/****** Object:  StoredProcedure [dbo].[sp_cloneTemplate]    Script Date: 26/03/2015 04:08:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

----------------------------------------- Alter Store Procedure sp_cloneTemplate ----------------------
ALTER PROCEDURE [dbo].[sp_cloneTemplate] 
	-- Add the parameters for the stored procedure here
	@TemplateID bigint,
	@submittedBy bigint,
    @submittedByName nvarchar(100)
AS
BEGIN


      
	declare @NewTemplateID bigint
	declare @NewCode nvarchar(10)
	--DECLARE  @WaterMarkTxt as [dbo].[tbl_company_sites]
	
	set @NewCode = ''
	
	--INSERT INTO @WaterMarkTxt (CompanySiteName)
	--Select Top 1 CompanySiteName from tbl_company_sites

	
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	INSERT INTO [dbo].[Template]
           ([Code]
           ,[ProductName]
           ,[Description]
           ,[ProductCategoryId]
           ,[Thumbnail]
           ,[Image]
           ,[IsDisabled]

           ,[PDFTemplateWidth]
           ,[PDFTemplateHeight]

           ,[CuttingMargin]
           ,[MultiPageCount]
           ,[Orientation]
           ,[MatchingSetTheme]
           ,[BaseColorId]
           ,[SubmittedBy]
           ,[SubmittedByName]
           ,[SubmitDate]
           ,[Status]
           ,[ApprovedBy]
           ,[ApprovedByName]
           ,[UserRating]
           ,[UsedCount]
           ,[MPCRating]
           ,[RejectionReason]
           ,[ApprovalDate]
           ,[IsCorporateEditable]
           ,[MatchingSetId]
           ,[TempString],[TemplateType],[isSpotTemplate],[isWatermarkText],[isCreatedManual]
          )
     



   
	SELECT 
      @NewCode
      ,[ProductName] + ' Copy'
      ,[Description]
      ,[ProductCategoryID]
      
      ,[Thumbnail]
      ,[Image]
      ,[IsDisabled]

      ,[PDFTemplateWidth]
      ,[PDFTemplateHeight]


      ,[CuttingMargin]
      ,[MultiPageCount]
      ,[Orientation]
      ,[MatchingSetTheme]
      ,[BaseColorId]
      ,@submittedBy
      ,@submittedByName
      ,NULL
      ,1
      ,NULL
      ,NULL
      ,0
      ,0
      ,0
      ,''
      ,NULL,IsCorporateEditable,MatchingSetId,'',[TemplateType],isSpotTemplate,isWatermarkText,isCreatedManual
      
  FROM [dbo].[Template] where productid = @TemplateID
	
	
	set @NewTemplateID = SCOPE_IDENTITY() 
	
	-- updating water mark text 
	UPDATE [dbo].[Template]
	SET TempString= (Select Top 1 OrganisationName from organisation)
	WHERE productid = @NewTemplateID
	
	--copying the pages
	INSERT INTO [dbo].[TemplatePage]
           ([ProductId]
           ,[PageNo]
           ,[PageType]
           ,[Orientation]
           ,[BackGroundType]
           ,[BackgroundFileName]
      ,[ColorC]
      ,[ColorM]
      ,[ColorY]
      ,[ColorK]
      ,[IsPrintable]
           ,[PageName],[hasOverlayObjects])

SELECT 
      @NewTemplateID
      ,[PageNo]
      ,[PageType]
      ,[Orientation]
      ,[BackGroundType]
      ,[BackgroundFileName]
      ,[ColorC]
      ,[ColorM]
      ,[ColorY]
      ,[ColorK]
      ,[IsPrintable]
      
      ,[PageName],[hasOverlayObjects]
  FROM [dbo].[TemplatePage]
where productid = @TemplateID


	--copying the objects
	INSERT INTO [dbo].[TemplateObject]
           ([ObjectType]
           ,[Name]
           ,[IsEditable]
           ,[IsHidden]
           ,[IsMandatory]
           
           ,[PositionX]
           ,[PositionY]
           ,[MaxHeight]
           ,[MaxWidth]
           ,[MaxCharacters]
           ,[RotationAngle]
           ,[IsFontCustom]
           ,[IsFontNamePrivate]
           ,[FontName]
           ,[FontSize]
           ,[IsBold]
           ,[IsItalic]
           ,[Allignment]
           ,[VAllignment]
           ,[Indent]
           ,[IsUnderlinedText]
           ,[ColorType]
 
           ,[ColorName]
           ,[ColorC]
           ,[ColorM]
           ,[ColorY]
           ,[ColorK]
           ,[Tint]
           ,[IsSpotColor]
           ,[SpotColorName]
           ,[ContentString]
           ,[ContentCaseType]
           ,[ProductID]
           ,[DisplayOrderPdf]
           ,[DisplayOrderTxtControl]
           ,[RColor]
           ,[GColor]
           ,[BColor]
           ,[LineSpacing]
           ,[ProductPageId]
           ,[ParentId]
           ,CircleRadiusX
           ,Opacity
           ,[ExField1],
           [IsTextEditable],
           [IsPositionLocked],
           [CircleRadiusY]
          ,[ExField2],
           ColorHex
           ,[IsQuickText]
           ,[QuickTextOrder],
		   [watermarkText],
		   [textStyles],[charspacing],[AutoShrinkText],
		   [IsOverlayObject],[ClippedInfo],[originalContentString],[originalTextStyles]
           )
	SELECT 
      O.[ObjectType]
      ,O.[Name]
      ,O.[IsEditable]
      ,O.[IsHidden]
      ,O.[IsMandatory]
      
      ,O.[PositionX]
      ,O.[PositionY]
      ,O.[MaxHeight]
      ,O.[MaxWidth]
      ,O.[MaxCharacters]
      ,O.[RotationAngle]
      ,O.[IsFontCustom]
      ,O.[IsFontNamePrivate]
      ,O.[FontName]
      ,O.[FontSize]
      ,O.[IsBold]
      ,O.[IsItalic]
      ,O.[Allignment]
      ,O.[VAllignment]
      ,O.[Indent]
      ,O.[IsUnderlinedText]
      ,O.[ColorType]
      ,O.[ColorName]
      ,O.[ColorC]
      ,O.[ColorM]
      ,O.[ColorY]
      ,O.[ColorK]
      ,O.[Tint]
      ,O.[IsSpotColor]
      ,O.[SpotColorName]
      ,O.[ContentString]
      ,O.[ContentCaseType]
      ,@NewTemplateID
      ,O.[DisplayOrderPdf]
      ,O.[DisplayOrderTxtControl]
      ,O.[RColor]
      ,O.[GColor]
      ,O.[BColor]
      ,O.[LineSpacing]
      ,NP.[ProductPageId]
      ,O.[ParentId]
      ,O.CircleRadiusX
      ,O.Opacity
      ,O.[ExField1],
       O.[IsTextEditable],
       O.[IsPositionLocked],
       O.[CircleRadiusY]
      ,O.[ExField2],
       O.ColorHex
       ,[IsQuickText]
        ,[QuickTextOrder],[watermarkText],O.[textStyles],
		O.[charspacing],O.[AutoShrinkText],O.[IsOverlayObject]
		,O.[ClippedInfo]
		,O.[originalContentString],O.[originalTextStyles]
  FROM [dbo].[TemplateObject] O
  inner join [dbo].[TemplatePage]  P on o.ProductPageId = p.ProductPageId and o.ProductId = @TemplateID
  inner join [dbo].[TemplatePage] NP on P.PageName = NP.PageName and P.PageNo = NP.PageNo and NP.ProductId = @NewTemplateID
  
	--theme tags
	--insert into dbo.TemplateThemeTags   ([TagID],[ProductID])
	--select [TagID] ,@NewTemplateID from dbo.TemplateThemeTags where ProductID = @TemplateID
	
	---- industry tags
	--insert into dbo.TemplateIndustryTags   ([TagID],[ProductID])
	--select [TagID] ,@NewTemplateID from dbo.TemplateIndustryTags where ProductID = @TemplateID

	INSERT INTO [dbo].[TemplateBackgroundImage]
			   ([ProductId]
			   ,[ImageName]
			   ,[Name]
			   ,[flgPhotobook]
			   ,[flgCover]
			   ,[BackgroundImageAbsolutePath]
			   ,[BackgroundImageRelativePath],
			   ImageType,
			   ImageWidth,
			   ImageHeight
			   
			   )
	SELECT 
		  @NewTemplateID
		  ,[ImageName]
		  ,[Name]
		  ,[flgPhotobook]
		  ,[flgCover]
		  ,[BackgroundImageAbsolutePath]
		  ,[BackgroundImageRelativePath]
		  ,ImageType,
			   ImageWidth,
			   ImageHeight
	  FROM [dbo].[TemplateBackgroundImage] where ProductId = @TemplateID




select @NewTemplateID
	
END


/* Execution Date: 30/03/2015 */

GO

/****** Object:  StoredProcedure [dbo].[usp_DeleteOrganisation]    Script Date: 3/30/2015 1:49:31 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[usp_DeleteOrganisation]
	@OrganisationID int
AS
	BEGIN

--declare @OrganisationID bigint = 14

delete from PaperSize where OrganisationId = @OrganisationID


delete ccr from CostcentreResource ccr
inner join CostCentre cc on cc.CostCentreId = ccr.CostCentreId
where cc.OrganisationId = @OrganisationID

delete ccwi from CostcentreWorkInstructionsChoice ccwi
inner join CostcentreInstruction cci on cci.InstructionId = ccwi.InstructionId
inner join CostCentre cc on cc.CostCentreId = cci.CostCentreId
where cc.OrganisationId = @OrganisationID

delete cci from CostcentreInstruction cci
inner join CostCentre cc on cc.CostCentreId = cci.CostCentreId
where cc.OrganisationId = @OrganisationID

delete ccc from CostCenterChoice  ccc 
inner join CostCentre cc on cc.CostCentreId = ccc.CostCenterId
where cc.OrganisationId = @OrganisationID


delete cca from CostCentreAnswer  cca 
inner join CostCentreQuestion ccq on ccq.Id = cca.QuestionId
where ccq.CompanyId = @OrganisationID

delete  from CostCentreQuestion where CompanyId = @OrganisationID

delete ccmd from CostCentreMatrixDetail ccmd
inner join CostCentreMatrix ccm on ccm.MatrixId = ccmd.MatrixId
where ccm.OrganisationId = @OrganisationID

 
delete from CostCentreMatrix where OrganisationId = @OrganisationID

delete from CostCentre where OrganisationId = @OrganisationID

delete ssc from StockSubCategory ssc 
inner join StockCategory sc on sc.CategoryId = ssc.CategoryId
where sc.OrganisationId = @OrganisationID

delete from StockCategory where OrganisationId = @OrganisationID

delete from Report where  OrganisationId = @OrganisationID

delete from ReportNote where OrganisationId = @OrganisationID


delete from prefix where OrganisationId = @OrganisationID

delete from Machine where OrganisationId = @OrganisationID

 
 delete from LookupMethod where OrganisationId = @OrganisationID


 delete p from phrase p
 inner join PhraseField pf on pf.FieldId = p.FieldId
  where p.OrganisationId = @OrganisationID

 delete from PhraseField where OrganisationId = @OrganisationID


 delete from SectionFlag where OrganisationId = @OrganisationID

 delete SCP from StockCostAndPrice SCP 
 inner join StockItem SI on SI.StockItemId = SCP.ItemId
 WHERE SI.OrganisationId = @OrganisationID


delete from StockItem where OrganisationId = @OrganisationID 


declare @OC table 
( 
    id INT IDENTITY NOT NULL PRIMARY KEY,
    CompanyID bigint
)

-- delete all companies of an organisation

INSERT INTO @OC (CompanyID)
		select  CompanyID from Company
		where OrganisationID = @OrganisationID

declare @CompanyID bigint
declare @TotalCompanies int
select @TotalCompanies = COUNT(*) from @OC
 
declare @CurrentCompany int
set @CurrentCompany = 1

WHILE (@CurrentCompany <= @TotalCompanies)
BEGIN
			 select @CompanyID = CompanyID from @OC where ID = @CurrentCompany
			 Exec usp_DeleteContactCompanyByID  @CompanyID
			 set @CurrentCompany = @CurrentCompany + 1
END

delete from Organisation where OrganisationId = @OrganisationID

end

GO

/* Execution Date: 01/04/2015 */

/****** Object:  StoredProcedure [dbo].[usp_DeleteOrderByID]    Script Date: 4/1/2015 6:52:58 PM ******/

GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


Create PROCEDURE [dbo].[usp_DeleteOrderByID]	
		@OrderID bigint
AS
BEGIN

--declare @OrderID bigint = 36704

declare @TItems table 
( 
    id INT IDENTITY NOT NULL PRIMARY KEY,
    ItemID bigint
)

INSERT INTO @TItems (ItemID)
		select  ItemID from Items
		where EstimateId = @OrderID


 declare @Totalrec int
 declare @ItemID bigint
 select @Totalrec = COUNT(*) from @TItems
 
 declare @currentrec int

 set @currentrec = 1

		 WHILE (@currentrec <= @Totalrec)
		 BEGIN
		     select @ItemID = ItemID from @TItems
						 where ID = @currentrec

			exec usp_DeleteProduct @ItemID
		    set @currentrec = @currentrec + 1
		 End

		 delete 
				from PrePayment where orderid = @OrderID

delete from Estimate where EstimateId = @OrderID

end
	   

GO

GO
/****** Object:  StoredProcedure [dbo].[usp_DeleteCarts]    Script Date: 4/1/2015 6:55:48 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


Create PROCEDURE [dbo].[usp_DeleteCarts]	
		@CompanyID bigint
AS
BEGIN

--declare @OrderID bigint = 36704

declare @TOrders table 
( 
    id INT IDENTITY NOT NULL PRIMARY KEY,
    OrderID bigint
)

INSERT INTO @TOrders (OrderID)
		select  Estimateid from estimate
		where CompanyId = @CompanyID and StatusId = 3


 declare @Totalrec int
 declare @OrderID bigint
 select @Totalrec = COUNT(*) from @TOrders
 
 declare @currentrec int

 set @currentrec = 1

		 WHILE (@currentrec <= @Totalrec)
		 BEGIN
		     select @OrderID = OrderID from @TOrders
						 where ID = @currentrec

			exec usp_DeleteOrderByID @OrderID
		    set @currentrec = @currentrec + 1
		 End
end
	   

GO


/* Execution Date: 03/04/2015 */

GO
/****** Object:  StoredProcedure [dbo].[usp_DeleteContactCompanyByID]    Script Date: 4/3/2015 7:34:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Naveed
-- Create date: 18-7-2013
-- Description:	Delete a company
-- =============================================
ALTER PROCEDURE [dbo].[usp_DeleteContactCompanyByID]
	@CompanyID int
AS
	BEGIN
		--declare @CompanyID bigint = 32874
declare @IsCustomer int
declare @EstimateID bigint = 0
declare @itemID bigint = 0
declare @RetailCompanyID bigint = 0

delete from CompanyDomain where companyid = @CompanyID

delete from CmsOffer where companyid = @CompanyID

delete from MediaLibrary where companyid = @CompanyID


delete cb from CompanyBanner cb
inner join CompanyBannerSet cbs on cbs.CompanySetId = cb.CompanySetId
where cbs.CompanyId = @CompanyID

delete from CompanyBannerSet where companyid = @CompanyID

delete from DiscountVoucher where companyid = @CompanyID

delete from CmsPage where companyid = @CompanyID

delete from RaveReview where companyid = @CompanyID

delete from PaymentGateway where companyid = @CompanyID

delete from CmsSkinPageWidget where companyid = @CompanyID

delete from CompanyCostCentre where companyid = @CompanyID

delete from CompanyCMYKColor where companyid = @CompanyID

delete from Campaign where companyid = @CompanyID

delete sv from ScopeVariable sv
inner join FieldVariable fv on fv.VariableId = sv.VariableId
where fv.Companyid = @CompanyID

delete sfd from SmartFormDetail sfd  inner join
 smartform sf on sf.SmartFormId = sfd.SmartFormId
 where sf.companyid = @CompanyID


delete from SmartForm where companyid = @CompanyID



delete from FieldVariable where Companyid = @CompanyID
-- delete template fonts
DELETE tf
				FROM TemplateFont tf
				where tf.CustomerID = @CompanyID
delete nls
from NewsLetterSubscriber nls
inner join CompanyContact c on c.ContactID = nls.ContactID
where c.CompanyId = @CompanyID

--deletinng the tbl_Inquiry Attachments
				delete  IA
				from InquiryAttachment IA
				inner join Inquiry I on IA.InquiryID = I.InquiryID
				inner join Company CC on CC.CompanyId = I.ContactCompanyID
				where CC.CompanyId = @CompanyID

--deletinng the tbl_Inquiry_Items
				delete  II
				from InquiryItem II
				inner join dbo.Inquiry I on II.InquiryID = I.InquiryID
				inner join Company CC on CC.CompanyID = I.ContactCompanyId
				where CC.CompanyID = @CompanyID

--deletinng the Inquiries
				delete  I
				from dbo.Inquiry I
				inner join CompanyContact CC on CC.ContactId = I.ContactId
				where CC.CompanyId = @CompanyID

delete from CompanyContact where companyid = @CompanyID

delete from Address where companyid = @CompanyID

delete from CompanyTerritory where companyid = @CompanyID

delete pci from productcategoryitem pci
inner join productcategory pc on pc.ProductCategoryId = pci.CategoryId
where pc.Companyid = @CompanyID

delete pci from productcategoryitem pci
inner join Items i on i.ItemId = pci.itemid
where i.companyid = @CompanyID

delete from productcategory where Companyid = @CompanyID

-- delete companyitems

declare @TCI table 
( 
    id INT IDENTITY NOT NULL PRIMARY KEY,
    ItemID bigint
)

INSERT INTO @TCI (ItemID)
		select  ItemID from items
		where companyID = @CompanyID

-- delete companyItems in loop
 declare @TotalCompItems int
 select @TotalCompItems = COUNT(*) from @TCI
 
 declare @CurrItems int

 set @CurrItems = 1

		 WHILE (@CurrItems <= @TotalCompItems)
		 BEGIN
			 select @itemID = ItemID from @TCI where ID = @CurrItems
			 Exec usp_DeleteProduct  @ItemID
			 set @CurrItems = @CurrItems + 1
		 END

-- delete prepayments of order
	delete  pp
				from PrePayment pp
				inner join Estimate E on E.EstimateID = pp.OrderID
				inner join Company CC on CC.CompanyId = E.CompanyId
				where CC.CompanyID = @CompanyID


-- to delete ordered items and order
--declare @TVP table(OrderID bigint)
--declare @OP table(ItemID bigint)
--declare @temp table(CompanyID bigint)
declare @TVP table 
( 
    id INT IDENTITY NOT NULL PRIMARY KEY,
    OrderID bigint
)
declare @OP table 
( 
    id INT IDENTITY NOT NULL PRIMARY KEY,
    ItemID bigint
)
declare @temp table(
	id INT IDENTITY NOT NULL PRIMARY KEY,
	CompanyID bigint
)

select @IsCustomer = iscustomer from company where companyid = @CompanyID

-- if corporate store
if (@IsCustomer = 3)
begin
	INSERT INTO @TVP (OrderID)
		select  EstimateID from estimate
		where companyID = @CompanyID
	
	 declare @Totalrec int
 select @Totalrec = COUNT(*) from @TVP
 
 declare @currentrec int

 set @currentrec = 1

		 WHILE (@currentrec <=@Totalrec)
		 BEGIN
			 select @EstimateID = OrderID from @TVP
			 where ID = @currentrec

			 INSERT INTO @OP (ItemID)
			 select ItemID from Items where estimateid = @EstimateID

			-- loop for ordered items	
				 declare @TotalItems int
			 select @TotalItems = COUNT(*) from @OP
 
			 declare @currentItemRec int
			  set @currentItemRec = 1
			  WHILE (@currentItemRec <= @TotalItems)
				 BEGIN
				  select @ItemID = ItemID from @OP
						 where ID = @currentItemRec

						Exec usp_DeleteProduct  @ItemID

						set @currentItemRec = @currentItemRec + 1
				 end
				 	delete 
				from PrePayment where orderid = @EstimateID

			 delete from estimate where estimateid = @EstimateID
		 SET @currentrec = @currentrec + 1
		 end

		
end
else if(@IsCustomer = 4) -- if retail
begin
   
    INSERT INTO @TVP (OrderID)
		select EstimateId from estimate e inner join
		(select b.companyid from company a inner join company b on a.companyid = b.storeid where a.companyid = @CompanyID) customers on customers.companyid =  e.companyid

		insert into @temp (CompanyID) 
		select b.companyid from company a inner join company b on a.companyid = b.storeid where a.companyid = @CompanyID

		declare @TotalrecR int
		 select @TotalrecR = COUNT(*) from @TVP
 
		 declare @currentrecR int

		 set @currentrecR = 1
		 -- order loop
		WHILE (@currentrecR <=@TotalrecR)
		 BEGIN
			 select @EstimateID = OrderID from @TVP
			 where ID = @currentrecR

			  INSERT INTO @OP (ItemID)
			 select ItemID from Items where estimateid = @EstimateID

			-- loop for ordered items	
				 declare @TotalItemsR int
			 select @TotalItemsR = COUNT(*) from @OP
 
			 declare @currentItemRecR int
			 set @currentItemRecR = 1
			 -- loop to delete items
			  WHILE (@currentItemRecR <=@TotalItemsR)
				 BEGIN
				   select @ItemID = ItemID from @OP
						 where ID = @currentItemRecR
					Exec usp_DeleteProduct  @ItemID
					set @currentItemRecR = @currentItemRecR + 1
				 end

			 
			-- delete prepayments of order
			delete 
				from PrePayment where orderid = @EstimateID

			 delete from Estimate where EstimateId = @EstimateID
	
		 SET @currentrecR = @currentrecR + 1
		 end

		declare @TotalComp int
			 select @TotalComp = COUNT(*) from @temp
 
			 declare @currentComp int
			 set @currentComp = 1
			  WHILE (@currentComp <=@TotalComp)
				 BEGIN
				   select @RetailCompanyID = CompanyID from @temp where ID = @currentComp
				    -- delete company 
					
						delete from CompanyDomain where companyid = @RetailCompanyID

						delete from CmsOffer where companyid = @RetailCompanyID

						delete from MediaLibrary where companyid = @RetailCompanyID


						delete cb from CompanyBanner cb
						inner join CompanyBannerSet cbs on cbs.CompanySetId = cb.CompanySetId
						where cbs.CompanyId = @RetailCompanyID

						delete from CompanyBannerSet where companyid = @RetailCompanyID

						delete from DiscountVoucher where companyid = @RetailCompanyID

						delete from CmsPage where companyid = @RetailCompanyID

						delete from RaveReview where companyid = @RetailCompanyID

						delete from PaymentGateway where companyid = @RetailCompanyID

						delete from CmsSkinPageWidget where companyid = @RetailCompanyID

						delete from CompanyCostCentre where companyid = @RetailCompanyID

						delete from CompanyCMYKColor where companyid = @RetailCompanyID

						delete from Campaign where companyid = @RetailCompanyID


						delete sfd from SmartFormDetail sfd  inner join
						 smartform sf on sf.SmartFormId = sfd.SmartFormId

						delete from SmartForm where companyid = @RetailCompanyID

						delete from FieldVariable where Companyid = @CompanyID
						-- delete template fonts
						DELETE tf
										FROM TemplateFont tf
										where tf.CustomerID = @RetailCompanyID
						delete nls
						from NewsLetterSubscriber nls
						inner join CompanyContact c on c.ContactID = nls.ContactID
						where c.CompanyId = @RetailCompanyID

						--deletinng the tbl_Inquiry Attachments
										delete  IA
										from InquiryAttachment IA
										inner join Inquiry I on IA.InquiryID = I.InquiryID
										inner join Company CC on CC.CompanyId = I.ContactCompanyID
										where CC.CompanyId = @RetailCompanyID

						--deletinng the tbl_Inquiry_Items
										delete  II
										from InquiryItem II
										inner join dbo.Inquiry I on II.InquiryID = I.InquiryID
										inner join Company CC on CC.CompanyID = I.ContactCompanyId
										where CC.CompanyID = @RetailCompanyID

						--deletinng the Inquiries
										delete  I
										from dbo.Inquiry I
										inner join CompanyContact CC on CC.ContactId = I.ContactId
										where CC.CompanyId = @RetailCompanyID

						delete from CompanyContact where companyid = @RetailCompanyID

						delete from Address where companyid = @RetailCompanyID

						delete from CompanyTerritory where companyid = @RetailCompanyID

						delete pci from productcategoryitem pci
						inner join productcategory pc on pc.ProductCategoryId = pci.CategoryId
						where pc.Companyid = @RetailCompanyID

						delete pci from productcategoryitem pci
						inner join Items i on i.ItemId = pci.itemid
						where i.companyid = @RetailCompanyID

						delete from productcategory where Companyid = @RetailCompanyID

					delete from Company where CompanyId = @RetailCompanyID
					set @currentComp = @currentComp + 1
				 end


end

delete from company where companyid = @CompanyID

	END


/* Execution Date: 14/04/2015 */

GO

alter table SectionInkCoverage
alter column SectionId bigint null

alter table SectionInkCoverage
add constraint FK_SectionInkCoverage_ItemSection
foreign key (SectionId)
references ItemSection (ItemSectionId)

alter table Estimate
add constraint FK_Estimate_SectionFlag
foreign key (SectionFlagId)
references SectionFlag (SectionFlagId)

GO

/* Execution Date: 16/04/2015 */

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[vw_SaveDesign]
AS
 SELECT  item.ItemID, ItemAttach.ItemID AS AttachmentItemId,ItemAttach.FileName AS AttachmentFileName, 
 (case when est.StatusID = 6 or est.StatusID = 7 or est.StatusID = 10 or est.StatusID = 36 then 'MPC_Content/Attachments/' else ItemAttach.FolderPath  end) AS AttachmentFolderPath, 
item.EstimateID, item.ProductName, --PCat.CategoryName AS ProductCategoryName, PCat.ProductCategoryID, PCat.ParentCategoryID, 
                      ISNULL(dbo.funGetMiniumProductValue(item.RefItemID), 0.0) AS MinPrice,  
                      item.IsEnabled, item.IsPublished,item.IsArchived, item.InvoiceID, contact.ContactID, contact.CompanyId, company.IsCustomer ,item.RefItemID, status_Check.StatusID, status_Check.StatusName,
                       item.IsOrderedItem, item.ItemCreationDateTime,item.TemplateID
FROM         Items AS item Inner JOIN
				      dbo.ItemAttachment AS ItemAttach ON ItemAttach.ItemID = item.ItemID INNER JOIN
                      dbo.Template AS temp ON temp.ProductID = item.TemplateID INNER JOIN
                      dbo.Estimate AS est ON item.EstimateID = est.EstimateID INNER JOIN
                      dbo.Status AS status_Check ON item.Status = status_Check.StatusID INNER JOIN
                      dbo.CompanyContact AS contact ON contact.ContactID = est.ContactID INNER JOIN
                      dbo.Company As company ON contact.CompanyId = company.CompanyId
					 -- inner join dbo.ProductCategoryItem pc2 on item.ItemId = pc2.ItemId
					--  inner join dbo.ProductCategory PCat on pc2.CategoryId = pcat.ProductCategoryId



GO

----------------------

ALTER TABLE estimate
ALTER COLUMN OrderReportSignedBy uniqueidentifier

------------------
/****** Object:  StoredProcedure [dbo].[usp_JobCardReport]    Script Date: 4/17/2015 4:31:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create procedure [dbo].[usp_JobCardReport]
 @organisationId bigint,
 @OrderID bigint,
 @ItemID bigint = 0
AS
Begin

SELECT     dbo.Estimate.EstimateID, dbo.Items.ItemID, dbo.Estimate.UserNotes, dbo.Items.ItemCode, dbo.CompanyContact.FirstName, dbo.Items.ProductCode,
					
					 dbo.Items.JobDescription1 as JobDescription1,dbo.Items.JobDescription2 as JobDescription2,dbo.Items.JobDescription3 as JobDescription3,dbo.Items.JobDescription4 as JobDescription4,dbo.Items.JobDescription5 as JobDescription5,
					 dbo.Items.JobDescription6 as JobDescription6, dbo.Items.JobDescription7 as JobDescription7,
					 
					  CASE 
						  WHEN dbo.Items.JobDescription1 is null then null
						  WHEN dbo.Items.JobDescription1 is not null THEN  dbo.Items.JobDescriptionTitle1
				       END As JobDescriptionTitle1,
				        CASE 
						  WHEN dbo.Items.JobDescription2 is null THEN Null
						  WHEN dbo.Items.JobDescription2 is not null THEN  dbo.Items.JobDescriptionTitle2
				       END As JobDescriptionTitle2,
				        CASE 
						  WHEN dbo.Items.JobDescription3 is null THEN Null
						  WHEN dbo.Items.JobDescription3 is not null THEN  dbo.Items.JobDescriptionTitle3
				       END As JobDescriptionTitle3,
				        CASE 
						  WHEN dbo.Items.JobDescription4 is null THEN Null
						  WHEN dbo.Items.JobDescription4 is not null THEN  dbo.Items.JobDescriptionTitle4
				       END As JobDescriptionTitle4,
				        CASE 
						  WHEN dbo.Items.JobDescription5 is null THEN Null
						  WHEN dbo.Items.JobDescription5 is not null THEN  dbo.Items.JobDescriptionTitle5
				       END As JobDescriptionTitle5,
				        CASE 
						  WHEN dbo.Items.JobDescription6 is null THEN Null
						  WHEN dbo.Items.JobDescription6 is not null THEN  dbo.Items.JobDescriptionTitle6
				       END As JobDescriptionTitle6,
				        CASE 
						  WHEN dbo.Items.JobDescription7 is null THEN Null
						  WHEN dbo.Items.JobDescription7 is not null THEN  dbo.Items.JobDescriptionTitle7
				       END As JobDescriptionTitle7,
					 
					 isnull(dbo.CompanyContact.FirstName,'') + ' ' + isnull(dbo.CompanyContact.LastName,'') As ContactFullName,
                      dbo.CompanyContact.MiddleName, dbo.CompanyContact.LastName, dbo.CompanyContact.Mobile, dbo.Company.Name AS CompanyName, dbo.Address.Address1, dbo.Address.AddressName,
                      dbo.Address.Address2, dbo.Address.Address3, dbo.Address.City, dbo.Address.StateId, dbo.Address.CountryId, 
                      dbo.Address.PostCode, dbo.Address.Fax, dbo.Address.Email, dbo.Address.URL, dbo.Address.Tel1, dbo.Items.Qty1, 
                      dbo.Items.ProductName, dbo.Items.WebDescription, dbo.Items.JobDescription, dbo.itemsection.SectionName, dbo.itemsection.SectionNo, 
                      BAddress.Address1 AS BAddress1, BAddress.Address2 AS BAddress2, BAddress.City AS BCity, BAddress.StateId AS BState, BAddress.Email AS BEmail, 
                      dbo.Estimate.FinishDeliveryDate, dbo.Estimate.CreationDate, dbo.Estimate.StartDeliveryDate, dbo.Estimate.CustomerPO, BAddress.AddressName AS BAddressName, 
                      dbo.systemuser.FullName, dbo.Estimate.Order_Date, dbo.sectioncostcentre.Qty1WorkInstructions,
                      dbo.CostCentre.Name AS CostCenterName,dbo.Items.ItemNotes, dbo.Items.Qty1NetTotal, dbo.Items.Qty1Tax1Value,(dbo.Items.Qty1NetTotal + dbo.Items.Qty1Tax1Value) as GrossTotal,
                      dbo.Items.ProductName as FullProductName,
                       (select ReportBanner from reportnote where ReportCategoryID=9 and Organisationid = @OrganisationId) as BannerPath,dbo.Items.EstimateDescription,

                       (select ISNULL(BannerAbsolutePath,'') + isnull(ReportBanner,'')  from reportnote where ReportCategoryID=9 and OrganisationId = @organisationId) as ReportBanner,

                       dbo.fn_GetOrderItemsList(dbo.Estimate.EstimateID, dbo.Items.ItemID) As OtherItems, BAddress.PostCode as BPostCode, BAddress.CountryId as BCountry
                       ,dbo.fn_GetItemAttachmentsList(dbo.Items.ItemID,0) As AttachmentsList ,
                       isnull((select machinename from machine where machineid = dbo.itemsection.pressid and OrganisationId = @organisationId),'N/A')as PressName,
                       isnull((select top 1 StockLabel from ItemstockOption where StockID = itemsection.StockItemID1 and  itemsection.SectionNo = 1  and itemID = dbo.Items.refItemID and Items.Organisationid = @OrganisationId),'N/A')as StockName,
                       case
                       when Estimate.isDirectSale = 1 then 'Direct Order'
                       when Estimate.isDirectSale = 0 then 'Web Order'
                       end as DirectOrderLabel,
                       isnull(Items.JobCode, Items.ItemCode) As JobCode, 
                        case when p.paymentmethodid = 1 then 'Paid into Paypal ref:'
							 when p.paymentmethodid = 2 or p.paymentmethodid is null then 'Payment on Account:'
							 End as PaymentType
                      , case when p.paymentmethodid = 1 then (select top 1 transactionid from PayPalResponse where orderid = Estimate.estimateid)
							 when p.paymentmethodid = 2 then p.ReferenceCode
							 End as PaymentRefNo,
							 isnull((select top 1 Productname from Items where estimateid = Estimate.estimateid and itemtype = 2),'N/A') as DeliveryMethod,
							 
                       (select c.CurrencySymbol from Currency c inner join Organisation o on o.CurrencyId = c.CurrencyId where o.OrganisationId = @organisationId)as CurrencySymbol,
					   
						Company.TaxLabel + ':' As StateTaxLabel 

FROM         dbo.Estimate inner JOIN

                      dbo.Items ON dbo.Estimate.EstimateID = dbo.Items.EstimateID inner JOIN
                      dbo.itemsection ON dbo.Items.ItemID = dbo.itemsection.ItemID Inner JOIN
                      dbo.Company ON dbo.Estimate.CompanyId = dbo.Company.CompanyId Inner JOIN
                      dbo.CompanyContact ON dbo.Estimate.ContactID = dbo.CompanyContact.ContactID Inner JOIN
                      dbo.Address ON dbo.Estimate.AddressID = dbo.Address.AddressID left JOIN
                      dbo.Address AS BAddress ON dbo.Estimate.BillingAddressID = BAddress.AddressID Inner JOIN
                      dbo.systemuser ON dbo.Estimate.SalesPersonID = dbo.systemuser.SystemUserID 
                      Left JOIN dbo.sectioncostcentre ON dbo.itemsection.ItemSectionID = dbo.sectioncostcentre.ItemSectionID 
                      left join prepayment p on dbo.Estimate.estimateid = p.orderid
                      Left JOIN dbo.CostCentre ON dbo.sectioncostcentre.CostCentreID = dbo.CostCentre.CostCentreID

					  where estimate.organisationid = @organisationId and estimate.EstimateId = @OrderID and Items.itemid = @ItemId
					--  where 1 = case when @ItemID = 0 then 


End







----------------------------------------------

/****** Object:  StoredProcedure [dbo].[usp_OrderReport]    Script Date: 4/17/2015 3:32:48 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



Create procedure [dbo].[usp_OrderReport]
 @organisationId bigint,
 @OrderID bigint
AS
Begin
SELECT   dbo.Items.ItemID, dbo.Items.Title, isnull(dbo.Items.Qty1,0) As Qty1, dbo.Items.Qty2, dbo.Items.Qty3, dbo.Items.Qty1NetTotal, dbo.Items.Qty2NetTotal, dbo.Items.Qty3NetTotal,dbo.Items.ProductCode, 
                      
					 dbo.Items.JobDescription1 as JobDescription1,dbo.Items.JobDescription2 as JobDescription2,dbo.Items.JobDescription3 as JobDescription3,dbo.Items.JobDescription4 as JobDescription4,dbo.Items.JobDescription5 as JobDescription5,
					 dbo.Items.JobDescription6 as JobDescription6, dbo.Items.JobDescription7 as JobDescription7,
					 
					  CASE 
						  WHEN dbo.Items.JobDescription1 is null then null
						  WHEN dbo.Items.JobDescription1 is not null THEN  dbo.Items.JobDescriptionTitle1
				       END As JobDescriptionTitle1,
				        CASE 
						  WHEN dbo.Items.JobDescription2 is null THEN Null
						  WHEN dbo.Items.JobDescription2 is not null THEN  dbo.Items.JobDescriptionTitle2
				       END As JobDescriptionTitle2,
				        CASE 
						  WHEN dbo.Items.JobDescription3 is null THEN Null
						  WHEN dbo.Items.JobDescription3 is not null THEN  dbo.Items.JobDescriptionTitle3
				       END As JobDescriptionTitle3,
				        CASE 
						  WHEN dbo.Items.JobDescription4 is null THEN Null
						  WHEN dbo.Items.JobDescription4 is not null THEN  dbo.Items.JobDescriptionTitle4
				       END As JobDescriptionTitle4,
				        CASE 
						  WHEN dbo.Items.JobDescription5 is null THEN Null
						  WHEN dbo.Items.JobDescription5 is not null THEN  dbo.Items.JobDescriptionTitle5
				       END As JobDescriptionTitle5,
				        CASE 
						  WHEN dbo.Items.JobDescription6 is null THEN Null
						  WHEN dbo.Items.JobDescription6 is not null THEN  dbo.Items.JobDescriptionTitle6
				       END As JobDescriptionTitle6,
				        CASE 
						  WHEN dbo.Items.JobDescription7 is null THEN Null
						  WHEN dbo.Items.JobDescription7 is not null THEN  dbo.Items.JobDescriptionTitle7
				       END As JobDescriptionTitle7,
                      dbo.Items.JobDescription, dbo.Estimate.Estimate_Name, dbo.Estimate.Order_Code, dbo.Estimate.Estimate_Total, dbo.Estimate.FootNotes, 
                      dbo.Estimate.HeadNotes, dbo.Estimate.Order_Date, dbo.Estimate.Greeting, dbo.Estimate.CustomerPO, dbo.address.AddressName, 
                      dbo.address.Address1, dbo.address.Address2, dbo.address.Address3, dbo.address.Email, dbo.address.Fax, dbo.address.Stateid, 
                      dbo.address.City, dbo.address.URL, dbo.address.Tel1, dbo.Company.AccountNumber, dbo.address.PostCode, dbo.address.Countryid,
                      dbo.Company.Name AS CustomerName, dbo.Company.URL AS CustomerURL, dbo.Estimate.EstimateID, dbo.items.ProductName, 
                      dbo.CompanyContact.FirstName + ISNULL(' ' + dbo.CompanyContact.MiddleName, '') + ISNULL(' ' + dbo.CompanyContact.LastName, '') AS ContactName, 
                      --dbo.SystemUser.FullName, (select ReportBanner from reportnote where ReportCategoryID=12) as BannerPath ,
                      (select top 1 ReportTitle from reportnote where ReportCategoryID=12 and organisationid = @organisationId) as ReportTitle,
                      (select top 1 ISNULL(BannerAbsolutePath,'') + isnull(ReportBanner,'')  from reportnote where ReportCategoryID=12 and organisationid = @organisationId) as ReportBanner,
                      isnull(dbo.Estimate.Greeting, 'Dear '+ dbo.CompanyContact.FirstName + ' ' + isnull(dbo.CompanyContact.LastName,'')) as Greetings,
                     -- isnull((select top 1 CategoryName from tbl_productCategory where ProductCategoryID = tbl_items.ProductCategoryID),'')+ ' ' + dbo.tbl_items.ProductName as FullProductName
                      (select top 1 categoryname from productcategory p inner join dbo.ProductCategoryItem pc2 on p.productcategoryid = pc2.categoryid
					   inner join dbo.items pcat on pc2.ItemId = pcat.ItemId and p.OrganisationId = @organisationId) + ' ' + dbo.items.ProductName as FullProductName
					  
					  ,isnull((select top 1 itemName from StockItem where stockitemid = dbo.itemsection.stockitemid1 and StockItem.OrganisationId = @organisationId),'N/A')as StockName
                      ,dbo.fn_GetItemAttachmentsList(dbo.Estimate.EstimateID, 1) As AttachmentsList 
                      ,p.PaymentDate
                      , case when p.paymentmethodid = 1 then 'Paypal'
							 when p.paymentmethodid = 2 then 'On Account'
							 when p.paymentmethodid = 3 then 'ANZ'
							 else 'On Account'
							 End as paymentType
                      , case when p.paymentmethodid = 1 then (select top 1 transactionid from paypalresponse where orderid = estimate.estimateid)
							 when p.paymentmethodid = 2 or p.paymentmethodid = 3 then p.ReferenceCode
							 else 'N/A'
							 End as paymentRefNo
					 , (dbo.Company.TaxRate) As TaxLabel,BAddress.AddressName AS BAddressName,BAddress.PostCode as BPostCode, BAddress.Countryid as BCountry
					 ,BAddress.Address1 AS BAddress1, BAddress.Address2 AS BAddress2, BAddress.City AS BCity, BAddress.Stateid AS BState
					, items.Qty1Tax1Value,
					(select top 1 currencysymbol from currency c inner join organisation o on o.CurrencyId = c.CurrencyId and o.OrganisationId = @OrganisationID) as CurrencySymbol,
					case when estimate.Estimate_Code is not null then 'Estimate Code:'
					     when estimate.Estimate_Code is null then ''
					     end as EstimateCodeLabel
					     , dbo.estimate.Estimate_Code, dbo.estimate.UserNotes

FROM         dbo.company INNER JOIN
                      dbo.estimate ON dbo.company.companyid = dbo.company.companyid INNER JOIN
                      dbo.companycontact ON dbo.companycontact.ContactID = dbo.estimate.ContactID INNER JOIN
                      dbo.items ON dbo.estimate.EstimateID = dbo.items.EstimateID left outer JOIN
                     dbo.systemuser ON dbo.estimate.OrderReportSignedBy = dbo.systemuser.SystemUserID 
					 INNER JOIN
                      dbo.address ON dbo.estimate.BillingAddressID = dbo.address.AddressID 
                      inner JOIN dbo.itemsection ON dbo.items.ItemID = dbo.itemsection.ItemID
					  left join prepayment p on dbo.estimate.estimateid = p.orderid
					  left JOIN  dbo.address AS BAddress ON dbo.estimate.addressID = BAddress.AddressID
					
					  where company.organisationid = @organisationId and estimate.EstimateId = @OrderID
					--  where 1 = case when @ItemID = 0 then 


End


GO


----------------------------------------------------------

/****** Object:  UserDefinedFunction [dbo].[fn_GetOrderItemsList]    Script Date: 4/16/2015 7:15:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER FUNCTION [dbo].[fn_GetOrderItemsList]

(

	@OrderID int,
	@itemID int
	

)

RETURNS varchar(1000)

AS

BEGIN

	-- Declare the return variable here

	DECLARE @otherItems  varchar(1000)

		set @otherItems = ''

		select @otherItems = @otherItems + convert(varchar(100), ROW_NUMBER() 

				OVER (ORDER BY ItemID)) + ': ' + ProductName + ' ' + isnull(ItemCode,'') + ' '  + s.StatusName  + CHAR(13)

		from items i

			inner join [status] s on i.Status = s.StatusID

		where estimateID = @OrderID and itemid <> @itemID

	

	RETURN @otherItems



END

GO
------------------------------------------------------------------------

/****** Object:  UserDefinedFunction [dbo].[fn_GetItemAttachmentsList]    Script Date: 4/16/2015 7:18:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER FUNCTION [dbo].[fn_GetItemAttachmentsList]

(
	@RecordID int,
	@isOrder bit
)

RETURNS varchar(1000)

AS

BEGIN

	-- Declare the return variable here

	DECLARE @otherAttachments  varchar(1000)

		set @otherAttachments = ''
	if(@isOrder = 1)-- Attchments of an order
		Begin
			select @otherAttachments = @otherAttachments + convert(varchar(100), ROW_NUMBER() 

				OVER (ORDER BY ItemAttachmentId)) + ': ' + isnull(convert(varchar(255), [FileName]),'') + ' ' + CHAR(13)

		from itemattachment i
		where itemid in(select itemid from items where estimateid = @RecordID)
		
		End
	Else -- Attachments List of an Item
		Begin
		select @otherAttachments = @otherAttachments + convert(varchar(100), ROW_NUMBER() 

				OVER (ORDER BY ItemAttachmentId)) + ': ' + isnull(convert(varchar(255), [FileName]),'') + ' ' + CHAR(13)

		from itemattachment i

		--print @otherAttachments
		where itemid = @RecordID
		End

		

	

	RETURN @otherAttachments



END

GO
--------------------------------------------------------------------------------------









SELECT     dbo.estimate.EstimateId, dbo.items.ItemId, dbo.estimate.UserNotes, dbo.Items.ItemCode, dbo.CompanyContact.FirstName, dbo.Items.ProductCode,
					
					 dbo.Items.JobDescription1 as JobDescription1,dbo.Items.JobDescription2 as JobDescription2,dbo.Items.JobDescription3 as JobDescription3,dbo.Items.JobDescription4 as JobDescription4,dbo.Items.JobDescription5 as JobDescription5,
					 dbo.Items.JobDescription6 as JobDescription6, dbo.Items.JobDescription7 as JobDescription7,
					 
					  CASE 
						  WHEN dbo.Items.JobDescription1 is null then null
						  WHEN dbo.Items.JobDescription1 is not null THEN  dbo.Items.JobDescriptionTitle1
				       END As JobDescriptionTitle1,
				        CASE 
						  WHEN dbo.Items.JobDescription2 is null THEN Null
						  WHEN dbo.Items.JobDescription2 is not null THEN  dbo.Items.JobDescriptionTitle2
				       END As JobDescriptionTitle2,
				        CASE 
						  WHEN dbo.Items.JobDescription3 is null THEN Null
						  WHEN dbo.Items.JobDescription3 is not null THEN  dbo.Items.JobDescriptionTitle3
				       END As JobDescriptionTitle3,
				        CASE 
						  WHEN dbo.Items.JobDescription4 is null THEN Null
						  WHEN dbo.Items.JobDescription4 is not null THEN  dbo.Items.JobDescriptionTitle4
				       END As JobDescriptionTitle4,
				        CASE 
						  WHEN dbo.Items.JobDescription5 is null THEN Null
						  WHEN dbo.Items.JobDescription5 is not null THEN  dbo.Items.JobDescriptionTitle5
				       END As JobDescriptionTitle5,
				        CASE 
						  WHEN dbo.Items.JobDescription6 is null THEN Null
						  WHEN dbo.Items.JobDescription6 is not null THEN  dbo.Items.JobDescriptionTitle6
				       END As JobDescriptionTitle6,
				        CASE 
						  WHEN dbo.Items.JobDescription7 is null THEN Null
						  WHEN dbo.Items.JobDescription7 is not null THEN  dbo.Items.JobDescriptionTitle7
				       END As JobDescriptionTitle7,
					 
					 isnull(dbo.companycontact.FirstName,'') + ' ' + isnull(dbo.companycontact.LastName,'') As ContactFullName,
                      dbo.companycontact.MiddleName, dbo.companycontact.LastName, dbo.companycontact.Mobile, dbo.Company.Name AS CompanyName, dbo.Address.Address1, dbo.Address.AddressName,
                      dbo.Address.Address2, dbo.Address.Address3, dbo.Address.City, dbo.Address.StateId, dbo.Address.CountryId, 
                      dbo.Address.PostCode, dbo.Address.Fax, dbo.Address.Email, dbo.Address.URL, dbo.Address.Tel1, dbo.Items.Qty1, 
                      dbo.Items.ProductName, dbo.Items.WebDescription, dbo.Items.JobDescription, dbo.ItemSection.SectionName, dbo.ItemSection.SectionNo, 
                      BAddress.Address1 AS BAddress1, BAddress.Address2 AS BAddress2, BAddress.City AS BCity, BAddress.StateId AS BState, BAddress.Email AS BEmail, 
                      dbo.Estimate.FinishDeliveryDate, dbo.Estimate.CreationDate, dbo.Estimate.StartDeliveryDate, dbo.Estimate.CustomerPO, BAddress.AddressName AS BAddressName, 
                      dbo.SystemUser.FullName, dbo.Estimate.Order_Date, dbo.SectionCostcentre.Qty1WorkInstructions,
                      dbo.CostCentre.Name AS CostCenterName,dbo.Items.ItemNotes, dbo.Items.Qty1NetTotal, dbo.Items.Qty1Tax1Value,(dbo.Items.Qty1NetTotal + dbo.Items.Qty1Tax1Value) as GrossTotal,
                      --(select top 1 CategoryName from tbl_productCategory where ProductCategoryID = tbl_items.ProductCategoryID)+ ' ' + dbo.items.ProductName as FullProductName,
                       (select top 1 categoryname from productcategory p inner join dbo.ProductCategoryItem pc2 on p.productcategoryid = pc2.categoryid
					   inner join dbo.items pcat on pc2.ItemId = pcat.ItemId) + ' ' + dbo.items.ProductName as FullProductName,
					   
					   (select ReportBanner from reportnote where ReportCategoryID=9) as BannerPath,dbo.Items.EstimateDescription,

                       (select ISNULL(BannerAbsolutePath,'') + isnull(ReportBanner,'')  from reportnote where ReportCategoryID=9) as ReportBanner,

                       dbo.fn_GetOrderItemsList(dbo.Estimate.EstimateID, dbo.Items.ItemID) As OtherItems, BAddress.PostCode as BPostCode, BAddress.Countryid as BCountry
                       ,dbo.fn_GetItemAttachmentsList(dbo.Items.ItemID,0) As AttachmentsList ,
                       isnull((select machinename from machine where machineid = dbo.ItemSection.pressid),'N/A')as PressName,
                       isnull((select top 1 StockLabel from ItemStockOption where StockID = ItemSection.StockItemID1 and  ItemSection.SectionNo = 1  and itemID = dbo.Items.refItemID),'N/A')as StockName,
                       case
                       when Estimate.isDirectSale = 1 then 'Direct Order'
                       when Estimate.isDirectSale = 0 then 'Web Order'
                       end as DirectOrderLabel,
                       isnull(Items.JobCode, Items.ItemCode) As JobCode, 
                        case when p.paymentmethodid = 1 then 'Paid into Paypal ref:'
							 when p.paymentmethodid = 2 or p.paymentmethodid is null then 'Payment on Account:'
							 End as PaymentType
                      , case when p.paymentmethodid = 1 then (select top 1 transactionid from PayPalResponse where orderid = Estimate.estimateid)
							 when p.paymentmethodid = 2 then p.ReferenceCode
							 End as PaymentRefNo,
							 isnull((select top 1 Productname from Items where estimateid = estimate.estimateid and itemtype = 2),'N/A') as DeliveryMethod,
							
							(select currencysymbol from currency c inner join organisation o on o.CurrencyId = c.CurrencyId) as CurrencySymbol,
						    (dbo.Company.TaxLabel) + ':' As StateTaxLabel  

					  
					   --(select CurrencySymbol from tbl_general_settings where companyid = 2)as CurrencySymbol,
				-- (Select top 1 Tax1 from tbl_company_sites)+ ':' As StateTaxLabel 

				

FROM         dbo.estimate inner JOIN

                      dbo.items ON dbo.estimate.EstimateId = dbo.items.EstimateID inner JOIN
                      dbo.ItemSection ON dbo.items.ItemID = dbo.ItemSection.ItemID Inner JOIN
                      dbo.company ON dbo.estimate.CompanyId = dbo.company.CompanyId Inner JOIN
                      dbo.companycontact ON dbo.estimate.ContactID = dbo.CompanyContact.ContactID Inner JOIN
                      dbo.Address ON dbo.Estimate.AddressID = dbo.Address.AddressID left JOIN
                      dbo.Address AS BAddress ON dbo.Estimate.BillingAddressID = BAddress.AddressID Inner JOIN
                      dbo.systemuser ON dbo.Estimate.SalesPersonID = dbo.systemuser.SystemUserID 
                      Left JOIN dbo.SectionCostcentre ON dbo.ItemSection.ItemSectionID = dbo.SectionCostcentre.ItemSectionId 
                      left join prepayment p on dbo.estimate.estimateid = p.orderid
                      Left JOIN dbo.costcentre ON dbo.sectioncostcentre.CostCentreID = dbo.CostCentre.CostCentreID
GO


ALTER TABLE estimate
ALTER COLUMN OrderReportSignedBy uniqueidentifier null

ALTER TABLE templatecolorstyle
add constraint FK_TemplateColorStyle_Company
foreign key (CustomerId)
references Company (CompanyId)


--------------------------------------------------------- download package scripts -------------


/****** Object:  StoredProcedure [dbo].[usp_OrderReport]    Script Date: 4/21/2015 6:52:57 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





ALTER procedure [dbo].[usp_OrderReport]
 @organisationId bigint,
 @OrderID bigint
AS
Begin

SELECT dbo.company.companyid, dbo.Items.ItemID, dbo.Items.Title, isnull(dbo.Items.Qty1,0) As Qty1, dbo.Items.Qty2, dbo.Items.Qty3, dbo.Items.Qty1NetTotal, dbo.Items.Qty2NetTotal, dbo.Items.Qty3NetTotal,dbo.Items.ProductCode, 
                      
					 dbo.Items.JobDescription1 as JobDescription1,dbo.Items.JobDescription2 as JobDescription2,dbo.Items.JobDescription3 as JobDescription3,dbo.Items.JobDescription4 as JobDescription4,dbo.Items.JobDescription5 as JobDescription5,
					 dbo.Items.JobDescription6 as JobDescription6, dbo.Items.JobDescription7 as JobDescription7,
					 
					  CASE 
						  WHEN dbo.Items.JobDescription1 is null then null
						  WHEN dbo.Items.JobDescription1 is not null THEN  dbo.Items.JobDescriptionTitle1
				       END As JobDescriptionTitle1,
				        CASE 
						  WHEN dbo.Items.JobDescription2 is null THEN Null
						  WHEN dbo.Items.JobDescription2 is not null THEN  dbo.Items.JobDescriptionTitle2
				       END As JobDescriptionTitle2,
				        CASE 
						  WHEN dbo.Items.JobDescription3 is null THEN Null
						  WHEN dbo.Items.JobDescription3 is not null THEN  dbo.Items.JobDescriptionTitle3
				       END As JobDescriptionTitle3,
				        CASE 
						  WHEN dbo.Items.JobDescription4 is null THEN Null
						  WHEN dbo.Items.JobDescription4 is not null THEN  dbo.Items.JobDescriptionTitle4
				       END As JobDescriptionTitle4,
				        CASE 
						  WHEN dbo.Items.JobDescription5 is null THEN Null
						  WHEN dbo.Items.JobDescription5 is not null THEN  dbo.Items.JobDescriptionTitle5
				       END As JobDescriptionTitle5,
				        CASE 
						  WHEN dbo.Items.JobDescription6 is null THEN Null
						  WHEN dbo.Items.JobDescription6 is not null THEN  dbo.Items.JobDescriptionTitle6
				       END As JobDescriptionTitle6,
				        CASE 
						  WHEN dbo.Items.JobDescription7 is null THEN Null
						  WHEN dbo.Items.JobDescription7 is not null THEN  dbo.Items.JobDescriptionTitle7
				       END As JobDescriptionTitle7,
                      dbo.Items.JobDescription, dbo.Estimate.Estimate_Name, dbo.Estimate.Order_Code, dbo.Estimate.Estimate_Total, dbo.Estimate.FootNotes, 
                      dbo.Estimate.HeadNotes, dbo.Estimate.Order_Date, dbo.Estimate.Greeting, dbo.Estimate.CustomerPO, dbo.address.AddressName, 
                      dbo.address.Address1, dbo.address.Address2, dbo.address.Address3, dbo.address.Email, dbo.address.Fax, dbo.address.Stateid, 
                     dbo.address.City, dbo.address.URL, dbo.address.Tel1, dbo.Company.AccountNumber, dbo.address.PostCode, dbo.address.Countryid,
                      dbo.Company.Name AS CustomerName, dbo.Company.URL AS CustomerURL, dbo.Estimate.EstimateID, dbo.items.ProductName, 
                      dbo.CompanyContact.FirstName + ISNULL(' ' + dbo.CompanyContact.MiddleName, '') + ISNULL(' ' + dbo.CompanyContact.LastName, '') AS ContactName, 
                      dbo.SystemUser.FullName, (select top 1 ReportBanner from reportnote where ReportCategoryID=12) as BannerPath ,
                      (select top 1 ReportTitle from reportnote where ReportCategoryID=12 and organisationid = @organisationId) as ReportTitle,
                      (select top 1 ISNULL(BannerAbsolutePath,'') + isnull(ReportBanner,'')  from reportnote where ReportCategoryID=12 and organisationid = @organisationId) as ReportBanner,
                      isnull(dbo.Estimate.Greeting, 'Dear '+ dbo.CompanyContact.FirstName + ' ' + isnull(dbo.CompanyContact.LastName,'')) as Greetings,
                      --isnull((select top 1 CategoryName from tbl_productCategory where ProductCategoryID = tbl_items.ProductCategoryID),'')+ ' ' + dbo.tbl_items.ProductName as FullProductName,
                      (select top 1 categoryname from productcategory p inner join dbo.ProductCategoryItem pc2 on p.productcategoryid = pc2.categoryid
					   inner join dbo.items pcat on pc2.ItemId = pcat.ItemId and p.OrganisationId = @organisationId) + ' ' + dbo.items.ProductName as FullProductName,
					  
					  isnull((select top 1 itemName from StockItem where stockitemid = dbo.itemsection.stockitemid1 and StockItem.OrganisationId = @organisationId),'N/A')as StockName
                      ,dbo.fn_GetItemAttachmentsList(dbo.Estimate.EstimateID, 1) As AttachmentsList 
                      ,p.PaymentDate
                      , case when p.paymentmethodid = 1 then 'Paypal'
							 when p.paymentmethodid = 2 then 'On Account'
							 when p.paymentmethodid = 3 then 'ANZ'
							 else 'On Account'
							 End as paymentType
                      , case when p.paymentmethodid = 1 then (select top 1 transactionid from paypalresponse where orderid = estimate.estimateid)
							 when p.paymentmethodid = 2 or p.paymentmethodid = 3 then p.ReferenceCode
							 else 'N/A'
							 End as paymentRefNo
					 , (dbo.Company.TaxRate) As TaxLabel,BAddress.AddressName AS BAddressName,BAddress.PostCode as BPostCode, BAddress.Countryid as BCountry
					 ,BAddress.Address1 AS BAddress1, BAddress.Address2 AS BAddress2, BAddress.City AS BCity, BAddress.Stateid AS BState
					, items.Qty1Tax1Value,
					(select top 1 currencysymbol from currency c inner join organisation o on o.CurrencyId = c.CurrencyId and o.OrganisationId = @OrganisationID) as CurrencySymbol,
					case when estimate.Estimate_Code is not null then 'Estimate Code:'
					     when estimate.Estimate_Code is null then ''
					     end as EstimateCodeLabel
					     , dbo.estimate.Estimate_Code, dbo.estimate.UserNotes

FROM         dbo.company INNER JOIN
                      dbo.estimate ON dbo.estimate.companyid = dbo.company.companyid INNER JOIN
                      dbo.companycontact ON dbo.companycontact.ContactID = dbo.estimate.ContactID INNER JOIN
                      dbo.items ON dbo.estimate.EstimateID = dbo.items.EstimateID left outer JOIN
                     dbo.systemuser ON dbo.estimate.OrderReportSignedBy = dbo.systemuser.SystemUserID 
					 INNER JOIN
                      dbo.address ON dbo.estimate.BillingAddressID = dbo.address.AddressID 
                      inner JOIN dbo.itemsection ON dbo.items.ItemID = dbo.itemsection.ItemID
					  left join prepayment p on dbo.estimate.estimateid = p.orderid
					  left JOIN  dbo.address AS BAddress ON dbo.estimate.addressID = BAddress.AddressID
					
					  where company.organisationid = @organisationId and estimate.EstimateId = @OrderID 
					--  where 1 = case when @ItemID = 0 then 



End






GO



-------------------------------------------------------------



/****** Object:  StoredProcedure [dbo].[usp_JobCardReport]    Script Date: 4/21/2015 6:53:29 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





ALTER procedure [dbo].[usp_JobCardReport]
 @organisationId bigint,
 @OrderID bigint,
 @ItemID bigint = 0
AS
Begin

SELECT top 1    dbo.Estimate.EstimateID, dbo.Items.ItemID, dbo.Estimate.UserNotes, dbo.Items.ItemCode, dbo.CompanyContact.FirstName, dbo.Items.ProductCode,
					
					 dbo.Items.JobDescription1 as JobDescription1,dbo.Items.JobDescription2 as JobDescription2,dbo.Items.JobDescription3 as JobDescription3,dbo.Items.JobDescription4 as JobDescription4,dbo.Items.JobDescription5 as JobDescription5,
					 dbo.Items.JobDescription6 as JobDescription6, dbo.Items.JobDescription7 as JobDescription7,
					 
					  CASE 
						  WHEN dbo.Items.JobDescription1 is null then null
						  WHEN dbo.Items.JobDescription1 is not null THEN  dbo.Items.JobDescriptionTitle1
				       END As JobDescriptionTitle1,
				        CASE 
						  WHEN dbo.Items.JobDescription2 is null THEN Null
						  WHEN dbo.Items.JobDescription2 is not null THEN  dbo.Items.JobDescriptionTitle2
				       END As JobDescriptionTitle2,
				        CASE 
						  WHEN dbo.Items.JobDescription3 is null THEN Null
						  WHEN dbo.Items.JobDescription3 is not null THEN  dbo.Items.JobDescriptionTitle3
				       END As JobDescriptionTitle3,
				        CASE 
						  WHEN dbo.Items.JobDescription4 is null THEN Null
						  WHEN dbo.Items.JobDescription4 is not null THEN  dbo.Items.JobDescriptionTitle4
				       END As JobDescriptionTitle4,
				        CASE 
						  WHEN dbo.Items.JobDescription5 is null THEN Null
						  WHEN dbo.Items.JobDescription5 is not null THEN  dbo.Items.JobDescriptionTitle5
				       END As JobDescriptionTitle5,
				        CASE 
						  WHEN dbo.Items.JobDescription6 is null THEN Null
						  WHEN dbo.Items.JobDescription6 is not null THEN  dbo.Items.JobDescriptionTitle6
				       END As JobDescriptionTitle6,
				        CASE 
						  WHEN dbo.Items.JobDescription7 is null THEN Null
						  WHEN dbo.Items.JobDescription7 is not null THEN  dbo.Items.JobDescriptionTitle7
				       END As JobDescriptionTitle7,
					 
					 isnull(dbo.CompanyContact.FirstName,'') + ' ' + isnull(dbo.CompanyContact.LastName,'') As ContactFullName,
                      dbo.CompanyContact.MiddleName, dbo.CompanyContact.LastName, dbo.CompanyContact.Mobile, dbo.Company.Name AS CompanyName, dbo.Address.Address1, dbo.Address.AddressName,
                      dbo.Address.Address2, dbo.Address.Address3, dbo.Address.City, dbo.Address.StateId, dbo.Address.CountryId, 
                      dbo.Address.PostCode, dbo.Address.Fax, dbo.Address.Email, dbo.Address.URL, dbo.Address.Tel1, dbo.Items.Qty1, 
                      dbo.Items.ProductName, dbo.Items.WebDescription, dbo.Items.JobDescription, dbo.itemsection.SectionName, dbo.itemsection.SectionNo, 
                      BAddress.Address1 AS BAddress1, BAddress.Address2 AS BAddress2, BAddress.City AS BCity, BAddress.StateId AS BState, BAddress.Email AS BEmail, 
                      dbo.Estimate.FinishDeliveryDate, dbo.Estimate.CreationDate, dbo.Estimate.StartDeliveryDate, dbo.Estimate.CustomerPO, BAddress.AddressName AS BAddressName, 
                      dbo.systemuser.FullName, dbo.Estimate.Order_Date, dbo.sectioncostcentre.Qty1WorkInstructions,
                      dbo.CostCentre.Name AS CostCenterName,dbo.Items.ItemNotes, dbo.Items.Qty1NetTotal, dbo.Items.Qty1Tax1Value,(dbo.Items.Qty1NetTotal + dbo.Items.Qty1Tax1Value) as GrossTotal,
                      dbo.Items.ProductName as FullProductName,
                       (select ReportBanner from reportnote where ReportCategoryID=9 and Organisationid = @OrganisationId) as BannerPath,dbo.Items.EstimateDescription,

                       (select ISNULL(BannerAbsolutePath,'') + isnull(ReportBanner,'')  from reportnote where ReportCategoryID=9 and OrganisationId = @organisationId) as ReportBanner,

                       dbo.fn_GetOrderItemsList(dbo.Estimate.EstimateID, dbo.Items.ItemID) As OtherItems, BAddress.PostCode as BPostCode, BAddress.CountryId as BCountry
                       ,dbo.fn_GetItemAttachmentsList(dbo.Items.ItemID,0) As AttachmentsList ,
                       isnull((select machinename from machine where machineid = dbo.itemsection.pressid and OrganisationId = @organisationId),'N/A')as PressName,
                       isnull((select top 1 StockLabel from ItemstockOption where StockID = itemsection.StockItemID1 and  itemsection.SectionNo = 1  and itemID = dbo.Items.refItemID and Items.Organisationid = @OrganisationId),'N/A')as StockName,
                       case
                       when Estimate.isDirectSale = 1 then 'Direct Order'
                       when Estimate.isDirectSale = 0 then 'Web Order'
                       end as DirectOrderLabel,
                       isnull(Items.JobCode, Items.ItemCode) As JobCode, 
                        case when p.paymentmethodid = 1 then 'Paid into Paypal ref:'
							 when p.paymentmethodid = 2 or p.paymentmethodid is null then 'Payment on Account:'
							 End as PaymentType
                      , case when p.paymentmethodid = 1 then (select top 1 transactionid from PayPalResponse where orderid = Estimate.estimateid)
							 when p.paymentmethodid = 2 then p.ReferenceCode
							 End as PaymentRefNo,
							 isnull((select top 1 Productname from Items where estimateid = Estimate.estimateid and itemtype = 2),'N/A') as DeliveryMethod,
							 
                       (select c.CurrencySymbol from Currency c inner join Organisation o on o.CurrencyId = c.CurrencyId where o.OrganisationId = @organisationId)as CurrencySymbol,
					   
						Company.TaxLabel + ':' As StateTaxLabel 

FROM         dbo.Estimate inner JOIN

                      dbo.Items ON dbo.Estimate.EstimateID = dbo.Items.EstimateID inner JOIN
                      dbo.itemsection ON dbo.Items.ItemID = dbo.itemsection.ItemID Inner JOIN
                      dbo.Company ON dbo.Estimate.CompanyId = dbo.Company.CompanyId Inner JOIN
                      dbo.CompanyContact ON dbo.Estimate.ContactID = dbo.CompanyContact.ContactID Inner JOIN
                      dbo.Address ON dbo.Estimate.AddressID = dbo.Address.AddressID left JOIN
                     dbo.Address AS BAddress ON dbo.Estimate.BillingAddressID = BAddress.AddressID Inner JOIN
                      dbo.systemuser ON dbo.Estimate.SalesPersonID = dbo.systemuser.SystemUserID 
                      Left JOIN dbo.sectioncostcentre ON dbo.itemsection.ItemSectionID = dbo.sectioncostcentre.ItemSectionID 
                      left join prepayment p on dbo.Estimate.estimateid = p.orderid
                      Left JOIN dbo.CostCentre ON dbo.sectioncostcentre.CostCentreID = dbo.CostCentre.CostCentreID

					  where estimate.organisationid = @organisationId and estimate.EstimateId = @OrderID and Items.itemid = @ItemId



End




GO


--------------------------------------------------------------- update report template

update Report set ReportTemplate = 
'<?xml version="1.0" encoding="utf-8"?>
<ActiveReportsLayout Version="3.2" PrintWidth="11203.2" DocumentName="ARNet Document" ScriptLang="C#" MasterReport="0">
  <StyleSheet>
    <Style Name="Normal" Value="font-family: Arial; font-style: normal; text-decoration: none; font-weight: normal; font-size: 10pt; color: Black" />
    <Style Name="Heading1" Value="font-size: 16pt; font-weight: bold" />
    <Style Name="Heading2" Value="font-family: Times New Roman; font-size: 14pt; font-weight: bold; font-style: italic" />
    <Style Name="Heading3" Value="font-size: 13pt; font-weight: bold" />
  </StyleSheet>
  <Sections>
    <Section Type="ReportHeader" Name="ReportHeader1" Height="0" BackColor="16777215" />
    <Section Type="PageHeader" Name="PageHeader1" Height="4979.28" BackColor="16777215">
      <Control Type="AR.Shape" Name="Shape2" Left="89.28005" Top="3139.2" Width="11070.72" Height="1814.4" BackColor="13882323" BackStyle="1" LineWeight="0" RoundingRadius="9.999999" />
      <Control Type="AR.Field" Name="txtAddressName1" DataField="AddressName" Left="119.5203" Top="3208.32" Width="3720.96" Height="233.2798" Text="txtAddressName1" Style="font-weight: bold" />
      <Control Type="AR.Field" Name="txtAddress11" DataField="Address1" Left="119.5203" Top="3467.52" Width="3749.76" Height="247.6792" Text="txtAddress11" Style="font-weight: bold" />
      <Control Type="AR.Field" Name="txtAddress21" DataField="Address2" Left="119.5203" Top="3758.4" Width="3749.76" Height="244.7994" Text="txtAddress21" Style="font-weight: bold" />
      <Control Type="AR.Field" Name="txtAddress31" DataField="State" Left="1529.28" Top="4042.08" Width="1409.76" Height="243.3591" Text="txtAddress31" Style="font-weight: bold" />
      <Control Type="AR.Label" Name="label1" Left="7390.082" Top="3208.32" Width="1308.96" Height="288" Caption="Date:" Style="color: Black; font-size: 8pt; font-weight: normal; text-align: right" />
      <Control Type="AR.Field" Name="txtOrderDate1" DataField="Order_Date" Left="8808.48" Top="3208.32" Width="1938.24" Height="288" Text="txtOrderDate1" OutputFormat="dd-MMM-yyyy" Style="font-size: 10pt; font-weight: bold" />
      <Control Type="AR.Label" Name="label2" Left="7390.077" Top="3467.52" Width="1308.96" Height="288" Caption="Account No:" Style="color: Black; font-size: 8pt; font-weight: normal; text-align: right" />
      <Control Type="AR.Label" Name="label3" Left="7390.082" Top="3758.4" Width="1308.96" Height="288" Caption="Order Code :" Style="color: Black; font-size: 8pt; font-weight: normal; text-align: right; vertical-align: middle" />
      <Control Type="AR.Field" Name="txtAccountNumber1" DataField="AccountNumber" Left="8808.48" Top="3467.52" Width="1938.24" Height="288" Text="txtAccountNumber1" />
      <Control Type="AR.Field" Name="txtOrder_Code1" DataField="Order_Code" Left="8808.48" Top="3758.4" Width="1938.24" Height="288" Text="txtOrder_Code1" />
      <Control Type="AR.Image" Name="imgReport" Left="43.2" Top="28.8" Width="11072.16" Height="2828.16" LineWeight="0" SizeMode="1">0D0D0004021D70000089504E470D0A1A0A0000000D494844520000031B000000D10806000000A445AF86000000017352474200AECE1CE90000000467414D410000B18F0BFC6105000000097048597300000EC200000EC20115284A8000006FB249444154785EEDBD099C54D599F7DF492693CD998C99994C6A26F39F99B7DFCCE44D9C99443489B895D1748CC4252A31A296A61305252E412DEDB8A112B7D276890BA0A5881B02A508A2D0502ACAD674213B4D2B6BD31442B3350D344D53FDFB3FE7DC7BAA6E55DDDABABB7AA17F95DC4F2DF7DEB37CCF697C7EF7799E734A5052021E64C039C039C039C039C039C039C039C039C039C039D0D573A04417C81709900009900009900009900009900009742501D119141B5D0994659100099000099000099000099000095804283638134880044880044880044880044880048A428062A328585928099000099000099000099000099000C506E7000990000990000990000990000990405108506C14052B0B250112200112200112200112200112A0D8E01C200112200112200112200112200112280A018A8DA26065A1244002244002244002244002244002141B9C03244002244002244002244002244002452140B15114AC2C94044880044880044880044880044880628373800448800448800448800448800448A0280428368A829585920009900009900009900009900009506C700E900009900009900009900009900009148500C54651B0B2501220011220011220011220011220018A0DCE01122001122001122001122001122081A210A0D8280A56164A022440022440022440022440022440B1C139400224400224400224400224400224501402141B45C1CA42498004488004488004488004488004283638074880044880044880044880044880048A428062A328585928099000099000099000099000099000C506E7000990000990000990000990000990405108506C14052B0B250112200112200112200112200112A0D8E01C200112200112200112200112200112280A018A8DA26065A1244002244002244002244002244002141B9C03244002244002244002244002244002452140B15114AC2C94044880044880044880044880044880628373800448800448800448800448800448A0280428368A82958592000990000990401F24D084BAC85A34F7C196F7FB2637AF45A4AEA9DF6320805E488062A3170E0A9B44022440022440023908B44502282D198A50B42DE5CA7A847CA528F18510CD49D1BAB63410419B488C48C09BE5BE564423E3E1F77A50A28C879232F8C755231A73ABC42E4B5FE772A8B6B54510282DB1EB762923D7F99C7D3317B4A02E3818259E0A849B1C8D6D0AC3EF91B6950551E7F83956174459C900F8C38D79D790FF85456A8B66550A5FA83EFFA6E475651BA2A1A128290D20923ACDF4FDF638673C9FA5127B7CD3E687D78F60B82E8BE075CED9BC3AC18B7A9A00C5464F8F00EB2701122001122081C209748DD870D69B5D6CC41A4228F71C83F2D04668DB3CD68070C52094056BADEF49AF5CC2452ECE2526729D2F00992520BC0844123E1BEB372584062358D76297661BD7A9C2A480BA725D5A94B6144D6CE4EA4D67C546AA4012CF5AA802DE12C73CCBD5049EEFFD0428367AFF18B18524400224400224904A203FB1611B83DE1108F8CB6C2FC331F055CEB53D12E62971180B9457C37821D29E54BB1BE1BA0DAE867947C48631349500F0C03B62047CE279B0BC2EA26DA27351E93B26D187C00CD4352B9963FA38043EED75F1A40B20EDC570FE6E79183CFE673056CA4C08A64684FD036C6F87B4271C747872A45DF2D47D5CA401BBC215F0248914D5C05A04CB06E6F68874A82D3134D785118C8FA1C3B3D46A7988CCD859BCC40B55FDA4E6A77FF7F810A8B2BD055A98085FDF1031EA6DB155BB1E91717EFBBBF3FA54CF866A4728C1C43B1C7E3526F1F992A5DEF4099CC11B93ECFDB1E6B9173E9F3D3FCBAEC3CD17DBDE38CD32C50BE5FC2D164575A54FC6CAE2E0F155A28AA166DDFF8F29C546F733678D24400224400224D0590205890D79526C090C3116239562541A032DDF302AF7709ACC6D28546CC4D0A40DF83254841B10D35E134B1C69E3B9B91A016F29BC155596486A5E81A018B99EF2101A622664CB7E1ADEBC1175D1D614BC9688F0F8C3D0590D7161506F8B0EFB77DB1057E2A34D7B3EECF668B153850A256654D8D5AE54F122E7D5F57979440A6F4B4CB7D7D1FF381FDB2B93E4D930636CDA2EDF6BC789F0B0F998F025CF70841A5AD0BCB60EB5558AFD20F1FCEC969E36A136586E8BA9E6E4302A5B2859E320C2223CD212285A6CE4A8376FB161B3B43D51D61C13A1A0C7BA096BEBAA31311EFA97C2527C6C7A1EE971D82961818344208E4458CF07BB5FBADFA9F3A3B37F8DBC3F2B018A0D4E1012200112200112E87B040A121B4E2338293C295FB1610CC084F16D0CFE9294F0248B64B69C0D3B74C6A51D7131A08AB0732A4A03D5D819372013015B56FF95B1DD68E59A6435F46D23D43C818F86E0B33D134E9160853839C3AA9CF3C21932B4DB3264E3751A4F892D5AB24EA7AE688B8A42B39EF8EBD0B024B1916A803BC6430925DB1392606DBC566E46B85364BA79B76C4F90E69AA3DED458BB6CA15F7A7CAC7962F5D3E9BD70CED9D436D9ED5139416E5E0F8798744D35EA7BFF0CF48D16536CF48D71622B49800448800448C049A020B1E10C8BEAA0D8800ACD7126887B2B302138C236F84DCE836961819E0D57233043885752C2B932B6B75A62234792724248ECB79E7E9BC4F0B897639BE3A9B86D8A36D7211C0A21149A940861B2EB494A24CF3784CAC6D3A1B6C893F9BAF014698BB42768429E5CC446A6C46BE38168494FCC8F7B6DD4352A413B341D11ED0D708A0D7B4C9312EA1DE7ED725D1704701B9B82C48633DF2639413C691C1C216AADB647C4AD3D26348FFFA27413018A8D6E02CD6A48800448800448A00B09E4121BD6936B9704DE0E8B8DD4C69BD027B715B18A20363ABB22921604124E168A4848D540479E86D5568F7F3C263942ADAC847815E71FC02465E087E53EA7A8D1E5797468D6EEBC43A88CDA28AC2D886D44A85C722354EEC524257EDEC7AAF0FDEE9E8D5C89F519CF2B31391DA149013BD7438555353AC2A8F2131B791BF259C48633893EC983A3F1A5AC46151F872AD43B7269D2EFEBC23F3E165518018A8DC278F16A12200112200112E80D04322ED19AE42528A6D870333E3BE8D9B00DC8AC6154798537651B19676897F349B989F31F08EF402546D4F2B176927292C071860CA97A5AD1101A2EB90D56F27252DB734E9042DA929CC360156D845EE630AAD4257DE34DCA2546D485F16B16A03EBEF46D7E615419EB4D6592516CB827882756124B5DFAD65C7F1E7C43449019CF4B5A227ECE41E105C52240B1512CB22C97044880044880048A48C03CED9670A6905961C7ACBE134F82ED80D848D977C2F4C01237664952B32AD180C452B8495D2DD0B311379E4D82786215A14482B878117CE350AB56A08AF753250237E515469530D0D56A4B19F6DC880B1A63C0962358AB52CA1D2B65390588D9ABA3E07D398C58C8A72D466C88100AAE105F95734528A7D830AB6D99446D737D62252F2D88D2C4466A5F4D42B9CA93D89A3D41DCAC789594209EA1DEBCC446FAD2B7393D1B4A7AC5973176AE3866F26ACC189A95B28AB5874A11FFD6FB7AD1141B7D7D04D97E1220011220817E4B40E514C4E3F7DD96F72C446C18235395E3161A25866055A5FB72AA69039063533F557EFD82944DFD9CE5A72E7DAB0CEC1908C497BE752E635AC05E0FB63848F7423897BCB53BD35C8B507CA959698F7F2C2605D432AACE0472FBBE14F16219C83936D92BA42D4EB163F22A74B89331AECD0A5232767A33C794B152AB9199A582DD3C1B328FAA74DFCC12BA66C346B7A56F1DE390B6F46D967A5DC546FA868F2A6C2D1489C6F76EC9476C58AB8BA9658F5392FB53FBE55C02B8DFFEA3D1031DA7D8E801E8AC920448800448800448E008209069152A254206E5DE73E30820C02E90404E02141B3911F102122001122001122001124827A0F7FF18E2D881DCBE44792DCA2A11D19B0EF24502FD9C00C5463F9F00EC3E0990000990000990408104EC3C07B5D3B9D968B0C012783909F41B02141BFD66A8D95112200112200112200112200112E85E02141BDDCB9BB59100099000099000099000099040BF2140B1D16F869A1D2501122001122001122001122081EE2540B1D1BDBC591B09900009900009900009900009F41B02479ED868EF3763C78E9200099000099000099000099040AF267064890D0A8D5E3DD9D838122001122001122001122081FE45A02F890D2525B2CB098A8DFE357BD95B1220011220011220011220815E4DA0778A0DA7AC90CFED9688D0BFCA67EB88E9F74F76EDC1C435EB31B1763D166CD96E5D17BFA657A367E348800448800448800448800448E0C826D03BC586616E090BEB657DDE2DC7031FD5E0079317E0732FCC4749FC98879271F3F139398E1E371737CE5B8A6797D639EE37A55862842F12200112200112E8BB04DA100D0D4549C95084A26DDDDB8DB60802A525280D44D0CD35776F3F591B099040D710E8DD62C3EA638B8883393BF7E09C998BF10FA367E007AFCFC34DD5EBF09A78335E575E0DDBB3A1BC1B136BD7A17CDE5A9CF8660DFE71F44C94BEFC212E7B7721A6447761AFEDF1E81A722C85044880044880047A8A00C5464F9167BD2440020512E88D62C3781E62220ED6EDDD8F33DFAAC6978273F0EBF75621BCAB05EB0FB661C7FE56EC6C69C59E8387D0DCDA86FD87DAD0D27618AD8763688BC5B02FD68EED72CCD9DE84DB16AEC18F43F3F08357E6604CED26EC38D84AEF4681F38497930009900009F42602B9C4460CCD756104FD65E2FD28B18F32F8C755231A03DA22019496C8F7C00878EDF31EDF93A88EB6DA9D6C425DA8C23EE781D73F0A7EAFC7F266A47A3662515457FAE089975389AABA26A0290CBFC783B2602DA44AFBD582BAE06078FC61C8157C910009F40702BD516C28EE4A7034B7B6E2FB13E749B8D43C5CBB702D223BF662C9CE7D58BE6B1F56EDDE8F357B0EA0AEE980089283D828477D732BB6EC6FC3D603ADD8DE7208BBE4D82DC264EFA1C33820C75F566FC037C6BE2765CEC7CA6D8DFD6178D9471220011220812392400EB111AB45B0AC14DE8A2A2D2E106B40B842098FC108D6B5D862A3047181D15C8D8088094B04C44427548878284345B801B1F8BD76E85492D8D88D4860104ABC2311D642A509B5C172783CC3116A903AFD03505216449D511BBA5D03E10FF3BFC147E4B464A748C08D406F111BA92B4D298FC6F727CE959C8C79F86E6811DED9BC0BEF6EDE8DAA2D7BE41FB43DF860EB5E7CF45913E66DDB8B05DBF7625163B388917D58BA63BF16232B771FD062E4933D2D22465AB0A159C4C8BE83F854C4C7BD8B56E37F27CCC3A8EA5AEC924473F3622607FF46488004488004FA06815C9E8DF45E58DE0C2F0291665B6C0C7018FDF2DFD0801725A50144DAEA11F295267B1FB497C2456CE8DF9DE548BD5A8C288FC62A34452AC53B92381FAB0BA2CC53817053E2BFBD7D83375B490224D06102BD4A6CD889DBADF25EFE7EAD161AEAF8C5CC1578F1936D78F9D36D78756D2326AC6BC4E4F53BF0E6869D98B27127A66DDA8577EA7762A68891D90DBB11DEB21B73B636616E742FE6892059208264D1F6662C1641B25404C90AF18E3C2F62E53F9E7A0BDE0973B0A7C3F4782309900009900009F404817CC486844285A720140A2114F4DB21514EB1617DB65E0EB1D162C48233FCC91220A961542D5AC09830ADE4777DADF664188F0943A87A62A6B04E12E87102BD456C3841ACD9BB0F7F37BA2A2E362E99F32902CB1B50B9620B9E5819C593ABB6E099D55B31B6762B9E5DF3195EA8FBCC1623DB458C6CC7EB224826AF6FD462E4AD8D3B448CEC1431B20B3336EFC4AC865D784FC44858C4C834111EA7BFBB04C789076576FDB61E1F0B368004488004488004F223902B8C6A2342E5C7A0C4E3436092888DD0FB5815BE3FC5B3D15562C3594E6AEB2D8151A2BC19BB5731842ABFC1E55524706411E82D62C3ECA1A1E8DEF0D14A7B495BE5D9988F93DF598E3F2EDA809B6B36E196C826FC498E3B166FC29D4BEA71CF92CD18B5B4010F2C6BC043CBB7E01111248FAE8AE20911234FADFE0CA36B3FC3181124CFD56DC338F18E8CFF74BB78481AF19A7847266DD88137E4B8F4A335F8A7F11FE1D9B55BE37B741C59A3CCDE9000099000091C5904B28B0D1DAE64874C59FD3679187978365A9DDE089B5AD630AAD424F064D2B18610CA2551DCEB1B022F43A88EAC69C8DE90403E047A8BD8306D95DD3450367389EDD550FB68CCC3375EADC6251FD5E18AB99FA07CEEA7B85296B61D367F1DAE59B00E7F58B81E37546FC08D5A8C6C448508122D463EAEC7C88F37E35E1123F72DDD8CFB459004448C54AE88E231F18EFC4504C9D322489418794EC44879753DFE6EEC2CBCD6D098B46E38F338B2CFA2DCBBBAE7330B790D0990000990406104F2111BC7C0175C2101526A65AA905E4DAA242967238367A32D35413CB1DA54FA6A547682B8A71CC15AB5BE542BA2D54FC29794C7D168258A8BC1C155A80A1B655E4D02470481DE2636D472B75F938DF974BE866CCE57F2821CCFCFC37FBEB918BF9CB50AE7CE5E8D5F8557E3C2F7D6E0A2F7D7E0E20FD6E0D2399FC0A7C5481D7E2F62E4AA799FE2EAF96BF1071123D72D5C2762643D6E5AB4117E11244A8CDCA6C5C8265B8C348818D98451CBA338E5EDE5F872F003DCB3E8131C32BB961FE1D67467BBD7D9FB8F883F22768204488004BA9D80111B6EF9126AA3BF9D8EA56BE51AAF1FC1490111019617A2D5912C6E35DD9920AEBE17B0F46D731DAA0289A56F75E856559D94685E46BCA42492773B3356480224D023047A85D87058AC6AC9DB92E717D862C3161D3A515C76067F69017EF2CE2A9C347D194E7977054E7B77254E9FB11265552BF08BAA55182462E49CD96B70DE7B4A8CD462F0FB9FE0371FD4E2923975B8EC434B8C948B77E4F72244946764B888916BC533F2C7EAB5B8B17A13FC2244BEFBE6527CFEF9B9786AE566EEC5D123339295920009900009F43A02F115A69C49E3F9B792AB50E5CF8A5792C01147A037890D2534D4F157CF7F9414466556A552EF5F90E39BAF2FC277428BF17D1106FF3D65297EF0D6320C98BA0C3F9AB61C3F11EFC489D3976B31E295E3F419ABF03311226756ADC6592246CE16CFC8B922442E90E342F18C5CF4411D86CC592362E4135CFE918469CD5B877F9F18C1D75E5A88B16BB6487BB83C5FF649DF31DF86D9B8F188FB83628748800448A0CF13B0C39EBC958834ABFF06DA7B67D87B7414DE3D156A755ECAE67E8597C23B488004FA2881DE23368CD1DA8E1F85AAE32B51258486F17258EF9F7FE1237C75FC02FCDD2B0BF1AD0935F8B61CFF3AB106FF478442E9A408FE53C4C877DF88E098373EC6FFBCB9043F98B24404C9121124CB44902C1341B24C279E9F2A82E4A73396E3673357E2E7552BB5202913EFC8D75E94F02D09E77A796DB44792C68D31DE5B8DF262B4AB186566FAB374E36BC4EEF6CFB6A1AEB6166BF4B1467FAE93F7E816253E99C5D347FFA963B34980040A20108B56639C73F77109C31A17893A7602CFAF302B515D8571994DFFF2BB8F579100091C41047A85D848E1F9DBAAA58ED5A8AC10AAE4C3DA7FC3FACDFAFC0511067F3D7E3EBEFAD27C1C255E89AFBFB200474B62F93FBCB610DF9CB008FFFCBA2546FE4D0992491FE3BF268B181141F2BD373F16EFC8622D468E9DBA14C7CBF163F1907CEF8D25F89C84537DE5B9F7F056C38E6E1D7165D0CEFBE823CCFBF04339E674E050F7A51FF3A5CC8F23111C3A74C812509DE8956E63416D736F9369E78A65CB6597DA585CD8759751AF7284962D5D86892FBF8247EEBB1F8FAAE37EF3FE80FE5C799FF5AE8EC7ECCFAFBFFC32162D58A03D5FDDD5D64E0C176F25011220011220011220819E21D0DBC48632DCA6D56DC25163C329DE0D67FE86111B99DF3F2742E47322403E3F4E84C88B22445E5C802F8B27E42BE317EA10A9BF158FC8D1AF54E3EF5F5DA4C5C8B7448C7C5BBC22FF3649C488081115A6F5F7AF2ED409EAFF1A9C8D25B2FBB858953248CA48EF8C999E799CADE57F8103FB0F20F0E73FE361391EB1DF533FABEF1D3954798F8C927BE578F985173077CE9CF813FB24A3394774D48675EBA5FEFB92DAE06C6BE16DB3CA7A440CFA37264CD46269FB36D9FB44DA613C0E1DF90B711302EAB72D9B37E3ADC96F08DFFB93FA91CA5BF34A619D74CDA8FBF0C66BAF63C5926539454771664D47A8F01E12200112200112200112E82602BD4D6CA86E6FD9B31727BE9C2A365284858808672E47BAF7C37843D4AA56F2D979282122DE1075A81C902F48595F94F35F124112F78CBCAC8448B59C53C9EAF371FCA4F9B631593CB1610D793B6A57AD46E0DE5172DC8BC03DF7E22179EF8A4397173FA47C295B7D7F508E59EFCEB064949D37A3F55416EBB87AFEFC78FB4C1BBBB2AD0F49FF55BBA6BFF51676346EEF92BF06DDB7583BAADE7957979D896932278B5136FEBADFF78CC26BE2ED68B3BD46CE06270204BBA41B2C8404488004488004488004FA0E81DE28369451F8E6C72BF065F14894BC60AF4C65874B95A4890C6748556AB8958B37C4213A94E7C38810F5D91C4A5C7C4EEA515E91CF69B161856CBD69E76F5892A0EB5F5668533B22D58B70FFC891B84F0EF5EEFCECFCCDFCAE7ECBF777535EF2FBDDB8EFEEBBF1FCB363D1B4678F25AAB2880D75FE8D4913E36DB3CABA3BA9AD85B4C9B4DDB50F778FC413958FE0C30FDE47ECF0E14E71DFBD7B37C63EFDB46B5BDDB924F83BC7C1D937DD6661A7C742DE1F1091B46AC58A242F47C752E8BB7E7EB14412200112200112200112E87602BD4D6C58866EBB18BD4D983057F22C5E542B53E51342E5263472DC97EAF1D0FB7BA47841CC352238FEEF2BF3B07CBFCA77B0E546111487121B135E7A09A3EEBC538E3BBAE95075DD897BA5BEC703011DBE94290FC1783E1E7F3890D2C662B43751A66ADB5F1E7D047B94182AF0AF44B579FB679FE16109D11A75E7ED5DCA54B52B6D9CEEB803D50BE6D961770536969793000990000990000990C09144A0B7890DC35619888D3B1A11AC5E89FFEFD92AF14064CBD3C82434B2FDAEC2ABECC388192D2CC4931117210BB4B7437F576157B2B9E090194B70C0196AD48593C1246DDF73FBEDB8FBF6DBE4F853D2718FE3BBF373EA751DFFAEEABC0DE39E7D36635E8A6AE3AE9D3BEDF659D75BF5393F27B73B9FF6A4F7CD2AF71E55EE6D09164F3DF144520E473EC2A371FB76294B3135ED326527DAD9399EB761A4E1709BC542B57BC3BA7539F338BA70FAB02812200112200112200112E87D047AB3D850B4363534E0D5054BF07F5E55BB89A7782AE262C16DC5AA1C02448B88D43C9064AF860EA18A8B0DABEEA39E7D0F73B6ED2D8E112996F3C1830771E7ADB7E2CE0AEBB84B7D763BD4F97C7E37D7D9E5C5EF49FDDDD4A5AFAB90BC91554993D5E9E9786FD62C7D8D6EE3ADF29EDA0E67DB52EB755E9BAB0FE65E5597D463584C7CF5B538FF5C6243B57BFAD4A972FF2DBA9DBA8C4C6D4A6D4F6A3FCCBDA60F69E5481B755BAD7AEE110F870ADDE26A55BDEFDF3DB6880448800448800448A09B08F456B111EFBE58931B366C404D4D0DAE9BB95896A29D63E75028F1E1140B057A378C50C92838A46C953362E7722484CE7C5C1E5EDAA91592B20DED725986F5B69BFDF1E376C7E7DB6EBE25E99CF3BACC9F6F76DC93FDF3ED7EA9D7AFAEA9C0ABE3C7BB365319CEF749C2B4559FBAF616DC9ED6AE4CF524FA95DE5EF7BEA5976D95B1A42692D75F88126FC96D54F598BA9C75AACF8E760B0B8B87D5CFDBFDA69FE97D336D5463953C5E7ECC9E31936223AF91E2452440022440022440024724815E2F3684BA327077ECD88165CB643F84B9115C3C5596AE1D3B4B56934A151CB996C435D73B3D24193E6B91E10CA9725C27E73E1323D62CCBDA1513C33CFD7EE7ED69A8B8F146FB18E1F87C236EBD29F9F7A725A4E89517C7CBF162DAA1C4C22BE3E577797F60D428DC7AE34D52962A6F847C4E2E37519F55BEBA56852E3993234CAE867AB7CA326DC9FC7EBFACD2A4EA7F3543FBC63DF71CEE104FC0AD49FDB5DA98ABFC0744F064F31898F62E5BBA24A9AC3FE9FE997E26D773FFBDF760F6CC995082AFA5A525E13D913E47A35BE4F7259836658A7846FE9493A1E983F26E98F0B85C5E98AE98472C83044880044880044880047A1581BE20360C309598BD5DE2EF57CAD2B0933F5E8DEB672EC249935257A34AF576A8EFB944896399DC246F89DBB2B956F967BE5DD3E54FAC9551FA8218E0FE1B6ED0C7CD375C2F877A4F1C7EF9CD9C771AC4A9932A690524F9D272E000664C7F47979528DFD493FEAEAEB333E1938A5EBA648994F1C7B47639DB78935DC7ECAA2ADBD07637B3CD6200EDB299DFA2850B71FF3DF7C4DB67CAB338B81F93264ECA3E0652ED4B22B8747FAFB758A69667583C70F73DD823214FCE57F2FE1E89056C5568D428597DCAADBCF4B6FE111FC99E215C91AA57FDB3C7C690C01140A00DD1D05094A8FF88271D1E78FDE31189B676AC8FD1107C255E0422CD05DCDF8C48C08B92D200226D6EB7D523E42B45692002D7D305D4D47F2E8DA1B92E8CA07317778F0F815004D1984D418F55297CA17A772CB128AA2B7DF0E8F991C7BC68AE4355C05C5F028FAF1255754DBAECB64800A56973CDCCBDA10845DB325FD3C1DDE7FBCF58F7839EF62DB16119ADCA08DC262B2645228B5113A9C1EB7317E1F29911FCD34B1F89B7C3B154AE0E914A1512F6F978F8944BEE46CAEEE4F1DDCA1DC9E225CF7F84358D3BBB7486A8DDA86F143131E2BA6B71E3F5D761841CEADD1CFAFB75D6F7C72B1FED50DD0F3FF8802EF7A6EBA49E94F2753D52FE0831CCD5B9A4659FECCDF5DE0C85EC6BACF6A51ECE762F1161A2044BBE390BFBF6EDC3DD77DC29E55BFD773B46A8FEABB6EB765E9773D5AC67473F13EFA7696B12CFEBAFD5BCDF95BD37B2ADC0950A7BE9C74B30E2DAEBE3E3E4CAC21EABCA071E2C7805AD0E0D2E6F220112E847048CD8B00C3DF38A45AB50E1F5C0E30FC332130B7C15456C14D8065E8E584308E59E63E0AB9C6B8B0B253E42F07B4BE10D54434BC1AC6263B708C04128F18E445809CFE61508FA8E81A73C8406235692383722EC1F2002631C6A9BE5825803C2156528290BA24EBE5A622379AEA50E93FB354DA80D96C3E3198E5043070530E743DF27D097C48693B6B5DBB6AC8CB46B17D6AE5DAB733AD4F1F4FB35B8EBBD087E199A8BEFBDF43EFE7EF4BBF8EAB3EFA7E4777464652B8797C3F67EDCFBF1BA2EDB7043F5A56E4D2DAE1F3E5C8E6B321C8973EFBC3D3D6F23DEC9ED79F19C5875B8D77383FC7E9D3AF787E1C993DB161BCF0783B82163FBAE493A673C05F9890D6B8F9165CB966665A0DBE6687FEDEADA8C7F846A03BF7BEEBA2B37CF3F5C83A9121E955F3BADEA62E28DF14B3896C5229565FAF742CAEEFBFFAAB007244002C527E02E36C4AAB4BC0CB69108B4225AFD247C1EFB29B47A3A5E556719AB6D11044AE589B76F08BCFAA9F56004678F777836E4DEF0483957868A7083FCBB1745649CDFBE56AE8F9765D729DFEFBFDF3C19174339300375CA7085C3B3E156675D0B62D1B9A81463D8F2D238EF2D3EC9DE5743A6B16D415D70B070AF40B849B866131B4D61F83D1E94056B61690BBB4C736F4AA763754194950C803FDCE88AA3E362239728EA7DF4D9A22210E87B62C30A487186A5987097A6A6266CDCB8312E3CC2CB6BF1E6EA8DA85C588B4BA7CCC3B79F9A8ABF7D76760EE19165BF0E932CAEBC22CFCFC579EFD7E290B5E94697BCD4AA497F183614D7CAA1DED38F61F66FC3743F3B62C006C78ED5650C1F96282BB59EE1570FD3E7DDC2A86E945024F7B639DB3B0C378FF86381EDB3C754788E79FA99AC7598B60FBF7A285E7BE595AC1E8951B229E070074BE767D30FFDDBD0A1886ED992DF38DA93EFED69D3A49D86A3E9BFF5DDAA477D1E26E379353D1BF991E5552440027913C8E2D9282B476575548C4C791A1EA94C8805F5BD769C088F63501EDA889836FC9568504F9D5BD02C0FEEA2F244DD0AA3DA992C34E4DEA6708584E40C9273BBA595F6136B2550EA1A2D81A344827E126FEA35C66BAAD848A9B3A91A01F5C4BEA2CA7A8A9FF3297CDE90FAEC8596F19F4374E50AA372F4DE883977CF8619DBCC9E8BCE8B8DCC42A6CF0E121B9E3F813E2B3632EC726D0CF0FDFBF769AF875AC96AEDDA4F1159B2143356D4E1B9F94B30FC9D85F8EFD71DA2629C33A723DFCD01E7E29F9E7907BBBA486CA8763FF4C003B8E6AA2BE5F83DAE96437DBEFA4A39D4BB3EAE92EFBFC74D126AD511A1715876E01E2E46B52ED71C52FE358E3AD4E76152CF30A9C7F952F6757D7DBDAEDF6A63FAA1CAD465C9F1ECE8D105B7D12452EB7AECF625B5D5AE53956FDAACEA497D3993D9C74A189562196F9BA3DDFA77BBEFAA9E5B6FBE096F8BE0DBB56367F6A46E5B6C4422113C3B660CC6CAA17243AA66CCC06209EB5B2C1EB6C5726EB1AC98A5DFE530E3D59171CBFFAF9957920009F41F029972369467C0F644C00E8D490AA972783E5A2DB1911472A50DD881F0072A1C224551354FC6DDC2618C67C37EE2AE2FB7CAB6F234D2C546A24EDBD04D79E26E19B74AC8B4F49F214DEA6913EA426A0C4C5E4419FCC14908856DAF94BA362FB161CD01ED3112CF932542535FF6D8968E407042A24E8FEF4954DBB93F5973367C2144F590BB845A99702C8651F5D3796C77BBEF898D7CC72B91DFE1F47CEC940DE9EAEA3E11EF4704D3167E8C9BDE9D8FFF7EE903D93F239CEEF1B037F34B2CB1EB1422736593BFB978B37EB73448C518E5DB2EF7EBB6C90ED75796FF561F57B91E57E0CADFA9735760F4334FE565C83B0DDBB6B636DC251BCDC5EB5065C58F2B129FA57C55FF938F3FA61B6ABAA5FC0E53A7BC256D28B7DAA10FC77DCECFE5E550615E9D6172A5DD8EABA42CCD23A9BDF677F9FDA107EECF0A7EEAD4293637EB9E2B5579A62CFDD9F4C1FAACFAA68E175F94B8D5DADAB8E83095B88905A7B8E9DC2CE0DD24400224902F810CA136B6574087DAEC148F81F25CB825F6AA64EE16A720B0EBD506ACB9C77831AC73261F44972749BFC1D0743B11DD25413C87D848248BDBF7BA261F179AA89E2FBBBE749D84B245A623141A2BF91A1E2BD1DB7880F2121BA6AF26242E794CADB3662E397344EC9C8F827336D2E79B33D1BC2F91675BBB90C01123360A30F64DBE47836C181811D1F1FEB255787CD11AFCD733D39257AE4ADBB5DC2936ACBC8FE73738E21B0B6883730895B1BA685135CAAFF0C971397E2BEFF1E372F59B75A8DFD4FB8409AFE65CF635B9FC18EE96702255765AF9CEBACCE7DFFAB070C1FC34AD70ABECC161DA91D446739FB455FF7EF9E558BC7871A7C4C6B0A1573A78249824B1D0F55D9E9585E2AAAEB1DA6BFA9FC2388D813A6F5D73ADE4647CECF04C74E19F1E8B22011220814E10C814D76F9E308BA1BE20ECF02EB8549524089C62438CCEB12104CBDD128A6DE37752C0CE0351C66B7DFA6A54858A8D8C2B597502D111776B2B1A42C32594CD166105890DA5295CC4A5536CA48C81D3BBD4A930AA236E1CD8A18209F426B1916CAB27BE399F1CA73E458E2F4FAA139863E98667E2D17C5A688C33C97CCD279F62E6925518F64E35BEF972AEA5721309E697BEBBC82AD78475153002CEBEBCF5E69BB8FCB2CBE0BBEC52FD7EB9FD7E85FE9CF8AE7EFFF8E3C549613999F8A81C84850B17E00EF1685865380E9FA9C7942DDFEDDF7C975DA29718768EC79E3D7B70F9A5CE7638EE8BB72F516667C3852A64076EC3C07AB7CA563CD4E1D3755EA2DFB3A91AD58E9B2591DBBAFF127DEFE5BE649E6EF5983A2DFE56DD2FBFFCB216859DED5B0153849792000990400602B9C4860A41AAB74268E2C9E2294565141B96316BF206747E875B2BE2F787B12075E9DBBCC586C917E8CF2153A970EDD027B77173AE16964D6CE87329791276727E22693C51AF1E6BD75036E7B2B61D598D8A7FC02420047A93D8481B10B53FC4A6DD5872CFDB587DC314ECF8FA7DD8F3F9BBD024877ADFF5F93BB0E3EFEEC3CA5BDFC6F2BBA6639D2C7B9B6E08BAEF70909A60AEEA56391E1F7FFC31E62C5986135EFDC00EAB72DB83C3DA595C2DABFBD369B2DF060EEBA677D0B1A1DBFCC4638FE1928B2FC22543E4B8F837F2FE1B0C91437FB6BFABDFD4E7C9935EC7E4C993E57D9275C87E1393274D8C7F7F38F090DC7FB12EE3928BCDBB55A6EB21F50E9172D5A1CEABB2DB1DFF6951ED5BB8703E2EF98D94A5DBA3DECD67D3C644BB6F90255FAD74EF8EBFFCFE1BEDF65BE55FAA0FC5C7EE837CBFE057E7E2173F2FCBE9E59937776E1A43AB1F43127C4D7F74F9763D86BDE2638F8B1A13BFE4762C5AB4A8E39DE39D24400224D06902D9C3A8AC446093A82D9E8AE00ABD02553C5158E571E4101B801D4AA3E3ED258740AF84548E60AD5A54D7249B2B83767D273C1B5254B34A1097E57AE3CBAEDAFB43645839A9D3E87A7D018E718BAFE8A538D522A4F6DDF05622A256F9CA2636621B114AF24CD96154997227F4F50312CBEADAB91626A19C9E8D5E3F697A77037B8BD87026D0B637B6E1B3C73EC4A681CF60F5D1B760DF8513D174E5346C7C2D82F5AFD5A069413D0EAEDC86D69A2DD836EE6344477D889DBF791DCD273D8B867FBC1B9F0E780C5B6E7D07AD1357A06DC35EB4CBD2CED6D3FFCC82C0597F7DFD667C545D832BDF5980BF0DBE9792CB618B0F5B6C7C41DE53937F0BDDC04DDD7FFEB9E7E237BFBED03A065BEF17390FF9CD7CFFB5FA5D7DB77F8B5FEBF82DE95EBBDCD4DFCC77EBFEC1FAB85A56783A74E8507CD29ABEBD33FDEDF436A5B6D1FE3E71E2EBA95B7414FC47A03C3CA68F179E7F1E7E79D65938EBCC9FE3A7A79E8A138EFF117E7CEC71F8D1B1C7E2F80103B28B0D250285AFDA00F06C29E3BCB3CF9672555F1D7C6DDE9A7BEA6158DBEFE6BCBAFFB63FDD8A391FBC9FF06C15DC4BDE40022440021D259029415CAD6014726CEA2722A1AA32B1F4AD7385A39C62C32104943849D9F44D25A2FBC755CB0A529DC9D950FD577B48CC4020BEF46DF286721D25D4B7EF53E16AA12426D692C08EB14DCAAF71E64AD81BFD65D9A4CF958D11332E9B0066DFD4CFAA2F1F41D2B7C784ADEF3081DE22365407F635EEC5E6BB67E0D3FF773F365F330D0727AE44FBF64388B5C8D1DC82C38DCD68ABDF85B6BA6D685BBC0587E66EC4C1D99FA275CA2A1C7C6D295A5E90552F2A23387843159ACE7811DBFFF1417CFAB715D875EA5834DE36137BB6EC8A8B8E5460F15028DB383D203B6EAF9604E11722ABF1FD97447064C9DF480DA12A546CAC5FBF5E0CE701F89118CEA79D720ACE1E3408179E7FBE3E2E38FF5772A4BEFF0A17FECAFCA6CEBB1F17DABFABF70B7F957CCDF917B8DFA373355236E253DFEFFBF328AB3D19EE4BB4F37C343737775A6C949D71064E3BF914FCE4B8E33517CD4704861219D6617DBF4038E47A99FE54CDACC24F44A8A8B24E1978A22EBFECA7A763D099BF4862E864A5D89D6FF86A86E7255F2B4C86C9F2B6EFBCF30EC3AB720D04CF930009900009900009F43F02DD2236DC226A1C16B93606B7B562E30F1FC7F67F7A00DB5636A05DDCBFB1832232761FC0E12D7BD0F6E976B42DFF0C87AA37A3F5837538F4B6888CC92BD1F2F252B48EA946CBA3F370E0810F70F08E305AFC33B06FF8141CBCE24DEC396B3C767DF341ECFEFC9D68FCE29D58FBEB9771282AB1A866C95ABB6D6E0241B5AB41F21EC2CB57E398F16A6340E3D5B037F8D3DFE7A5E48A14163EA4EA183B7A4C92217DBC6D48FFF4542FCE3BE75C39CED1EFBFD29FADF76C87754FE2DAF3C46BA20E73BFF3BB29475DBF549607CEB4DA52A67AD3EA3AFB9C4E19DD46181851A105C680C4A1BE9B435D53E1BF25EFFA74DF64A3BF61575EA50587E51D1900C5DBFA7C1C4E3DE9649C71FAE9E9BC15430777C3D3C9F4B5575ED5FF8030A7A3FFFD3BCA1E9300099000099000096420D02D62230B7D6598ED7DA3162BFEE32E34DE310BED870EA3BDF92062DB9A71B87E2762B58DE29A132FC6471BD056B51687A6AC41EB84E538F4DC621C7A72210E063E42EBDDEFE1E0AD3371E0FAE968B9EA2DB45C1AC281F35FC3FEB35EC27EEFF368FEF168EC2D7D147BBE7C0F767FE14EECFEABBBB0EDFE30DA1B0EA2FDB02533324904D5BE2D2238DE5918C1BF8CFFD0115265090D751C3AEC4C9D4BDE7030D7C453E5FFFC8C9F25890DCB98569E8EE3244CE8380CFCC909F09E722ACA4E3F03679695E19C41BFC439BFFC25CE4E39F4EF2EC7D9F29B3EE47A735E7FB60F75EEBDF07B198DE4BD4D7BADBAEC7274DD763DCEDFD5E7FBFE7C5FAE2E673C6F12F627BEFE7ADCF8D7A2A38BC48611028AF9AC59B370CB4D376BA1613C26092163FDA6BC20279F7492F67EA8314A65EBE4A9CE29262F8F7F096D8E30B40EC3E08D24400224400224400224702410E87EB19162D61F006ABF3B0A6D93D722D6B81F87B7EE456CDD4EEDC568AB9618C00F36A075FA1A1C142FC6C19796E0E0D81A1C7C7C3E5AEE9B830377CCC6819B67A04542AEF6FFF60DECBB7812F69FFB32F6FFFC45EC95FC8DE601CF60EFFF7B1C4DFFF108F688C7A4E9A85122344662D717EEC20EF1722CFEC79BD1FCC27231B245E0987C0E47F39C4FA8A3D12D18F95E045F7ECE9938AEC4C67CD4EF56C9728957216154AA0EE7937AF384DDBCC78547FC89BE65089F72E24922404E96E3144992FEB9E433FC428E33F5BB0A0B5287F96C9D93E31729E77E71167E21BF7FF8E187599FC67F38678EBE6E90DCAFCA889717AFF34C9CF1D3D370AAB4E775110A1D7D19D9F7FBF2DF253349151BF25D0931C5E8C927FED2294FC2A79F7C822B7C975B82C6883CFB5DFDE6F480A83A4F11F1A1980FB2591BAE4E262FBFF4524711F03E1220011220011220011238B20874AFD8504FFD2D6B5E87CB4852D876FF346C17A170789D844AADD98E434BC58B317F135A67ADC5C1A9B538F8FA72B43CBF182D4F8B17E3918FD032EA031CB86D160EDCF00EF60F9D827D974FC6BE0B26A079D04B68FEE90BD87BC258ECFDDFA7B0E73B9568FA9787B0E71B7FC6EEAF8A47E38BB28295088D3D2234B47743C2AAD4FBD62FDD8EFD974EC21EF1A2E896A5B93812ED5DB1B11EE7BE391F9F7FDE2C7D6B793736EC69EAB0C1AB564A720A8C444E42C2F84D162056C84FC23036F90CF2BB18C3279D301027CB71D20927D8EFEAB31C0307E2C48127C8A1DED5F713F0C4E38F63E3C60D19DB6EC4D6E38F3EE62857EE154F8BCEA5D05E012BD7C41CB3AA6675EA0F64C58A1571C3DF088034CF862D3694574209A5CEBC4CD8D6A285D518FDF4333A54CA08193711E8ECF389C241B13CCD7B1A4E57DE8F9F95E9E337BFBEA8C3F3A1337DE1BD24400224400224400224D0EB0874AFD8703CFD17B1D136652D367FE536B4480EC6E18F364A9894E4614C5D83431226D52A02A3F5A985687D782E0E4A98544BC54C49FCB6C2A4F6FB2663FFF913B04FC2A4F69D2661523F1A8DE6FF790A7BBFF318F6FEF3C3681281B1E72B776B71B15BC48516194987FA4D890E593E57090F798F7EEB5EB44C5B9779652115EE2F792491BAB538F16567C2F87C44F73A13A20BCBD978F8C1871C49CFCE0468F7CFDA00763CD94F15275A84D84FE4E386B12D0A7E7BF915B855721C66BCFBAE167D96E0CB1E42A6AEB9E1BAEBF36AA3AAF760CBC1B8982C74B2ABBAFE7CEFBD565893C39B91298C4A8598A526F6175AA7F37A53D6C60D1B50F9F0C3187CFE05B6872591D3E1F438A58A40D5EE137EF4632811A242C1F822011220011220011220817E4FA0BBC5467C8FBD96766C387B8C0E693A34BD0E87EC30A99667254CEA890568B97F0E5AEE9464EF9B6662FF356F63DFEFDEC081DF4CC4FEF35EC5BE9F8FC33E1526759C84497DFF09ECFDF747D0F4CD07B0E76F244CEA4B77639708094B5C58A2222134D23FABFC0D2536949763F3576F47D3DB6B75C277FC6562A2749895F86244703C3D7F899D2CAE12C5E7A1E5E0018743A430B15171CB2DD90DF97878CF7138F79767EBE39C0CC795127EA48481DAB363EC9831F6311AD3DF7E3B6D79DE7C26BE31BE4D02B51556641D4922C76EE335C3AEEED432B07B76EF8E7B15728A0DA9F3D1471F4DEA57AAF0887FD79B3DCA98CAB1654B83ECA7B2D3CD859584C4DCAB3C35179CF7ABA4D5B02C3167445D82853379FD4FB7DC9A0F625E43022440022440022440024736816E171B767244DB9246ACFD9B3F6943FFA01D26D5F2C85CEC1FF53E0EDC3E0BFB254C6ADFB0B770E0F290ECB3F19A0E93DAA7C2A4068E41D30F9E44D37F4AC2F7B7254CEA1F64A33F3B4C4A0907E3CDB004845374582154FA48122096D7C35CAF04C78E898E5599E20918764681B47F53742BFECFAB26946A2EDA0E1FB2FD0385090D9528F23309BF317901695E0A471EC1AF6435246B895DDB2391E3BD2B66ADAA6FEE871FD94BCF9AD0AD84A16DC48779C2AF56D5B2BC2585BFD47D8F041ECE436C58392BEA509B309A502FF5AE56A6BAF18F7F94DDBE7D9AAB12496A1F0E7598302FF57956D5CCAC614EA9AB49A9659067CE98895F4A8E8B5982D77897D284979D705EC82A5985D3E21D24400224400224400224D0470874B7D83096A8DA704F1BF8B213F8F68B27E2E09DEFE1802C597BE0DA6938F0BB29D82FC9DEFBCE7B05CD92ECAD36EB6B3AEE69ECFDDE133AD9BBE95B0F62CF51F74A1E86957761844247DF8D00890B0E09EDDABF786B9690AA7654BEBF28BEC37847973ADDBA756BC644E8A4D021111DB714B0C46B574D3DD5AF675396E58D0B223B942B6E7C8B91BD61FD860E55ADEA59B870A12357C2CA3F713B8C3053C6BCF3A53C4EAA6DDA0B63C2CC4C1BEDB2F432B7F27976555587DAF98924931BAF467C95AC542F8F2D84AE10C1D3D179D1A1C6F12612200112200112200112E88D04BA5D6CD8105A65C33E63DCEFFCFB513874812C577BE91BD83F7802F62B2F86E462EC3B41723124D97BEF771E45F3BF04B0F71BF7DBC9DE094F446162C30893548192F8DD24906FF9F787B0EFE32DDA936005502596B45546E406D988EFABCF7F84AFBE3037214ACCDE1D790EF42C317A9DCBAEBA1BD756B8CED4B7A6E6596AD75E76B1243B3B43A7E24FF21D2155EA37B52CAF1292CE50A67C5BF2C1071FC4EB70969F496C28D1B07CD9B2A4E213AB7A25F22B52F7E4302B7B190F4CBEED33D7A93A46DCF0C744827EA6B032E1A142DA28360A25CCEB4980044880044880048E38023D2236C428B53C1BC633A136DCBB033B554894E74134FDA3E45F7C433E7F5D7230C483B1EBCBB25CED5FDD11F76298A4EEC28486C9CDC8F49E2E40B61C7D0F9A369AF87E87D850F2430CCF53DF5A849342D51D161B958F5426C2863224459B709D484D4DB7CFBDF8CEE66E791A29390B2AF1DC1266EEAF4C86F718F19CA8A46AD73C10E3DD706CE4A7784C13E1955A9EFAEEB66786DB52C2378A60285408181175E10517385603CB90C322ED553C0AADA3DB0798159200099000099000099040B109F498D89084F05D5F5002C2CAA3700A8FC467F93D9EC0ED2E12AC7BBBEE489477A7B44BF239CE7BC9DEF8CF1A096BE3394B6CF83F58828B67449213CA0B18B03F5C7D4D22D13A53D890FC7E9CE419F4C44B2547C78D753B7FC49957124F9416E3FAE9279F4C6A6226433BBE4080F0BBFD4FB7C577EF4E4B3877F230A14AF2DB65975C92D17BF2BBF2F2C44A5629E14DC9DE990158BB766DC148A7BC3925BED297339C2CBE478783D1AD4C102F982F6F200112200112200112380209748FD8487EE2AD0CD1B64D7B51F3DD546F820A8F4AE461388584FAAC56AE7217169DCFDB482E37515EF4CBB7A379DC72E0705C6AC467C1B34B3EC5E8C59F5822A480102AF394DCE43B98E56A33855129A3BC275EA39F199DF4143F753F10A7D89823A150CE646D37CF83526AAB56AEC473639FD51BE9A57A3392BEA7880DB31CEE675B3FCB884279139C65A4276F2792DC2FBEE83792282E3BD6C777737429D65E1C60CF9E3D18337A344E93CDFC127B9C583925C9F52542B8DE79E79D9E1832D64902244002244002244002BD8B404F880D45E0F0C143F8F48F1364533DCBBB9138DC854366B1914D6824C2B4F2A9C3E965D963279EAB90ADE57F7F2B622B1A95A4B00F4B5CBCB9368A29EBB6163CA0EADE575E7925391722C5B3E15C5A5519BA850A9A821BE572835A96D72DEF21E9A9BE1DE23456DAE83C1E7AE0010925F2E3D22143AC15A15472B6DE00D036F8DD3C254E6F8439AF0D7A6BF340956B91ED355BC44362C76F97257A1DE53B5793BAEB8E3B304684D5F8712FE2EDA9D3306DEA545D973A7E77457922E95C259F3B42BA12BB8E278753A9B20B119F5D31562C83044880044880044880047A2581EE111B89AE3B9F7E372CDF88DA33473B56944A170EEE6152CEEB920545B2F743ED149ED8317CE75FDD891DFFFC001AFEF73144BF5F891DDF18A5BD25E9615C26AC2BF1BEF55A2B4F209E002DBA63CEDA4DF864DFA182C6D5DCFFD4934F65DD5FC33CC957AB2735362AA15398F7A4A046A55C6CDAF863BD4BB86DB46708F34AF6C6A895A05C0EB77D391CF918AE1B133AF334C4C857822B97011F3B1CC329034F8A6F7A9835D93CBE0B7B8A50706C9AE8145A5A24B91C4EEF8E1188958F3CD219FCBC97044880044880044880048E1C02DD2D369CE40E1F3E8C95CB5762E559CFA0FEABB765F06EE45EDE568B85CFDF295E92DBB1FAE80AACFBFE03587B61104B1E9C8A8F0353B0E29DF9A89104EB9A9A45F6BBFA6C1D91480435914558FED4BB5876FB646C2E7B066BFFFE769DB09E1AB2B56F93DAD72161F4EFDCB903870FB7153C1994D17CC28F7F929CAF91F2D4DD18E0BE4B2ECD696417DC803C6EA8AFAF4F0E73CA4B6C64D8F93B93D870E4622419F2494240ED8B61853BE5121BDA63F4F2CBE922CE4D24644A7A7759CA362D3F23B53C477BAFB8FC72343535E541989790000990000990000990403F20D09362431987070EECC78AB94B107DF87D2CFCBF15D819DF3BC3E9B148FF6C56A46A94EB177DEB662C3FF9217C72E7546C796715D67DB0342E246A6B6B65D7E82D507B5A1C3A249BEF39BD1332BECA3054E7D7AF5FA745472452832DD35763D9797F41EDD1FEA47D3C1A2BEC187F7B5E28B194CB00769B426A65A96CFB3538F300F42A4FDDFC527D52067ED266839D111BA9215339BC1A96713F00A79C78925E6258BF12116C39690CBBEA2AF7DC8A2C7DC894DF913171DD290E6DE172D185833B341F7276881790000990000990000990405F25D0936243EF5F2186EDEEDDBBB166F96AD4894858FDF054AC1FFC3CD67B9FC68EBFB9477B2C9487A149DE5518D4D67FBD1FEB4E1F834FCE7C06EB6F9C8C358F4CC7DAF032AC9CFB316A57AFD6AB0CEDD8B1235958285B35C7B2AC4684343737433DD5AF8BAC40DDE4F9681814C4F6BFB6DAB0E5078FA2BDD9F184BD235B654B5BF4FE1ACEE4E21463DCEC07A10CDDD75E7DB547A6D6DD778DB40CF6CE8A8C6C067E4A82753C1F427E1FFAFB2B91940C9EA7D8B0560B6BD74BCF3A19E71D0E968F67C399B7617F3EFBAC41C9EDED915163A524400224400224400224D0CB08F4ACD848C05006E2CE9D3BB166CD1ADB2B618538E930273BDCC97C37BFA9DFD5F5CA33A17690CEFB954B24C8F9B6B6365D6E4D4D046B1E7D4FC2BCFE84ADB232D5BEF1AB1CC2254F0B38A561F7FFF9CF8910256570DB46B7C90B886FF427BF2F5EBC38EF6E75C5854674A91DBABB456C38BD1C038EC745837F8DA992A0DD118F91B3FFEAFE679E7A3A2961BCABFA93BA11E350F1A4EC6DDADB15F85906099000099000099000091C59047A546CB8D8EACA486C6969D1C2431D9B376FD6DE0AB5C15C341AC50EFBF7BD7BF73AE2F8530A727C751AADDA90D6C3E748F4B67F49DDFDDA18DDFBF6EDC3CA152BB0ECF1E9887EF94E6C3CEF59E060E7E680EFD2CBD296688D27233B9EF61F2F4FCD3B6B7417D252834DD579E24F4E48DF682FDB53FF4CE7CCEA5329DE00B3DCAFEAF74F446454DC7C0BA64E792B2DCCAD90F69BA134F7A8FEA89DC687C832B769CBEAA68441C5854886FD394CF2B773E95B1536F5AAAC2A961A9A57509B7931099000099000099000091CC9047A546C64019B2A12DC2F8D9BC7D94AD29BEE390DC2C467F3BBE45E6809E27C25BEA9EB55CEC7A777CFC0B27FF0A3AD669B75612E0F894BAB54596A29589593E03CCCF2AE3F927356AEC4005C7CD1453D2236DE08BD61AF42652DF59A68A7FA7C6C5ADBADF3CEDF8F8D2F57FBA3F8EFD6AA4FE79F7B9E0E71BA5596D59DF4FA442D06F249FEEEF0DFA0BD8FC667327ECF0783501E9B934F1868B72FF30EE04E5161C2DAD4B8288171CFC8BBF57E21456D77873BCC1B49800448800448800448A01711E8AD62A37044C97B60B4B75BC9DBCDDBC660CFE63BB077E350B447BE8CF69A12393E87C391AFE0C0A767C9B9DBB0A76124DA0EA995A69C1E8FF416A864E57557BE8CD6892B5040D056B28471D4E12E80126DE84EAF4692CC2AB08D99FAA1F59823572693715E6CA3DDADFC77DF7D17A36539DDC71F7B0C3F3DD56BE5A624793B94E83B0E57FDEEF7183B660CC6C8A13C6D3D352685FF3DF00E12200112200112200112E805048E0CB161098DF6F616E0D0521C8A067060CDCF108B7C410B8BF688080CFB80BCAB23E9BB0890C3355F424BDD4FD1D270976C38B85CCA6A559672DC79A18CCCD6D6566C8CACC6D2E7667778E4721BABF9786B3A5C7DBFBE31957D3E622F9B404A1553FD1A2E3B4F0224D08D04EA11F295A244FD07DCEDF08510CDD29AB64800A52543118A16BE747B377692559100091C2904FABAD8B08CC103220CAAD1B2FE66EC5C743CDA96FC0BDA977E4B8EBFD36203EA889843090D5B8088C8803A6C016244C8DE45DFC3E1C6876488B7C49F649B558EF6EE6DC2C68F9C49E25D3D132836BA9A28CB2301122081239780253C4A0311E42B1D8E28B1D15C8D80D793A1FF3134472AE12DF1221069CE3A05620D2194975522D21C432C3A1795BE631242CEEBC7B848D48A6850F5955D8F50833C90CCF58A35205C51E6685B1BA2A1A11944A2696323C2FE0129D794C217AAB76A8B45515DE983478B4C0FBCFEF1884433B52586A670857DAD43981A312A6545C6F9858F7DCED94FDDD73A54054C5D25F0F82A515567F6916A45B4FA49F83CEEF7666598C46D3722814128290D2092EF04CEC59DE77B1F81BE2A362CE33F26E14F9FE1E0A61BB067E539887DFA2BB46F90E39353D1BEE23B882DFE4A5C64284F86111C6E9E8E24C1A13C1D1266D5B2E204B46C7F516A6A49F270ECD9B19BE134BD6F2AB34524400224D00F09F467B1611BAA62C8B88A2D5B8894E4121BB18D08957B511EDA2882C232F63DBE2751AD8C785B3094782A106E5272A3150DA1E1282D0FA1215B3CB5B92F53DBCC4CD56D2C8537500D2D8762B5089695651047767FBD2311566D6B5E81A088224FC6B6B4A02E38388310B3FAE1F10C8F0BA758B40A15DE2108D64994489CC338D48A008B73280BA24E56FFB4445C192AC20DC2AC09B5C1724759B9189ACE9B7244B0506C1CD9FF76F525B161E7FA2A9981F6D65A346FBE0F2D1B7E87B61D6310FBEC21C4EAAF45FBA767A37DF5FF20B6E4EFB4D72293B0B0C4877D5EAE3B14F92A9A6AFE1B3B236760474D19B647CEC78EC8CFD0BCFC2CEC5B77035A762736F453CBEC762ECFA0B0CCF28E2DB06BE54BA4850EE5399D13ACF3BCA1AB2ECB822677085A7A2392EF298C7BAE2EB9B5276B1BBBB6FA5CCDE3791220817E412083D848796AED7C2A9DE4D9688B20509AC158EF697EBA6D8EA7FA49EDB10DD5815E78E5E97ABAD85086F97918E81D284FF6B37936ECA7FF5ECBAB01973A63754194950CB68D70694453187ECF00F8C38DAE842CA3FD3C110C4B73789D8C78B0EB56A54543E22D30C226A5785DAF0765C15A3B6FD4F69464BA1E6A6E7833B4D39A371E7F58A4425CF90833AFFE6D97EE73A63EDADE172D3CEC7B55BBCDF5F93054B769A135105E115B141B3DFDC756E4FA7BA5D8C8605D6B03588EC32D1F8B77EF06B4EF1E8DF6BDEFA2BDF12911DD1588ADBF14EDB527A37DF9BF89C8F86B5B6C582153CE3C0DF35D891195D7B17DD1E9D8B4D88F558B27EB1DC4D5FE1D666F8F1AD9E7E3B36D2B70A8F9BD9C09E4F90E55BB43AC240902DDBFF40471F39BD94BA43D668350B22B4732776A9BF4F572BF3EECCFE69A8C6539EA53D7A6ADEE65F7275E574AA27D52B9A65E172164959D1048AE39168EB63BAFCF968FA1DBA591655A952CA54E97A4F6D47639FB946092A06DB1B5EA4B70C97786F03A12200112C897809BD8B09E689778CA11AC1553D284DED846699F09A3CA2636B4A12A06FD82192E62C9162222201684EF97FC946C62C3329CE362451BCD29D76B23DF297AEC27F749867A62BC62D108A6E9B0ABEC5E274BC41C637B54D4FD46F80C814F42C3743E8EC78740559DE5F548799950A58C9E0DDD6E2F7C2238ACDC9E63E00BCC409D1255AE6D6BD662A3C43711B53AFC2ACFBC9EF8FCB2BD247931B4C4A0375085B0AA939E8D7CFFE0FBE6757D4B6CB4E3F081B9D8B7E63788EDFF00B17DD588ED9A84D8D651886D1A26615467A17DD5FFA07DC9D72557E3F33A57C39908EEF472A8CF2D35DFC6E6C80D5816998E55AB5761DBB66DB2E2D00E1C3C686DA4118B1DD6BB9BAB55881A1B1BBB26744A6CCFE31D9BF89DE13D0D6365552465948E1D3DC65E72D65A92F5BC73CEC1FC79F3745BD452B1EABCBA4E2D8F7BC96F2EB6363294F22AE49CDA953CF57E55C6715297D3E09D356B96A30E595EF7D717C9BD96D7E659295F2DED6AEA8BEFFD21E55C70DEAF30F7A38FF475C749FD6AF95EB3F7845A12D632B8A58C316371C28F7EAC97961D74E62F30E619AB6FF13EABFD36A44DEABBAAD7F952DF4F1E78127E7EFACFF093E37F84D0E4C9F172C748DB069D7996AEF37829FB91C0C3F173EA37753E5510A8369ABE294EEABBDAA871B661E0D80344DDAFAE55D728C1A976A1FFF3BDA370CA8927E9F6AAF63C5A596935D71EC33BEFB8C3622BFF3FEEB863E375FDF9CFA3F43E25A7CABD8A9BDA78922F12200112280E0117835687E2943A9E804BCD8EA7F17D5F6C184355428FDC3C33468844768BA34225C367111BA95E0A3743394DF4D8A220A347C18C7436B1E1E21D90906D4B2426429BACA7FF031C824495EDC8EB103152596DE793A44CB074316379522C7162D76542B244E834D78EB372304A47213441724B4A472038A1229ED3110F2D8BD7E3CC09112153391751A56372324C88C1487393257028368AF3CF436F29B5578A0D5738F2A4F8D012ECA83E096DFBC2683F20CBCF36BD8358A384506DFE2362EB7E2D5E8D132429FC5F4560FC559237C3998F613EEF889C821591695829FB253435ED495EA2D5369CB5F16AD9960E43B6A3414D76A7E47665BCBEF596B581DD638F548AF17EAC2536C68CC62DB7F8E3752911A1F685502FF5598909D518B317C7B8E75FD0D7AA6B94D830C6B6DAC74219F3495E13BB7A25367E7EFA19F173A3EEB9471BF0466CFCC621366EF127DA72CDB0AB71EBCDD6F7E3A5BDB7DC7A539A57E5FDF7DFD34260F9F2E5BAB63122A2D47725D862B6C742F5DD88264B04259EFCDF227D9C396386F65E2941A084927ACD9E55A5F7F67877FA3BBACEC6ED8D38EDE453F40EE1EABB5AB6D69469366C14B782703A16B36D21E5141B86CBDD778D1476893ECE927AD43D8B456C98B6AF5BBB4ED7B16AE52ACDE9B9E79EB3049FDE5B2421988E3F2E216C541F4D1DB7C8589C729278DB927ADA5BFEFAD90E122081BE4FC0C5A0B50DF0F495AAAC109CD65EBD1A95FD74DD75A52DF5A4BDD5CA17480A7B7286513984880C6E2EB16119E40E3192D350B6668C55AE23B4CA752265111B6921519966A2CDC319B214BF5492B4C323450C0C9290ADDD794DE5A4762785DAA964F32082FE8128297B06B327AA44768780801DF2E5DA0EE53C53A16376EE492E860E3128B154141B798D5C1FBFA84F880D6D94EEC19ED517E1F0BEB7113BB40DEDCD7310DB395EBC1AF7A17DE315887D723A622BBF8BF68FBF165F5DAA7D71F212B7CA9BD1267B6D34467E81254B3FD64FB953C36F8A3E9CEAA9B818AA0FDEFF803684870F1D86813FF989ED151883DF5D7E85160EA1899370B63CC97FF38D3774936E151162890DE5D93816D70EBF16A79E7C329431ACC4C6ECAA99FA3AD51F253694F7C1AD6FCAA05662C35CDBBCB7593FCD5FB060812EFFA2C109CF46F91557E836AACDF7CEFC5959DC7057D70FFAC599D2A65BB5C7451D9657630C2E1B32248E30C9D3601BDBCA105786BCDB4BD5A3764D577B5B3CFC5000AB57ADD697DD211E04677F8CC03AFE3831EA25AC4A8900CDC6EA95FD66798066DB222C496CD857DD638B0D739B12358AADF26CDCEABF1977DF7557BC2C55E788EB6FC048F59B2D18D518FEF2ACB370A8F590783612755DFB873FE0F4534F9332FC78E5A597B1B7A9A9FBE759D127322B200112E81D04327936C450342B18A534B44F7B36920C55E9589267C3F9C4DC4A26C82536D258E432940D4BB7EBD2264426B1613C02B9C48A2AD0F640647AF25F60CE4D9AB84A6AB3ED31F14D408D5A352BA5CEEC02CB211AEA5D42D1E2DEA1A576F8949D104FB1D13BFE1929762BFA82D850865ED3BA6B7170EB03683FBC1DB103B27BF3EEC99214FE280E6F9627EC6BCF466CD50F115BFA4DBDAC6DEA5E1A267CEA70CDD7B0B5E632AC5AF6810E95EA76A1A16D61CB08BEE2321FEEBCFD0E4C7CFD756CDAB829EED928BFE27231505FD24FCEAF19362CDEC684D8B0C281A64D9986C0830FE14C59B5A25C044A9AD890FBDDFAA70C6AA7D8D81A8D6A037BC5B2E55618D5E00BB531AD04C46FA52D4A6CFC52448F32FE8D51AEEA574244851D294F893A2CB1311AE79E2D09FA769EC2679F7D8639733E489AC26E62C3F836AA172EC4CA152B759D4F3DF92406FED81261F788F765802D9E8C805102EBC712DA64BC0C09B161F481E2AC3C0F96C7C7880DB513BC795962E3E6F877C546796D2CB1E11741F787785F541BAF2CFFBD88C4FBED3A2DEFC51FAEBE06BF166F90F24E291EBB76EEC27BEFBD8788E4FD282ECA5374EE2F134C8AFDF7CCF2498004FA1B013783D65E31C967AF24242B28399F80F765B161B53DD3FE22418454488EEB79F744F334E3DB2D4FC4455874CEB3E11642A5E6AD5B2E88C9A390BD539C49D8669AEBF63A93C6CD09F750AFAC63EF28AB4D797C52C2C412F7AECF985C5EA23C1FAD2E89FD86E182B0CEB1711FA33C7344FADB9FF891D0DFBE20365AF6CBBE1635121A156B9555A86469BABD1F4A52F8681C8EDE86C31B7F8BF6352722A693C2BF9424345245C7969A2BC5088CE8CDF96C7BB87343D891D818E5D990A7E053A74E4D0A735245A930AA5B6FB959FF1E5954A39FD82F5DB254B7B1423C1B6A176BF552F74F7BEB6DCBE815235C8518CD9E95F06C28435C794F5C3D1B627CFFFC0C2B8CEAF0E1C3187AD55538E7EC5FC63D13BF5162435ECA3BA2048E7A2D5EBC588B1F952C6F8C76658C1B88460028235DB5679EE499A8DF9417407D57F59897EA93DAB9DBED75D92597C6C3A1564978DB8F8E3D565FA6C4872552ACBC0C65C8ABFEBD24A2CC62E0F46C5825ABDFD535AA0DFA1E69BB6A8BF395E4D950F5A85C0E5B6CBCF6CAAB3A146DCA94293A04ECC3391FEAF150024231502154AADCCD9B376BF167C4860A0353ED59B776AD3E7FE76DB7C7AFEDDC64E3DD24400224E04620BFD5A89C89C647C66A54368B1C4FF6737936D25796CA67D9D64EE66CE89C1AB7BD414CB9A9391B6A65ABDD12ABA496E8752E756B8B48678E87738AE8502D6702BA33C42C353FA4097521C9CF3065E9BA063896E4B5F60CB1F23D322D9B6B724BF261681ACA30AA7EF1EF5AEF161BD66A3E2DD147B077BBE427B4354A62F862C476BF215E8DFB2457E31A490A3F07ED2BFF9F844F495278E4F3F15C8DD48DFA76D79C8865CB96DA42C3B142903DCAA986B9FAFEEEBBEFE275F13C3C294FD92FBDF452FCFAD7BF9627F573D0D2A2D6A04E18B5854E94ABC4C0570679D24B9A34EDADA949F90CC1601081071ED40C94A1AD048A3274AFBA52EE9F6B19F4EBD6ADC330294F19E0C6E857A244D5E1D6272508865EF57B5C35F4F7F2E4FE1A9D0BB27A95B549E134297FE49D77E966A9FA8C71AFBEAB90A11BAEBB1EAD075B31F4CA2B314C1D57250E55AE2A63E6CC99B85A3C32C769CFCCD5F8E4934F12DE01393FECCAA1982AFD74F5BACC9E8DCB84B30AA53AF7EC73F0E20B2FC4113D2F2C5478952AD777E96578F22F7F89F77798F018267D52ED192AC7230F07F4B9F7DF7F1FBFFB6DB9161D5679E3E25E0E55F00B92F36242D3D4772544D5FD756B6A6DE6A371B184951D27A267A8D4F1462894A8D3C1373C3B2CFD526310D1E71F7FEC31FC4C049D122B6A6C94B7862F1220011220812210E8ACD870F126A46D489796849D7D35AA442F3308C1AC4BFA8AD15F5599D82C2F7535AAAC1BEDA5F295B0B23A59AD2BBE41A173352AB9365759CDB508F9CB6C2F44EA0682D9DB999B21C54611FE1A7A6F91BD5B6C28DBFA10DAD60E11AF8624711FFC14ED4DB324295C92741B6E96A56E8720B6E614B42FFB672B29DCDE57233521BC79D177B03AF23C0E1C503B8D671E0B63AC8F1F3F1E5FFFFAD793DC7C3F91BC8AF5EBD7277923BA6554B3B4B75BEA2FA492AE6AAB8A7A4A713D99B1716D4E8E312DA40B69D75A115879BDDC04545E37F2221220011220811E2290AF97C2D1BC1CFB6CF45047582D09F45E02BD5F6C6CC1AE4FFF602585EF5B204BDD4EC0E1AD0FCB52B7574A52F82F24574396BA5D7C54C6D5A75A6B8EC6A6C82DD8BE6DB3EBD3743532C6907DFCF1C7F1BDEF7D2F2D965079099447A3BEBE5E2F83BB7FFF7E3DA03D6D5C6632C0F36D57B6EBB21AF78EE91CBFCE0E29CB5577AEF3A66863E09BEBDDC447A6BFAA4CF7C4CB7611326E6599BE65131BF9F6A7F7FE0BC09691000990403F2790B483782E1679EE209EAB189E2781FE44A0778A0D63B92A837E1D1A3F192E49E1CBD1BEE72DC4B63F21E15392D7B0EE5CC4561F87D8324FDA2EE1F1BD35649F8D8D353762C3860D598581DA53E1820B2E704D581A3E7CB85E1E57090C1A96FDE92F837D250112200112E81F04EC3D26CA1C3B7967EAB85A0DABEC7A841A5AFB071AF69204BA8240EF141B76CF7438CD46EC5E2A09CD7BDF47FB8E17642DE7BB2547EA4AB4D749F8D4F27F971DC0FF3A69E33E6752F8F6EAB3F1C96A2BB721F5657E5BBD7A35BEF6B5AF69A1F1B9CF7D2E49701C75D4515A68506474C54C6319244002244002244002244002FD8E40AF161B321AEDEDFBB14B92BB0F7E7A9124853F2C4BDDFE1187D75E88C35A687C319EA791B453B8E46E34D51C273B83BF9794CC9D3AB8FBF6EDC37FFEE77FE20B5FF8429AD050E2E395575EB1F23C743B6483BF2E59C2AADF4D3176980448800448800448800448A0BF12E8FD62A31DDB965EABBD176DB28F46EBEA1FA06DD9B79242A78CD050EF907D360ED4FC0756462660EFDEBD1987550907AFD78BFFFAAFFFC297BEF4A5B4102A75AEA1A18102A3BFFE61B0DF2440022440022440022440029D27D0DBC586EA61C3FA8538B4E81B59044662A7F03D8B7E8855352F41E56164F34454545460B02C6B7AFCF1C7E3CB5FFE729A67E30FB20B34BD199D9F5F2C810448800448800448800448A01F13E80B62A3B9B9197591B13858A376084F080BCB932187844DA9735B22BFC3B2C5B364C5A85D19858212101B376EC465975D866BAFBD163FFEF18FB5D848DDCDF28D37DEA057A31FFF5DB0EB2440022440022440022440025D40A02F880D251036C9B2B32B226F6047E4E738B4F8CB09D1212B4E6DAAB91ECB236FCBA67DCBB0A7A9C9DED93AF382A52FBEF822D45E1AD75D771D4E38E1047CE52B5F49131B6A895BBE4880044880044880044880044880043A41A0578A0D7BE55BD32D13CEA472282291083E8ECCC2DA9A07B0A6660C3EAE9983A54B9762CB962D19770777E251658D1A350A2FBFFC326EB8E1069C78E289AE62A3B595CBDA75625AF15612200112200112200112200112803CD187FC4F42917AD32B456CC44587ECE5ACC4C2962D0DD8B469934EE0DEB64D36FBCB73952875DDBDF7DE8B152B56E895A6468C1881934E3A095FFDEA57D33C1BF996D99BB0B12D24400224400224400224400224D0AB08F44AB1919150B6BD9C7363550242ED10AE44CA6BAFBD861B6FBC11279F7C727C9F0D93B7A17238283672F3E4152440022440023D44201A824FFD073CED28852F54DF438DEA816AD5267B5E0F4A0311B4A5552F9BF5452AE12DF1221069CEDAB8584308E5F6A67EB1E85C54FA8E49B0F5FA312E12454C9550C8A67EB106842BCA1C6D6B433434D47503E192781B1B11F60F48B9C631A6B128AA2B7DF0E871F7C0EB1F8F48345324460C4DE10AFB5AC75CF18510557D91B222E3FCC2C73EE7ECA7EE6B1DAA02A6AE12787C95A8AA935075B77B4BCAE00FD5C2508E45AB31CE5F66F7C3A59DD9CAEE8169C42A8B4CA06F898DCEC15002E2C20B2F44341AC584091370D34D37E1D4534F85DABCCFF90FB6FA8D2F12200112200112E8B504B4D8C86D44F7DAF67749C376231218A4FFFBED2A366C219230E433541ADB8850B917E5A18D22282C63DFE37B12D5CA88B7054389A702E12625375AD1101A8ED2F2101AB4FAC854A6253432B6CDDCA6DB580A6FA0DA32D463B5089695651047767FBD2311566D6B5E81A088224FC6B6B4A02E38388310B3FAE1F10C8FEF861E8B56A1C23B04C1BA166988E1300EB5CDD251C3A12C88BA98B9B71CC15A253E5A110D8F14D1728CC550F374B6CB3EAFEF559DCC5676974C0C16D2DB08F437B13172E4487CF6D96798387122FC7E3F4E3BED34FCCDDFFC4D92D838E79C737ADB30B13D244002244002249020D01FC4465B0481D24C9E1ADB6B31D00BAFC74D6C28C3FC3C0CF40E9427FBD94499FDF4DF5B898832AA5DEA8CD505515632D836C265089AC2F07B06C01F6E749D9196D17E9E0886A508F94A3318FBEA56231EECBAD54F6A5CE3C226A5785DAF0765C15ACBCB22BE1CED29C9743DEAA57E6F8676AA73A5F0F8C3B07D15525EB330F3EADF76E93E67EAA3DBBDD66F25CA6BA2E766CABD9AABF59BC533333FFE991F8104FA93D850C3A7F6D550791E93264DC2ADB7DE8A9FFEF4A76962E391471E6118D51138D7D92512200112386208E4101BC9612C1226E3F1A1B25A850235A13658EE78A2ED7C5ABE531B9B25A50144D26392BA1F5D36B1A13D0262D02F9821466CAAD8B08588088805E1FB519A556C584FD9E39E1137AEDAC8778A1EFBC97C92A19EC0138B46304D875D5906B87B8897380BB4D16D7B03F4ED46F80C814F42C374C4858C5BA0AA2E1E9EE41C0413EE95D1B3A1DBED854F048715BD710C7C8119A853A2CAB56D96D828F14D44AD0EBF1A8A5034CF89A03D321E4BBCB8318C8FE5063BB4AB80B2BB7FE6B1C6AE26D09FC4860AA3FAF6B7BF8DEDDBB763F2E4C9F8D39FFE8433CE38035FFCE217933C1B6FBFFD36C546574F3496470224400224D0750432E56CE8787C3BEEDF3CADB7437F3CE609783CCC65126AAB554EC320790ABFBBEBDAD6552565141B96D742871EE96B52C4861122D2A7B64820BBD848F5526435944D2E8C2D0A327A140C806C62C31EA3786891BAC70A7B2A718436E91C11EF003BC4CB94EBC8EB888BC874E8E962C612969638B1EB32215922749A6BC78957454449E928842688C7A4740482132AE2391DF1D0B2B4AA4C6E8C3D8FB4F090D0B08A2A44B50BC616B83ADC6D01EA953726EFB2BB6A32B19C1E25D0DFC4C6430F3DA443A8D4A67D4A6CA8047167BEC6B1C71E8B5DBBAC4D01F922011220011220815E492057189524E08627052CE3319E449E789AAC13A2F53949DE35F902BDA2A3F6D375D7E477D5FE562BE93B29ECC929361C4244FA934B6C5806B923CC2A2FB161CA758456B9B2CB2236D242A232C1B77924891273ADC995C85F2C5A3CEC76272588AB24EE2082FE8128297B06B327AA4476F18454CEB50583ED017369873597E4DAE08A0C09E2923C3E612CFCA52A046C85E48AE45F76AF98926C44E709F427B1A168A97C0DB57BF835D75C83F3CF3F3F2984EA5BDFFA16AAAAAA28343A3FAD5802099000099040310964151B76988F2371D832329DA12BE6E9782E83B9989DC851B69B67C3E1B5D07727793612E1533AFF220FB191C6254FB1A1732B7226E867121B6695A87CD8DB1E884CA16D6E9E9D2C58D3C455D2B5F69CF04D408DF63E2487D3250915FB3E2B3FC591E09EA9EE7828DA7A2BCF248FB27B70E6B1EAAE26D0DFC486F258A84D004B25E92C75C9C0D75F7F1D7BF6ECA1D8E8EA49C6F2488004488004BA96403663D7C5484F36AA4DD8CB00F110489270AE9595BAB6E5F99796B11F6E4BFECA6FBE20422AE7A0802581D38C6F3781E3C2DACDF04EEF5826B1E11642A5EE76CB05317914D912AF9D49E3A615EEA15EE9A2D3D16ADD77ABAC36E5F14909134BBEB709755595E239737A3F320FAD9397669EB5ECFCA708AFEC2304FA93D8303B91AB30A9C58B17E381071ED05E0E95283E7DFA74BD5120C3A7FAC8C4653349800448A03F13C826364CB26E5C449825626DCF8663B9D5263B04C65AF6B597BDB2AE4665B735C793FD5C6154E92B4BE55AFA56D5DBC99C0D7B7CD213C74DB989E568AD9C0DB5B2D56EA936C392B2CE1C0FE7106A6F823301DD1962969A1F22E22124F919A62C5D9788D1F892BCD652BE9630B597BE5561568ED0A978D5A9F3AFB91621D973232E6AB396DDCBE6209BD33504FA93D830C494A0508243ED245E5353A38FFAFA7A2D342836BA665EB1141220011220812212C81AC6239E8BBA10FC664523D9706D847F88B5BA50C3466BFF87B4E471B5674243EF5A8D2A1F7C9D151B2EDE84B44DFDD292B0B3AF4695687606CF465611653C06B6F7267535AA8236C353F34056EB8A6F50E85C8D4A5A99AB2C5B24589E22C7C67C5AC464F02EE9F0A81CF52A4099CACE67CC794DDF23D01FC5865374F4BD11638B498004488004488004BA8640BE5E0A476D39F6D9E89A76B11412388208F467B171040D23BB420224400224400224D01102493B88E72A20CF1DC47315C3F324D09F08506CF4A7D1665F4980044880044880049209D87B4C943976F2CE8448E550945D2FE168AD84480224902F018A8D7C49F13A1220011220011220011220011220818208506C14848B17930009900009900009900009900009E44B8062235F52BC8E0448800448800448800448800448A02002141B05E1E2C5244002244002244002244002244002F912A0D8C89714AF230112200112200112200112200112288800C54641B87831099000099000099000099000099040BE042836F225C5EB4880044880044880044880044880040A2240B151102E5E4C0224400224400224400224400224902F018A8D7C49F13A12200112200112E82504A221F8D47FC0DD0E5F08D12E6B663322012F4A54996D11044A4BE10BD57759E97DA7A03634376E4534BA0DBB5B623DD06CD978B06E29EA9A7BA2EE1CDD6D5E8B485D531198F4E23E17A1B7477491141B47F4F0B2732440022440024722012D36BAD9F0EFED6243B7AF04A58108DABA64CCDBD1B67D09DE7CC28FC1033C0E61F71D9C36EC6184220D6869EF44457A0CBD08449A7316D21609A0B4642842D17C7B668BC4D200226D6D888686A2447FCE5955611714714E14DEE7C29ACEABBB9100C54637C2665524400224400224D015042836D22976A9D8388CE695E370C5778EC251C75F817B9F0B21BCA00691C87C84A7BE88C0B03231FE7F882B82CBD0DC19C191E75C28DCF0768A8D3C2BE9C865141B1DA1D6FFEEA1D8E87F63CE1E930009900009F47102B9C48636023DF0FA47C1EF554FE5D5E749622C4F4A7CAFA8425447E5B4221A196FFF6E8566797C4FA23ADA2AE732855115CF988D45AB31CE5F16F724787C95A832613ACD750807FDF0C6C3C754BFC623A2DA9A2A3662515457FAE0B1AF4D2A47CE45C639CAF1F810A8AA93DE5AAFF66DEFE28FDF1D00DF131FA0BEA50D2DD165786FDA5B98A9BC19871A511759800F43A330E8A8D370E787DBD16E38794720106F7B19FCA15AAB4C331EBE2176DB0723387B7CDCB3618909B93E3022DE373306D639133297C913A2428E428931F40E87DF778CEDCD707A36EC71F30E81CF9E1765C15AC4B2B192E6278FC931F055CE45B4D5F2249950BE748F92A9CBC9C4BED7448365A837BF3EF7F1BFE1FED47C8A8DFE34DAEC2B0990000990C01141204BCE8636FA6CC3BBC45B819018EAB1BA20CAD47FF0BD231116C3DCFA6E1BAE4D61F83D62B407AA2DC338B611A1F263E0F187D194516C148B6223C2FE01D2CE4A44547E42F30A0495D15C16445DAC0575C1C1F13E6891141E29C6B907DA604E121BBB25D76490E3DA26D406CBE1F10C47A8A1054DE10A112183248469B774C43E572202A0AE45BEEFC087779C82EFDE300D5BDA0E61FBFCC7719178382CA3FA87B864E845F86EC96F31A17E8FB467088EBE780236B5DB86B512755AC499B61D83F2D0C678DB4AECFA9BD7AE45B4211146658CEBB8C86BAE4640C48035064AABE408A332639854B7B4372D8CCAB4D36E57F346D4491E4A665622E2ECF9608D89E4AE442A5398670AE74BD4A5C5494C0491BE7700FCE146E955B6316ACDDDE7624D4196DBF5042836BA9E294B2401122001122081A212C8CBB3E1300253C35D92BEABA7E2EF635220E105D086B54E34EFEE04F17A847CA5F09487D0904F2EB45360383F6BE3DB18B5F648D8DE85B2E00A34A81C066DF82BEF4DF2AB7DDB340C3BFA7C3CB36A9FEDE1F82E2E7AE22344958763E3240CF3089B1F3E8EA587C50E57A2EDE81B3163E71E2B91DE5381709369B82D9C9450B2BD00463CE81A1D391B969870B637D973945D6CD89E0BB7BA33890DE7B55959D5A24D0BD30C82226B1895DD07675D798F512D5A7309ACA2FE81B1F02E2540B1D1A538591809900009900009149F40578A0D6D6CDA4FBA75CB1D02A3DBC586F108282F820A911A8BD094881DEEA5DA6609A3502824C7D878D890D39BA33EB724851E25AFDAA5CEB746AB50A1C38894B7C78F6068BA158A250151FB3EBC0BFF7AD2335875D8F6A49CF03022FB4D62862D866C8F83251854E2B67A4A6FAFDA151F7DFB7E65F0B7B824AFA7890D6788542162C3BE567B7F4CE5CED02997302A47B27872C8523A2B8B6586F0AD7CC4863331DD2136728D9175BE90A4F8E2FFD9B1860E12A0D8E82038DE46022440022440023D45A00BC546FA53F39E141B36509553312594F0B6E8109E16F1480C97F02789FB0FBC2262630AC2ABAA122B50A519B2B9567A52B92AD3119A14804F792B745855A3B57293F6EA6CC1B4A1DFC5BFDEF121F699713EBC02CF9C548A73C77F2AB2C41626FF2B62E2A09399B9B84862C384C8993C8E523F02A344E8744A6C646665CD8F62898D5CF5526CF4D43F315D5A2FC54697E2646124400224400224507C025D2836ACFC0D8767C3CE17E89930AA74747163B77A1182659EE4A56DB557C65EEE362D44C7CEE5C86734E2F72E40BD121BE78EC7FAF654B171184D0B1EC409253F135122991487EA30FEE21FA2EC9965923DE21232043B8C2ABE4749CAB2BC5DE6D9E8401895D3DB60E77BE8BC171756D6FCE844185506CF465B8E7A73E6A9E433AEBCA67710A0D8E81DE3C0569000099000099040DE04B26DEAA7424FEA17246FC0973567A30975A18AC40A4F6625A3AC391B455A8D2A56AB0585C7370EB57A033B93D82DB910BB56259F6BAE45C85EF929358CAACD241F7BCA11AC5529D6E2C5A87E523C182A2FA2DE4A348F9F93D0ACDA71F6B9461C5EF50C4EF2DC2C79182DD812BA1A477FE7B778FCDD8FF0E1D487E1D389E2E723202B533D3DF4241C7DCE5358DA2CC91B466C28AF8B4E8636E160761E86DBB2BC058B0D93C0EE324B5213C4755FB3248827EDB961276ABBB29244EEA404F198AC4C6585A0E9FC93781E8C9B507199236E49FC19EAB5C446963EE7FDC7C20B7B9C00C5468F0F011B400224400224400224A009A89C8C1908A815A84C9890E4548C8B44E54CCAF2AE6AA9D8E078EB5A4712767C09565926B7CA99F4EE5CDE36F59C2A6B5CB5951BD2BA048F0FFC0ECE118F454BDB6684FF7C815E7AF6A8E37F8FC75E7D0243B5E0F857786F902577B76C40E4EDE95850BFDD4E101F02FF087BD95E677D9D141BF155B9326EE498C22DD7D2B7A91BFC6563A54625693962954B13B27733372B79D90B0A2479DC72890DA5D1B28D91BD1259776F5EC9BFC4AE2740B1D1F54C59220990000990000990405F257018BBE63F84D38EFE196E18BF5056A13A88E6C66DD8DDA29488EC2ADEBC03D146591438BA10E36FF899EDDDB057A32AC62EDD7D1523DB4D028600C506E7020990000990000990000938091C943D3C1EC439A5E2C5282DC3D03B1E4570C264BD0AD6E4F14FE3CF375F8401471D85EF5C311635DB0FAA47F49667836283D38804D209506C705690000990000990000990402A01F16234AEC4CCF101F8870E8657090F154E35E01C5CE90F607CB80EBBDBCC92B8141B9C3F24909100C50627070990000990000990000990000990405108506C14052B0B250112200112200112200112200112A0D8E01C200112200112200112200112200112280A018A8DA26065A1244002244002244002244002244002141B9C03244002244002244002244002244002452140B15114AC2C94044880044880044880044880044880628373800448800448800448800448800448A0280428368A829585920009900009900009900009900009506C700E90000990000990401F2210AB45B0CC038F3F8C26D36CFBB79292A10845DBEC5F63680A57C0533218C1BA96CC1D6C8B20505A0A5FA8BE0F41E8EB4D6D425D64ADEC3BDEDB5EC56C5731CBEE6D1CD99E2402141B9C1024400224400224D09708D8BB5597055117B3DBDD1486DF53223B5C0F803FDC68FFE8729D5B372936BA60F0DB100D0D4549690011A3F532966A8F8B2F8468BE35EB312A4169208236D423E42BB53FE75B403ED775A05DF914ABAF2966D979378217F614018A8D9E22CF7A49800448800448A023048CC722E1C5688B0450AAFE835EE24159B0165A83D8DE0ECB40CDF2A2D8E8C82074E29E0E18DE4962A3135567BDB503EDCABB29C52C3BEF46F0C29E2240B1D153E4592F09900009900009749080F664182F460BEA828351E2390FBE21C7A0C43C314FBAA615D1C878F8BD1E1124224A3C3E04AAEAAC301E6DC87AE0F58F8A9FF7F89E4475B435BD7145367A6375419439C3BEA221F84A9C215E565FBFE9F56260175E9726C85C9894782B10AAB302D72C71E785CFE7B578963D83D9138D67C336ACBD2310F09759E74B8E81AF722EA231FB9CFE4D8E8C9E1009390A55C06B0B48EF8811F089E72ACDB361DAE91B625F6B85CCC5A27351E993B960EA0ECC405DB37183A5CF85CAEA3A5407ECBE646897D5E732F80323ECBA4A903C4FA4DCEA27753B93E758BE7DEEE0DF026FEBFD0428367AFF18B185244002244002249044407B2D4A6D2F4623C2FE0192C351857A95A3E1A940B829661BC496F7C332E2C5E00DAE1081618C425BACD802226E4C37AF40500C554F79080DC63EED2EFC5A201971613C3862D49AFC14DDEF81F04F7AA96BAF8B879ED91D354CC4B8AE083720166B40B84284836738420DAD365B699766D484B5750D6888875119E3DA088C189A239562A01B7198EB29BFE9774ADD62B0B98B0D251E55BB5AD0BC762DA24DD508784BE1ADA8127123FD4919CF584308E51E1197816A990BBB11090C1271A0444AA37C16C19121BCCB78CFE202A359D56372874C1FED368B6FADB9769C088F63501EDAA8BE652DBBBBA617EBE92102141B3D049ED592000990000990408709D8DE0C95B7B1DBE1E5D0C6BA178148A3E5EDD0791D8E6BE3E2C1081449324F0BA34A0FD3EA70330BBED1D12ED8ED763C69D7A2498BA96DB6C05249F25D715D8AAAB2C5465212BE666B85A9B5EAA7FCCEFC1867CE866D58DBA2CF768538722E7219DE564E467ADD99C546E25A7BEC9C75C73D314A50ECB6E645D242026690B2B7CB121B2E3941DA3BE31CB794F2F41CCCD5E782270A6FE84B042836FAD268B1AD24400224400224A008248CCAAAEAB1B601AE0C6665A81E2306F1744CD2EF2A7F23258CC584F0E8F01F31045BD357A3B20CCB1CAB581565201C467BABF16248F8972761285B8675575F97D219B73C16470E4C8B1D46158898F5A45CC48633442A29FC2C87E16D8746C5736F74D39C49E18ECF69616D59C65AC2BE0291AD9687C1357C2B1FB1A1CA307DB6AF5765B55809EC56D856CAA1EBA2D828CA9F4B5F299462A3AF8C14DB49022440022440020E02764E86CF37C8F114DC7ACAEF19E2C390D49C8E4CAB1FF52ACF86C82893B7513DCD16197522A006C8D2BC1FCABB37BEDA56575F9734B7BA4D6CA48A0331E61784750E4DA7C446C65C108740485B35A0F36223F36204141BFDFADF2E8A8D7E3DFCEC3C0990000990409F25603DE14E5A814AF93CB4B1AEE2F8ADDC0DED075171FAA5E508D65A09CEB168152A545CBF8ADB3721432629BC27733674E3AC7C9481DE81F038C2C0B4802A4DF4A9CBAF73CE838C6154563E8949102F8A67C3F6621412469530F273EDAD6242CE9CFBB1988E77426CD8615456D89EDB1F14C5469FFD67A62B1A4EB1D115145906099000099000097437016338A6843B990DFE920CBF94158854B2B859A1C80EDBD1C6BD59FDC83F1E911E588DCA229878DA6F19DC2E89E245B9CE317E26415C56EDAAAC8E3A12C49DC9F7CE90A20E84516534CC5313C4A3A8AEF4E9B1C994209EE4513089DBBE71A8552B50C5ECFB6DF1999C202EF3223CD24E5E5F6F8558656857BAC0727A494C82B8598440556BAD88658D619E7BBE74F79F10EBEB1E02141BDDC399B5900009900009900009E443C018DB8E64E4A4657C4D195D7C9D7399DDB8B7670446D8CB057B7C95A84A59FAB6639E0DB35293CA6D70F330A8FEC9D2B75595F632B2B27254D6A56F8D08497069AE9B81407CE95BB5446DA2ED50AB9139974156CBD9866A450EA4B6ABC5DAA8D06E6376B191DA667BB9DFF892BBF9F4399FB9C16BFA24018A8D3E396C6C34099000099000099040B10814793F9162359BE59240AF2440B1D12B87858D220112200112200112E82902141B3D459EF51E890428368EC451659F4880044880044880043A4C8062A3C3E8782309A411A0D8E0A4200112200112200112200112200112280A018A8DA26065A1244002244002244002244002244002141B9C03244002244002244002244002244002452140B15114AC2C94044880044880044880044880044880628373800448800448800448800448800448A0280428368A829585920009900009900009900009900009506C700E900009900009900009900009900009148500C54651B0B2501220011220011220011220011220018A0DCE01122001122001122001122001122081A210A0D8280A56164A022440022440022440022440022440B1C139400224400224400224400224400224501402141B45C1CA42498004488004488004488004488004283638074880044880044880044880044880048A428062A328585928099000099000099000099000099000C506E7000990000990000990000990000990405108506C14052B0B250112200112200112200112200112A0D8E01C200112200112200112200112200112280A018A8DA26065A124400224400224D0A5044AD47FB0799001E700E7803D07BAF41F98621646B1514CBA2C9B04488004488004488004488004FA31018A8D7E3CF8EC3A09900009900009900009900009149300C54631E9B26C12200112200112200112200112E8C7042836FAF1E0B3EB24400224400224400224400224504C02141BC5A4CBB249800448800448800448800448A01F13A0D8E8C783CFAE93000990000990000990000990403109506C14932ECB26011220011220011220011220817E4C202E36E4832C5CCC830C3807380738073807380738073807380738073807BA6C0EFCFF97DB9582AB4DE2100000000049454E44AE426082</Control>
      <Control Type="AR.Field" Name="txtHiddenPicture" DataField="ReportBanner" Visible="0" Left="10709.28" Top="3948.48" Width="388.7996" Height="288" Text="text" />
      <Control Type="AR.Field" Name="TextBox20" DataField="PostCode" Left="119.5203" Top="4327.2" Width="1409.76" Height="243.3591" Text="txtAddress31" Style="font-weight: bold" />
      <Control Type="AR.Field" Name="TextBox22" DataField="City" Left="119.5203" Top="4042.08" Width="1409.76" Height="243.3591" Text="txtAddress31" Style="font-weight: bold" />
      <Control Type="AR.Field" Name="TextBox3" Left="119.5203" Top="2880" Width="1589.76" Height="233.2798" Text="Billing Address:" Style="color: DarkGray; font-size: 8pt; font-weight: normal" />
      <Control Type="AR.Field" Name="TextBox5" Left="3913.92" Top="2880" Width="1935.36" Height="233.2798" Text="Shipping Address:" Style="color: DarkGray; font-size: 8pt; font-weight: normal" />
      <Control Type="AR.Field" Name="TextBox16" DataField="BAddress2" Left="3913.92" Top="3758.4" Width="3540.96" Height="259.201" Text="txtAddress21" Style="font-weight: bold" />
      <Control Type="AR.Field" Name="TextBox17" DataField="BPostCode" Left="3913.92" Top="4327.2" Width="1320.48" Height="259.201" Text="txtAddress31" Style="font-weight: bold" />
      <Control Type="AR.Field" Name="TextBox19" DataField="BCity" Left="3913.92" Top="4042.08" Width="1306.08" Height="259.201" Text="txtAddress31" Style="font-weight: bold" />
      <Control Type="AR.Field" Name="TextBox15" DataField="BState" Left="5361.122" Top="4042.08" Width="1080" Height="259.201" Text="txtAddress31" Style="font-weight: bold" />
      <Control Type="AR.Field" Name="TextBox14" DataField="BAddressName" Left="3913.92" Top="3208.32" Width="3540.96" Height="259.201" Text="txtAddressName1" Style="font-weight: bold" />
      <Control Type="AR.Field" Name="TextBox13" DataField="BAddress1" Left="3913.92" Top="3467.52" Width="3540.96" Height="259.201" Text="txtAddress11" Style="font-weight: bold" />
      <Control Type="AR.Field" Name="TextBox21" DataField="CustomerPO" Left="8808.48" Top="4042.08" Width="1938.24" Height="288" Text="txtOrder_Code1" />
      <Control Type="AR.Label" Name="Label9" Left="7390.082" Top="4042.08" Width="1308.96" Height="288" Caption="Customer PO:" Style="color: Black; font-size: 8pt; font-weight: normal; text-align: right; vertical-align: middle" />
      <Control Type="AR.Field" Name="TextBox6" DataField="Estimate_Code" Left="8820.001" Top="4320" Width="1938.24" Height="288" Text="txtEstimate_Code1" />
      <Control Type="AR.Label" Name="Label7" DataField="EstimateCodeLabel" Left="7401.601" Top="4320" Width="1308.96" Height="288" Caption="lblEstimateCode" Style="color: Black; font-size: 8pt; font-weight: normal; text-align: right; vertical-align: middle" />
    </Section>
    <Section Type="GroupHeader" Name="GroupHeader1" Height="428.64" BackColor="16777215">
      <Control Type="AR.Shape" Name="Shape9" Left="89.28005" Top="0" Width="11070.72" Height="400.3199" BackColor="0" BackStyle="1" RoundingRadius="9.999999" />
      <Control Type="AR.Field" Name="TextBox18" Left="3780" Top="72" Width="2731.68" Height="288" Text="Items Ordered" Style="color: White; font-weight: bold; text-align: center" />
    </Section>
    <Section Type="Detail" Name="Detail1" Height="1589.68" BackColor="16777215">
      <Control Type="AR.Label" Name="label5" Left="7724.161" Top="779.04" Width="974.88" Height="288" Caption="Quantity:" Style="color: DarkGray; font-size: 8pt; font-weight: normal; text-align: right" />
      <Control Type="AR.Field" Name="txtQty11" DataField="Qty1" Left="8808.48" Top="779.04" Width="1634.4" Height="288" Text="txtQty11" Style="font-weight: bold; text-align: left" />
      <Control Type="AR.Label" Name="Label4" Left="7724.161" Top="1152" Width="974.88" Height="230.4001" Caption="Total:" Style="color: DarkGray; font-size: 8pt; font-weight: normal; text-align: right" />
      <Control Type="AR.Field" Name="txtQty1NetTotal1" DataField="Qty1NetTotal" Left="9110.88" Top="1152" Width="1166.401" Height="259.2" Text="00.00" OutputFormat="#,##0.00" Style="font-weight: bold; text-align: right" />
      <Control Type="AR.Line" Name="line1" X1="11160" Y1="1579.68" X2="89.27971" Y2="1579.68" />
      <Control Type="AR.Label" Name="Label8" Left="133.92" Top="28.8" Width="1499.04" Height="288" Caption="Product Name:" Style="color: Black; font-size: 9pt; font-weight: normal; text-align: right" />
      <Control Type="AR.Field" Name="TextBox2" DataField="FullProductName" Left="1912.32" Top="28.8" Width="9254.88" Height="288" Text="txtProductName" Style="color: Red; font-weight: bold" />
      <Control Type="AR.Label" Name="Label10" Left="10258.56" Top="1152" Width="950.4001" Height="259.2" Caption=" (Ex. Tax)" Style="font-weight: bold" />
      <Control Type="AR.Field" Name="TextBox1" DataField="StockName" Left="1912.32" Top="779.04" Width="5253.121" Height="288" Text="stock" Style="font-weight: bold" />
      <Control Type="AR.Label" Name="Label11" Left="912.9597" Top="779.04" Width="720.0002" Height="288" Caption="Stock:" Style="color: DarkGray; font-size: 8pt; font-weight: normal; text-align: right" />
      <Control Type="AR.Field" Name="TextBox30" DataField="JobDescriptionTitle7" Left="112.3204" Top="678.2401" Width="1520.64" Height="14.4" Text="txtJob Description" Style="color: DarkGray; font-size: 8pt; text-align: right; ddo-char-set: 0" />
      <Control Type="AR.Field" Name="TextBox29" DataField="JobDescriptionTitle1" Left="112.3204" Top="508.3201" Width="1520.64" Height="14.4" Text="txtJob Description" Style="color: DarkGray; font-size: 8pt; text-align: right; ddo-char-set: 0" />
      <Control Type="AR.Field" Name="TextBox33" DataField="JobDescriptionTitle2" Left="112.3204" Top="537.1201" Width="1520.64" Height="14.4" Text="txtJob Description" Style="color: DarkGray; font-size: 8pt; text-align: right; ddo-char-set: 0" />
      <Control Type="AR.Field" Name="TextBox24" DataField="JobDescription7" Left="1912.32" Top="678.3999" Width="9254.88" Height="14.4" Text="txtJob Description" Style="font-size: 10pt" />
      <Control Type="AR.Field" Name="TextBox23" DataField="JobDescription3" Left="1912.32" Top="558.2799" Width="9254.88" Height="14.4" Text="txtJob Description" Style="font-size: 10pt" />
      <Control Type="AR.Field" Name="TextBox31" DataField="JobDescriptionTitle5" Left="112.3204" Top="617.76" Width="1520.64" Height="14.4" Text="txtJob Description" Style="color: DarkGray; font-size: 8pt; text-align: right; ddo-char-set: 0" />
      <Control Type="AR.Field" Name="TextBox32" DataField="JobDescriptionTitle4" Left="112.3204" Top="587.8401" Width="1520.64" Height="14.4" Text="txtJob Description" Style="color: DarkGray; font-size: 8pt; text-align: right; ddo-char-set: 0" />
      <Control Type="AR.Field" Name="TextBox27" DataField="JobDescription2" Left="1912.32" Top="537.2799" Width="9254.88" Height="14.4" Text="txtJob Description" Style="font-size: 10pt" />
      <Control Type="AR.Field" Name="TextBox25" DataField="JobDescription4" Left="1912.32" Top="587.9999" Width="9254.88" Height="14.4" Text="txtJob Description" Style="font-size: 10pt" />
      <Control Type="AR.Field" Name="TextBox26" DataField="JobDescription1" Left="1912.32" Top="508.4799" Width="9254.88" Height="14.4" Text="txtJob Description" Style="font-size: 10pt" />
      <Control Type="AR.Field" Name="TextBox28" DataField="JobDescription5" Left="1912.32" Top="617.9198" Width="9254.88" Height="14.4" Text="txtJob Description" Style="font-size: 10pt" />
      <Control Type="AR.Field" Name="TextBox34" DataField="JobDescription6" Left="1912.32" Top="648" Width="9254.88" Height="14.4" Text="txtJob Description" Style="font-size: 10pt" />
      <Control Type="AR.Field" Name="TextBox35" DataField="JobDescriptionTitle3" Left="112.3204" Top="558.1201" Width="1520.64" Height="14.4" Text="txtJob Description" Style="color: DarkGray; font-size: 8pt; text-align: right; ddo-char-set: 0" />
      <Control Type="AR.Field" Name="TextBox36" DataField="JobDescriptionTitle6" Left="112.3204" Top="647.8403" Width="1520.64" Height="14.4" Text="txtJob Description" Style="color: DarkGray; font-size: 8pt; text-align: right; ddo-char-set: 0" />
      <Control Type="AR.Field" Name="TextBox38" DataField="CurrencySymbol" Left="8808.48" Top="1152" Width="289.4403" Height="259.2" Text="txtCurrency" OutputFormat="#,##0.00" Style="font-weight: bold; text-align: left" />
    </Section>
    <Section Type="GroupFooter" Name="GroupFooter1" Height="1912.44" BackColor="16777215">
      <Control Type="AR.Shape" Name="Shape3" Left="89.27971" Top="0" Width="11070.72" Height="1886.4" BackColor="13882323" BackStyle="1" LineWeight="0" RoundingRadius="9.999999" />
      <Control Type="AR.Label" Name="Label12" Left="102.239" Top="25.92" Width="1530.721" Height="243.36" Caption="Payment  Type:" Style="font-size: 9pt; font-weight: normal; text-align: right" />
      <Control Type="AR.Field" Name="TextBox8" DataField="PaymentDate" Left="1912.32" Top="316.8" Width="2609.28" Height="266.4" Text="PaymentDate" OutputFormat="dd MMM yyyy" Style="font-weight: bold" />
      <Control Type="AR.Label" Name="Label13" Left="102.239" Top="316.8" Width="1530.721" Height="243.36" Caption="Payment Date:" Style="font-size: 9pt; font-weight: normal; text-align: right" />
      <Control Type="AR.Label" Name="Label14" Left="-77.76003" Top="614.88" Width="1710.72" Height="243.36" Caption="Payment Ref. No." Style="font-size: 9pt; font-weight: normal; text-align: right" />
      <Control Type="AR.Label" Name="Label15" DataField="TaxLabel" Left="7932.96" Top="25.92001" Width="766.08" Height="288" Caption="Tax:" Style="font-size: 9pt; font-weight: normal; text-align: right" />
      <Control Type="AR.Label" Name="Label16" Left="7392.96" Top="316.8" Width="1306.08" Height="288" Caption="Gross Total:" Style="font-size: 9pt; font-weight: normal; text-align: right" />
      <Control Type="AR.Field" Name="TextBox9" DataField="Qty1Tax1Value" Left="9110.88" Top="25.92001" Width="1166.401" Height="288" Text="00.00" OutputFormat="#,##00.00" Style="font-weight: bold; text-align: right" SummaryType="1" SummaryRunning="2" />
      <Control Type="AR.Field" Name="TextBox10" DataField="Estimate_Total" Left="9110.88" Top="316.8" Width="1166.401" Height="288" Text="00.00" OutputFormat="#,##0.00" Style="font-weight: bold; text-align: right" />
      <Control Type="AR.Field" Name="TextBox11" DataField="paymentType" Left="1912.32" Top="25.92" Width="2609.28" Height="266.4" Text="PaymentType" OutputFormat="dd MMM yyyy" Style="font-weight: bold" />
      <Control Type="AR.Field" Name="TextBox12" DataField="paymentRefNo" Left="1912.32" Top="629.28" Width="2609.28" Height="266.4" Text="" OutputFormat="" Style="font-weight: bold" />
      <Control Type="AR.Field" Name="TextBox7" DataField="UserNotes" Left="1912.32" Top="931.68" Width="3816" Height="907.2002" Text="" Style="color: White; font-weight: bold; text-align: center" />
      <Control Type="AR.Label" Name="Label6" Left="-77.76003" Top="931.68" Width="1710.72" Height="243.36" Caption="User Notes:" Style="font-size: 9pt; font-weight: normal; text-align: right" />
      <Control Type="AR.Field" Name="TextBox39" DataField="CurrencySymbol" Left="8808.48" Top="25.92001" Width="289.4403" Height="288" Text="txtCurrency" OutputFormat="#,##0.00" Style="font-weight: bold; text-align: left" />
      <Control Type="AR.Field" Name="TextBox40" DataField="CurrencySymbol" Left="8808.48" Top="316.8" Width="289.4403" Height="288" Text="txtCurrency" OutputFormat="#,##0.00" Style="font-weight: bold; text-align: left" />
    </Section>
    <Section Type="PageFooter" Name="PageFooter1" Height="419.76" BackColor="16777215">
      <Control Type="AR.ReportInfo" Name="reportInfo1" Left="8670.24" Top="146.88" Width="1800" Height="288" FormatString="Page {PageNumber} of {PageCount}" Style="text-align: right" />
      <Control Type="AR.Label" Name="Label20" Left="180" Top="146.88" Width="900.0001" Height="208.8" Caption="Printed:" Style="font-size: 10pt; font-style: normal; font-weight: normal; vertical-align: middle" />
      <Control Type="AR.ReportInfo" Name="reportInfo2" Left="914.4001" Top="146.88" Width="2383.2" Height="208.8" FormatString="{RunDateTime:dd-MMM-yyyy}" Style="font-size: 8pt; text-align: left" />
    </Section>
    <Section Type="ReportFooter" Name="ReportFooter1" Height="45" BackColor="16777215" />
  </Sections>
  <ReportComponentTray />
  <Script><![CDATA[public void Detail1_Format()
{
	
}




]]></Script>
  <PageSettings LeftMargin="432" RightMargin="432" TopMargin="432" BottomMargin="432" />
  <Parameters />
</ActiveReportsLayout>' where ReportId = 103


/* Execution Date: 22/04/2015 */

update fieldVariable set VariableType = 45 where VariableType = 1 and IsSystem =1 



/* Execution Date: 23/04/2015 */
USE [MPCLive]
GO

/****** Object:  StoredProcedure [dbo].[sp_cloneTemplate]    Script Date: 23/04/2015 05:23:02 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


----------------------------------------- Alter Store Procedure sp_cloneTemplate ----------------------
ALTER PROCEDURE [dbo].[sp_cloneTemplate] 
	-- Add the parameters for the stored procedure here
	@TemplateID bigint,
	@submittedBy bigint,
    @submittedByName nvarchar(100)
AS
BEGIN


      
	declare @NewTemplateID bigint
	declare @NewCode nvarchar(10)
	--DECLARE  @WaterMarkTxt as [dbo].[tbl_company_sites]
	
	set @NewCode = ''
	
	--INSERT INTO @WaterMarkTxt (CompanySiteName)
	--Select Top 1 CompanySiteName from tbl_company_sites

	
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	INSERT INTO [dbo].[Template]
           ([Code]
           ,[ProductName]
           ,[Description]
           ,[ProductCategoryId]
           ,[Thumbnail]
           ,[Image]
           ,[IsDisabled]

           ,[PDFTemplateWidth]
           ,[PDFTemplateHeight]

           ,[CuttingMargin]
           ,[MultiPageCount]
           ,[Orientation]
           ,[MatchingSetTheme]
           ,[BaseColorId]
           ,[SubmittedBy]
           ,[SubmittedByName]
           ,[SubmitDate]
           ,[Status]
           ,[ApprovedBy]
           ,[ApprovedByName]
           ,[UserRating]
           ,[UsedCount]
           ,[MPCRating]
           ,[RejectionReason]
           ,[ApprovalDate]
           ,[IsCorporateEditable]
           ,[MatchingSetId]
           ,[TempString],[TemplateType],[isSpotTemplate],[isWatermarkText],[isCreatedManual]
          )
     



   
	SELECT 
      @NewCode
      ,[ProductName] + ' Copy'
      ,[Description]
      ,[ProductCategoryID]
      
      ,[Thumbnail]
      ,[Image]
      ,[IsDisabled]

      ,[PDFTemplateWidth]
      ,[PDFTemplateHeight]


      ,[CuttingMargin]
      ,[MultiPageCount]
      ,[Orientation]
      ,[MatchingSetTheme]
      ,[BaseColorId]
      ,@submittedBy
      ,@submittedByName
      ,NULL
      ,1
      ,NULL
      ,NULL
      ,0
      ,0
      ,0
      ,''
      ,NULL,IsCorporateEditable,MatchingSetId,'',[TemplateType],isSpotTemplate,isWatermarkText,isCreatedManual
      
  FROM [dbo].[Template] where productid = @TemplateID
	
	
	set @NewTemplateID = SCOPE_IDENTITY() 
	
	-- updating water mark text 
	UPDATE [dbo].[Template]
	SET TempString= (Select Top 1 OrganisationName from organisation)
	WHERE productid = @NewTemplateID
	
	--copying the pages
	INSERT INTO [dbo].[TemplatePage]
           ([ProductId]
           ,[PageNo]
           ,[PageType]
           ,[Orientation]
           ,[BackGroundType]
           ,[BackgroundFileName]
      ,[ColorC]
      ,[ColorM]
      ,[ColorY]
      ,[ColorK]
      ,[IsPrintable]
           ,[PageName],[hasOverlayObjects],[Width],[Height])

SELECT 
      @NewTemplateID
      ,[PageNo]
      ,[PageType]
      ,[Orientation]
      ,[BackGroundType]
      ,[BackgroundFileName]
      ,[ColorC]
      ,[ColorM]
      ,[ColorY]
      ,[ColorK]
      ,[IsPrintable]
      
      ,[PageName],[hasOverlayObjects],[Width],[Height]
  FROM [dbo].[TemplatePage]
where productid = @TemplateID


	--copying the objects
	INSERT INTO [dbo].[TemplateObject]
           ([ObjectType]
           ,[Name]
           ,[IsEditable]
           ,[IsHidden]
           ,[IsMandatory]
           
           ,[PositionX]
           ,[PositionY]
           ,[MaxHeight]
           ,[MaxWidth]
           ,[MaxCharacters]
           ,[RotationAngle]
           ,[IsFontCustom]
           ,[IsFontNamePrivate]
           ,[FontName]
           ,[FontSize]
           ,[IsBold]
           ,[IsItalic]
           ,[Allignment]
           ,[VAllignment]
           ,[Indent]
           ,[IsUnderlinedText]
           ,[ColorType]
 
           ,[ColorName]
           ,[ColorC]
           ,[ColorM]
           ,[ColorY]
           ,[ColorK]
           ,[Tint]
           ,[IsSpotColor]
           ,[SpotColorName]
           ,[ContentString]
           ,[ContentCaseType]
           ,[ProductID]
           ,[DisplayOrderPdf]
           ,[DisplayOrderTxtControl]
           ,[RColor]
           ,[GColor]
           ,[BColor]
           ,[LineSpacing]
           ,[ProductPageId]
           ,[ParentId]
           ,CircleRadiusX
           ,Opacity
           ,[ExField1],
           [IsTextEditable],
           [IsPositionLocked],
           [CircleRadiusY]
          ,[ExField2],
           ColorHex
           ,[IsQuickText]
           ,[QuickTextOrder],
		   [watermarkText],
		   [textStyles],[charspacing],[AutoShrinkText],
		   [IsOverlayObject],[ClippedInfo],[originalContentString],[originalTextStyles]
           )
	SELECT 
      O.[ObjectType]
      ,O.[Name]
      ,O.[IsEditable]
      ,O.[IsHidden]
      ,O.[IsMandatory]
      
      ,O.[PositionX]
      ,O.[PositionY]
      ,O.[MaxHeight]
      ,O.[MaxWidth]
      ,O.[MaxCharacters]
      ,O.[RotationAngle]
      ,O.[IsFontCustom]
      ,O.[IsFontNamePrivate]
      ,O.[FontName]
      ,O.[FontSize]
      ,O.[IsBold]
      ,O.[IsItalic]
      ,O.[Allignment]
      ,O.[VAllignment]
      ,O.[Indent]
      ,O.[IsUnderlinedText]
      ,O.[ColorType]
      ,O.[ColorName]
      ,O.[ColorC]
      ,O.[ColorM]
      ,O.[ColorY]
      ,O.[ColorK]
      ,O.[Tint]
      ,O.[IsSpotColor]
      ,O.[SpotColorName]
      ,O.[ContentString]
      ,O.[ContentCaseType]
      ,@NewTemplateID
      ,O.[DisplayOrderPdf]
      ,O.[DisplayOrderTxtControl]
      ,O.[RColor]
      ,O.[GColor]
      ,O.[BColor]
      ,O.[LineSpacing]
      ,NP.[ProductPageId]
      ,O.[ParentId]
      ,O.CircleRadiusX
      ,O.Opacity
      ,O.[ExField1],
       O.[IsTextEditable],
       O.[IsPositionLocked],
       O.[CircleRadiusY]
      ,O.[ExField2],
       O.ColorHex
       ,[IsQuickText]
        ,[QuickTextOrder],[watermarkText],O.[textStyles],
		O.[charspacing],O.[AutoShrinkText],O.[IsOverlayObject]
		,O.[ClippedInfo]
		,O.[originalContentString],O.[originalTextStyles]
  FROM [dbo].[TemplateObject] O
  inner join [dbo].[TemplatePage]  P on o.ProductPageId = p.ProductPageId and o.ProductId = @TemplateID
  inner join [dbo].[TemplatePage] NP on P.PageName = NP.PageName and P.PageNo = NP.PageNo and NP.ProductId = @NewTemplateID
  
	--theme tags
	--insert into dbo.TemplateThemeTags   ([TagID],[ProductID])
	--select [TagID] ,@NewTemplateID from dbo.TemplateThemeTags where ProductID = @TemplateID
	
	---- industry tags
	--insert into dbo.TemplateIndustryTags   ([TagID],[ProductID])
	--select [TagID] ,@NewTemplateID from dbo.TemplateIndustryTags where ProductID = @TemplateID

	INSERT INTO [dbo].[TemplateBackgroundImage]
			   ([ProductId]
			   ,[ImageName]
			   ,[Name]
			   ,[flgPhotobook]
			   ,[flgCover]
			   ,[BackgroundImageAbsolutePath]
			   ,[BackgroundImageRelativePath],
			   ImageType,
			   ImageWidth,
			   ImageHeight
			   
			   )
	SELECT 
		  @NewTemplateID
		  ,[ImageName]
		  ,[Name]
		  ,[flgPhotobook]
		  ,[flgCover]
		  ,[BackgroundImageAbsolutePath]
		  ,[BackgroundImageRelativePath]
		  ,ImageType,
			   ImageWidth,
			   ImageHeight
	  FROM [dbo].[TemplateBackgroundImage] where ProductId = @TemplateID




select @NewTemplateID
	
END






GO


/* Execution Date : 24/04/2015 */

GO

alter table ShippingInformation
add EstimateId bigint null

alter table ShippingInformation
add constraint FK_ShippingInformation_Estimate
foreign key (EstimateId)
references Estimate (EstimateId)

GO

/* Execution Date: 29/04/2105 */

GO

alter table ReportNote
add CompanyId bigint null

update purchase
set createdby = null

alter table purchase
drop constraint DF__tbl_purch__Creat__7E22B05D

alter table Purchase
alter Column CreatedBy nvarchar null

alter table Purchase
alter Column CreatedBy uniqueidentifier null

GO
/****** Object:  StoredProcedure [dbo].[usp_EstimateReport]    Script Date: 4/29/2015 2:10:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[usp_EstimateReport]
 @OrganisationId bigint,
 @EstimateID bigint
AS
Begin

SELECT  i.ItemID,i.Title, i.Qty1 as Qty1, i.Qty2 as Qty2, i.Qty3 as Qty3, i.Qty1NetTotal as Qty1NetTotal,Company.OrganisationId,
  
    i.Qty2NetTotal as Qty2NetTotal, i.Qty3NetTotal as Qty3NetTotal, 
                      i.JobDescription, i.JobDescription1 as JobDescription1,		 
                      i.JobDescription2 as JobDescription2 ,i.JobDescription3 as JobDescription3,i.JobDescription4  as JobDescription4,
                      i.JobDescription5 as JobDescription5 ,i.JobDescription6 as JobDescription6 ,i.JobDescription7 as JobDescription7,
                       CASE 
						  WHEN i.JobDescription1 is null then null
						  WHEN i.JobDescription1 is not null THEN  i.JobDescriptionTitle1
				       END As JobDescriptionTitle1,
				        CASE 
						  WHEN i.JobDescription2 is null THEN Null
						  WHEN i.JobDescription2 is not null THEN  i.JobDescriptionTitle2
				       END As JobDescriptionTitle2,
				        CASE 
						  WHEN i.JobDescription3 is null THEN Null
						  WHEN i.JobDescription3 is not null THEN  i.JobDescriptionTitle3
				       END As JobDescriptionTitle3,
				        CASE 
						  WHEN i.JobDescription4 is null THEN Null
						  WHEN i.JobDescription4 is not null THEN  i.JobDescriptionTitle4
				       END As JobDescriptionTitle4,
				        CASE 
						  WHEN i.JobDescription5 is null THEN Null
						  WHEN i.JobDescription5 is not null THEN  i.JobDescriptionTitle5
				       END As JobDescriptionTitle5,
				        CASE 
						  WHEN i.JobDescription6 is null THEN Null
						  WHEN i.JobDescription6 is not null THEN  i.JobDescriptionTitle6
				       END As JobDescriptionTitle6,
				        CASE 
						  WHEN i.JobDescription7 is null THEN Null
						  WHEN i.JobDescription7 is not null THEN  i.JobDescriptionTitle7
				       END As JobDescriptionTitle7,
							
                       dbo.Estimate.Estimate_Name, dbo.Estimate.Estimate_Code, dbo.Estimate.Estimate_Total, dbo.Estimate.FootNotes, 
                      dbo.Estimate.HeadNotes, dbo.Estimate.EstimateDate, dbo.Estimate.Greeting, dbo.Estimate.CustomerPO, dbo.Address.AddressName, 
                      dbo.Address.Address1, dbo.Address.Address2, dbo.Address.Address3, dbo.Address.Email, dbo.Address.Fax, st.StateName, 
                      ct.CountryName, dbo.Address.URL, dbo.Address.Tel1, dbo.Company.AccountNumber, 
                      dbo.Company.Name AS CustomerName, dbo.Company.URL AS CustomerURL, dbo.Estimate.EstimateID, i.ProductName, 
                      
                       isnull((select top 1 CategoryName from ProductCategory where ProductCategoryID = pcat.ProductCategoryID),'')+ SPACE(1) + i.ProductName as FullProductName,
                       
                      dbo.CompanyContact.FirstName + ISNULL(' ' + dbo.CompanyContact.MiddleName, '') + ISNULL(' ' + dbo.CompanyContact.LastName, '') AS ContactName, 
                      dbo.SystemUser.FullName,(select top 1 ReportBanner from ReportNote where ReportCategoryID=3 and isdefault = 1) as BannerPath,
                      (select top 1 ReportTitle from ReportNote where ReportCategoryID=3 and isdefault = 1) as ReportTitle,
                      (select top 1 ISNULL(BannerAbsolutePath,'') + isnull(ReportBanner,'')  from ReportNote where ReportCategoryID=3 and isdefault = 1) as ReportBanner,
                      dbo.Address.PostCode,
                       CASE 
						  WHEN i.Qty1NetTotal is null or i.Qty1NetTotal = '' then null
						  WHEN i.Qty1NetTotal is not null THEN  (select top 1 currencysymbol from currency c inner join organisation o on o.CurrencyId = c.CurrencyId and o.OrganisationId = @OrganisationID)
				        
				       END As CurrencySymbol,
				        CASE 
						  WHEN i.Qty2NetTotal is null or i.Qty2NetTotal = '' then null
						  WHEN i.Qty2NetTotal is not null THEN  (select top 1 currencysymbol from currency c inner join organisation o on o.CurrencyId = c.CurrencyId and o.OrganisationId = @OrganisationID)
				       
				       END As CurrencySymbol2,
				        CASE 
						  WHEN i.Qty3NetTotal is null or i.Qty3NetTotal = '' then null
						  WHEN i.Qty3NetTotal is not null THEN  (select top 1 currencysymbol from currency c inner join organisation o on o.CurrencyId = c.CurrencyId and o.OrganisationId = @OrganisationID)
						 
				       END As CurrencySymbol3
                                
                      
FROM         dbo.Company INNER JOIN
                      dbo.Estimate ON dbo.Company.CompanyId = dbo.Estimate.CompanyId INNER JOIN
                      dbo.CompanyContact ON dbo.CompanyContact.ContactID = dbo.Estimate.ContactID INNER JOIN
                      dbo.Items i ON dbo.Estimate.EstimateID = i.EstimateID INNER JOIN
                      dbo.SystemUser ON dbo.Estimate.ReportSignedBy = dbo.SystemUser.SystemUserID INNER JOIN
                      dbo.Address ON dbo.Estimate.BillingAddressID = dbo.Address.AddressID
					  inner join dbo.ProductCategoryItem pc2 on i.ItemId = pc2.ItemId
					 inner join dbo.ProductCategory pcat on pc2.CategoryId = pcat.ProductCategoryId
					 inner join dbo.State st on Address.StateId = st.StateId
					 inner join dbo.Country ct on Address.CountryId = ct.CountryID 

					 where Estimate.estimateid = @EstimateID and Company.OrganisationId = @OrganisationId

End

GO
/****** Object:  StoredProcedure [dbo].[usp_PurchaseOrderReport]    Script Date: 4/29/2015 2:12:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[usp_PurchaseOrderReport]
 @OrganisationId bigint,
 @PurchaseID bigint
AS
Begin

select Purchase.PurchaseID,Purchase.Code, Purchase.date_Purchase, Purchase.SupplierID,
			Purchase.TotalPrice, Purchase.UserID,Purchase.RefNo, Purchase.ContactID, 
			Purchase.isproduct, Purchase.TotalTax, Purchase.Discount, Purchase.discountType, 
			Purchase.NetTotal, PurchaseDetail.ItemID, PurchaseDetail.quantity, 
			PurchaseDetail.price AS DetailPrice, PurchaseDetail.packqty,                         
			PurchaseDetail.ItemCode , PurchaseDetail.ItemName,
			(PurchaseDetail.TotalPrice + PurchaseDetail.NetTax) as PriceIncTax,
			Purchase.TotalTax as TaxSum, Purchase.GrandTotal as GrandTotal,
			(select '<font size=2px face="arial"><b>' + isnull(ProductCode,'N/A') + '</b> <br> <i>' + isnull(ProductName,'N/A') + '</i> <br>' + isnull(WebDescription,'N/A') + '</font>' from Items where itemid = PurchaseDetail.ItemId) as ProductDetail, 

			--(select ProductCode from tbl_items where itemid = tbl_purchasedetail.ItemId) as ProductCode,
			--(select ProductName from tbl_items where itemid = tbl_purchasedetail.ItemId) as ProductName,
			--(select WebDescription from tbl_items where itemid = tbl_purchasedetail.ItemId) as ProductDescription, 
           PurchaseDetail.TotalPrice AS DetailTotalPrice, 
			(select ReportTitle from ReportNote where ReportCategoryID=5 and isdefault = 1) as ReportTitle,
			(select ISNULL(BannerAbsolutePath,'') + isnull(ReportBanner,'')  from ReportNote where ReportCategoryID=5 and isdefault = 1) as ReportBanner,                        
			PurchaseDetail.Discount AS DetailDiscount, PurchaseDetail.NetTax AS DetailNetTax,
			PurchaseDetail.freeitems, Purchase.Comments, Purchase.FootNote, Purchase.UserNotes,
			Purchase.Status, Purchase.CreatedBy, Company.Name,                        
			(select HeadNotes from ReportNote where ReportCategoryID = 5 and isdefault = 1) as HeadNotes,
			Address.Address1, Address.Address2,
			Address.City, Address.PostCode, Country.CountryName, State.StateName,
			(isnull(Address.City, '') + space(1) + isnull(State.StateName, '') + ' ' + isnull(Address.PostCode, '') + space(1) + isnull(Country.CountryName, '')) As FullAddress,
			CompanyContact.FirstName AS SuppContactFirstName, CompanyContact.MiddleName AS SuppContactMiddleName,  
			CompanyContact.LastName AS SuppContactLastName,SystemUser.FullName,
			(CompanyContact.FirstName + space(1) + isnull(CompanyContact.LastName, '')) As SupplierContactFullName, 
			(select top 1 currencysymbol from currency c inner join organisation o on o.CurrencyId = c.CurrencyId and o.OrganisationId = @OrganisationID) as CurrencySymbol
			,est.FinishDeliveryDate as FinishDeliveryDate, est.AddressName as dAddressName, est.address1 as DAddress1
			,est.address2 As DAddress2
			,est.city As DCity
			,est.stateName As DState
			,est.PostCode As DPostCode
			,est.CountryName As DCountry
			,est.UserNotes As EstimateUserNotes
			,est.Order_Code As OrderCode
			,est.CompanyName, est.ContactFullName
     FROM  
			purchase 
			INNER JOIN   purchasedetail ON purchase.PurchaseID = purchasedetail.PurchaseID
			INNER JOIN Company ON purchase.SupplierID = Company.CompanyId and Company.iscustomer = 2
			INNER JOIN  CompanyContact ON purchase.ContactID = CompanyContact.ContactID
			INNER JOIN Address ON purchase.SupplierContactAddressID = Address.AddressID  
			inner join state on address.StateId = State.StateId
			inner join Country on address.CountryId = Country.CountryId
			inner join SystemUser on purchase.createdby = SystemUser.SystemUserID
			
			left join (select e.UserNotes, e.Order_Code, e.FinishDeliveryDate, i.ItemID, a.AddressID, a.AddressName, a.address1, a.address2, a.city, State.StateName, a.postcode, Country.CountryName
			,(c.FirstName + space(1) + isnull(c.LastName, '')) As ContactFullName, cc.Name as CompanyName
			from Estimate e 
			inner join items i on i.EstimateID = e.EstimateID and e.isEstimate = 0
			inner join address a on a.AddressID = e.AddressID
			inner join state on a.StateId = State.StateId
			inner join Country on a.CountryId = Country.CountryId
			inner join companycontact c on c.ContactID = e.ContactID
			inner join Company cc on cc.companyid = e.companyid
			)as est on est.ItemID = PurchaseDetail.ItemId where company.OrganisationId = @OrganisationId and Purchase.PurchaseId = @PurchaseID

end

GO
/****** Object:  StoredProcedure [dbo].[usp_InvoiceReport]    Script Date: 4/29/2015 2:14:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------- invoice -------------------------------


CREATE procedure [dbo].[usp_InvoiceReport]
 @Organisationid bigint,
 @InvoiceId bigint
AS
Begin

SELECT     dbo.Invoice.InvoiceID, dbo.Invoice.InvoiceCode, dbo.Invoice.OrderNo, dbo.Invoice.InvoiceTotal, dbo.Invoice.InvoiceDate, 
                      dbo.Invoice.AccountNumber, dbo.Invoice.HeadNotes, dbo.Invoice.FootNotes, dbo.Invoice.TaxValue, dbo.Company.Name, dbo.Address.AddressName, dbo.Address.Address1, 
                      dbo.Address.Address2, dbo.Address.Address3, dbo.Address.City, dbo.State.StateName, dbo.Country.CountryName, dbo.Address.Email,  isnull(dbo.State.StateName, '') + ' ' + isnull(dbo.Address.PostCode, '') As StatePostCode,
                      dbo.Address.URL,Address.PostCode, dbo.invoicedetail.Quantity, dbo.invoicedetail.ItemTaxValue, dbo.items.InvoiceDescription, dbo.items.Qty1BaseCharge1, 
                      dbo.Items.Qty1Tax1Value, (dbo.Items.Qty1BaseCharge1 +  dbo.Items.Qty1Tax1Value) as TotalPrice , dbo.Invoice.GrandTotal, dbo.items.ProductName,
                       (select ReportBanner from ReportNote where ReportCategoryID=13) as BannerPath,
                      (select ISNULL(BannerAbsolutePath,'') + isnull(ReportBanner,'')  from ReportNote where ReportCategoryID=13) as ReportBanner,
                       (select Top 1 FootNotes  from ReportNote where ReportCategoryID=13 and isdefault = 1) as ReportFootNotes,
                      dbo.Company.TaxLabel As TaxLabel, (select top 1 currencysymbol from currency c inner join organisation o on o.CurrencyId = c.CurrencyId and o.OrganisationId = @OrganisationID) as CurrencySymbol,
					  isnull((select isnull(order_code,'') from Estimate where estimateid = Invoice.estimateid),'') as OrderCode,	
					  isnull((select isnull(Estimate_Code,'') from Estimate where estimateid = Invoice.estimateid),'') as Estimate_Code,	
				( select FinishDeliveryDate from Estimate where estimateid = invoice.estimateid) as FinishDeliveryDate,
					 ISNULL((select AddressName from address where addressid in (select BillingAddressID from estimate where estimateid = invoice.estimateid)),'N/A') as BAddressName,
                    ISNULL((select address1 from address where addressid in (select BillingAddressID from estimate where estimateid = invoice.estimateid)),'N/A') as BAddress1,
					ISNULL((select address2 from address where addressid in (select BillingAddressID from estimate where estimateid = invoice.estimateid)),'N/A') as BAddress2,
					ISNULL((select city from address where addressid in (select BillingAddressID from estimate where estimateid = invoice.estimateid)),'N/A') as BCity,
					ISNULL(State.StateName,'N/A') as BState,
					ISNULL((select PostCode from address where addressid in (select BillingAddressID from estimate where estimateid = invoice.estimateid)),'N/A') as BPostCode,
					ISNULL(Country.CountryName,'N/A') as BCountry,
					case when (select Estimate_Code from estimate where estimateid = invoice.estimateid) is not null then 'Estimate Code:'
					     when (select Estimate_Code from estimate where estimateid = invoice.estimateid) is null then ''
					     end as EstimateCodeLabel,
					case when (select Order_Code from estimate where estimateid = invoice.estimateid) is not null then 'Order Code:'
					     when (select Order_Code from estimate where estimateid = invoice.estimateid) is null then ''
					     end as OrderCodeLabel
					
FROM         dbo.InvoiceDetail INNER JOIN
                      dbo.invoice ON dbo.invoicedetail.InvoiceID = dbo.invoice.InvoiceID INNER JOIN
                      dbo.company ON dbo.invoice.CompanyId = dbo.company.CompanyId INNER JOIN
                      dbo.address ON dbo.invoice.AddressID = dbo.address.AddressID INNER JOIN
                      dbo.items ON dbo.invoicedetail.ItemID = dbo.items.ItemID
                      inner join dbo.state on Address.StateId = dbo.State.StateId
					  inner join dbo.Country on Address.CountryId = dbo.Country.CountryID

					  where Company.OrganisationId = @Organisationid and dbo.Invoice.InvoiceId = @InvoiceId

end


GO
/****** Object:  StoredProcedure [dbo].[usp_DeliveryReport]    Script Date: 4/29/2015 2:16:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create procedure [dbo].[usp_DeliveryReport]
 @OrganisationID bigint,
 @DeliveryID bigint
AS
Begin

select	  cc.Name As CompanyName,

		  sup.Name As SupplierName,cc.OrganisationId,

		  ISNULL(c.FirstName, '') + ' ' + ISNULL(c.LastName, '') As CotactFullName, 

		  ad.AddressName, ad.Address1,ad.City,st.StateName as StateName, ad.PostCode,ad.Tel1,ct.CountryName as CountryName,

		 dd.Description, dd.ItemQty, dd.GrossItemTotal, 

          dn.Code,dn.DeliveryDate,dn.OrderReff,dn.CustomerOrderReff,dn.CsNo,dn.DeliveryNoteID,

          CASE 

			  WHEN dn.IsStatus = 1 THEN 'Un Delivered'

			  WHEN dn.IsStatus = 2 THEN 'Delivered' 

		 END As DeliveryStatus,

		 (select top 1 ISNULL(BannerAbsolutePath,'') + isnull(ReportBanner,'')  from ReportNote where ReportCategoryID=6) as ReportBanner

		

from	deliverynote dn

	    left join company cc on cc.companyID = dn.ContactCompanyId

		left join company sup on sup.CompanyId = dn.SupplierId

		left join CompanyContact c on c.contactID = dn.ContactId

		left join address ad on ad.AddressID = dn.AddressID

		inner join state st on ad.StateId = st.StateId

		inner join country ct on ad.CountryId = ct.CountryID

		left join deliverynotedetail dd on dd.DeliveryNoteID = dn.DeliveryNoteID

		where cc.OrganisationId = @OrganisationID and dn.DeliveryNoteId = @DeliveryID


end


GO

/* Execution Date: 30/04/2015 */

GO

update shippingInformation
set itemid = null

alter table shippingInformation
drop constraint DF__tbl_shipp__ItemI__1D66518C

alter table shippingInformation
alter column itemid bigint null

alter table shippinginformation
add constraint FK_ShippingInformation_Items
foreign key (ItemId)
references Items (ItemId)

update shippingInformation
set addressId = null

alter table shippingInformation
drop constraint DF__tbl_shipp__Addre__1E5A75C5

alter table shippingInformation
alter column addressid bigint null

alter table shippinginformation
add constraint FK_ShippingInformation_Address
foreign key (AddressId)
references Address (AddressId)

GO


alter table Reportnote
add CompanyId bigint null

GO

/* Execution Date: 04/05/2015 */
ALTER TABLE TemplateObject
ADD hasInlineFontStyle bit null

GO
drop table StockItemHistory
GO

EXEC sp_rename 'ItemStockUpdateHistory', 'StockItemHistory'

GO

/* Execution Date: 08/05/2015 */

GO

alter table costcentre
alter Column isDisabled bit not null

GO

/*  Execution Date: 08/05/2015  update costcenter types */

SET identity_insert CostCentreType ON

insert into CostCentreType ( TypeId, TypeName, IsSystem, IsExternal, OrganisationId ) Values (2, 'Pre Press', 0,1,null)
insert into CostCentreType ( TypeId, TypeName, IsSystem, IsExternal, OrganisationId ) Values (3, 'Post Press', 0,1,null)

SET identity_insert CostCentreType OFF

Update CostCentre set Type = 2

delete CostCentreType
  where TypeId not in (1,2,3,11,29)

/* Execution Date:  11/05/2105 */

alter table SectionInkCoverage
drop constraint FK_SectionInkCoverage_ItemSection

alter table SectionInkCoverage
add constraint FK_SectionInkCoverage_ItemSection
foreign key (SectionId)
references ItemSection (ItemSectionId)
on delete cascade

alter table SectionCostcentre
drop constraint FK_tbl_section_costcentres_tbl_item_sections

alter table SectionCostcentre
add constraint FK_SectionCostcentre_ItemSection
foreign key (ItemSectionId)
references ItemSection (ItemSectionId)
on delete cascade

alter table SectionCostCentreDetail
drop constraint FK_tbl_section_costcentre_detail_tbl_section_costcentres

alter table SectionCostCentreDetail
add constraint FK_SectionCostCentreDetail_SectionCostcentre
foreign key (SectionCostCentreId)
references SectionCostcentre (SectionCostCentreId)
on delete cascade

/* Execution Date: 13/05/2015 */

exec sp_rename 'DeliveryNote.ContactCompanyId', 'CompanyId'

alter table DeliveryNote
alter Column CompanyId bigint null

update deliveryNote
set companyId = null
where companyid not in (select companyid from company)

alter table DeliveryNote
add constraint FK_DeliveryNote_Company
foreign key (CompanyId)
references Company (CompanyId)

alter table DeliveryNote
drop constraint DF__tbl_deliv__Raise__5D95E53A

alter table DeliveryNote
alter Column RaisedBy nvarchar(max) null

update deliveryNote
set RaisedBy = null

alter table DeliveryNote
alter Column RaisedBy uniqueidentifier null

exec sp_rename 'Inquiry.ContactCompanyId', 'CompanyId'

alter table Inquiry
alter Column CompanyId bigint null

alter table Inquiry
add constraint FK_Inquiry_Company
foreign key (CompanyId)
references Company (CompanyId)

alter table Inquiry
drop Column BrokerContactCompanyId
/*Executed on Staging on 2015 05 13*/

/* Execution Date: 14/05/2015 */

alter table Estimate
alter Column allowJobWoCreditCheckSetBy nvarchar(max) null

update Estimate
set creditLimitSetBy = null, allowJobWoCreditCheckSetBy = null

alter table Estimate
alter Column creditLimitSetBy uniqueidentifier null

alter table Estimate
alter Column allowJobWoCreditCheckSetBy uniqueidentifier null
/*Executed on Staging on 2015 05 14*/

/* Execution Date: 19/05/2015 */

alter table inquiry
alter column SystemUserId nvarchar(max) null

update inquiry
set systemUserid = null

alter table inquiry
alter column SystemUserId uniqueidentifier null

alter table inquiry
alter column CreatedBy nvarchar(max) null

update inquiry
set createdBy = null

alter table inquiry
alter column CreatedBy uniqueidentifier null

alter table itemSection
add PressIdSide2 bigint null

alter table itemSection
add Side1LookUp bigint null

alter table itemSection
add Side2LookUp bigint null

alter table itemSection
add PassesSide1 bigint null

alter table itemSection
add PassesSide2 bigint null

alter table inquiry
alter column SourceId nvarchar(max) null

alter table inquiry
alter column SourceId int null

alter table invoiceDetail
add TaxValue float null

--Executed on Staging, Preview, Australia and Europe Servers-----on 2015 05 19---
/* Execution Date: 20/05/2015 */

alter table activity
drop constraint DF_tbl_activity_CreatedBy

alter table activity
alter column createdby nvarchar(max) null

update activity
set createdby = null

alter table activity
alter column createdby uniqueidentifier null

alter table invoicedetail
add ItemGrossTotal float null

/* Execution Date: 21/05/2015 */

alter table ItemSection 
alter column PassesSide1 int null

alter table ItemSection 
alter column PassesSide2 int null
--Executed on Staging------
--Executed on Preview, Aus, Eu servers on 2015 05 26------
/* Execution Date: 22/05/2015 */

alter table itemsection
alter column Side1LookUp int null

alter table itemsection
alter column Side2LookUp int null

exec sp_rename 'ItemSection.Side1LookUp', 'ImpressionCoverageSide1'

exec sp_rename 'ItemSection.Side2LookUp', 'ImpressionCoverageSide2'

/* Execution Date: 25/05/2105 */

alter table Machine
add SetupSpoilage int null

alter table Machine
add RunningSpoilage float null

alter table Machine
add CoverageHigh float null

alter table Machine
add CoverageMedium float null

alter table Machine
add CoverageLow float null

alter table Machine
add constraint FK_Machine_LookupMethod
foreign key (LookupMethodId)
references LookupMethod (MethodId)

alter table ProductMarketBriefQuestion
alter column ItemId bigint null

update ProductMarketBriefQuestion
set ItemId = null

alter table ProductMarketBriefQuestion
add constraint FK_ProductMarketBriefQuestion_Item
foreign key (ItemId)
references Items (ItemId)

/* Execution Date: 26/05/2015 */

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ImpositionProfile](
 [ImpositionId] [bigint] IDENTITY(1,1) NOT NULL,
 [Title] [nvarchar](200) NULL,
 [SheetSizeId] [bigint] NULL,
 [SheetHeight] [float] NULL,
 [SheetWidth] [float] NULL,
 [ItemSizeId] [bigint] NULL,
 [ItemHeight] [float] NULL,
 [ItemWidth] [float] NULL,
 [Area] [float] NULL,
 [PTV] [int] NULL,
 [Region] [varchar](50) NULL,
 [OrganisationId] [bigint] NULL,
 CONSTRAINT [PK_ImpositionProfile] PRIMARY KEY CLUSTERED 
(
 [ImpositionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO



alter table machine add isSheetFed bit null
alter table machine add Passes int null
alter table impositionProfile add isPortrait bit null
----Executed on Staging on 26/05/2015---------

/* Execution Date: 27/05/2015 */

alter table productmarketbriefanswer
drop constraint FK_tbl_ProductMarketBriefAnswers_tbl_ProductMarketBriefQuestions

alter table productmarketbriefanswer
add constraint FK_ProductMarketBriefAnswer_ProductMarketBriefQuestion
foreign key (MarketBriefQuestionId)
references ProductMarketBriefQuestion (MarketBriefQuestionId)
on delete cascade



 update fieldVariable set Scope = 7 where RefTableName = 'Company'  

  update fieldVariable set Scope = 8 where RefTableName = 'CompanyContact'  


  update fieldVariable set Scope = 9 where RefTableName = 'addresses' 


  update fieldVariable set Scope = 9 where RefTableName = 'address' 



  update fieldVariable set Scope = 7 where RefTableName = 'tbl_section_flags' 


  update fieldVariable set Scope = 8 where RefTableName = 'tbl_ContactDepartments' 

alter table machine
add IsSpotColor bit null

update DeliveryNote
set FlagId = null

alter table DeliveryNote
add constraint FK_DeliveryNote_SectionFlag
foreign key (FlagId)
references SectionFlag (SectionFlagId)
----Executed on Staging on 28/05/2015---------
/* Execution Date: 28/05/2015 */

alter table deliveryNotedetail
drop constraint FK_tbl_deliverynote_details_tbl_deliverynotes

alter table deliverynotedetail
add constraint FK_DeliveryNoteDetail_DeliveryNote
foreign key (DeliveryNoteId)
references DeliveryNote (DeliveryNoteId)
on delete cascade


/* Execution Date: 29/05/2015 */

alter table templateObject add autoCollapseText bit null

alter table itemsection 
alter column PressIdSide2 int null

alter table itemsection 
add constraint FK_ItemSection_Machine2
foreign key (PressIdSide2)
references Machine (MachineId)

update inquiryItem
set ProductId = null

alter table inquiryitem
add constraint FK_InquiryItem_Pipelineproduct
foreign key (ProductId)
references PipeLineProduct (ProductId)

drop table ImpositionProfile

----Executed on Staging on 29/05/2015---------
----Executed on 3 live servers 01/06/2015---------

/* Execution Date: 01/06/2015 */


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[sp_cloneTemplate] 
	-- Add the parameters for the stored procedure here
	@TemplateID bigint,
	@submittedBy bigint,
    @submittedByName nvarchar(100)
AS
BEGIN


      
	declare @NewTemplateID bigint
	declare @NewCode nvarchar(10)
	--DECLARE  @WaterMarkTxt as [dbo].[tbl_company_sites]
	
	set @NewCode = ''
	
	--INSERT INTO @WaterMarkTxt (CompanySiteName)
	--Select Top 1 CompanySiteName from tbl_company_sites

	
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	INSERT INTO [dbo].[Template]
           ([Code]
           ,[ProductName]
           ,[Description]
           ,[ProductCategoryId]
           ,[Thumbnail]
           ,[Image]
           ,[IsDisabled]

           ,[PDFTemplateWidth]
           ,[PDFTemplateHeight]

           ,[CuttingMargin]
           ,[MultiPageCount]
           ,[Orientation]
           ,[MatchingSetTheme]
           ,[BaseColorId]
           ,[SubmittedBy]
           ,[SubmittedByName]
           ,[SubmitDate]
           ,[Status]
           ,[ApprovedBy]
           ,[ApprovedByName]
           ,[UserRating]
           ,[UsedCount]
           ,[MPCRating]
           ,[RejectionReason]
           ,[ApprovalDate]
           ,[IsCorporateEditable]
           ,[MatchingSetId]
           ,[TempString],[TemplateType],[isSpotTemplate],[isWatermarkText],[isCreatedManual]
          )
     



   
	SELECT 
      @NewCode
      ,[ProductName] + ' Copy'
      ,[Description]
      ,[ProductCategoryID]
      
      ,[Thumbnail]
      ,[Image]
      ,[IsDisabled]

      ,[PDFTemplateWidth]
      ,[PDFTemplateHeight]


      ,[CuttingMargin]
      ,[MultiPageCount]
      ,[Orientation]
      ,[MatchingSetTheme]
      ,[BaseColorId]
      ,@submittedBy
      ,@submittedByName
      ,NULL
      ,1
      ,NULL
      ,NULL
      ,0
      ,0
      ,0
      ,''
      ,NULL,IsCorporateEditable,MatchingSetId,'',[TemplateType],isSpotTemplate,isWatermarkText,isCreatedManual
      
  FROM [dbo].[Template] where productid = @TemplateID
	
	
	set @NewTemplateID = SCOPE_IDENTITY() 
	
	-- updating water mark text 
	UPDATE [dbo].[Template]
	SET TempString= (Select Top 1 OrganisationName from organisation)
	WHERE productid = @NewTemplateID
	
	--copying the pages
	INSERT INTO [dbo].[TemplatePage]
           ([ProductId]
           ,[PageNo]
           ,[PageType]
           ,[Orientation]
           ,[BackGroundType]
           ,[BackgroundFileName]
      ,[ColorC]
      ,[ColorM]
      ,[ColorY]
      ,[ColorK]
      ,[IsPrintable]
           ,[PageName],[hasOverlayObjects],[Width],[Height])

SELECT 
      @NewTemplateID
      ,[PageNo]
      ,[PageType]
      ,[Orientation]
      ,[BackGroundType]
      ,[BackgroundFileName]
      ,[ColorC]
      ,[ColorM]
      ,[ColorY]
      ,[ColorK]
      ,[IsPrintable]
      
      ,[PageName],[hasOverlayObjects],[Width],[Height]
  FROM [dbo].[TemplatePage]
where productid = @TemplateID


	--copying the objects
	INSERT INTO [dbo].[TemplateObject]
           ([ObjectType]
           ,[Name]
           ,[IsEditable]
           ,[IsHidden]
           ,[IsMandatory]
           
           ,[PositionX]
           ,[PositionY]
           ,[MaxHeight]
           ,[MaxWidth]
           ,[MaxCharacters]
           ,[RotationAngle]
           ,[IsFontCustom]
           ,[IsFontNamePrivate]
           ,[FontName]
           ,[FontSize]
           ,[IsBold]
           ,[IsItalic]
           ,[Allignment]
           ,[VAllignment]
           ,[Indent]
           ,[IsUnderlinedText]
           ,[ColorType]
 
           ,[ColorName]
           ,[ColorC]
           ,[ColorM]
           ,[ColorY]
           ,[ColorK]
           ,[Tint]
           ,[IsSpotColor]
           ,[SpotColorName]
           ,[ContentString]
           ,[ContentCaseType]
           ,[ProductID]
           ,[DisplayOrderPdf]
           ,[DisplayOrderTxtControl]
           ,[RColor]
           ,[GColor]
           ,[BColor]
           ,[LineSpacing]
           ,[ProductPageId]
           ,[ParentId]
           ,CircleRadiusX
           ,Opacity
           ,[ExField1],
           [IsTextEditable],
           [IsPositionLocked],
           [CircleRadiusY]
          ,[ExField2],
           ColorHex
           ,[IsQuickText]
           ,[QuickTextOrder],
		   [watermarkText],
		   [textStyles],[charspacing],[AutoShrinkText],
		   [IsOverlayObject],[ClippedInfo],[originalContentString],[originalTextStyles],[autoCollapseText]
           )
	SELECT 
      O.[ObjectType]
      ,O.[Name]
      ,O.[IsEditable]
      ,O.[IsHidden]
      ,O.[IsMandatory]
      
      ,O.[PositionX]
      ,O.[PositionY]
      ,O.[MaxHeight]
      ,O.[MaxWidth]
      ,O.[MaxCharacters]
      ,O.[RotationAngle]
      ,O.[IsFontCustom]
      ,O.[IsFontNamePrivate]
      ,O.[FontName]
      ,O.[FontSize]
      ,O.[IsBold]
      ,O.[IsItalic]
      ,O.[Allignment]
      ,O.[VAllignment]
      ,O.[Indent]
      ,O.[IsUnderlinedText]
      ,O.[ColorType]
      ,O.[ColorName]
      ,O.[ColorC]
      ,O.[ColorM]
      ,O.[ColorY]
      ,O.[ColorK]
      ,O.[Tint]
      ,O.[IsSpotColor]
      ,O.[SpotColorName]
      ,O.[ContentString]
      ,O.[ContentCaseType]
      ,@NewTemplateID
      ,O.[DisplayOrderPdf]
      ,O.[DisplayOrderTxtControl]
      ,O.[RColor]
      ,O.[GColor]
      ,O.[BColor]
      ,O.[LineSpacing]
      ,NP.[ProductPageId]
      ,O.[ParentId]
      ,O.CircleRadiusX
      ,O.Opacity
      ,O.[ExField1],
       O.[IsTextEditable],
       O.[IsPositionLocked],
       O.[CircleRadiusY]
      ,O.[ExField2],
       O.ColorHex
       ,[IsQuickText]
        ,[QuickTextOrder],[watermarkText],O.[textStyles],
		O.[charspacing],O.[AutoShrinkText],O.[IsOverlayObject]
		,O.[ClippedInfo]
		,O.[originalContentString],O.[originalTextStyles],O.[autoCollapseText]
  FROM [dbo].[TemplateObject] O
  inner join [dbo].[TemplatePage]  P on o.ProductPageId = p.ProductPageId and o.ProductId = @TemplateID
  inner join [dbo].[TemplatePage] NP on P.PageName = NP.PageName and P.PageNo = NP.PageNo and NP.ProductId = @NewTemplateID
  
	--theme tags
	--insert into dbo.TemplateThemeTags   ([TagID],[ProductID])
	--select [TagID] ,@NewTemplateID from dbo.TemplateThemeTags where ProductID = @TemplateID
	
	---- industry tags
	--insert into dbo.TemplateIndustryTags   ([TagID],[ProductID])
	--select [TagID] ,@NewTemplateID from dbo.TemplateIndustryTags where ProductID = @TemplateID

	INSERT INTO [dbo].[TemplateBackgroundImage]
			   ([ProductId]
			   ,[ImageName]
			   ,[Name]
			   ,[flgPhotobook]
			   ,[flgCover]
			   ,[BackgroundImageAbsolutePath]
			   ,[BackgroundImageRelativePath],
			   ImageType,
			   ImageWidth,
			   ImageHeight
			   
			   )
	SELECT 
		  @NewTemplateID
		  ,[ImageName]
		  ,[Name]
		  ,[flgPhotobook]
		  ,[flgCover]
		  ,[BackgroundImageAbsolutePath]
		  ,[BackgroundImageRelativePath]
		  ,ImageType,
			   ImageWidth,
			   ImageHeight
	  FROM [dbo].[TemplateBackgroundImage] where ProductId = @TemplateID

select @NewTemplateID
	
END


GO

/*Execution date 02-06-2015*/


INSERT INTO [dbo].[FieldVariable]
           ([VariableName]
           ,[RefTableName]
           ,[CriteriaFieldName]
           ,[VariableSectionId]
           ,[VariableTag]
           ,[SortOrder]
           ,[KeyField]
           ,[VariableType]
           ,[CompanyId]
           ,[Scope]
           ,[WaterMark]
           ,[DefaultValue]
           ,[InputMask]
           ,[OrganisationId]
           ,[IsSystem]
           ,[VariableTitle])
     VALUES
           ('State Abbreviation'
           ,'Address'
           ,'State'
           ,18
           ,'{{contact_StateAbbreviation}}'
           ,37
           ,'AddressID'
           ,45
           ,NULL
           ,9
           ,'State Abbreviation'
           ,NULL
           ,NULL
           ,NULL
           ,1
           ,NULL)
GO




INSERT INTO [dbo].[FieldVariable]
           ([VariableName]
           ,[RefTableName]
           ,[CriteriaFieldName]
           ,[VariableSectionId]
           ,[VariableTag]
           ,[SortOrder]
           ,[KeyField]
           ,[VariableType]
           ,[CompanyId]
           ,[Scope]
           ,[WaterMark]
           ,[DefaultValue]
           ,[InputMask]
           ,[OrganisationId]
           ,[IsSystem]
           ,[VariableTitle])
     VALUES
           ('State Abbreviation'
           ,'Address'
           ,'State'
           ,16
           ,'{{DefaultShipping_StateAbbreviation}}'
           ,37
           ,'AddressID'
           ,45
           ,NULL
           ,9
           ,'State Abbreviation'
           ,NULL
           ,NULL
           ,NULL
           ,1
           ,NULL)
GO






INSERT INTO [dbo].[FieldVariable]
           ([VariableName]
           ,[RefTableName]
           ,[CriteriaFieldName]
           ,[VariableSectionId]
           ,[VariableTag]
           ,[SortOrder]
           ,[KeyField]
           ,[VariableType]
           ,[CompanyId]
           ,[Scope]
           ,[WaterMark]
           ,[DefaultValue]
           ,[InputMask]
           ,[OrganisationId]
           ,[IsSystem]
           ,[VariableTitle])
     VALUES
           ('State Abbreviation'
           ,'Address'
           ,'State'
           ,17
           ,'{{DefaultBilling_StateAbbreviation}}'
           ,37
           ,'AddressID'
           ,45
           ,NULL
           ,9
           ,'State Abbreviation'
           ,NULL
           ,NULL
           ,NULL
           ,1
           ,NULL)
GO


/* Execution Date: 02/06/2015 */

alter table SmartFormDetail
drop constraint FK__SmartForm__Varia__012C6796

alter table SmartFormDetail
add constraint FK_SmartFormDetail_FieldVariable
foreign key (VariableId)
references FieldVariable (VariableId)
on delete cascade

alter table ScopeVariable
drop constraint FK_CompanyContactVariable_FieldVariable

alter table ScopeVariable
add constraint FK_ScopeVariable_FieldVariable
foreign key (VariableId)
references FieldVariable (VariableId)
on delete cascade

alter table VariableOption
drop constraint FK_VariableOption_FieldVariable

alter table VariableOption
add constraint FK_VariableOption_FieldVariable
foreign key (VariableId)
references FieldVariable (VariableId)
on delete cascade

alter table TemplateVariable
add constraint FK_TemplateVariable_FieldVariable
foreign key (VariableId)
references FieldVariable (VariableId)
on delete cascade
GO



/* Execution Date: 03/06/2015 */

/*
Created by: khurram , 2015-06-03
*/
CREATE PROCEDURE usp_TotalEarnings (@fromdate as datetime, @todate as datetime)
AS
--define limits
--set @fromdate = '2015-01-01'
--set @todate = '2015-12-31'
 BEGIN
;With DateSequence( [Date] ) as
(
    Select @fromdate as [Date]
        union all
    Select dateadd(day, 1, [Date])
        from DateSequence
        where Date < @todate
)
 
--select result
Select
    CONVERT(VARCHAR,[Date],112) as ID,
    [Date] as [Date],
    DATEPART(DAY,[Date]) as [Day],
    CASE
         WHEN DATEPART(DAY,[Date]) = 1 THEN CAST(DATEPART(DAY,[Date]) AS VARCHAR) + 'st'
         WHEN DATEPART(DAY,[Date]) = 2 THEN CAST(DATEPART(DAY,[Date]) AS VARCHAR) + 'nd'
         WHEN DATEPART(DAY,[Date]) = 3 THEN CAST(DATEPART(DAY,[Date]) AS VARCHAR) + 'rd'
         ELSE CAST(DATEPART(DAY,[Date]) AS VARCHAR) + 'th'
    END as [DaySuffix],
    DATENAME(dw, [Date]) as [DayOfWeek],
    DATEPART(DAYOFYEAR,[Date]) as [DayOfYear],
    DATEPART(WEEK,[Date]) as [WeekOfYear],
    DATEPART(WEEK,[Date]) + 1 - DATEPART(WEEK,CAST(DATEPART(MONTH,[Date]) AS VARCHAR) + '/1/' + CAST(DATEPART(YEAR,[Date]) AS VARCHAR)) as [WeekOfMonth],
    DATEPART(MONTH,[Date]) as [Month],
    DATENAME(MONTH,[Date]) as [MonthName],
    DATEPART(QUARTER,[Date]) as [Quarter],
    CASE DATEPART(QUARTER,[Date])
        WHEN 1 THEN 'First'
        WHEN 2 THEN 'Second'
        WHEN 3 THEN 'Third'
        WHEN 4 THEN 'Fourth'
    END as [QuarterName],
    DATEPART(YEAR,[Date]) as [Year]
	into #dt
from DateSequence option (MaxRecursion 10000)


--select * from #dt

select sum(Estimate_Total) Total,count(*) Orders,store,Month,monthname,year 
	from
	(
	
		select estimateid, o.Estimate_Total,
		DATENAME(MONTH,O.Order_Date) as [Month],
		DATEPART(MONTH,O.Order_Date) as [MonthName],
		DATEPART(YEAR,O.Order_Date) as [Year],
		(Case when C.Iscustomer = 3 then C.name when C.IsCustomer = 1 then S.name End) as Store,
		C.IsCustomer
		from Estimate O
		inner join Company C on O.CompanyId = C.companyid
		left outer join Company S on C.StoreId = S.companyid
		where (O.StatusId <> 3 and O.StatusId <> 34)
		

	) data
group by month,store,monthname,year

END


/* Executed on Staging, USA server */
/* Execution Date: 04/06/2015 */

update fieldVariable set CriteriaFieldName = 'StateAbbr' where VariableName = 'State Abbreviation'

alter table Organisation add IsImperical bit null

alter table PaperSize add IsImperical bit null

alter table StockItem add IsImperical bit null

alter table PurchaseDetail
alter column itemid bigint null

update PurchaseDetail
set ItemId = null
where ItemId not in (select ItemId from Items)

alter table PurchaseDetail
add constraint FK_PurchaseDetail_Items
foreign key (ItemId)
references Items (ItemId)

alter table PurchaseDetail
add ProductType int null

alter table PurchaseDetail
add RefItemId bigint null

/* Executed on Staging */

/* Execution Date: 08/06/2015 */

alter table purchase
drop constraint DF__tbl_purch__Suppl__74994623

alter table purchase
alter column supplierid bigint null

update purchase
set supplierid = null

alter table purchase
add constraint FK_Purchase_Company
foreign key (SupplierId)
references Company (CompanyId)

update purchase
set flagid = null

alter table purchase
add constraint FK_Purchase_SectionFlag
foreign key (FlagId)
references SectionFlag (SectionFlagId)

alter table PurchaseDetail
add TaxValue float null

/* Execution Date: 09/06/2015 */

alter table purchasedetail
drop constraint FK_PurchaseID

alter table purchaseDetail
add constraint FK_PurchaseDetail_Purchase
foreign key (PurchaseId)
references Purchase (PurchaseId)
on delete cascade

/* Execution Date: 10/06/2015 */


/****** Object:  Table [dbo].[StagingImportCompanyContactAddress]    Script Date: 6/9/2015 6:17:18 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[StagingImportCompanyContactAddress](
	[StagingId] [bigint] IDENTITY(1,1) NOT NULL,
	[CompanyName] [nvarchar](250) NULL,
	[CompanyId] [bigint] NULL,
	[AddressId] [bigint] NULL,
	[AddressName] [varchar](200) NULL,
	[Address1] [varchar](250) NULL,
	[Address2] [varchar](250) NULL,
	[Address3] [varchar](250) NULL,
	[City] [varchar](250) NULL,
	[State] [varchar](250) NULL,
	[StateId] [bigint] NULL,
	[Country] [varchar](50) NULL,
	[CountryId] [bigint] NULL,
	[Postcode] [varchar](50) NULL,
	[TerritoryId] [bigint] NULL,
	[TerritoryName] [varchar](250) NULL,
	[AddressPhone] [nvarchar](50) NULL,
	[AddressFax] [nvarchar](50) NULL,
	[ContactId] [bigint] NULL,
	[ContactFirstName] [varchar](250) NULL,
	[ContactLastName] [varchar](250) NULL,
	[JobTitle] [varchar](250) NULL,
	[Email] [varchar](250) NULL,
	[password] [nvarchar](50) NULL,
	[Mobile] [varchar](50) NULL,
	[RoleId] [bigint] NULL,
	[ContactPhone] [nvarchar](50) NULL,
	[ContactFax] [varchar](50) NULL,
	[AddInfo1] [nvarchar](1500) NULL,
	[AddInfo2] [nvarchar](1000) NULL,
	[AddInfo3] [nvarchar](1000) NULL,
	[AddInfo4] [nvarchar](1500) NULL,
	[AddInfo5] [nvarchar](1500) NULL,
	[OrganisationId] [bigint] NULL,
 CONSTRAINT [PK_StagingImportCompanyContactAddress] PRIMARY KEY CLUSTERED 
(
	[StagingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO



/****** Object:  StoredProcedure [dbo].[usp_importTerritoryContactAddressByStore]    Script Date: 5/25/2015 12:46:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

 CREATE Procedure [dbo].[usp_importTerritoryContactAddressByStore]
		@OrganisationId bigint,
		@StoreId bigint
	 
	 as 
	 Begin
  
		  update StagingImportCompanyContactAddress set CompanyId = @StoreId, OrganisationId = @OrganisationId
		  
		  --update Country Id by country name
		  update StagingImportCompanyContactAddress set CountryId = c.CountryId
		  from StagingImportCompanyContactAddress i , country c
		  where c.CountryName = i.Country
		  --update StateId by State Name 
		  update StagingImportCompanyContactAddress set StateId = c.StateId
		  from StagingImportCompanyContactAddress i , State c
		  where c.StateName = i.State
		  --update TerritoryId from Territory Name
		  update StagingImportCompanyContactAddress set TerritoryId = c.TerritoryId
		  from StagingImportCompanyContactAddress i , CompanyTerritory c
		  where c.TerritoryCode = i.TerritoryName

				BEGIN TRY

					Declare @count int = (select count(*) from StagingImportCompanyContactAddress)
					Declare @counter int = 1
					Declare @StagingId bigint, @AddressId bigint, @TerritoryId bigint, @AddrssName varchar(200), @Address1 varchar(200), @Address2 varchar(200), @City varchar(100),
							@TerritoryName varchar(200), @isNewTerritory bit
  
					While @counter <= @count
					Begin
					set @isNewTerritory = 0
					select @AddrssName = AddressName, @Address1 = Address1, @Address2 = Address2, @City = City, @TerritoryName = TerritoryName from (select row_number() OVER (ORDER BY stagingid) r, * from StagingImportCompanyContactAddress) x
					where r = @counter
					 --Check Territory Exist otherwise Insert Territory
					 if(exists(select * from CompanyTerritory where companyId = @StoreId and TerritoryName = @TerritoryName))
					 begin
						set @TerritoryId = (select top 1 TerritoryId from CompanyTerritory where companyId = @StoreId and TerritoryName = @TerritoryName)
					 end
					 else
					 begin
							 insert into CompanyTerritory(CompanyID, TerritoryCode, TerritoryName)
							 select CompanyId, 'TCImport', TerritoryName   from (select row_number() OVER (ORDER BY stagingid) r, * from StagingImportCompanyContactAddress) xx
									where r = @counter
							set @TerritoryId = (select SCOPE_IDENTITY())
							set @isNewTerritory = 1
					 end


				    --Check If Address Exist otherwise insert Address
					if(exists(select * from Address where companyId = @StoreId and addressname = @AddrssName and Address1 = @Address1 and Address2 = @Address2 and City = @City))
						begin
							set @AddressID = (select top 1 AddressId from Address where companyId = @StoreId and addressname = @AddrssName and Address1 = @Address1 and Address2 = @Address2 and City = @City)
						end
					else
						begin
							insert into Address(OrganisationId, CompanyID, AddressName, Address1, Address2, Address3, City, StateId, PostCode, TerritoryID, CountryId, Tel1, isDefaultTerrorityBilling, isDefaultTerrorityShipping, isArchived, IsDefaultAddress)
							select @OrganisationId, CompanyId, AddressName, Address1, Address2, Address3, City, StateId,Postcode, @TerritoryId, CountryId, ContactPhone, 0, 0, 0, 0   from (select row_number() OVER (ORDER BY stagingid) r, * from StagingImportCompanyContactAddress) x
							where r = @counter
							set @Addressid = (select SCOPE_IDENTITY())
							if(@isNewTerritory = 1 and @TerritoryId > 0)
								Begin
									update address set isDefaultTerrorityBilling = 1, isDefaultTerrorityShipping = 1 where AddressId = @AddressId
								End
						end
					 --Insert New Contact
						insert into CompanyContact(OrganisationId, CompanyId, TerritoryID, AddressID, ShippingAddressID, FirstName, LastName, Email, Password, HomePostCode, Mobile, isArchived, ContactRoleID, JobTitle, HomeTel1, Notes, IsDefaultContact, isWebAccess, canPlaceDirectOrder, canUserPlaceOrderWithoutApproval, IsPricingshown)
						select @OrganisationId, CompanyId, TerritoryId, @Addressid, @Addressid, ContactFirstName, ContactLastName, Email,password, postcode, Mobile,0, RoleId, JobTitle, ContactPhone, 'contact import for this store, default password is guest', 0, 1, 1, 1, 1  
						from (select row_number() OVER (ORDER BY stagingid) r, * from StagingImportCompanyContactAddress) x
						where r = @counter


						set @counter = @counter + 1;
					End
						Commit Transaction
				End Try
				BEGIN CATCH
						IF @@TRANCOUNT > 0
							ROLLBACK
				END CATCH
End

/****** Object:  StoredProcedure [dbo].[usp_DeletePurchaseOrders]    Script Date: 6/10/2015 11:50:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[usp_DeletePurchaseOrders]
@OrderID as int
AS
BEGIN
	
	SET NOCOUNT OFF
	DECLARE @Err int
	
    Declare 
         @ItemsCount as int,
         @Counter as int,
		 @DeletedPurchaseID as int,
		 @POCounter as int,
		 @POCount as int,
         @ItemID as int
         
    DECLARE @ITEMS AS TABLE(
     ROWID  INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
     ItemID int
    
     )
   
  													
     
 --   INSERT INTO @ITEMS 
	--SELECT ItemID
	--			FROM tbl_items 
	--			WHERE EstimateID = @OrderID and (itemType is null or itemType <> 2)
	
	 INSERT INTO @ITEMS 
	SELECT ItemID
				FROM items 
				WHERE EstimateID = @OrderID 
			
	--select @ItemsCount = count(*) from tbl_items where EstimateID = @OrderID and (itemType is null or itemType <> 2)
	select @ItemsCount = count(*) from items where EstimateID = @OrderID							
	        
	SET @Counter = 1

    WHILE (@Counter <= @ItemsCount)
    BEGIN

					Select @ItemID = ItemID	 
					from @ITEMS where ROWID = @Counter
					select @DeletedPurchaseID = purchaseid from purchasedetail where itemid = @ItemID
					delete from purchasedetail where purchaseid = @DeletedPurchaseID
					delete from purchase where purchaseid = @DeletedPurchaseID
					select 'delete successfully' as MSG
							
			   SET @Counter = @Counter + 1
		end
		
		 SET @Err = @@Error	
		RETURN @Err
END

/****** Object:  StoredProcedure [dbo].[usp_GeneratePurchaseOrders]    Script Date: 6/10/2015 11:32:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



ALTER PROCEDURE [dbo].[usp_GeneratePurchaseOrders]
@OrderID as int,
@CreatedBy as uniqueidentifier
AS
BEGIN
	
	SET NOCOUNT OFF
	DECLARE @Err int
	
    Declare 
         @ItemsCount as int,
         @Counter as int,
		 @RefItemID as int,
		 @ItemQty as int,
		 @IsExternal as bit,
		 @IsQtyLimit as bit,
		 @AutoCreateUnPostedPO as bit,
		 @QtyLimit as int,
		 @SupplierID as int,
		 @ItemID as int,
		 @SupplierName as varchar(50),
		 @SupplierAdressID as int,
		 @SupplierContactID as int,
		 @ContactCompanyID as int,
		 @Code as varchar(50),
		 @ProductCategoryID as int,
		 @ProductCompleteName as varchar(100),
         @ItemType as int,
         @MarkupVaue as float,
         @NetTotal as float,         
		@POPrefix as varchar(10),
		@POStart as bigint,
		@PONext as varchar(20),
		@NewPO as bigint,
		@UserNotes as nvarchar(2000)
		

    DECLARE @ITEMS AS TABLE(
     ROWID  INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
     ItemID int,
     RefItemID INT,
     Qty1 int,
     Qty1GrossTotal float,
     ProductName varchar(150),
     ItemCode varchar(50),
    -- ProductCategoryID INT,
     Qty1Tax1Value float,
     ItemType int,
     Qty1MarkUp1Value float
     )
     
     DECLARE @ITEMDETAIL AS TABLE(
     ROWID  INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
     IsInternalActivity bit,
     IsQtyLimit bit,
     QtyLimit int,
     ItemID int,
     isAutoCreateSupplierPO bit)	
     
     --Declare @SupplierIDs as table(
     --ROWID  INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
     --SupplierID int)
     
    declare @SupplierIDs table
    (
       SupplierID int,
       PurchaseID int,
       LTotalPrice float,
       LTotalTax float,
       LGrandTotal float,
       LNetTotal float
																				   
    )
      declare @LastTotalPrice as float,
		      @LastDiscount as float,
			  @LastTotalTax as float,
		      @LastGrandTotal as float,
			  @LastNetTotal as float															
     
    INSERT INTO @ITEMS  --- insert items in temp table
	SELECT ItemID,RefItemID, Qty1, Qty1GrossTotal,ProductName,ItemCode,Qty1Tax1Value,ItemType,Qty1MarkUp1Value
				FROM Items 
				WHERE EstimateID = @OrderID
    select @UserNotes = usernotes from estimate where EstimateID = @OrderID			
	-- getting count of items
	select @ItemsCount = count(*) from items where EstimateID = @OrderID
									
	
	declare @Qty1GrossTotal as float,
	        @Qty1 as int,
	        @ItemCode as varchar(50),
	        @ProductName as varchar(100),
	        @CategoryName as Varchar(100),
	        @Qty1Tax1Value as float,
	        @ProductDetailCount as int,
	        @SupplierTableCount as int,
	        @PurchaseID as int,
	        @PurchaseDetailID as int,
	        @StockItemID1 as int,
	        @Sequence as int,
	        @PricePaperType as int,
	        @SystemSiteID as int,
			--@TaxIDGS as int,
	        @NetTax as float
	SET @Counter = 1
    
    -- loop start for each item
    WHILE (@Counter <= @ItemsCount)
    BEGIN
    	
    	            -- getting ref item id of particular item
					SELECT @RefItemID = RefItemID FROM @ITEMS WHERE ROWID = @Counter
					
					-- setting variables/fields from particular item
					Select @ItemID = ItemID,
								@RefItemID = RefItemID,	  
									   @Qty1 = Qty1,
									   @ItemCode = ItemCode,
									   @ProductName = ProductName,
									  -- @ProductCategoryID = ProductCategoryID,
									   @ItemType = ItemType
									    --@Qty1GrossTotal = Qty1GrossTotal,
									   --@Qty1Tax1Value = Qty1Tax1Value,
									  -- @MarkupVaue = Qty1MarkUp1Value
								from @ITEMS where ROWID = @Counter

					-- if item is stoct
					if (@ItemType = 3) -- stock
					   begin
					       -- select supplierand name   of particular stock item
						   select @SupplierID = (Select TOP 1 isnull(SupplierID,0) from stockitem where StockItemID = @RefItemID),
						          @ProductCompleteName = (Select TOP 1 isnull(ItemName,'') from stockitem where StockItemID = @RefItemID)
						          
					       if (@SupplierID > 0)
					           begin    
					              -- getting price of stock item	
								   Select @Qty1GrossTotal = (select top 1 isnull(CostPrice,0) from StockCostAndPrice where ItemID = @RefItemID and CostOrPriceIdentifier = 0)
                                   if @Qty1GrossTotal is null
                                      begin
                                         select @Qty1GrossTotal = 0
                                      end
							  
							  end
					   end
					else if (@ItemType = 2)
					   begin
					   
					     -- select supplier and name   of particular cost center item
							select @SupplierID = (Select TOP 1 isnull(PreferredSupplierID,0) from CostCentre where CostCentreID = @RefItemID),
							   @ProductCompleteName = (Select TOP 1 isnull(Name,'') from CostCentre where CostCentreID = @RefItemID)
						      if (@SupplierID > 0)
					           begin    	
					           
					                -- getting price of cost center
									declare @CostPrice as float
									select @CostPrice = CostPerUnitQuantity from CostCentre where CostCentreID = @RefItemID
									select @Qty1GrossTotal = @CostPrice / @Qty1
								    if @Qty1GrossTotal is null
                                      begin
                                         select @Qty1GrossTotal = 0
                                      end
							  end
					   end
					else		
					  begin			 
                                -- getting product name
								--select @CategoryName = CategoryName from productcategory where ProductCategoryID = @ProductCategoryID
								
								-- making complete name
								select @ProductCompleteName =  @ProductName
								
								-- getting item qty
								SELECT @ItemQty = isnull(Qty1,0) FROM @ITEMS WHERE ROWID = @Counter
									
							    -- insert item detail in temp table				
								INSERT INTO @ITEMDETAIL 
										SELECT top 1 isInternalActivity,isQtyLimit,QtyLimit, ItemID, isAutoCreateSupplierPO
										FROM ItemProductDetail 
										WHERE ItemID = @RefItemID
										
										
										
								SET @ProductDetailCount = ISNULL(@@ROWCOUNT, 0)
								
								-- check product detail count is greater then 1 or not
								if (@ProductDetailCount > 0)
								  begin
									if (@MarkupVaue > 0)
										begin
										   select @MarkupVaue = 0
										end	
										
									
									-- checking is internal flag
									Select @IsExternal = isInternalActivity from @ITEMDETAIL where ItemID = @RefItemID
									if (@IsExternal = 1)
									  begin
										 select * from items where itemid = @itemid
									  end
									else
									  begin
									        -- check isAutoCreateSupplierPO is On or off
									        select @AutoCreateUnPostedPO =  isAutoCreateSupplierPO from @ITEMDETAIL where itemid = @RefItemID
									        
									        if (@AutoCreateUnPostedPO = 1)
									           begin
														-- check is qty limit flag
														select @IsQtyLimit = IsQtyLimit from  @ITEMDETAIL where ItemID = @RefItemID
														
													
														-- getting qty limit
														select @QtyLimit = isnull(QtyLimit,0) from @ITEMDETAIL where ItemID = @RefItemID
														
														-- getting supplier id for item
														select @SupplierID = (Select TOP 1 isnull(SupplierID,0) from ItemPriceMatrix where ItemID = @RefItemID and SupplierSequence = 1)
							 
													  --  if (@SupplierID is null)
															--select @SupplierID = (Select TOP 1 isnull(SupplierID,0) from tbl_items_PriceMatrix where ItemID = @RefItemID and SupplierSequence = 2)
														
														-- if is qty limit is true
														if(@IsQtyLimit = 1)
														  begin
																-- if order qty is greater than qty limit
																if (@ItemQty > @QtyLimit)
																   begin
																	  select @SupplierID = (Select TOP 1 isnull(SupplierID,0) from ItemPriceMatrix where ItemID = @RefItemID and SupplierSequence = 1)
																	  if (@SupplierID is null)
																		select @SupplierID = (Select TOP 1 isnull(SupplierID,0) from ItemPriceMatrix where ItemID = @RefItemID and SupplierSequence = 2)
																	  
																	 -- insert into @SupplierIDs
															
																	  
																	end
																 else --  else part of qty conditions
																   begin
																	  select @SupplierID = (Select TOP 1 isnull(SupplierID,0) from ItemPriceMatrix where ItemID = @RefItemID and SupplierSequence = 2)
																	   if (@SupplierID is null)
																		select @SupplierID = (Select TOP 1 isnull(SupplierID,0) from ItemPriceMatrix where ItemID = @RefItemID and SupplierSequence = 1)
														               
																   end
																 
													 					
														  end
											  
														 if (@SupplierID > 0)
															begin  
																	select @StockItemID1 = (select top 1 StockItemID1 from ItemSection where itemid = @ItemID and sectionno = 1)
																   
																   
																	 select @Sequence = (select top 1 OptionSequence from ItemStockOption where itemid = @RefItemID and StockID = @StockItemID1 and companyid is null)
																   
																	if (@Sequence = 1)
																	   begin
																		 select @Qty1GrossTotal = (select top 1 PricePaperType1 from ItemPriceMatrix where Quantity = @Qty1 and SupplierID = @SupplierID and ItemID = @RefItemID)
																	   end
																	else if (@Sequence = 2)
																	  begin
																		 select @Qty1GrossTotal = (select top 1 PricePaperType2 from ItemPriceMatrix where Quantity = @Qty1 and SupplierID = @SupplierID and ItemID = @RefItemID)
																	  end
																	else if (@Sequence = 3)
																	  begin
																		 select @Qty1GrossTotal = (select top 1 PricePaperType3 from ItemPriceMatrix where Quantity = @Qty1 and SupplierID = @SupplierID and ItemID = @RefItemID)
																	  end
																	else if (@Sequence = 4)
																	  begin
																		 select @Qty1GrossTotal = (select top 1 PriceStockType4 from ItemPriceMatrix where Quantity = @Qty1 and SupplierID = @SupplierID and ItemID = @RefItemID)
																	  end
																	else if (@Sequence = 5)
																	  begin
																		 select @Qty1GrossTotal = (select top 1 PriceStockType5 from ItemPriceMatrix where Quantity = @Qty1 and SupplierID = @SupplierID and ItemID = @RefItemID)
																	  end
																	else if (@Sequence = 6)
																	  begin
																		 select @Qty1GrossTotal = (select top 1 PriceStockType6 from ItemPriceMatrix where Quantity = @Qty1 and SupplierID = @SupplierID and ItemID = @RefItemID)
																	  end
																	else if (@Sequence = 7)
																	  begin
																		 select @Qty1GrossTotal = (select top 1 PriceStockType7 from ItemPriceMatrix where Quantity = @Qty1 and SupplierID = @SupplierID and ItemID = @RefItemID)
																	  end
																	 else if (@Sequence = 8)
																	  begin
																		 select @Qty1GrossTotal = (select top 1 PriceStockType8 from ItemPriceMatrix where Quantity = @Qty1 and SupplierID = @SupplierID and ItemID = @RefItemID)
																	  end
																	else if (@Sequence = 9)
																	  begin
																		 select @Qty1GrossTotal = (select top 1 PriceStockType9 from ItemPriceMatrix where Quantity = @Qty1 and SupplierID = @SupplierID and ItemID = @RefItemID)
																	  end
																	   else if (@Sequence = 10)
																	  begin
																		 select @Qty1GrossTotal = (select top 1 PriceStockType10 from ItemPriceMatrix where Quantity = @Qty1 and SupplierID = @SupplierID and ItemID = @RefItemID)
																	  end
																	else if (@Sequence = 11)
																	  begin
																		 select @Qty1GrossTotal = (select top 1 PriceStockType11 from ItemPriceMatrix where Quantity = @Qty1 and SupplierID = @SupplierID and ItemID = @RefItemID)
																	  end
													    
														  end -- if supplier > 0 end
											   end -- isAutoPOCreate end
										end	 -- is external end
								  end -- product detail count 
								  
						end -- item end
						declare @OrderCompanyID as int
						declare @OrderCompanyIsCustomer as int
						declare @OrderCompanyTaxRate as float
						declare @StoreID as int
											  if (@SupplierID > 0)
													   begin
													   
													    	    select @OrderCompanyID =  CompanyID from Estimate where EstimateId = @OrderID

																select @OrderCompanyIsCustomer = iscustomer from Company where companyid = @OrderCompanyID

																if (@OrderCompanyIsCustomer = 3)
																  begin
																	select @OrderCompanyTaxRate = TaxRate from Company where companyid = @OrderCompanyID
																  end
																else
																  begin
																	select @StoreID = StoreId from Company where companyid = @OrderCompanyID
																	
																    select @OrderCompanyTaxRate = TaxRate from Company where companyid = @StoreID
																  end

																 --select @TaxIDGS =  StateTaxID from tbl_company_sites where companyid =  2
																 declare @TaxPercentage as float
																 -- direct form company
																select @TaxPercentage = @OrderCompanyTaxRate
																		   
																select @NetTax = (@Qty1GrossTotal * @TaxPercentage) / 100
																		   					   
																select @Qty1Tax1Value = 0
																--select @NetTotal = @Qty1GrossTotal + @NetTax
																			
																  IF EXISTS(SELECT SupplierID From @SupplierIDs where SupplierID = @SupplierID)
																	begin
																		--SET @PurchaseID = (SELECT SCOPE_IDENTITY());
																		
																		declare @TotalPrice as float,
																				@Discount as float,
																				@TotalTax as float,
																				@GrandTotal as float,
																				@AgainNetTotal as float,
																                @FootNotes as varchar(2000),
																                @SystemUserName as varchar(500),
																                @LastPurchaseID as int,
																                 @LTotalPrice as float,
																                  @LTotalTax as float,
																                  @LGrandTotal as float,
																                   @LNetTotal as float
																                
																                select @LastPurchaseID = PurchaseID,
																                       @LTotalPrice = LTotalPrice,
																                       @LTotalTax = LTotalTax,
																                        @LGrandTotal = LGrandTotal,
																                        @LNetTotal = LNetTotal
									
																                
																                 from @SupplierIDs where SupplierID = @SupplierID
																                
																		 -- select @TotalPrice =   
																		--select @TotalPrice = @LastTotalPrice + @Qty1GrossTotal,
																		--	   --@Discount = @LastDiscount + @MarkupVaue,
																		--	   @TotalTax = @LastTotalTax + @NetTax,
																		--	  -- @TotalTax = null,
																		--	   @GrandTotal = @LastGrandTotal + @Qty1GrossTotal,
																		--	   @AgainNetTotal = @LastNetTotal + @Qty1GrossTotal + @NetTax
																			   
																			   
																			   	select @TotalPrice = @LTotalPrice + @Qty1GrossTotal,
																			   --@Discount = @LastDiscount + @MarkupVaue,
																			   @TotalTax = @LTotalTax + @NetTax,
																			  -- @TotalTax = null,
																			   @GrandTotal = @LGrandTotal + @Qty1GrossTotal,
																			   @AgainNetTotal = @LNetTotal + @Qty1GrossTotal + @NetTax
																               
																		--insert into tbl_purchasedetail (ItemID,quantity,PurchaseID,price,TotalPrice,packqty,ItemCode,
																		--	ItemName, Discount, NetTax, freeitems,ItemBalance, TaxID,ServiceDetail) values 
																		--	(@ItemID,0,@PurchaseID, @Qty1GrossTotal, @Qty1GrossTotal, @Qty1, @ItemCode, @ProductName,@MarkupVaue,@Qty1Tax1Value,0,0,@TaxID,@ProductCompleteName)
															                
															                
																			insert into PurchaseDetail (ItemID,quantity,PurchaseID,price,TotalPrice,packqty,ItemCode,
																			ItemName, freeitems,ItemBalance,ServiceDetail,NetTax) values 
																			(@ItemID,1,@LastPurchaseID, @Qty1GrossTotal, @Qty1GrossTotal, @Qty1, @ItemCode, @ProductName,0,0,@ProductCompleteName,@NetTax)
																		 set @PurchaseDetailID = (SELECT SCOPE_IDENTITY());
																	     
																	    
																		update Purchase
																		set TotalPrice = @TotalPrice,
																			--Discount = @Discount,
																			TotalTax = @TotalTax,
																			GrandTotal = @GrandTotal,
																			NetTotal = @AgainNetTotal where PurchaseID = @LastPurchaseID
																	        
																	   --select @LastTotalPrice = @TotalPrice,
																			 -- --@LastDiscount =  @Discount,
																			 -- @LastTotalTax =  @TotalTax,
																			 -- @LastGrandTotal = @GrandTotal,
																			 -- @LastNetTotal = @AgainNetTotal
																			 
																			 
																	
																			 
																			 	insert into @SupplierIDs values (@SupplierID,@LastPurchaseID,@TotalPrice,@TotalTax,@GrandTotal,@AgainNetTotal)
																		-- total price
																		-- discount
																		-- total tax 
																		-- grand total
																		-- net total
																	     
																	 end
																   else
																	 begin
																		 --- 1p and 1pd create
																		 
																		  select @SupplierName = Name,
																				  @ContactCompanyID = CompanyId
																			from Company where companyid = @SupplierID
																          
																		   SELECT @SupplierContactID = ContactID,
																				  @SupplierAdressID = AddressID
																		   FROM   CompanyContact where companyid = @ContactCompanyID 
																		   
																		   --Naveed Commenting to test the change -- comment starts--																	        
																		   --select @POPrefix = POPrefix,
																				 -- @POStart  = POStart,
																				 -- @PONext = PONext
																		   --from tbl_prefixes where SystemSiteID = 1
																		   
																		   --set @Code = @POPrefix + '-001-' + @PONext
																		   
																		   --set @NewPO = @PONext + 1
																		   
																		   
																		   --Update tbl_prefixes set PONext = @NewPO where SystemSiteID = 1	
																		   		-------mnz comments end----	  
																				
																				--New line added mnz---
																				select @Code = (select top 1 POPrefix + '-001-'+ cast(PONext as varchar) from prefix)
																				------ 
																		 --	 select @TaxIDGS =  StateTaxID from tbl_company_sites where companyid =  2
																		 --  declare @TaxPercentage as float
																		 --  select @TaxPercentage = Tax1 from tbl_taxrate where taxid = @TaxIDGS
																		   
																		 --  select @NetTax = (@Qty1GrossTotal * @TaxPercentage) / 100
																		   					   
																		 --  select @Qty1Tax1Value = 0
																			select @NetTotal = @Qty1GrossTotal + @NetTax
																		    
																		   
																		   --insert into tbl_purchase (date_Purchase,TotalPrice,isproduct,Status,Discount,TotalTax,GrandTotal,NetTotal,
																		   --CreatedBy,SupplierID,Code,SupplierContactCompany,SupplierContactAddressID,ContactID) values (GetDate(), @Qty1GrossTotal, 2, 31, @MarkupVaue, @Qty1Tax1Value, @Qty1GrossTotal, @NetTotal, @CreatedBy, @SupplierID,@Code,@SupplierName,
																		   --@SupplierAdressID,@SupplierContactID)  
																		   select @SystemSiteID = 1
																	       
																		  
																		   
																           select @FootNotes =  isnull(FootNotes,'') from ReportNote where reportcategoryid = 5 and isdefault = 1 
																		  
																		 
																		   declare @FlagId as int
																		   select @FlagId = (select top 1 sectionflagid from sectionflag where SectionId = 7)
																		   insert into Purchase (date_Purchase,TotalPrice,isproduct,Status,GrandTotal,NetTotal,
																		   CreatedBy,SupplierID,Code,SupplierContactCompany,SupplierContactAddressID,ContactID,SystemSiteID,FootNote,TotalTax, UserNotes,FlagID) values (GetDate(), @Qty1GrossTotal, 2, 31,  @Qty1GrossTotal, @NetTotal, @CreatedBy, @SupplierID,@Code,@SupplierName,
																		   @SupplierAdressID,@SupplierContactID,@SystemSiteID, @FootNotes,@NetTax, @UserNotes,@FlagId)  
																           
																			SET @PurchaseID = (SELECT SCOPE_IDENTITY());
																            
																			
																			--insert into tbl_purchasedetail (ItemID,quantity,PurchaseID,price,TotalPrice,packqty,ItemCode,
																			--ItemName, Discount, NetTax, freeitems,ItemBalance, TaxID,ServiceDetail) values 
																			--(@ItemID,0,@PurchaseID, @Qty1GrossTotal, @Qty1GrossTotal, @Qty1, @ItemCode, @ProductName,@MarkupVaue,@Qty1Tax1Value,0,0,@TaxID,@ProductCompleteName)
															            
																		   insert into PurchaseDetail (ItemID,quantity,PurchaseID,price,TotalPrice,packqty,ItemCode,
																			ItemName,  freeitems,ItemBalance,ServiceDetail,NetTax) values 
																			(@ItemID,1,@PurchaseID, @Qty1GrossTotal, @Qty1GrossTotal, @Qty1, @ItemCode, @ProductName,0,0,@ProductCompleteName,@NetTax)
																			set @PurchaseDetailID = (SELECT SCOPE_IDENTITY());
																			
																			--select @LastTotalPrice = @Qty1GrossTotal,
																			--	   @LastTotalTax = @NetTax,
																			--	   @LastGrandTotal = @Qty1GrossTotal,
																			--	   @LastNetTotal = @NetTotal
																			
																			
																				   --@LastDiscount = @MarkupVaue  
																			
																		
																			
																			insert into @SupplierIDs values (@SupplierID,@PurchaseID,@Qty1GrossTotal,@NetTax,@Qty1GrossTotal,@NetTotal)
																			Update top(1) prefix set PONext = PONext + 1  --line added by naveed after commenting lines above
																			
																	 end  -- end of else part of exist 
							   end -- end of supplier > 0
							
						
						
		    SET @Counter = @Counter + 1
		 end -- end of while
		
		 SET @Err = @@Error	
		 select @PurchaseDetailID as MSG
	
		RETURN @Err
END


/* Execution Date: 11-06-2015 */

/****** Object:  StoredProcedure [dbo].[usp_TotalEarnings]    Script Date: 6/11/2015 1:02:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO 

ALTER PROCEDURE [dbo].[usp_TotalEarnings] (@fromdate as datetime, @todate as datetime, @Organisationid bigint)

AS

--define limits

--set @fromdate = '2015-01-01'

--set @todate = '2015-12-31'

 BEGIN
 
;With DateSequence( [Date] ) as

(

    Select @fromdate as [Date]

        union all

    Select dateadd(day, 1, [Date])

        from DateSequence

        where Date < @todate

)

 

--select result

Select

    CONVERT(VARCHAR,[Date],112) as ID,

    [Date] as [Date],

    DATEPART(DAY,[Date]) as [Day],

    CASE

         WHEN DATEPART(DAY,[Date]) = 1 THEN CAST(DATEPART(DAY,[Date]) AS VARCHAR) + 'st'

         WHEN DATEPART(DAY,[Date]) = 2 THEN CAST(DATEPART(DAY,[Date]) AS VARCHAR) + 'nd'

         WHEN DATEPART(DAY,[Date]) = 3 THEN CAST(DATEPART(DAY,[Date]) AS VARCHAR) + 'rd'

         ELSE CAST(DATEPART(DAY,[Date]) AS VARCHAR) + 'th'

    END as [DaySuffix],

    DATENAME(dw, [Date]) as [DayOfWeek],

    DATEPART(DAYOFYEAR,[Date]) as [DayOfYear],

    DATEPART(WEEK,[Date]) as [WeekOfYear],

    DATEPART(WEEK,[Date]) + 1 - DATEPART(WEEK,CAST(DATEPART(MONTH,[Date]) AS VARCHAR) + '/1/' + CAST(DATEPART(YEAR,[Date]) AS VARCHAR)) as [WeekOfMonth],

    DATEPART(MONTH,[Date]) as [Month],

    DATENAME(MONTH,[Date]) as [MonthName],

    DATEPART(QUARTER,[Date]) as [Quarter],

    CASE DATEPART(QUARTER,[Date])

        WHEN 1 THEN 'First'

        WHEN 2 THEN 'Second'

        WHEN 3 THEN 'Third'

        WHEN 4 THEN 'Fourth'

    END as [QuarterName],

    DATEPART(YEAR,[Date]) as [Year]

	into #dt

from DateSequence option (MaxRecursion 10000)

--select * from #dt

select sum(Estimate_Total) Total,count(*) Orders,store,Month,monthname,year

	from

	(
	

		select  estimateid, o.Estimate_Total,

		DATENAME(MONTH,O.Order_Date) as [Month],

		DATEPART(MONTH,O.Order_Date) as [MonthName],

		DATEPART(YEAR,O.Order_Date) as [Year],

		(Case when C.Iscustomer = 3 then C.name when C.IsCustomer = 1 then S.name End) as store,

		C.IsCustomer


		from Estimate O

		inner join Company C on O.CompanyId = C.companyid

		left outer join Company S on C.StoreId = S.companyid

		where (O.StatusId <> 3 and O.StatusId <> 34) and C.OrganisationId = @Organisationid and C.IsCustomer <> 0



		



	) data

group by month,store,monthname,year
order by monthname


END

------------------------------------------------------------------------

/****** Object:  Table [dbo].[VariableExtension]    Script Date: 11/06/2015 11:20:58 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[VariableExtension](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FieldVariableId] [int] NULL,
	[CompanyId] [int] NULL,
	[OrganisationId] [int] NULL,
	[VariablePrefix] [nvarchar](max) NULL,
	[VariablePostfix] [nvarchar](max) NULL,
	[CollapsePrefix] [bit] NULL,
	[CollapsePostfix] [bit] NULL,
 CONSTRAINT [PK_VariableExtension] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

CREATE TABLE [dbo].[VariableExtension](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FieldVariableId] [int] NULL,
	[CompanyId] [int] NULL,
	[OrganisationId] [int] NULL,
	[VariablePrefix] [nvarchar](max) NULL,
	[VariablePostfix] [nvarchar](max) NULL,
	[CollapsePrefix] [bit] NULL,
	[CollapsePostfix] [bit] NULL,
 CONSTRAINT [PK_VariableExtension] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

alter table goodsreceivednote
drop constraint DF__tbl_goods__Suppl__52E34C9D

alter table goodsreceivednote
alter column supplierid bigint null

--Executed on Staging, USA, Europe, Australia servers on 20150611---
update goodsreceivednote
set supplierid = null

alter table goodsreceivednote
alter column supplierid bigint null

update goodsreceivednote
set supplierid = null

alter table goodsreceivednote
add constraint FK_GoodsReceivedNote_Company
foreign key (SupplierId)
references Company (CompanyId)

alter table goodsreceivednoteDetail
add constraint FK_GoodsReceivedNoteDetail_GoodsReceivedNote
foreign key (GoodsReceivedId)
references GoodsReceivedNote (GoodsReceivedId)
on delete cascade

delete from goodsreceivednote

alter table goodsreceivednote
add constraint FK_GoodsReceivedNote_SectionFlag
foreign key (FlagId)
references SectionFlag (SectionFlagId)

alter table goodsreceivednoteDetail
alter column itemid bigint null

update goodsreceivednoteDetail
set ItemId = null
where ItemId not in (select ItemId from Items)

alter table goodsreceivednoteDetail
add constraint FK_GoodsReceivedNoteDetail_Items
foreign key (ItemId)
references Items (ItemId)

alter table goodsreceivednoteDetail
add ProductType int null

alter table goodsreceivednoteDetail
add RefItemId bigint null

alter table productcategory
alter column ImagePath nvarchar(400) null

alter table productcategory
alter column ThumbnailPath nvarchar(400) null

/* Execution Date: 12/06/2015 */

alter table VariableExtension
alter column FieldVariableId bigint null

alter table VariableExtension
add constraint FK_VariableExtension_FieldVariable
foreign key (FieldVariableId)
references FieldVariable (VariableId)

alter table CompanyContact
add SecondaryEmail varchar(200) null

/* Execution Date: 15/06/2015 */

alter table VariableExtension
drop constraint FK_VariableExtension_FieldVariable

alter table VariableExtension
add constraint FK_VariableExtension_FieldVariable
foreign key (FieldVariableId)
references FieldVariable (VariableId)
on delete cascade

/* Execution Date: 16/06/2015 */

alter table goodsreceivednote
alter column createdby nvarchar(max) null

alter table goodsreceivednote
alter column createdby uniqueidentifier null

/* Execution Date: 17/06/2015 */

/****** Object:  StoredProcedure [dbo].[usp_DeleteCRMCompanyByID]    Script Date: 06/17/2015 5:30:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_DeleteCRMCompanyByID] 
    @CompanyID int
AS 
    SET NOCOUNT ON;
    declare @IsCustomer int
	declare @EstimateID bigint = 0
	declare @itemID bigint = 0
	declare @invoiceId bigint = 0
	declare @DeliveryNoteId bigint = 0
	declare @PurchaseId bigint = 0
	declare @GoodsReceivedId bigint = 0

	select @IsCustomer = iscustomer from company where companyid = @CompanyID
	--delete nls
	--from NewsLetterSubscriber nls
	--inner join CompanyContact c on c.ContactID = nls.ContactID
	--where c.CompanyId = @CompanyID

	--deleting the tbl_Inquiry Attachments
	delete  IA
		from InquiryAttachment IA
		inner join Inquiry I on IA.InquiryID = I.InquiryID
		inner join Company CC on CC.CompanyId = I.CompanyId
		where CC.CompanyId = @CompanyID

	--deleting the tbl_Inquiry_Items
	delete  II
	from InquiryItem II
	inner join dbo.Inquiry I on II.InquiryID = I.InquiryID
	inner join Company CC on CC.CompanyID = I.CompanyId
	where CC.CompanyID = @CompanyID

	--deleting the Inquiries
	delete  I
	from dbo.Inquiry I
	--inner join CompanyContact CC on CC.ContactId = I.ContactId
	where I.CompanyId = @CompanyID

	-- checking if is supplier
if (@IsCustomer = 2) 
begin
	/* Delete Purchase. First delete purchase details*/ 
	select @PurchaseId = PurchaseId from Purchase where SupplierId = @CompanyID
	delete from PurchaseDetail where PurchaseId = @PurchaseId
	delete from Purchase where SupplierId = @CompanyID

	/* Delete Goods Received Note. First delete Goods Received Note Details */ 
	select @GoodsReceivedId = GoodsReceivedId from GoodsReceivedNote where SupplierId = @CompanyID
	delete from GoodsReceivedNoteDetail where GoodsreceivedId = @GoodsReceivedId 
	delete from GoodsReceivedNote where SupplierId = @CompanyID
end

else
begin
	/* Delete Invoice. Items from items table against that invoice, invoice detail table */ 
	select @invoiceId = invoiceId from invoice where companyid = @CompanyId
	delete from InvoiceDetail where invoiceId = @invoiceId
	delete from items where invoiceId = @invoiceId 
	delete from invoice where companyid = @CompanyId

	/* Delete Delivery Note. First delete delivery note detail */ 
	select @DeliveryNoteId = DeliveryNoteId from DeliveryNote where companyid = @CompanyID
	delete from DeliveryNoteDetail where DeliveryNoteId =  @DeliveryNoteId
	delete from DeliveryNote where companyid = @CompanyID
end

	delete from CompanyContact where companyid = @CompanyID
	delete from Address where companyid = @CompanyID
	delete from CompanyTerritory where companyid = @CompanyID
	-- to delete ordered items and order
	declare @TVP table 
	( 
	id INT IDENTITY NOT NULL PRIMARY KEY,
	OrderID bigint
	)
	declare @OP table 
	( 
	id INT IDENTITY NOT NULL PRIMARY KEY,
	ItemID bigint
	)
	declare @temp table(
	id INT IDENTITY NOT NULL PRIMARY KEY,
	CompanyID bigint
	)

	INSERT INTO @TVP (OrderID)
		select  EstimateID from estimate
		where companyID = @CompanyID
	
	declare @Totalrec int
	select @Totalrec = COUNT(*) from @TVP
 
	declare @currentrec int

	set @currentrec = 1

	WHILE (@currentrec <=@Totalrec)
		 BEGIN
			 select @EstimateID = OrderID from @TVP
			 where ID = @currentrec

			 INSERT INTO @OP (ItemID)
			 select ItemID from Items where estimateid = @EstimateID

			-- loop for ordered items	
			 declare @TotalItems int
			 select @TotalItems = COUNT(*) from @OP
 
			 declare @currentItemRec int
			 set @currentItemRec = 1
			  WHILE (@currentItemRec <= @TotalItems)
				 BEGIN
				  select @ItemID = ItemID from @OP
						 where ID = @currentItemRec

						Exec usp_DeleteProduct  @ItemID

						set @currentItemRec = @currentItemRec + 1
				 end
				 	delete 
				from PrePayment where orderid = @EstimateID

			 delete from estimate where estimateid = @EstimateID
		 SET @currentrec = @currentrec + 1
		 end

	delete from company where companyid = @CompanyID

/* Execution Date: 18/06/2015 */

alter table ItemStockUpdateHistory
drop column LastOrderedQty

alter table ItemStockUpdateHistory
drop column LastAvailableQty

alter table ItemStockUpdateHistory
add StockItemId bigint null

ALTER TABLE ItemStockUpdateHistory  
WITH CHECK ADD  CONSTRAINT [FK_ItemStockUpdateHistory_StockItem] FOREIGN KEY([StockItemID])
REFERENCES [StockItem] ([StockItemId])
on delete cascade

ALTER TABLE ItemStockUpdateHistory CHECK CONSTRAINT [FK_ItemStockUpdateHistory_StockItem]
GO

alter table ItemAddonCostCentre
drop constraint FK_ItemStockOption_ItemAddonCostCentre


alter table ItemAddonCostCentre
add constraint FK_ItemAddonCostCentre_ItemStockOption
foreign key (ItemStockOptionId)
references ItemStockOption (ItemStockOptionId)
on delete cascade

-----Executed on All servers on 20150618------------
/* Execution Date: 19/06/2015 */

alter table ItemStockUpdateHistory
alter column LastModifiedBy nvarchar(max) null

update ItemStockUpdateHistory
set LastModifiedBy = null

alter table ItemStockUpdateHistory
alter column LastModifiedBy uniqueidentifier null

alter table ItemStockUpdateHistory
add constraint FK_ItemStockUpdateHistory_SystemUser
foreign key (LastModifiedBy)
references SystemUser (SystemUserId)


-----Executed on All servers on 20150622------------

update FieldVariable set CriteriaFieldName = 'SecondaryEmail' where VariableTag like '{{Email}}'

/* Execution Date: 29/06/2015 */

alter table templateBackgroundImage add hasClippingPath bit null


alter table templateBackgroundImage add clippingFileName nvarchar(max) null

alter table templateObject add hasClippingPath bit null




SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[sp_GetTemplateImages]
	@isCalledFrom int = 0, 
	@imageSetType int = 0, 
	@templateID bigint = 0, 
	@contactCompanyID bigint = 0, 
	@contactID bigint = 0, 
	@territory bigint = 0, 
	@pageNumber int = 0,
	@pageSize int =0,
	@sortColumn nvarchar = '',
	@search varchar(255) = '',
	@imageCount int = 0 output 
AS

BEGIN
	SET NOCOUNT ON;
	declare @result TABLE (			ID int,
	ProductID int,
	ImageName varchar(300),
	Name varchar(300),
	flgPhotobook bit,
	flgCover bit,
	BackgroundImageAbsolutePath nvarchar(500),
	BackgroundImageRelativePath nvarchar(500),
	ImageType int,
	ImageWidth int,
	ImageHeight int,
	ImageTitle nvarchar(max),
	ImageDescription nvarchar(max),
	ImageKeywords nvarchar(max),
	UploadedFrom int,
	ContactCompanyID int,
	ContactID int,
	hasClippingPath bit,
	clippingFileName nvarchar(max)
	) 
	IF(@isCalledFrom = 1)  --DESIGNER V2
		BEGIN 
			-- getting old template images and all the images uploaded by DESIGNERS
			IF(@contactCompanyID = -999) 
				BEGIN
					IF(@imageSetType = 1) 
						BEGIN
							Insert into @result
								select * from templateBackgroundImage where (imagetype = 2 and ProductID =  @templateID)
								and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%')
								ORDER BY  ID DESC
						END
					ELSE IF (@imageSetType = 12) 
						BEGIN
						Insert into @result
							select * from templateBackgroundImage where (imagetype = 4 and ProductID =  @templateID)
								and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%')
								ORDER BY  ID DESC
						END
					ELSE IF (@imageSetType = 6 ) 
						BEGIN
							Insert into @result
								select * from templateBackgroundImage where (imagetype = 1 and ContactCompanyID = -999)
								and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%')
								 ORDER BY  ID DESC
						END
					ELSE IF ( @imageSetType = 7) 
						BEGIN
							Insert into @result
								select * from templateBackgroundImage where ( imagetype = 3 and ContactCompanyID = -999)
								and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%')
								 ORDER BY  ID DESC
						END
					ELSE IF ( @imageSetType = 13) 
						BEGIN
							Insert into @result
								select * from templateBackgroundImage where ( imagetype = 13 and ContactCompanyID = -999)
								and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%')
								 ORDER BY  ID DESC
						END
					ELSE IF ( @imageSetType = 14) 
						BEGIN
							Insert into @result
								select * from templateBackgroundImage where ( imagetype = 14 and ContactCompanyID = -999)
								and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%')
								 ORDER BY  ID DESC
						END
				END
			ELSE
				BEGIN
					-- DESINGER V2 CUSTOMER MODE
					IF(@imageSetType = 1) 
						BEGIN
							Insert into @result
								select * from templateBackgroundImage where (imagetype = 2 and ProductID =  @templateID)
								and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%')
								ORDER BY  ID DESC
						END
					ELSE IF(@imageSetType = 12) 
						BEGIN
							Insert into @result
								select * from templateBackgroundImage where (imagetype = 4 and ProductID =  @templateID)
								and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%')
								ORDER BY  ID DESC
						END
					ELSE IF (@imageSetType = 10) 
						BEGIN
							Insert into @result
								select * from templateBackgroundImage where (imagetype = 1 and  ContactCompanyID = @contactCompanyID and (ContactID = 0 or ContactID is null)) 
								and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%')
								ORDER BY  ID DESC
						END
					ELSE IF (@imageSetType = 11) 
						BEGIN
							Insert into @result
								select * from templateBackgroundImage where (imagetype = 3 and ContactCompanyID = @contactCompanyID and (ContactID = 0 or ContactID is null)) 
								and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%')
								ORDER BY  ID DESC
						END
				END
		END
	ELSE IF(@isCalledFrom = 2)  --mis
		BEGIN 
		-- getting old template images and all the images uploaded by that Company
			IF(@imageSetType = 1) 
				BEGIN
					Insert into @result
						select * from templateBackgroundImage where (imagetype = 2 and ProductID =  @templateID) 
						and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%') 
						ORDER BY  ID DESC
				END
			ELSE IF(@imageSetType = 12) 
				BEGIN
					Insert into @result
						select * from templateBackgroundImage where (imagetype = 4 and ProductID =  @templateID) 
						and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%') 
						ORDER BY  ID DESC
				END
			ELSE IF (@imageSetType = 2) 
				BEGIN
					Insert into @result
						select * from templateBackgroundImage where (imagetype = 1 and ContactCompanyID = @contactCompanyID and (ContactID = 0 or ContactID is null)) 
						and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%') 
						ORDER BY  ID DESC
				END
			ELSE IF (@imageSetType = 3) 
				BEGIN
					Insert into @result
						select * from templateBackgroundImage where (imagetype = 3 and ContactCompanyID = @contactCompanyID and (ContactID = 0 or ContactID is null)) 
						and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%') 
						ORDER BY  ID DESC
				END
			ELSE IF (@imageSetType = 16) 
				BEGIN
					Insert into @result
						select * from templateBackgroundImage where (imagetype = 16 and ContactCompanyID = @contactCompanyID and (ContactID = 0 or ContactID is null)) 
						and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%') 
						ORDER BY  ID DESC
				END	 
			ELSE IF (@imageSetType = 17) 
				BEGIN
					Insert into @result
						select * from templateBackgroundImage where (imagetype = 17 and ContactCompanyID = @contactCompanyID and (ContactID = 0 or ContactID is null)) 
						and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%') 
						ORDER BY  ID DESC
				END	
		END
	ELSE IF (@isCalledFrom = 3) --retail end user
	BEGIN
	   -- -999 is contact company id of designerv2 mpc users 
		IF(@imageSetType = 1) 
			BEGIN
				Insert into @result
					select * from templateBackgroundImage where (imagetype = 2 and ProductID =  @templateID) 
					and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%') 
					ORDER BY  ID DESC
			END
		ELSE IF(@imageSetType = 12) 
			BEGIN
				Insert into @result
					select * from templateBackgroundImage where (imagetype = 4 and ProductID =  @templateID) 
					and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%') 
					ORDER BY  ID DESC
			END
		ELSE IF (@imageSetType = 6 ) 
			BEGIN
				Insert into @result
					select * from templateBackgroundImage where  imagetype = 1 and ContactCompanyID = -999 
					and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%')
				    ORDER BY  ID DESC
			END
		ELSE IF (@imageSetType = 7) 
			BEGIN
				Insert into @result
					select * from templateBackgroundImage where imagetype = 3 and  ContactCompanyID = -999 
					and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%')
				    ORDER BY  ID DESC
			END
		ELSE IF (@imageSetType = 8) 
			BEGIN
				Insert into @result
					select * from templateBackgroundImage where ( imagetype = 1 and ContactCompanyID = @contactCompanyID and ContactID =@contactID) 
					and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%')
					ORDER BY  ID DESC
			END		
		ELSE IF (@imageSetType = 9 ) 
			BEGIN
				Insert into @result
					select * from templateBackgroundImage where ( imagetype = 3 and ContactCompanyID = @contactCompanyID and ContactID =@contactID) 
					and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%')
					ORDER BY  ID DESC
			END		
		ELSE IF (@imageSetType = 15 ) 
			BEGIN
				Insert into @result
					select * from templateBackgroundImage where ( imagetype = 14 and ContactCompanyID = @contactCompanyID and ContactID =@contactID) 
					and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%')
					ORDER BY  ID DESC
			END	
		ELSE IF ( @imageSetType = 13) 
						BEGIN
							Insert into @result
								select * from templateBackgroundImage where ( imagetype = 13 and ContactCompanyID = -999)
								and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%')
								 ORDER BY  ID DESC
						END
		ELSE IF ( @imageSetType = 14) 
						BEGIN
							Insert into @result
								select * from templateBackgroundImage where ( imagetype = 14 and ContactCompanyID = -999)
								and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%')
								 ORDER BY  ID DESC
						END		
	END
	ELSE IF (@isCalledFrom = 4)  -- mis end user
	BEGIN
		IF(@imageSetType = 1) 
			BEGIN
				Insert into @result
					select * from templateBackgroundImage where (imagetype = 2 and ProductID =  @templateID) 
					and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%') 
					ORDER BY  ID DESC
			END
		ELSE IF(@imageSetType = 12) 
			BEGIN
				Insert into @result
					select * from templateBackgroundImage where (imagetype = 4 and ProductID =  @templateID) 
					and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%') 
					ORDER BY  ID DESC
			END
		ELSE IF (@imageSetType = 2) 
			BEGIN
				Insert into @result	
				 select tbi.* from templateBackgroundImage tbi
				 inner join ImagePermissions ip on ip.ImageID = tbi.ID
				 where ip.TerritoryID = @territory 
				 and  (imagetype = 1 and (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%'))
				 ORDER BY  ID DESC
			END
		ELSE IF (@imageSetType = 3) 
			BEGIN
				Insert into @result	
				 select tbi.* from templateBackgroundImage tbi
				 inner join ImagePermissions ip on ip.ImageID = tbi.ID
				 where ip.TerritoryID = @territory 
				 and  (imagetype = 3 and (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%'))
				 ORDER BY  ID DESC
			END
		ELSE IF (@imageSetType = 4) 
			BEGIN
				Insert into @result
				select * from templateBackgroundImage where (imagetype = 1 and ContactCompanyID = @contactCompanyID and ContactID =@contactID) 
				and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%') 
				ORDER BY  ID DESC
			END		
		ELSE IF (@imageSetType = 5) 
			BEGIN
				Insert into @result
				select * from templateBackgroundImage where (imagetype = 3 and ContactCompanyID = @contactCompanyID and ContactID =@contactID) 
				and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%') 
				ORDER BY  ID DESC
			END	
		ELSE IF (@imageSetType = 16) 
			BEGIN
				Insert into @result	
				 select tbi.* from templateBackgroundImage tbi
				 inner join ImagePermissions ip on ip.ImageID = tbi.ID
				 where ip.TerritoryID = @territory 
				 and  (imagetype = 16 and (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%'))
				 ORDER BY  ID DESC
			END	
		ELSE IF (@imageSetType = 17) 
			BEGIN
				Insert into @result	
				 select tbi.* from templateBackgroundImage tbi
				 inner join ImagePermissions ip on ip.ImageID = tbi.ID
				 where ip.TerritoryID = @territory 
				 and  (imagetype = 17 and (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%'))
				 ORDER BY  ID DESC
			END
	END
	
	select @imageCount = count(ID) from @result
	-- apply sort by column name
	--SELECT * FROM @result
		--ORDER BY @sortColumn;
	-- result selected now apply paging
	declare @currPage int = 1
   
	declare @RecCount int = 0
	select @recCount = count(ID) from @result
	declare @Start int = 1
	declare @end int = 10

	if(@pageNumber != 1)
	   Begin
		set @start = (@pageNumber * @pageSize) - @pageSize + 1
	   End
	else
	   Begin 
		 set @start = 1
	   End

	set  @end = @Start + @pageSize
 
	Select * From
	(SELECT ROW_NUMBER() OVER(ORDER BY ID desc) AS RowNum, *
	From @result) as sub
	Where sub.RowNum >= @Start and sub.RowNum < @end
	
	
END
GO


/* Execution Date: 02/07/2015 */

--Stored Procedure to delete all StagingImportCompanyContactAddress table records
--Procedure is using while uploading company contacts from csv file

Create PROCEDURE [dbo].[usp_DeleteStagingImportCompanyContactAddress]
AS
BEGIN
delete from StagingImportCompanyContactAddress
end

alter table SystemUser add EmailSignature nvarchar(max) null
alter table SystemUser add EstimateHeadNotes nvarchar(max) null
alter table SystemUser add EstimateFootNotes nvarchar(max) null 

/* Execution Date: 06/07/2015 */

Alter table templateObject
Add isBulletPoint bit null

/* Execution Date: 07/07/2015 */

alter table GoodsReceivedNote
alter Column CreatedBy nvarchar null

alter table GoodsReceivedNote
alter Column CreatedBy uniqueidentifier null

/* Execution Date: 10/07/2015 */

GO
/****** Object:  StoredProcedure [dbo].[usp_DeleteTemplate]    Script Date: 7/10/2015 10:02:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_DeleteTemplate]
	-- Add the parameters for the stored procedure here
	
	@TemplateID bigint

AS
BEGIN

 -- delete template objects

delete from TemplateObject where productid = @TemplateID


 -- delete template pages
 delete from TemplatePage where productid = @TemplateID

  -- delete image permisssions

 DELETE imgPer
				FROM ImagePermissions imgPer
				inner join TemplateBackgroundImage tbi on tbi.Id = imgPer.ImageID
				where tbi.ProductID = @TemplateID


 -- delete template background images
  delete from TemplateBackgroundImage where productid = @TemplateID


delete from TemplateVariable where templateid = @TemplateID
-- delete template 
DELETE from template where ProductId = @TemplateID

end

alter table organisation add AgileApiKey nvarchar(255) null
alter table organisation add AgileApiUrl nvarchar(255) null

/* Execution Date: 14/07/2015 */

alter table DiscountVoucher add VoucherName varchar(200)
alter table DiscountVoucher add DiscountType int 
alter table DiscountVoucher add HasCoupon bit
alter table DiscountVoucher add CouponCode nvarchar(255)
alter table DiscountVoucher add CouponUseType int
alter table DiscountVoucher add IsUseWithOtherCoupon bit
alter table DiscountVoucher add IsTimeLimit bit
alter table DiscountVoucher add IsQtyRequirement bit
alter table DiscountVoucher add MinRequiredQty int
alter table DiscountVoucher add MaxRequiredQty int
alter table DiscountVoucher add IsOrderPriceRequirement bit
alter table DiscountVoucher add MinRequiredOrderPrice int
alter table DiscountVoucher add MaxRequiredOrderPrice int
alter table DiscountVoucher add CustomerId bigint
alter table DiscountVoucher add IsSingleUseRedeemed bit
alter table DiscountVoucher add IsQtySpan bit


/****** Object:  Table [dbo].[ItemsVoucher]    Script Date: 7/13/2015 2:34:20 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ItemsVoucher](
      [ItemVoucherId] [bigint] IDENTITY(1,1) NOT NULL,
      [ItemId] [bigint] NULL,
      [VoucherId] [bigint] NULL,
CONSTRAINT [PK_ItemsVoucher] PRIMARY KEY CLUSTERED 
(
      [ItemVoucherId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


/****** Object:  Table [dbo].[ProductCategoryVoucher]    Script Date: 7/13/2015 2:34:49 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ProductCategoryVoucher](
      [CategoryVoucherId] [bigint] IDENTITY(1,1) NOT NULL,
      [ProductCategoryId] [bigint] NULL,
      [VoucherId] [bigint] NULL,
CONSTRAINT [PK_ProductCategoryVoucher] PRIMARY KEY CLUSTERED 
(
      [CategoryVoucherId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO



/****** Object:  Table [dbo].[TemplateVariableExtension]    Script Date: 24/07/2015 10:53:32 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TemplateVariableExtension](
	[TemplateVariableExtId] [bigint] IDENTITY(1,1) NOT NULL,
	[TemplateId] [bigint] NULL,
	[FieldVariableId] [bigint] NULL,
	[HasPrefix] [bit] NULL,
	[HasPostFix] [bit] NULL,
 CONSTRAINT [PK_TemplateVariableExtension] PRIMARY KEY CLUSTERED 
(
	[TemplateVariableExtId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


---Executed on Live Servers--------

alter table Organisation add isAgileActive bit
alter table Organisation add isTrial bit
alter table Organisation add LiveStoresCount int
alter table company add isStoreLive bit


/****** Object:  Table [dbo].[CompanyVoucherRedeem]    Script Date: 7/28/2015 2:05:27 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CompanyVoucherRedeem](
	[VoucherRedeemId] [bigint] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NULL,
	[DiscountVoucherId] [bigint] NULL,
	[RedeemDate] [datetime] NULL,
 CONSTRAINT [PK_CompanyVoucherRedeem] PRIMARY KEY CLUSTERED 
(
	[VoucherRedeemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

---Executed on Live Servers--------
alter table Organisation add WebStoreOrdersCount int
alter table Organisation add MisOrdersCount int

---Executed on Live Servers on 2015 29 2015-----


/****** Object:  StoredProcedure [dbo].[usp_ChartRegisteredUserByStores]    Script Date: 7/30/2015 8:24:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Muhammad Naveed
-- Create date: 2015 07 30
-- Description:	To Get Charts data of Registered Users by store
-- =============================================
-- Exec [usp_ChartRegisteredUserByStores] 1
create PROCEDURE [dbo].[usp_ChartRegisteredUserByStores]
	@OrganisationId bigint
AS
BEGIN
		select Name,sum(TotalContacts) as TotalContacts, Month, MonthName, Year
			from
			(
				select c.Name, ct.[TotalContacts],c.companyid,
				DATENAME(MONTH,c.CreationDate) as [MonthName],
				DATEPART(MONTH,c.CreationDate) as [Month],
				DATEPART(YEAR,c.CreationDate) as [Year]
				from company c
				inner join (
							SELECT  companyid,Count(*) AS TotalContacts
							FROM companycontact
							GROUP BY companyid
							HAVING  companyid in ( select companyId from company where organisationID = @OrganisationId and storeId is null)
							)
							ct on ct.companyid = c.CompanyId
			) data

		group by month,Name,monthname,year
		order by TotalContacts desc, Month

			RETURN 
	END

	
/****** Object:  StoredProcedure [dbo].[usp_ChartTopPerformingStores]    Script Date: 7/30/2015 8:25:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Muhammad Naveed
-- Create date: 2015 07 30
-- Description:	To Get Charts data of Top Performing Stores
-- =============================================
-- Exec [usp_ChartTopPerformingStores]
ALTER PROCEDURE [dbo].[usp_ChartTopPerformingStores]
	@OrganisationId bigint
AS
BEGIN

		select Name,sum(TotalCustomers) as TotalCustomers, Month, MonthName, Year
			from
			(		
				select c.Name, ct.[TotalCustomers],		
				DATENAME(MONTH,c.CreationDate) as [MonthName],
				DATEPART(MONTH,c.CreationDate) as [Month],
				DATEPART(YEAR,c.CreationDate) as [Year]
				from company c
				inner join	(
								SELECT storeId, COUNT(*) AS [TotalCustomers] FROM company
								GROUP BY StoreId
								HAVING  StoreId in ( select companyId from company where organisationID = @organisationid and storeId is null )
							) ct on ct.StoreId = c.CompanyId

			) data

		group by month,Name,monthname,year
		order by TotalCustomers desc
			RETURN 
	END


alter table DiscountVoucher
add OrganisationId bigint

alter table Items
add DiscountVoucherID bigint

alter table Estimate
alter column DiscountVoucherID bigint

alter table CompanyContact add RegistrationDate datetime

update companyContact set RegistrationDate = c.CreationDate
from  companyContact cc, Company c
where c.CompanyId = cc.CompanyId

alter table CompanyVoucherRedeem add ContactId bigint



------------------------


ALTER TABLE ProductCategoryVoucher
ADD CONSTRAINT FK_DiscountVoucher_ProductCategoruVoucher
FOREIGN KEY (VoucherId) REFERENCES DiscountVoucher(DiscountVoucherId)



ALTER TABLE ItemsVoucher
ADD CONSTRAINT FK_DiscountVoucher_ItemsVoucher
FOREIGN KEY (VoucherId) REFERENCES DiscountVoucher(DiscountVoucherId)


---------------



/****** Object:  StoredProcedure [dbo].[usp_OrderReport]    Script Date: 8/11/2015 12:21:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER procedure [dbo].[usp_OrderReport]
 @organisationId bigint,
 @OrderID bigint
AS
Begin


SELECT   dbo.Items.ItemID,dbo.company.companyid, dbo.Items.Title, isnull(dbo.Items.Qty1,0) As Qty1, dbo.Items.Qty2, dbo.Items.Qty3, dbo.Items.Qty1NetTotal, dbo.Items.Qty2NetTotal, dbo.Items.Qty3NetTotal,dbo.Items.ProductCode, 
                      
					 dbo.Items.JobDescription1 as JobDescription1,dbo.Items.JobDescription2 as JobDescription2,dbo.Items.JobDescription3 as JobDescription3,dbo.Items.JobDescription4 as JobDescription4,dbo.Items.JobDescription5 as JobDescription5,
					 dbo.Items.JobDescription6 as JobDescription6, dbo.Items.JobDescription7 as JobDescription7,
					 
					  CASE 
						  WHEN dbo.Items.JobDescription1 is null then null
						  WHEN dbo.Items.JobDescription1 is not null THEN  dbo.Items.JobDescriptionTitle1
				       END As JobDescriptionTitle1,
				        CASE 
						  WHEN dbo.Items.JobDescription2 is null THEN Null
						  WHEN dbo.Items.JobDescription2 is not null THEN  dbo.Items.JobDescriptionTitle2
				       END As JobDescriptionTitle2,
				        CASE 
						  WHEN dbo.Items.JobDescription3 is null THEN Null
						  WHEN dbo.Items.JobDescription3 is not null THEN  dbo.Items.JobDescriptionTitle3
				       END As JobDescriptionTitle3,
				        CASE 
						  WHEN dbo.Items.JobDescription4 is null THEN Null
						  WHEN dbo.Items.JobDescription4 is not null THEN  dbo.Items.JobDescriptionTitle4
				       END As JobDescriptionTitle4,
				        CASE 
						  WHEN dbo.Items.JobDescription5 is null THEN Null
						  WHEN dbo.Items.JobDescription5 is not null THEN  dbo.Items.JobDescriptionTitle5
				       END As JobDescriptionTitle5,
				        CASE 
						  WHEN dbo.Items.JobDescription6 is null THEN Null
						  WHEN dbo.Items.JobDescription6 is not null THEN  dbo.Items.JobDescriptionTitle6
				       END As JobDescriptionTitle6,
				        CASE 
						  WHEN dbo.Items.JobDescription7 is null THEN Null
						  WHEN dbo.Items.JobDescription7 is not null THEN  dbo.Items.JobDescriptionTitle7
				       END As JobDescriptionTitle7,
                      dbo.Items.JobDescription, dbo.Estimate.Estimate_Name, dbo.Estimate.Order_Code, dbo.Estimate.Estimate_Total, dbo.Estimate.FootNotes, 
                      dbo.Estimate.HeadNotes, dbo.Estimate.Order_Date, dbo.Estimate.Greeting, dbo.Estimate.CustomerPO, dbo.address.AddressName, 
                      dbo.address.Address1, dbo.address.Address2, dbo.address.Address3, dbo.address.Email, dbo.address.Fax, (select StateName from State where StateId in (select StateId from address where addressid =  dbo.Address.AddressId)) as State,(select countryname from country where countryid in (select countryid from address where addressid =  dbo.Address.AddressId)) as Country,
                      dbo.address.City, dbo.address.URL, dbo.address.Tel1, dbo.Company.AccountNumber, dbo.address.PostCode, 
                      dbo.Company.Name AS CustomerName, dbo.Company.URL AS CustomerURL, dbo.Estimate.EstimateID, dbo.items.ProductName, 
                      dbo.CompanyContact.FirstName + ISNULL(' ' + dbo.CompanyContact.MiddleName, '') + ISNULL(' ' + dbo.CompanyContact.LastName, '') AS ContactName, 
                      --dbo.SystemUser.FullName, (select ReportBanner from reportnote where ReportCategoryID=12) as BannerPath ,
                      (select top 1 ReportTitle from reportnote where ReportCategoryID=12 and organisationid = @organisationId) as ReportTitle,
                      
					   CASE 
						  WHEN dbo.Company.IsCustomer = 3 then 
						  isnull((select top 1 ISNULL(BannerAbsolutePath,'http://preview.myprintcloud.com/mis/') + isnull(ReportBanner,'MPC_Content/Reports/Banners/ReportBannerOrder.png')  from reportnote where ReportCategoryID=12 and organisationid = @organisationId and CompanyId = Company.CompanyId),'http://preview.myprintcloud.com/mis/MPC_Content/Reports/Banners/ReportBannerOrder.png')
						  WHEN (dbo.Company.IsCustomer = 4 or dbo.Company.IsCustomer = 1 or  dbo.Company.IsCustomer = 0 or dbo.Company.IsCustomer = 2)  THEN  
						    isnull((select top 1 ISNULL(BannerAbsolutePath,'http://preview.myprintcloud.com/mis/') + isnull(ReportBanner,'MPC_Content/Reports/Banners/ReportBannerOrder.png')  from reportnote where ReportCategoryID=12 and organisationid = @organisationId and CompanyId = Company.StoreId),'http://preview.myprintcloud.com/mis/MPC_Content/Reports/Banners/ReportBannerOrder.png')
						  
				       END As ReportBanner,
					  
					  --(select top 1 ISNULL(BannerAbsolutePath,'') + isnull(ReportBanner,'')  from reportnote where ReportCategoryID=12 and organisationid = @organisationId) as ReportBanner,
                      isnull(dbo.Estimate.Greeting, 'Dear '+ dbo.CompanyContact.FirstName + ' ' + isnull(dbo.CompanyContact.LastName,'')) as Greetings,
                     -- isnull((select top 1 CategoryName from tbl_productCategory where ProductCategoryID = tbl_items.ProductCategoryID),'')+ ' ' + dbo.tbl_items.ProductName as FullProductName
                       dbo.items.ProductName as FullProductName
					  
					  ,isnull((select top 1 itemName from StockItem where stockitemid = dbo.itemsection.stockitemid1 and StockItem.OrganisationId = @organisationId),'N/A')as StockName
                      ,dbo.fn_GetItemAttachmentsList(dbo.Estimate.EstimateID, 1) As AttachmentsList 
                      ,p.PaymentDate
                      , case when p.paymentmethodid = 1 then 'Paypal'
							 when p.paymentmethodid = 2 then 'On Account'
							 when p.paymentmethodid = 3 then 'ANZ'
							 else 'On Account'
							 End as paymentType
                      , case when p.paymentmethodid = 1 then (select top 1 transactionid from paypalresponse where orderid = estimate.estimateid)
							 when p.paymentmethodid = 2 or p.paymentmethodid = 3 then p.ReferenceCode
							 else 'N/A'
							 End as paymentRefNo
					 , (dbo.Company.TaxRate) As TaxLabel,BAddress.AddressName AS BAddressName,BAddress.PostCode as BPostCode, (select countryname from country where countryid in (select countryid from address where addressid =  dbo.Address.AddressId)) as BCountry
					 ,BAddress.Address1 AS BAddress1, BAddress.Address2 AS BAddress2, BAddress.City AS BCity,(select StateName from State where StateId in (select StateId from address where addressid =  BAddress.AddressId)) AS BState
					, items.Qty1Tax1Value,
					(select top 1 currencysymbol from currency c inner join organisation o on o.CurrencyId = c.CurrencyId and o.OrganisationId = @OrganisationID) as CurrencySymbol,
					case when estimate.Estimate_Code is not null then 'Estimate Code:'
					     when estimate.Estimate_Code is null then ''
					     end as EstimateCodeLabel
					     , dbo.estimate.Estimate_Code, dbo.estimate.UserNotes

FROM         dbo.company INNER JOIN
                      dbo.estimate ON dbo.estimate.companyid = dbo.company.companyid INNER JOIN
                      dbo.companycontact ON dbo.companycontact.ContactID = dbo.estimate.ContactID INNER JOIN
                      dbo.items ON dbo.estimate.EstimateID = dbo.items.EstimateID left outer JOIN
                     dbo.systemuser ON dbo.estimate.OrderReportSignedBy = dbo.systemuser.SystemUserID 
					 INNER JOIN
                      dbo.address ON dbo.estimate.BillingAddressID = dbo.address.AddressID 
                      inner JOIN dbo.itemsection ON dbo.items.ItemID = dbo.itemsection.ItemID
					  left join prepayment p on dbo.estimate.estimateid = p.orderid
					  left JOIN  dbo.address AS BAddress ON dbo.estimate.addressID = BAddress.AddressID
					
					  where company.organisationid = @organisationId and estimate.EstimateId = @OrderID
					--  where 1 = case when @ItemID = 0 then 


End

---Executed on Europe and USA servers---
alter table organisation add BillingDate datetime



------------------------------------------


ALTER TABLE discountvoucher
ALTER COLUMN MinRequiredOrderPrice float

ALTER TABLE discountvoucher
ALTER COLUMN MaxRequiredOrderPrice float

-------------------------------------------


alter table StagingImportCompanyContactAddress add DirectLine nvarchar(30)

alter table StagingImportCompanyContactAddress add CorporateUnit nvarchar(500)

alter table StagingImportCompanyContactAddress add POAddress nvarchar(500)

alter table StagingImportCompanyContactAddress add TradingName nvarchar(500)


  alter table StagingImportCompanyContactAddress add BPayCRN nvarchar(500)   

  alter table StagingImportCompanyContactAddress add ACN nvarchar(500)   

        alter table StagingImportCompanyContactAddress add ContractorName nvarchar(500)   

        alter table StagingImportCompanyContactAddress add ABN nvarchar(500)   

  
        alter table StagingImportCompanyContactAddress add Notes nvarchar(3000)   

		alter table StagingImportCompanyContactAddress add CreditLimit decimal(16,0)   

		alter table StagingImportCompanyContactAddress add IsNewsLetterSubscription bit
   
		alter table StagingImportCompanyContactAddress add IsEmailSubscription bit
       
	   	alter table StagingImportCompanyContactAddress add IsDefaultContact bit

             


/****** Object:  StoredProcedure [dbo].[usp_ChartTopPerformingStores]    Script Date: 8/19/2015 11:15:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--exec usp_ChartTopPerformingStores 1

ALTER PROCEDURE [dbo].[usp_ChartTopPerformingStores]--1
	(@OrganisationId bigint)
AS
 BEGIN
 
 declare @PreviousMonth int, @CurrentMonth int, @CurrentYear int

set @PreviousMonth = datepart(month, dateadd(month, -1, getdate()))
set @CurrentMonth = datepart(month, getdate())
set @CurrentYear = DATEPART(year, getdate())

----------------------------------Retails Stores-----------------------------------
select *, 'Jan' as MonthName,2015 as year,1 as Month  from (
select 
		(select CurrentMonthEarn from (select sum(Estimate_Total) as CurrentMonthEarn, r.RetailStoreName 
			from estimate e 
			inner join (select reg.CompanyId, store.Name as RetailStoreName 
						from Company reg 
						inner join company store on reg.storeid = store.companyid and store.OrganisationId = @OrganisationId and store.IsCustomer = 4) r
			on r.CompanyId = e.CompanyId
			where e.StatusId <> 3 and datepart(month, e.CreationDate) = @CurrentMonth and datepart(year, e.CreationDate) = @CurrentYear
			group by RetailStoreName) dt 
			where dt.RetailStoreName = retail.RetailStoreName) as CurrentMonthEarning, sum(Estimate_Total) as LastMonthEarning, retail.RetailStoreName as Name
 from estimate e 
 inner join (--Retail Customers
			select reg.CompanyId, store.Name as RetailStoreName 
			from Company reg 
			inner join company store on reg.storeid = store.companyid and store.OrganisationId = @OrganisationId and store.IsCustomer = 4) retail
			on retail.CompanyId = e.CompanyId
			where e.StatusId <> 3 and datepart(month, e.CreationDate) = @PreviousMonth and datepart(year, e.CreationDate) = @CurrentYear
			group by RetailStoreName


union
----------------------------------Corporate Stores-----------------------------------
select 
		(select CurrentMonthEarn from (select sum(Estimate_Total) as CurrentMonthEarn, r.CorporateStore 
			from estimate e 
			inner join (select companyId, Name as CorporateStore from company where organisationID = @OrganisationId and IsCustomer = 3) r
			on r.CompanyId = e.CompanyId
			where e.StatusId <> 3 and datepart(month, e.CreationDate) = @CurrentMonth and datepart(year, e.CreationDate) = @CurrentYear
			group by CorporateStore) dt 
			where dt.CorporateStore = retail.CorporateStore) as CurrentMonthEarning, sum(Estimate_Total) as LastMonthEarning, retail.CorporateStore as Name
 from estimate e 
 inner join (select companyId, Name as CorporateStore from company where organisationID = @OrganisationId and IsCustomer = 3) retail
			on retail.CompanyId = e.CompanyId
			where e.StatusId <> 3 and datepart(month, e.CreationDate) = @PreviousMonth and datepart(year, e.CreationDate) = @CurrentYear
			group by CorporateStore
) p
order by CurrentMonthEarning desc


end 


/****** Object:  StoredProcedure [dbo].[usp_ChartMonthlyEarningsbyStore]    Script Date: 8/19/2015 4:02:51 PM ******/

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Muhammad Naveed
-- Create date: 2015 07 30
-- Description:	To Get Charts data of Monthly Earnings by store
-- =============================================
-- Exec [usp_ChartMonthlyEarningsbyStore] 1
ALTER PROCEDURE [dbo].[usp_ChartMonthlyEarningsbyStore]
	@OrganisationId bigint
AS
BEGIN

		
--------------------------Retails Stores--------------------------------
					select  RetailStoreName as Name,sum(Estimate_Total)as TotalEarning, MonthName, Month, Year 
						from (select	Estimate_Total, retail.RetailStoreName, DATENAME(MONTH, CreationDate) as MonthName, DATEPart(MONTH,CreationDate) as Month,
										DATEPart(Year,CreationDate) as Year 
									from estimate e inner join
										(--Retail Stores
											select reg.CompanyId, store.Name as RetailStoreName 
												from Company reg 
												inner join company store on reg.storeid = store.companyid and store.OrganisationId = @OrganisationId and store.IsCustomer = 4) retail
												on retail.CompanyId = e.CompanyId
												where e.StatusId <> 3 
										) ct
					group by RetailStoreName, MonthName, Month, year

					Union
--------------------------Corporate Stores--------------------------------
					select  CorporateStore as Name ,sum(Estimate_Total)as TotalEarning, MonthName, Month, Year 
					from (select	Estimate_Total, corp.CorporateStore, DATENAME(MONTH, CreationDate) as MonthName, DATEPart(MONTH,CreationDate) as Month,
									DATEPart(Year,CreationDate) as Year 
							from estimate e inner join
								(--Corporate Stores
									select companyId, Name as CorporateStore from company where organisationID = @OrganisationId and IsCustomer = 3) corp
									on corp.CompanyId = e.CompanyId
									where e.StatusId <> 3 
								) ct
					group by CorporateStore, MonthName, Month, year
					order by TotalEarning desc

			RETURN 
	END


/****** Object:  StoredProcedure [dbo].[sp_cloneTemplate]    Script Date: 08/19/2015 17:04:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_cloneTemplate] 
	-- Add the parameters for the stored procedure here
	@TemplateID bigint,
	@submittedBy bigint,
    @submittedByName nvarchar(100)
AS
BEGIN


      
	declare @NewTemplateID bigint
	declare @NewCode nvarchar(10)
	--DECLARE  @WaterMarkTxt as [dbo].[tbl_company_sites]
	
	set @NewCode = ''
	
	--INSERT INTO @WaterMarkTxt (CompanySiteName)
	--Select Top 1 CompanySiteName from tbl_company_sites

	
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	INSERT INTO [dbo].[Template]
           ([Code]
           ,[ProductName]
           ,[Description]
           ,[ProductCategoryId]
           ,[Thumbnail]
           ,[Image]
           ,[IsDisabled]

           ,[PDFTemplateWidth]
           ,[PDFTemplateHeight]

           ,[CuttingMargin]
           ,[MultiPageCount]
           ,[Orientation]
           ,[MatchingSetTheme]
           ,[BaseColorId]
           ,[SubmittedBy]
           ,[SubmittedByName]
           ,[SubmitDate]
           ,[Status]
           ,[ApprovedBy]
           ,[ApprovedByName]
           ,[UserRating]
           ,[UsedCount]
           ,[MPCRating]
           ,[RejectionReason]
           ,[ApprovalDate]
           ,[IsCorporateEditable]
           ,[MatchingSetId]
           ,[TempString],[TemplateType],[isSpotTemplate],[isWatermarkText],[isCreatedManual]
          )
     



   
	SELECT 
      @NewCode
      ,[ProductName] + ' Copy'
      ,[Description]
      ,[ProductCategoryID]
      
      ,[Thumbnail]
      ,[Image]
      ,[IsDisabled]

      ,[PDFTemplateWidth]
      ,[PDFTemplateHeight]


      ,[CuttingMargin]
      ,[MultiPageCount]
      ,[Orientation]
      ,[MatchingSetTheme]
      ,[BaseColorId]
      ,@submittedBy
      ,@submittedByName
      ,NULL
      ,1
      ,NULL
      ,NULL
      ,0
      ,0
      ,0
      ,''
      ,NULL,IsCorporateEditable,MatchingSetId,'',[TemplateType],isSpotTemplate,isWatermarkText,isCreatedManual
      
  FROM [dbo].[Template] where productid = @TemplateID
	
	
	set @NewTemplateID = SCOPE_IDENTITY() 
	
	-- updating water mark text 
	UPDATE [dbo].[Template]
	SET TempString= (Select Top 1 OrganisationName from organisation)
	WHERE productid = @NewTemplateID
	
	--copying the pages
	INSERT INTO [dbo].[TemplatePage]
           ([ProductId]
           ,[PageNo]
           ,[PageType]
           ,[Orientation]
           ,[BackGroundType]
           ,[BackgroundFileName]
      ,[ColorC]
      ,[ColorM]
      ,[ColorY]
      ,[ColorK]
      ,[IsPrintable]
           ,[PageName],[hasOverlayObjects],[Width],[Height])

SELECT 
      @NewTemplateID
      ,[PageNo]
      ,[PageType]
      ,[Orientation]
      ,[BackGroundType]
      ,[BackgroundFileName]
      ,[ColorC]
      ,[ColorM]
      ,[ColorY]
      ,[ColorK]
      ,[IsPrintable]
      
      ,[PageName],[hasOverlayObjects],[Width],[Height]
  FROM [dbo].[TemplatePage]
where productid = @TemplateID


	--copying the objects
	INSERT INTO [dbo].[TemplateObject]
           ([ObjectType]
           ,[Name]
           ,[IsEditable]
           ,[IsHidden]
           ,[IsMandatory]
           
           ,[PositionX]
           ,[PositionY]
           ,[MaxHeight]
           ,[MaxWidth]
           ,[MaxCharacters]
           ,[RotationAngle]
           ,[IsFontCustom]
           ,[IsFontNamePrivate]
           ,[FontName]
           ,[FontSize]
           ,[IsBold]
           ,[IsItalic]
           ,[Allignment]
           ,[VAllignment]
           ,[Indent]
           ,[IsUnderlinedText]
           ,[ColorType]
 
           ,[ColorName]
           ,[ColorC]
           ,[ColorM]
           ,[ColorY]
           ,[ColorK]
           ,[Tint]
           ,[IsSpotColor]
           ,[SpotColorName]
           ,[ContentString]
           ,[ContentCaseType]
           ,[ProductID]
           ,[DisplayOrderPdf]
           ,[DisplayOrderTxtControl]
           ,[RColor]
           ,[GColor]
           ,[BColor]
           ,[LineSpacing]
           ,[ProductPageId]
           ,[ParentId]
           ,CircleRadiusX
           ,Opacity
           ,[ExField1],
           [IsTextEditable],
           [IsPositionLocked],
           [CircleRadiusY]
          ,[ExField2],
           ColorHex
           ,[IsQuickText]
           ,[QuickTextOrder],
		   [watermarkText],
		   [textStyles],[charspacing],[AutoShrinkText],
		   [IsOverlayObject],[ClippedInfo],[originalContentString],[originalTextStyles],[autoCollapseText]
           ,[isBulletPoint])
	SELECT 
      O.[ObjectType]
      ,O.[Name]
      ,O.[IsEditable]
      ,O.[IsHidden]
      ,O.[IsMandatory]
      
      ,O.[PositionX]
      ,O.[PositionY]
      ,O.[MaxHeight]
      ,O.[MaxWidth]
      ,O.[MaxCharacters]
      ,O.[RotationAngle]
      ,O.[IsFontCustom]
      ,O.[IsFontNamePrivate]
      ,O.[FontName]
      ,O.[FontSize]
      ,O.[IsBold]
      ,O.[IsItalic]
      ,O.[Allignment]
      ,O.[VAllignment]
      ,O.[Indent]
      ,O.[IsUnderlinedText]
      ,O.[ColorType]
      ,O.[ColorName]
      ,O.[ColorC]
      ,O.[ColorM]
      ,O.[ColorY]
      ,O.[ColorK]
      ,O.[Tint]
      ,O.[IsSpotColor]
      ,O.[SpotColorName]
      ,O.[ContentString]
      ,O.[ContentCaseType]
      ,@NewTemplateID
      ,O.[DisplayOrderPdf]
      ,O.[DisplayOrderTxtControl]
      ,O.[RColor]
      ,O.[GColor]
      ,O.[BColor]
      ,O.[LineSpacing]
      ,NP.[ProductPageId]
      ,O.[ParentId]
      ,O.CircleRadiusX
      ,O.Opacity
      ,O.[ExField1],
       O.[IsTextEditable],
       O.[IsPositionLocked],
       O.[CircleRadiusY]
      ,O.[ExField2],
       O.ColorHex
       ,[IsQuickText]
        ,[QuickTextOrder],[watermarkText],O.[textStyles],
		O.[charspacing],O.[AutoShrinkText],O.[IsOverlayObject]
		,O.[ClippedInfo]
		,O.[originalContentString],O.[originalTextStyles],O.[autoCollapseText],[isBulletPoint]
  FROM [dbo].[TemplateObject] O
  inner join [dbo].[TemplatePage]  P on o.ProductPageId = p.ProductPageId and o.ProductId = @TemplateID
  inner join [dbo].[TemplatePage] NP on P.PageName = NP.PageName and P.PageNo = NP.PageNo and NP.ProductId = @NewTemplateID
  
	--theme tags
	--insert into dbo.TemplateThemeTags   ([TagID],[ProductID])
	--select [TagID] ,@NewTemplateID from dbo.TemplateThemeTags where ProductID = @TemplateID
	
	---- industry tags
	--insert into dbo.TemplateIndustryTags   ([TagID],[ProductID])
	--select [TagID] ,@NewTemplateID from dbo.TemplateIndustryTags where ProductID = @TemplateID

	INSERT INTO [dbo].[TemplateBackgroundImage]
			   ([ProductId]
			   ,[ImageName]
			   ,[Name]
			   ,[flgPhotobook]
			   ,[flgCover]
			   ,[BackgroundImageAbsolutePath]
			   ,[BackgroundImageRelativePath],
			   ImageType,
			   ImageWidth,
			   ImageHeight
			   
			   )
	SELECT 
		  @NewTemplateID
		  ,[ImageName]
		  ,[Name]
		  ,[flgPhotobook]
		  ,[flgCover]
		  ,[BackgroundImageAbsolutePath]
		  ,[BackgroundImageRelativePath]
		  ,ImageType,
			   ImageWidth,
			   ImageHeight
	  FROM [dbo].[TemplateBackgroundImage] where ProductId = @TemplateID

select @NewTemplateID
	
END



	   /*Execution date 19/08/2015*/

	  
GO
/****** Object:  StoredProcedure [dbo].[sp_cloneTemplate]    Script Date: 08/19/2015 17:04:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[sp_cloneTemplate] 
	-- Add the parameters for the stored procedure here
	@TemplateID bigint,
	@submittedBy bigint,
    @submittedByName nvarchar(100)
AS
BEGIN


      
	declare @NewTemplateID bigint
	declare @NewCode nvarchar(10)
	--DECLARE  @WaterMarkTxt as [dbo].[tbl_company_sites]
	
	set @NewCode = ''
	
	--INSERT INTO @WaterMarkTxt (CompanySiteName)
	--Select Top 1 CompanySiteName from tbl_company_sites

	
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	INSERT INTO [dbo].[Template]
           ([Code]
           ,[ProductName]
           ,[Description]
           ,[ProductCategoryId]
           ,[Thumbnail]
           ,[Image]
           ,[IsDisabled]

           ,[PDFTemplateWidth]
           ,[PDFTemplateHeight]

           ,[CuttingMargin]
           ,[MultiPageCount]
           ,[Orientation]
           ,[MatchingSetTheme]
           ,[BaseColorId]
           ,[SubmittedBy]
           ,[SubmittedByName]
           ,[SubmitDate]
           ,[Status]
           ,[ApprovedBy]
           ,[ApprovedByName]
           ,[UserRating]
           ,[UsedCount]
           ,[MPCRating]
           ,[RejectionReason]
           ,[ApprovalDate]
           ,[IsCorporateEditable]
           ,[MatchingSetId]
           ,[TempString],[TemplateType],[isSpotTemplate],[isWatermarkText],[isCreatedManual]
          )
     



   
	SELECT 
      @NewCode
      ,[ProductName] + ' Copy'
      ,[Description]
      ,[ProductCategoryID]
      
      ,[Thumbnail]
      ,[Image]
      ,[IsDisabled]

      ,[PDFTemplateWidth]
      ,[PDFTemplateHeight]


      ,[CuttingMargin]
      ,[MultiPageCount]
      ,[Orientation]
      ,[MatchingSetTheme]
      ,[BaseColorId]
      ,@submittedBy
      ,@submittedByName
      ,NULL
      ,1
      ,NULL
      ,NULL
      ,0
      ,0
      ,0
      ,''
      ,NULL,IsCorporateEditable,MatchingSetId,'',[TemplateType],isSpotTemplate,isWatermarkText,isCreatedManual
      
  FROM [dbo].[Template] where productid = @TemplateID
	
	
	set @NewTemplateID = SCOPE_IDENTITY() 
	
	-- updating water mark text 
	UPDATE [dbo].[Template]
	SET TempString= (Select Top 1 OrganisationName from organisation)
	WHERE productid = @NewTemplateID
	
	--copying the pages
	INSERT INTO [dbo].[TemplatePage]
           ([ProductId]
           ,[PageNo]
           ,[PageType]
           ,[Orientation]
           ,[BackGroundType]
           ,[BackgroundFileName]
      ,[ColorC]
      ,[ColorM]
      ,[ColorY]
      ,[ColorK]
      ,[IsPrintable]
           ,[PageName],[hasOverlayObjects],[Width],[Height])

SELECT 
      @NewTemplateID
      ,[PageNo]
      ,[PageType]
      ,[Orientation]
      ,[BackGroundType]
      ,[BackgroundFileName]
      ,[ColorC]
      ,[ColorM]
      ,[ColorY]
      ,[ColorK]
      ,[IsPrintable]
      
      ,[PageName],[hasOverlayObjects],[Width],[Height]
  FROM [dbo].[TemplatePage]
where productid = @TemplateID


	--copying the objects
	INSERT INTO [dbo].[TemplateObject]
           ([ObjectType]
           ,[Name]
           ,[IsEditable]
           ,[IsHidden]
           ,[IsMandatory]
           
           ,[PositionX]
           ,[PositionY]
           ,[MaxHeight]
           ,[MaxWidth]
           ,[MaxCharacters]
           ,[RotationAngle]
           ,[IsFontCustom]
           ,[IsFontNamePrivate]
           ,[FontName]
           ,[FontSize]
           ,[IsBold]
           ,[IsItalic]
           ,[Allignment]
           ,[VAllignment]
           ,[Indent]
           ,[IsUnderlinedText]
           ,[ColorType]
 
           ,[ColorName]
           ,[ColorC]
           ,[ColorM]
           ,[ColorY]
           ,[ColorK]
           ,[Tint]
           ,[IsSpotColor]
           ,[SpotColorName]
           ,[ContentString]
           ,[ContentCaseType]
           ,[ProductID]
           ,[DisplayOrderPdf]
           ,[DisplayOrderTxtControl]
           ,[RColor]
           ,[GColor]
           ,[BColor]
           ,[LineSpacing]
           ,[ProductPageId]
           ,[ParentId]
           ,CircleRadiusX
           ,Opacity
           ,[ExField1],
           [IsTextEditable],
           [IsPositionLocked],
           [CircleRadiusY]
          ,[ExField2],
           ColorHex
           ,[IsQuickText]
           ,[QuickTextOrder],
		   [watermarkText],
		   [textStyles],[charspacing],[AutoShrinkText],
		   [IsOverlayObject],[ClippedInfo],[originalContentString],[originalTextStyles],[autoCollapseText]
           ,[isBulletPoint])
	SELECT 
      O.[ObjectType]
      ,O.[Name]
      ,O.[IsEditable]
      ,O.[IsHidden]
      ,O.[IsMandatory]
      
      ,O.[PositionX]
      ,O.[PositionY]
      ,O.[MaxHeight]
      ,O.[MaxWidth]
      ,O.[MaxCharacters]
      ,O.[RotationAngle]
      ,O.[IsFontCustom]
      ,O.[IsFontNamePrivate]
      ,O.[FontName]
      ,O.[FontSize]
      ,O.[IsBold]
      ,O.[IsItalic]
      ,O.[Allignment]
      ,O.[VAllignment]
      ,O.[Indent]
      ,O.[IsUnderlinedText]
      ,O.[ColorType]
      ,O.[ColorName]
      ,O.[ColorC]
      ,O.[ColorM]
      ,O.[ColorY]
      ,O.[ColorK]
      ,O.[Tint]
      ,O.[IsSpotColor]
      ,O.[SpotColorName]
      ,O.[ContentString]
      ,O.[ContentCaseType]
      ,@NewTemplateID
      ,O.[DisplayOrderPdf]
      ,O.[DisplayOrderTxtControl]
      ,O.[RColor]
      ,O.[GColor]
      ,O.[BColor]
      ,O.[LineSpacing]
      ,NP.[ProductPageId]
      ,O.[ParentId]
      ,O.CircleRadiusX
      ,O.Opacity
      ,O.[ExField1],
       O.[IsTextEditable],
       O.[IsPositionLocked],
       O.[CircleRadiusY]
      ,O.[ExField2],
       O.ColorHex
       ,[IsQuickText]
        ,[QuickTextOrder],[watermarkText],O.[textStyles],
		O.[charspacing],O.[AutoShrinkText],O.[IsOverlayObject]
		,O.[ClippedInfo]
		,O.[originalContentString],O.[originalTextStyles],O.[autoCollapseText],[isBulletPoint]
  FROM [dbo].[TemplateObject] O
  inner join [dbo].[TemplatePage]  P on o.ProductPageId = p.ProductPageId and o.ProductId = @TemplateID
  inner join [dbo].[TemplatePage] NP on P.PageName = NP.PageName and P.PageNo = NP.PageNo and NP.ProductId = @NewTemplateID
  
	--theme tags
	--insert into dbo.TemplateThemeTags   ([TagID],[ProductID])
	--select [TagID] ,@NewTemplateID from dbo.TemplateThemeTags where ProductID = @TemplateID
	
	---- industry tags
	--insert into dbo.TemplateIndustryTags   ([TagID],[ProductID])
	--select [TagID] ,@NewTemplateID from dbo.TemplateIndustryTags where ProductID = @TemplateID

	INSERT INTO [dbo].[TemplateBackgroundImage]
			   ([ProductId]
			   ,[ImageName]
			   ,[Name]
			   ,[flgPhotobook]
			   ,[flgCover]
			   ,[BackgroundImageAbsolutePath]
			   ,[BackgroundImageRelativePath],
			   ImageType,
			   ImageWidth,
			   ImageHeight
			   
			   )
	SELECT 
		  @NewTemplateID
		  ,[ImageName]
		  ,[Name]
		  ,[flgPhotobook]
		  ,[flgCover]
		  ,[BackgroundImageAbsolutePath]
		  ,[BackgroundImageRelativePath]
		  ,ImageType,
			   ImageWidth,
			   ImageHeight
	  FROM [dbo].[TemplateBackgroundImage] where ProductId = @TemplateID

select @NewTemplateID
	
END



/****** Object:  StoredProcedure [dbo].[usp_ChartMonthlyOrdersCount]    Script Date: 8/20/2015 1:10:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[usp_ChartMonthlyOrdersCount]
	@OrganisationId bigint
AS
BEGIN

	
		
--------------------------Retails Stores--------------------------------
					select  RetailStoreName as CompanyName,count(Estimate_Total)as TotalOrders, Month, MonthName,  Year 
						from (select	Estimate_Total, retail.RetailStoreName, DATENAME(MONTH, CreationDate) as MonthName, DATEPart(MONTH,CreationDate) as Month,
										DATEPart(Year,CreationDate) as Year 
									from estimate e inner join
										(--Retail Stores
											select reg.CompanyId, store.Name as RetailStoreName 
												from Company reg 
												inner join company store on reg.storeid = store.companyid and store.OrganisationId = @OrganisationId and store.IsCustomer = 4) retail
												on retail.CompanyId = e.CompanyId
												where e.StatusId <> 3 
										) ct
					group by RetailStoreName, MonthName, Month, year

					Union
--------------------------Corporate Stores--------------------------------
					select  CorporateStore as CompanyName ,count(Estimate_Total)as TotalOrders, Month, MonthName, Year 
					from (select	Estimate_Total, corp.CorporateStore, DATENAME(MONTH, CreationDate) as MonthName, DATEPart(MONTH,CreationDate) as Month,
									DATEPart(Year,CreationDate) as Year 
							from estimate e inner join
								(--Corporate Stores
									select companyId, Name as CorporateStore from company where organisationID = @OrganisationId and IsCustomer = 3) corp
									on corp.CompanyId = e.CompanyId
									where e.StatusId <> 3 
								) ct
					group by CorporateStore, MonthName, Month, year
					order by TotalOrders desc

	END

/*Execution date 19/08/2015*/
Go
ALTER VIEW [dbo].[vw_SaveDesign]
AS
 SELECT  item.ItemID, ItemAttach.ItemID AS AttachmentItemId,ItemAttach.FileName AS AttachmentFileName, 
 ItemAttach.FolderPath AS AttachmentFolderPath, 
item.EstimateID, item.ProductName, --PCat.CategoryName AS ProductCategoryName, PCat.ProductCategoryID, PCat.ParentCategoryID, 
                      ISNULL(dbo.funGetMiniumProductValue(item.RefItemID), 0.0) AS MinPrice,  
                      item.IsEnabled, item.IsPublished,item.IsArchived, item.InvoiceID, contact.ContactID, contact.CompanyId, company.IsCustomer ,item.RefItemID, status_Check.StatusID, status_Check.StatusName,
                       item.IsOrderedItem, item.ItemCreationDateTime,item.TemplateID
FROM         Items AS item Inner JOIN
				      dbo.ItemAttachment AS ItemAttach ON ItemAttach.ItemID = item.ItemID INNER JOIN
                      dbo.Template AS temp ON temp.ProductID = item.TemplateID INNER JOIN
                      dbo.Estimate AS est ON item.EstimateID = est.EstimateID INNER JOIN
                      dbo.Status AS status_Check ON item.Status = status_Check.StatusID INNER JOIN
                      dbo.CompanyContact AS contact ON contact.ContactID = est.ContactID INNER JOIN
                      dbo.Company As company ON contact.CompanyId = company.CompanyId
				

GO



/****** Object:  StoredProcedure [dbo].[usp_ChartEstimateToOrderConversion]    Script Date: 8/20/2015 3:00:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--[usp_ChartEstimateToOrderConversion] 1
ALTER PROCEDURE [dbo].[usp_ChartEstimateToOrderConversion]
	@OrganisationId bigint
AS
BEGIN

select sum(EstimateTotal) as EstimateTotal, sum(ConvertedTotal) as ConvertedTotal,Month,MonthName,Year
	from
	(	select	estimate_total as EstimateTotal,
				(select estimate_total from estimate es where es.EstimateId = e.RefEstimateId) as ConvertedTotal,
				DATENAME(MONTH,e.Order_Date) as [MonthName],
				DATEPART(MONTH,e.Order_Date) as [Month],
				DATEPART(YEAR,e.Order_Date) as [Year]
		from	estimate e where RefEstimateId is not null and Order_Code is not null and OrganisationId = @OrganisationId
	) data

group by month,monthname,year
order by ConvertedTotal desc
End


/****** Object:  StoredProcedure [dbo].[usp_ChartEstimateToOrderConversionCount]    Script Date: 8/20/2015 6:40:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--[usp_ChartEstimateToOrderConversionCount]1
ALTER PROCEDURE [dbo].[usp_ChartEstimateToOrderConversionCount]
	@OrganisationId bigint
AS
BEGIN


select count(EstimateId) as EstimateCount, count(ConvertedEstimate) as ConvertedCount,Month,MonthName,Year
	from
	(	select	EstimateId,
				(select EstimateId from estimate es where es.EstimateId = e.EstimateId and Order_Code is not null) as ConvertedEstimate,
				(convert(varchar, datepart(year, e.Order_Date)) + '-' + convert(varchar, datepart(month, e.Order_Date))) as [MonthName],
				DATEPART(MONTH,e.Order_Date) as [Month],
				DATEPART(YEAR,e.Order_Date) as [Year]
		from	estimate e where Estimate_Code is not null and OrganisationId = @OrganisationId
	) data

group by month,monthname,year
order by Month 


	END

	-------------Executed on USA server on 20150821----------
	alter table Company add CanUserUpdateAddress bit

	
/****** Object:  StoredProcedure [dbo].[usp_ChartTop10PerfomingCustomers]    Script Date: 8/24/2015 12:10:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- usp_ChartTop10PerfomingCustomers 1
ALTER PROCEDURE [dbo].[usp_ChartTop10PerfomingCustomers]
	@OrganisationId bigint
AS
BEGIN

	declare @PreviousMonth int, @CurrentMonth int, @CurrentYear int
set @PreviousMonth = datepart(month, dateadd(month, -1, getdate()))
set @CurrentMonth = datepart(month, getdate())
set @CurrentYear = DATEPART(year, getdate())

select top 5 isnull(c.firstname,'') + ' ' + isnull(c.lastName,'') as ContactName, isnull(sum(e.Estimate_Total), 0) as CurrentMonthOrders,
	(select Orders from (select cc.ContactId, sum(e.Estimate_Total) as Orders from 
						estimate e inner join (
						select reg.CompanyId, store.Name as RetailStoreName 
						from Company reg 
						inner join company store on reg.storeid = store.companyid and store.OrganisationId = @OrganisationId and store.IsCustomer = 4
					) cust on cust.CompanyId = e.CompanyId
			inner join CompanyContact cc on cc.ContactId = e.ContactId
			where e.StatusId <> 3 and datepart(month, e.CreationDate) = @PreviousMonth and datepart(year, e.CreationDate) = @CurrentYear
			group by cc.ContactId) curOrders where curOrders.ContactId = c.ContactId) as LastMonthOrders
	from 
	estimate e inner join (
						select reg.CompanyId, store.Name as RetailStoreName 
						from Company reg 
						inner join company store on reg.storeid = store.companyid and store.OrganisationId = @OrganisationId and store.IsCustomer = 4
					) cust on cust.CompanyId = e.CompanyId
			inner join CompanyContact c on c.ContactId = e.ContactId
			where e.StatusId <> 3 and datepart(month, e.CreationDate) = @CurrentMonth and datepart(year, e.CreationDate) = @CurrentYear
			group by firstname, lastName, c.ContactId
			order by CurrentMonthOrders Desc		
			


	END

alter table organisation add OfflineStoreClicks int
------------- Executed on all servers on 20150805-----------------------------------------------------

alter table widgets add WidgetCss varchar(Max)
alter table Widgets add ThumbnailUrl varchar(255)
alter table Widgets add Description varchar(500)


/****** Object:  StoredProcedure [dbo].[usp_ChartMonthlyOrdersCount]    Script Date: 8/26/2015 12:53:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--[usp_DashboardROICounter]1
Create PROCEDURE [dbo].[usp_DashboardROICounter]
	@OrganisationId bigint
AS
BEGIN
	
declare @LastSixMonths int, @CurrentMonth int, @CurrentYear int

set @LastSixMonths = datepart(month, dateadd(month, -6, getdate()))
set @CurrentMonth = datepart(month, getdate())
set @CurrentYear = DATEPART(year, getdate())

select 
-----------------------------Registered Users Count----------------------------------------
	(select sum(RegisterCount) as RegUsersCount
		from(
				select count(reg.CompanyId) as RegisterCount, store.Name as RetailStoreName 
				from Company reg 
				inner join company store on reg.storeid = store.companyid and store.OrganisationId = @OrganisationId and store.IsCustomer = 4
				where datepart(month, reg.CreationDate) >= @LastSixMonths and datepart(year, reg.CreationDate) = @CurrentYear
				group by store.Name
			) RegisterUsers) as RegisteredUsersCount,

--------------------------------------Orders Count----------------------------------------
		(select sum(TotalOrders) As OrdersCount from(
				select RetailStoreName, count(EstimateID)as TotalOrders, Month, year 
						from (select EstimateID, retail.RetailStoreName, DATEPart(MONTH,CreationDate) as Month,
										DATEPart(Year,CreationDate) as Year 
									from estimate e inner join
										(--Retail Stores
											select reg.CompanyId, store.Name as RetailStoreName 
												from Company reg 
												inner join company store on reg.storeid = store.companyid and store.OrganisationId = @OrganisationId and store.IsCustomer = 4) retail
												on retail.CompanyId = e.CompanyId
												where e.StatusId <> 3										
										) ct
					where ct.EstimateId >= @LastSixMonths and ct.Year = @CurrentYear
					group by RetailStoreName, Month, year
					
					Union
--------------------------Corporate Stores------------------------------------------------
					select CorporateStore, count(EstimateID)as TotalOrders, Month,  year 
					from (select	EstimateID, corp.CorporateStore, DATEPart(MONTH,CreationDate) as Month,
									DATEPart(Year,CreationDate) as Year 
							from estimate e inner join
								(--Corporate Stores
									select companyId, Name as CorporateStore from company where organisationID = @OrganisationId and IsCustomer = 3) corp
									on corp.CompanyId = e.CompanyId
									where e.StatusId <> 3 
								) ct
					where ct.EstimateId >= @LastSixMonths and ct.Year = @CurrentYear
					group by CorporateStore, Month, year
					) OrgTotalOrders) As TotalOrdersCount,

--------------------------------------------------Directs Ordrs Total---------------------
			(select	sum(estimate_Total) as DirectOrdersTotal 
			from	estimate e 
			where	organisationid = @OrganisationId and isDirectSale = 1 and isEstimate = 0 
			and		datepart(month, e.CreationDate) >= @LastSixMonths and datepart(year, e.CreationDate) = @CurrentYear) as DirectOrdersTotal,

-----------------------------------------------Online Orders Total-------------------------
			(select sum(estimate_Total) as OnlineOrdersTotal from estimate e 
			 where	organisationid = @OrganisationId and isDirectSale = 0 and isEstimate = 0 
			 and	datepart(month, e.CreationDate) >= @LastSixMonths and datepart(year, e.CreationDate) = @CurrentYear) as OnlineOrdesTotal
	
	END






	alter table templateObject add textPaddingTop int null 
/****** Object:  StoredProcedure [dbo].[usp_importCRMCompanyContacts]    Script Date: 8/27/2015 10:21:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



 Create Procedure [dbo].[usp_importCRMCompanyContacts]
		@OrganisationId bigint
		
	 
	 as 
	Begin
		
		BEGIN TRY	
		Begin Transaction		
		update StagingImportCompanyContactAddress set  OrganisationId = @OrganisationId,
		 password = 'xjMtQdRX7BYzjc5fW0UwEpfSMnKkSUWNCQ==', canPlaceORder = 1
		update StagingImportCompanyContactAddress set Address2 = '' where Address2 is null
		update StagingImportCompanyContactAddress set Address3 = '' where Address3 is null		
		--update Country Id by country name
		update StagingImportCompanyContactAddress set CountryId = c.CountryId
		from StagingImportCompanyContactAddress i , country c
		where c.CountryName = i.Country
		--update StateId by State Name 
		update StagingImportCompanyContactAddress set StateId = c.StateId
		from StagingImportCompanyContactAddress i , State c
		where c.StateCode = i.State
		--update TerritoryId from Territory Name
		update StagingImportCompanyContactAddress set TerritoryId = c.TerritoryId
		from StagingImportCompanyContactAddress i , CompanyTerritory c
		where c.TerritoryCode = i.TerritoryName

			declare @SystemUserId varchar(100)

			select @SystemUserId = (select top 1 systemuserid from systemuser where OrganizationId = @OrganisationId)
					Declare @count int = (select count(*) from StagingImportCompanyContactAddress)
					Declare @counter int = 1
					Declare @StagingId bigint, @AddressId bigint, @TerritoryId bigint, @AddrssName varchar(200), @Address1 varchar(200), @Address2 varchar(200), @City varchar(100),
					@TerritoryName varchar(200), @isNewTerritory bit

						While @counter <= @count
						Begin
							set @isNewTerritory = 0
							select @AddrssName = AddressName, @Address1 = Address1, @Address2 = Address2, @City = City, @TerritoryName = TerritoryName from (select row_number() OVER (ORDER BY stagingid) r, * from StagingImportCompanyContactAddress) x
							where r = @counter
						

						-- check whether company exist or not
						declare @WebAccessCode varchar(500)
						declare @CompanyId bigint
						
						declare @Typeid bigint
						select @WebAccessCode = WebAccessCode from (select row_number() OVER (ORDER BY stagingid) r, * from StagingImportCompanyContactAddress) x
						where r = @counter 

						
						if(exists(select WebAccessCode from company where WebAccessCode = @WebAccessCode and organisationid = @organisationId))
						begin

						select @CompanyId = companyid from company where WebAccessCode = @WebAccessCode and organisationid = @organisationId
							--Insert New Contact
						declare @currEmail varchar(255)
						select @currEmail = email from (select row_number() OVER (ORDER BY stagingid) r, * from StagingImportCompanyContactAddress) x
						where r = @counter 
						if(not exists(select email from companycontact where CompanyId in (select CompanyId from company where storeid = @CompanyId) and email = @currEmail))
						begin
							
								-- insert company
						

						select @Typeid = TypeId from company where WebAccessCode = @WebAccessCode and organisationid = @organisationId 

						declare @NewCompanyID bigint

						



								insert into Company(OrganisationId, StoreId, Name, TypeId, DefaultNominalCode, DefaultMarkupId,AccountManagerId, Status, IsCustomer, AccountStatusId, 
							IsDisabled, LockedBy, AccountBalance, CreationDate
							)
							values
							(@OrganisationId,@CompanyId,'Import Customer',@Typeid,0,0,@SystemUserId,0,0,0,0,0,0,GETDATE())

							
							
							select @NewCompanyID = SCOPE_IDENTITY()

							--Check Territory Exist otherwise Insert Territory
							if(exists(select * from CompanyTerritory where companyId = @NewCompanyID and TerritoryName = @TerritoryName))
							begin
								set @TerritoryId = (select top 1 TerritoryId from CompanyTerritory where companyId = @NewCompanyID and TerritoryName = @TerritoryName)
							end
							else
							begin
								insert into CompanyTerritory(CompanyID, TerritoryCode, TerritoryName)
								select @NewCompanyID, 'TCImport', TerritoryName   from (select row_number() OVER (ORDER BY stagingid) r, * from StagingImportCompanyContactAddress) xx
								where r = @counter
								set @TerritoryId = (select SCOPE_IDENTITY())
								set @isNewTerritory = 1
							end


						--Check If Address Exist otherwise insert Address
						if(exists(select * from Address where companyId = @NewCompanyID and addressname = @AddrssName and Address1 = @Address1 and Address2 = @Address2 and City = @City))
						begin
							set @AddressID = (select top 1 AddressId from Address where companyId = @NewCompanyID and addressname = @AddrssName and Address1 = @Address1 and Address2 = @Address2 and City = @City)
						end
						else
						begin
							insert into Address(OrganisationId, CompanyID, AddressName, Address1, Address2, Address3, City, StateId, PostCode, TerritoryID, fax, CountryId, Tel1, isDefaultTerrorityBilling, isDefaultTerrorityShipping, isArchived, IsDefaultAddress)
							select @OrganisationId, @NewCompanyID, AddressName, Address1, Address2, Address3, City, StateId,Postcode, @TerritoryId, AddressFax, CountryId, ContactPhone, 0, 0, 0, 0   from (select row_number() OVER (ORDER BY stagingid) r, * from StagingImportCompanyContactAddress) x
							where r = @counter
							set @Addressid = (select SCOPE_IDENTITY())
						if(@isNewTerritory = 1 and @TerritoryId > 0)
						Begin
							update address set isDefaultTerrorityBilling = 1, isDefaultTerrorityShipping = 1 where AddressId = @AddressId
						End
						end



							insert into CompanyContact(OrganisationId, CompanyId, TerritoryID, AddressID, ShippingAddressID, FirstName, LastName, Email, Password, 
							HomePostCode, Mobile, isArchived, ContactRoleID, JobTitle, HomeTel1, Fax, Notes, IsDefaultContact, isWebAccess, canPlaceDirectOrder, 
							canUserPlaceOrderWithoutApproval, IsPricingshown, SkypeId, LinkedinURL, FacebookURL, TwitterURL, CanUserEditProfile, IsPayByPersonalCreditCard,
							SecondaryEmail,isPlaceOrder, AdditionalField1, AdditionalField2,AdditionalField3, AdditionalField4, AdditionalField5,CorporateUnit,OfficeTradingName,BPayCRN,
							ACN,ContractorName,ABN,CreditLimit,IsNewsLetterSubscription,IsEmailSubscription,POBoxAddress
							)


							select @OrganisationId, @NewCompanyID, TerritoryId, @Addressid, @Addressid, ContactFirstName, ContactLastName, Email,password, 
							postcode, Mobile,0, RoleId, substring(JobTitle, 1, 49), ContactPhone, ContactFax, 'contact import for this store, default password is password', 0, HasWebAccess, CanPlaceDirectOrder, 
							CanPlaceOrderWithoutApproval, CanSeePrices, SkypeId, LinkedInUrl, FacebookUrl,  TwitterUrl, CanEditProfile, CanPayByPersonalCreditCard,
							Email, 1, AddInfo1, AddInfo2, AddInfo3, AddInfo4, AddInfo5,CorporateUnit,TradingName,BPayCRN,ACN,ContractorName,ABN,CreditLimit,IsNewsLetterSubscription,IsEmailSubscription,POAddress
							from (select row_number() OVER (ORDER BY stagingid) r, * from StagingImportCompanyContactAddress) x
							where r = @counter
						End
						else
						Begin
							update CompanyContact set FirstName = i.ContactFirstName,
							LastName = i.ContactLastName, HomePostCode = i.Postcode, Mobile = i.Mobile, JobTitle = substring(i.JobTitle, 1, 49),
							HomeTel1 = i.ContactPhone, Fax = i.ContactFax, isWebAccess = i.HasWebAccess, canPlaceDirectOrder = i.CanPlaceDirectOrder,
							canUserPlaceOrderWithoutApproval = i.CanPlaceOrderWithoutApproval, IsPricingshown = i.CanSeePrices,
							SkypeId = i.SkypeId, LinkedinURL = i.LinkedInUrl, FacebookURL = i.FacebookUrl, TwitterURL = i.TwitterUrl,
							CanUserEditProfile = i.CanEditProfile, IsPayByPersonalCreditCard = i.CanPayByPersonalCreditCard,
							CorporateUnit = i.CorporateUnit,OfficeTradingName = i.TradingName,BPayCRN = i.BPayCRN,
							ACN = i.ACN,ContractorName = i.ContractorName,ABN = i.ABN,CreditLimit = i.CreditLimit,IsNewsLetterSubscription = i.IsNewsLetterSubscription,IsEmailSubscription = i.IsEmailSubscription,POBoxAddress = i.POAddress,ContactRoleId = i.RoleId

							from CompanyContact c, (select ContactFirstName, ContactLastName, Email,
							postcode, Mobile, JobTitle, ContactPhone, ContactFax, HasWebAccess, CanPlaceDirectOrder, 
							CanPlaceOrderWithoutApproval, CanSeePrices, SkypeId, LinkedInUrl, FacebookUrl,  TwitterUrl, CanEditProfile, CanPayByPersonalCreditCard,CorporateUnit,TradingName,BPayCRN,ACN,ContractorName,ABN,CreditLimit,IsNewsLetterSubscription,IsEmailSubscription,POAddress,RoleId
							from (select row_number() OVER (ORDER BY stagingid) r, * from StagingImportCompanyContactAddress) x
							where r = @counter) i
							where c.Email = i.Email
						End




						end
			
						
						
					set @counter = @counter + 1;
					
				End -- End of Loop
		Commit Transaction
		End Try
		
		BEGIN CATCH
			IF @@TRANCOUNT > 0
			ROLLBACK
		END CATCH
End --End of Procedure Begin

alter table Company add IsRegisterAccessWebStore bit null
alter table Company add IsRegisterPlaceOrder bit null
alter table Company add IsRegisterPayOnlyByCreditCard bit null
alter table Company add IsRegisterPlaceDirectOrder bit null
alter table Company add IsRegisterPlaceOrderWithoutApproval bit null


-------------Executed on All Servers---------------------
CREATE TABLE [dbo].[MarketingBriefHistory](
 [MarketingBriefHistoryId] [bigint] IDENTITY(1,1) NOT NULL,
 [HtmlMsg] [nvarchar](max) NULL,
 [CompanyId] [bigint] NULL,
 [OrganisationId] [bigint] NULL,
 [ContactId] [bigint] NULL,
 [ItemId] [bigint] NULL,
 CONSTRAINT [PK_MarketingBriefHistory] PRIMARY KEY CLUSTERED 
(
 [MarketingBriefHistoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE TemplateVariable
ADD VariableText nvarchar(max) null

ALTER TABLE Template
ADD contactId bigint null


ALTER TABLE Template
ADD realEstateId bigint null
