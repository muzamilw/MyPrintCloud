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



