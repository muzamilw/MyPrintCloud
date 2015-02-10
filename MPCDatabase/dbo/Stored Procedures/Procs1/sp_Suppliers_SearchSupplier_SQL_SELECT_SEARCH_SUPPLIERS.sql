CREATE PROCEDURE dbo.sp_Suppliers_SearchSupplier_SQL_SELECT_SEARCH_SUPPLIERS
	(
		@SearchText varchar
	)
AS
SELECT distinct tbl_suppliers.SupplierID,tbl_suppliers.SupplierName,
         tbl_suppliers.AccountNumber, (tbl_suppliercontacts.Title + ' '+ tbl_suppliercontacts.FirstName  + ' '+  tbl_suppliercontacts.MiddleName  + ' '+  tbl_suppliercontacts.LastName) as FullName,
         tbl_supplieraddresses.AddressName,tbl_supplieraddresses.City,tbl_state.StateName,tbl_country.CountryName,
         tbl_supplieraddresses.Tel1,tbl_suppliers.FlagID FROM tbl_suppliers
         INNER JOIN tbl_suppliercontacts ON (tbl_suppliers.SupplierID = tbl_suppliercontacts.SupplierID and tbl_suppliercontacts.IsDefaultContact<>0)
         INNER JOIN tbl_supplieraddresses ON (tbl_suppliers.SupplierID = tbl_supplieraddresses.SupplierID and tbl_supplieraddresses.IsDefaultAddress<>0)
         INNER JOIN tbl_country ON (tbl_supplieraddresses.CountryID = tbl_country.CountryID)
         INNER JOIN tbl_state ON (tbl_supplieraddresses.StateID = tbl_state.StateID)
         where (tbl_suppliers.SupplierName like @SearchText or tbl_suppliers.AccountNumber like @SearchText 
         AND tbl_suppliers.IsDisabled=0 OR tbl_suppliers.URL LIKE @SearchText OR tbl_suppliers.Terms LIKE @SearchText
         OR tbl_suppliers.Notes LIKE @SearchText OR tbl_supplieraddresses.Address1 LIKE @SearchText OR tbl_supplieraddresses.Address2 LIKE @SearchText 
         OR tbl_supplieraddresses.Address3 LIKE @SearchText OR tbl_supplieraddresses.AddressName LIKE @SearchText
         OR tbl_supplieraddresses.City LIKE @SearchText OR tbl_supplieraddresses.PostCode LIKE @SearchText
         OR tbl_supplieraddresses.Fax LIKE @SearchText OR tbl_supplieraddresses.Email LIKE @SearchText 
         OR tbl_supplieraddresses.Tel1 LIKE @SearchText OR tbl_supplieraddresses.Tel2 LIKE @SearchText 
         OR tbl_suppliercontacts.FirstName LIKE @SearchText OR tbl_suppliercontacts.MiddleName LIKE @SearchText 
         OR tbl_suppliercontacts.LastName LIKE @SearchText OR tbl_suppliercontacts.Title LIKE @SearchText 
         OR tbl_suppliercontacts.Title LIKE @SearchText
         OR tbl_suppliercontacts.JobTitle LIKE @SearchText OR tbl_suppliercontacts.Department LIKE @SearchText 
         OR tbl_suppliercontacts.DOB LIKE @SearchText OR tbl_suppliercontacts.Notes LIKE @SearchText)
	RETURN