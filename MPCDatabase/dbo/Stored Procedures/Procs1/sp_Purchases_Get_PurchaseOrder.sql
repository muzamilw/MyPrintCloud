CREATE PROCEDURE [dbo].[sp_Purchases_Get_PurchaseOrder]

	(
		@PurchaseID int
	)

AS
	/* SET NOCOUNT ON */
	SELECT tbl_purchase.*,tbl_Addresses.Address1,tbl_Addresses.Address2,
tbl_Addresses.Address3,tbl_Addresses.City,tbl_Addresses.State,tbl_Addresses.Country,
tbl_Addresses.PostCode,tbl_Addresses.Fax,tbl_Addresses.Extension1,tbl_Addresses.Tel1,
tbl_ContactCompanies.Name,tbl_ContactCompanies.ContactCompanyID
FROM tbl_ContactCompanies 
INNER JOIN tbl_purchase ON (tbl_ContactCompanies.ContactCompanyID = tbl_purchase.SupplierID)
INNER JOIN tbl_Addresses ON (tbl_purchase.SupplierContactAddressID = tbl_Addresses.AddressID)
WHERE PurchaseID = @PurchaseID
	RETURN