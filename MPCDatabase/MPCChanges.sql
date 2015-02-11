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