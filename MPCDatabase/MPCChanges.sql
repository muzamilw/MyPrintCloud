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