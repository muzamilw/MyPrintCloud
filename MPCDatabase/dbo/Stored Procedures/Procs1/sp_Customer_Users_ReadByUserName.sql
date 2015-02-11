CREATE PROCEDURE dbo.sp_Customer_Users_ReadByUserName
	(
	@UserName varchar(50),
	@Password varchar(50)
	)
AS
	SELECT     tbl_customeruserroles.CustomerUserRoleName, tbl_customerusers.*, tbl_customers.CustomerName ,tbl_customers.HomeContact,tbl_customers.AbountUs,tbl_customers.ContactUs,tbl_customers.CustomerImage,tbl_customers.IsShowFinishedGoodPrices,tbl_customercontacts.CustomerContactID,tbl_customers.CustomerSalesPerson,tbl_customers.SystemSiteID,tbl_company_sites.CompanyID
FROM         tbl_customerusers 
						INNER JOIN tbl_customeruserroles ON tbl_customerusers.CustomerUserRoleId = tbl_customeruserroles.CustomerUserRoleId 
						INNER JOIN tbl_customers ON tbl_customerusers.CustomerID = tbl_customers.CustomerID
						INNER JOIN tbl_customercontacts ON tbl_customercontacts.CustomerID = tbl_customers.CustomerID and tbl_customercontacts.IsDefaultContact <>0
						INNER JOIN tbl_company_sites ON tbl_customers.SystemSiteID = tbl_company_sites.SiteID
						
WHERE     (tbl_customerusers.UserName = @UserName) AND (tbl_customerusers.Password LIKE @Password)

	RETURN