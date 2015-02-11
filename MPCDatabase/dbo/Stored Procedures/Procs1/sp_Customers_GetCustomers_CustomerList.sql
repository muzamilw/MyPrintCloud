﻿CREATE PROCEDURE dbo.sp_Customers_GetCustomers_CustomerList

	(
		@SystemSiteID int,
		@SystemCompanyID int,
		@CompanyLevel smallint
	)
AS
 if(@CompanyLevel=0) 
	BEGIN 
			SELECT  tbl_customers.CustomerID,tbl_customers.CustomerName,tbl_customers.AccountNumber,
         (isnull(tbl_customercontacts.Title,'') + ' ' + isnull(tbl_customercontacts.FirstName,'') + ' ' +  isnull(tbl_customercontacts.MiddleName,'') + ' ' +  isnull(tbl_customercontacts.LastName,'')) as FullName,
         tbl_customeraddresses.AddressName,tbl_customeraddresses.City,tbl_state.StateName,tbl_country.CountryName,
         tbl_customeraddresses.Tel1,tbl_company_sites.SiteName,tbl_customers.FlagID,tbl_customers.IsReed
         FROM tbl_customers INNER JOIN tbl_customeraddresses ON (tbl_customers.CustomerID = tbl_customeraddresses.CustomerID AND tbl_customeraddresses.IsDefaultAddress<>0)
         INNER JOIN tbl_country ON (tbl_customeraddresses.CountryID = tbl_country.CountryID)
         INNER JOIN tbl_state ON (tbl_customeraddresses.StateID = tbl_state.StateID)
         INNER JOIN tbl_customercontacts ON (tbl_customers.CustomerID = tbl_customercontacts.CustomerID AND tbl_customercontacts.IsDefaultContact<>0)
         INNER JOIN tbl_company_sites ON (tbl_company_sites.SiteID=tbl_customers.SystemSiteID) 
         WHERE tbl_customers.SystemSiteID=@SystemSiteID AND tbl_customers.IsCustomer=0 AND tbl_customers.IsDisabled=0 Order By CustomerName ASC 

    END
    
ELSE
    
    BEGIN 
 
  
 

	SELECT  tbl_customers.CustomerID,tbl_customers.CustomerName,tbl_customers.AccountNumber,
         (isnull(tbl_customercontacts.Title,'') + ' ' + isnull(tbl_customercontacts.FirstName,'') + ' ' +  isnull(tbl_customercontacts.MiddleName,'') + ' ' +  isnull(tbl_customercontacts.LastName,'')) as FullName,
         tbl_customeraddresses.AddressName,tbl_customeraddresses.City,tbl_state.StateName,tbl_country.CountryName,
         tbl_customeraddresses.Tel1,tbl_company_sites.SiteName,tbl_customers.FlagID,tbl_customers.IsReed
         FROM tbl_customers INNER JOIN tbl_customeraddresses ON (tbl_customers.CustomerID = tbl_customeraddresses.CustomerID AND tbl_customeraddresses.IsDefaultAddress<>0)
         INNER JOIN tbl_country ON (tbl_customeraddresses.CountryID = tbl_country.CountryID)
         INNER JOIN tbl_state ON (tbl_customeraddresses.StateID = tbl_state.StateID)
         INNER JOIN tbl_customercontacts ON (tbl_customers.CustomerID = tbl_customercontacts.CustomerID AND tbl_customercontacts.IsDefaultContact<> 0)
         INNER JOIN tbl_company_sites ON (tbl_company_sites.SiteID=tbl_customers.SystemSiteID) 
         WHERE tbl_company_sites.CompanyID=@SystemCompanyID AND tbl_customers.IsCustomer=0 AND tbl_customers.IsDisabled=0 Order By CustomerName ASC 

  END

	RETURN