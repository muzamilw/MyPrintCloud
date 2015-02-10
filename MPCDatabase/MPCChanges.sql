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