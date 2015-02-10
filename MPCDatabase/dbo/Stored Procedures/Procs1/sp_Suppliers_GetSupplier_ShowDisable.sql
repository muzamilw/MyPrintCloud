CREATE PROCEDURE [dbo].[sp_Suppliers_GetSupplier_ShowDisable]
	(
		@SystemSiteID int,
		@SystemCompanyID int,
		@CompanyLevel smallint
	)
AS
        if(@CompanyLevel=0) 
			BEGIN
					SELECT  tbl_ContactCompanies.ContactCompanyID,tbl_ContactCompanies.Name,tbl_ContactCompanies.AccountNumber,
(tbl_Contacts.Title + ' '+ tbl_Contacts.FirstName  + ' '+  tbl_Contacts.MiddleName  + ' '+  tbl_Contacts.LastName) as FullName,
tbl_Addresses.AddressName,tbl_Addresses.City,tbl_Addresses.state as StateName,tbl_Addresses.country as CountryName,
tbl_Addresses.Tel1,tbl_company_sites.CompanySiteName,FlagID,IsReed
FROM tbl_ContactCompanies INNER JOIN tbl_Addresses ON (tbl_ContactCompanies.ContactCompanyID = tbl_Addresses.ContactCompanyID AND tbl_Addresses.IsDefaultAddress = 1)
INNER JOIN tbl_Contacts ON (tbl_ContactCompanies.ContactCompanyID = tbl_Contacts.ContactCompanyID AND tbl_Contacts.IsDefaultContact = 1)
INNER JOIN tbl_company_sites ON (tbl_company_sites.CompanySiteID=tbl_ContactCompanies.SystemSiteID) 
where tbl_ContactCompanies.SystemSiteID=@SystemSiteID AND tbl_ContactCompanies.IsDisabled=1 AND tbl_ContactCompanies.Iscustomer = 2 ;
			END
       ELSE
        
			BEGIN
		        
				SELECT  tbl_ContactCompanies.ContactCompanyID,tbl_ContactCompanies.Name,tbl_ContactCompanies.AccountNumber,
		 (tbl_Contacts.Title + ' '+ tbl_Contacts.FirstName  + ' '+  tbl_Contacts.MiddleName  + ' '+  tbl_Contacts.LastName) as FullName,
				tbl_Addresses.AddressName,tbl_Addresses.City,tbl_Addresses.state as StateName,tbl_Addresses.country as CountryName,
				tbl_Addresses.Tel1,tbl_company_sites.CompanySiteName,FlagID,IsReed
				FROM tbl_ContactCompanies INNER JOIN tbl_Addresses ON (tbl_ContactCompanies.ContactCompanyID = tbl_Addresses.ContactCompanyID AND tbl_Addresses.IsDefaultAddress = 1)
				INNER JOIN tbl_Contacts ON (tbl_ContactCompanies.ContactCompanyID = tbl_Contacts.ContactCompanyID AND tbl_Contacts.IsDefaultContact = 1)
				INNER JOIN tbl_company_sites ON (tbl_company_sites.CompanySiteID=tbl_ContactCompanies.SystemSiteID) 
			where tbl_company_sites.CompanyID=@SystemSiteID AND tbl_ContactCompanies.IsDisabled=1 AND tbl_ContactCompanies.Iscustomer = 2 ;
		    
		  END
       
        	RETURN