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

GO
