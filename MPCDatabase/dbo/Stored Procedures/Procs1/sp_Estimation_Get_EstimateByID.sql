CREATE PROCEDURE [dbo].[sp_Estimation_Get_EstimateByID]
(@EstimateID int)
AS
	SELECT tbl_estimates.EstimateID,isnull(tbl_estimates.Estimate_Code,'') as Estimate_Code,isnull(tbl_estimates.Estimate_Name,'') as Estimate_Name,
         isnull(tbl_estimates.ContactCompanyID,0) AS ContactCompanyID,isnull(tbl_estimates.ContactID,0)AS ContactID ,isnull(tbl_estimates.Estimate_Total,0) AS Estimate_Total ,isnull(tbl_estimates.Estimate_ValidUpto,0) AS Estimate_ValidUpto,
         isnull(tbl_estimates.UserNotes,'') AS UserNotes,isnull(tbl_estimates.LastUpdatedBy,0)AS LastUpdatedBy,isnull(tbl_estimates.CreationDate,GetDate()) AS CreationDate,isnull(tbl_estimates.CreationTime,GetDate()) AS CreationTime,isnull(tbl_estimates.Created_by,0) AS Created_by,
         isnull(tbl_estimates.SalesPersonID,0) AS SalesPersonID ,isnull(tbl_estimates.HeadNotes,'') AS HeadNotes,isnull(tbl_estimates.FootNotes,'') AS FootNotes,isnull(tbl_estimates.StatusID,0) AS StatusID,
         isnull(tbl_estimates.EstimateDate,GetDate()) AS EstimateDate,isnull(tbl_estimates.SectionFlagID,0) as SectionFlagID,
         isnull(tbl_estimates.Greeting,'') as Greeting,isnull(tbl_estimates.AccountNumber,'') as AccountNumber,isnull(tbl_estimates.StatusID,0) as StatusID,isnull(tbl_estimates.OrderNo,'') as OrderNo,isnull(tbl_estimates.SuccessChanceID,0) as SuccessChanceID,isnull(tbl_estimates.CompanyName,'') as CompanyName,isnull(tbl_estimates.ProjectionDate,GetDate()) as ProjectionDate,isnull(tbl_estimates.ParentID,0) as ParentID,isnull(tbl_estimates.Version,0)as Version ,isnull(tbl_estimates.IsInPipeLine,0) as IsInPipeLine,isnull(tbl_estimates.AddressID,0) as AddressID,
         isnull(tbl_contactcompanies.Name,'') as Name,isnull(tbl_contacts.FirstName,'') as FirstName,isnull(tbl_estimates.CompanySiteID,0) as CompanySiteID ,isnull(tbl_estimates.EnquiryID,0)as EnquiryID,isnull(tbl_estimates.EstimateSentTo,0) as EstimateSentTo,isnull(tbl_estimates.EstimateValueChanged, 0) as EstimateValueChanged,isnull(tbl_estimates.NewItemAdded,0) as NewItemAdded,isnull(tbl_estimates.IsEstimate,0) as IsEstimate,isnull(tbl_estimates.SourceID,0) as SourceID,isnull(tbl_estimates.ProductID,0) as ProductID,isnull(tbl_contactcompanies.IsCustomer,0) as IsCustomer
         FROM tbl_contactcompanies
         RIGHT OUTER JOIN tbl_estimates ON (tbl_contactcompanies.ContactCompanyID = tbl_estimates.ContactCompanyID) 
         LEFT OUTER JOIN tbl_contacts ON (tbl_estimates.ContactID = tbl_contacts.ContactID) 
         WHERE tbl_estimates.EstimateID = @EstimateID
	RETURN