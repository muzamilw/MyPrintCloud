CREATE PROCEDURE [dbo].[sp_Jobs_Get_JobCardByJobID]
(
	@ItemID int

)
AS
	SELECT tbl_items.ItemID,tbl_items.AdditionalInformation,tbl_items.JobDescriptionTitle1,
tbl_items.JobDescriptionTitle2,tbl_items.JobDescriptionTitle3,tbl_items.JobDescriptionTitle4,tbl_items.JobDescriptionTitle5,
tbl_items.JobDescriptionTitle6,tbl_items.JobDescriptionTitle7,tbl_items.JobDescriptionTitle8,tbl_items.JobDescriptionTitle9,
tbl_items.JobDescriptionTitle10,
tbl_items.JobDescription1,tbl_items.JobDescription2,tbl_items.JobDescription3,tbl_items.JobDescription4,tbl_items.JobDescription5,
tbl_items.JobDescription6,tbl_items.JobDescription7,tbl_items.JobDescription8,tbl_items.JobDescription9,tbl_items.JobDescription10, 
tbl_items.JobEstimatedCompletionDateTime,
tbl_systemusers.FullName AS SalesPerson,
tbl_items.JobCreationDateTime,JobProgress.FullName as JobProgressedBy,
tbl_estimates.Estimate_Code,tbl_Addresses.Address1,tbl_Addresses.City,tbl_ContactCompanies.Name,
tbl_Contacts.FirstName,
( case tbl_items.jobSelectedQty 
when 1 then tbl_items.qty1title
when 2 then tbl_items.qty2title
when 3 then tbl_items.qty3title
end ) as QtyDescription,
tbl_Addresses.Tel1
,tbl_systemusers.FullName as EstimatedBy 
FROM tbl_items
Inner JOIN tbl_estimates ON (tbl_estimates.EstimateID = tbl_items.EstimateID) 
Inner JOIN tbl_ContactCompanies ON (tbl_estimates.ContactCompanyID = tbl_ContactCompanies.ContactCompanyID) 
Inner JOIN tbl_Addresses ON (tbl_estimates.AddressID = tbl_Addresses.AddressID) 
Inner JOIN tbl_Contacts ON (tbl_estimates.ContactID = tbl_Contacts.ContactID) 
Inner JOIN tbl_systemusers JobProgress ON (tbl_items.JobProgressedBy = JobProgress.SystemUserID) 
INNER JOIN tbl_systemusers ON (tbl_estimates.SalesPersonID = tbl_systemusers.SystemUserID) 
where tbl_items.ItemID=@ItemID
	RETURN