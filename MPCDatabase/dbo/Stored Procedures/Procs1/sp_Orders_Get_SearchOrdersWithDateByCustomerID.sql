CREATE PROCEDURE [dbo].[sp_Orders_Get_SearchOrdersWithDateByCustomerID]
(
	@CustomerID int,
	@SearchText varchar (100),
	@sDate datetime,
	@eDate datetime
)	
AS
	SELECT tbl_estimates.EstimateID,
        tbl_estimates.Estimate_Code,tbl_estimates.Estimate_Name,tbl_estimates.ContactCompanyID,tbl_estimates.ContactID,
        tbl_Statuses.StatusName as status,tbl_estimates.Estimate_Total,tbl_estimates.Estimate_ValidUpto,
        tbl_estimates.UserNotes,tbl_estimates.LastUpdatedBy,tbl_estimates.CreationDate,tbl_estimates.CreationTime,
        tbl_estimates.Created_by,tbl_systemusers.FullName as SalesPerson,tbl_estimates.HeadNotes,tbl_estimates.FootNotes,
        tbl_estimates.EstimateDate,tbl_estimates.ProjectionDate,tbl_estimates.Greeting,
        tbl_estimates.AccountNumber,tbl_estimates.OrderNo,tbl_estimates.SuccessChanceID,tbl_estimates.LockedBy,
        tbl_estimates.AddressID,tbl_estimates.CompanyName,tbl_estimates.sectionflagid,
        tbl_estimates.IsEstimate,tbl_estimates.IsInPipeLine,tbl_estimates.Order_Code,
        tbl_estimates.Order_Date,tbl_estimates.Order_CreationDateTime,tbl_estimates.Order_DeliveryDate,
        tbl_estimates.Order_ConfirmationDate,tbl_estimates.Order_Status,tbl_estimates.Order_CompletionDate,
        tbl_ContactCompanies.SystemSiteID,tbl_estimates.NotesUpdateDateTime,tbl_estimates.NotesUpdatedByUserID,
        (Title +' ' +tbl_Contacts.FirstName +' ' +tbl_Contacts.MiddleName +' '+ tbl_Contacts.LastName) as FullName
        FROM tbl_Contacts
        INNER JOIN tbl_estimates ON (tbl_Contacts.ContactID = tbl_estimates.ContactID)
		Inner join tbl_ContactCompanies ON (tbl_estimates.contactcompanyid = tbl_ContactCompanies.contactcompanyid)
		INNER JOIN tbl_company_sites ON (tbl_company_sites.CompanySiteID = tbl_ContactCompanies.SystemSiteID) 
		INNER JOIN tbl_Statuses on (tbl_Statuses.statusid = tbl_estimates.statusid)
		INNER JOIN tbl_systemusers ON (tbl_estimates.created_by = tbl_systemusers.systemuserid) 
        WHERE ((tbl_estimates.IsEstimate = 0) and(tbl_estimates.ContactCompanyID = @CustomerID))
        AND (tbl_estimates.Estimate_Name LIKE @SearchText OR  tbl_estimates.Order_Code LIKE @SearchText 
        OR tbl_Contacts.FirstName LIKE @SearchText OR tbl_Contacts.MiddleName LIKE @SearchText 
        OR tbl_Contacts.LastName LIKE @SearchText)
        AND ((tbl_estimates.Order_Date BETWEEN @sDate AND @eDate) OR (tbl_estimates.Order_CreationDateTime BETWEEN @sDate AND @eDate))
	RETURN