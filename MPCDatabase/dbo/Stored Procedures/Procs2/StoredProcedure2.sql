CREATE PROCEDURE dbo.StoredProcedure2

	(
		@SystemSiteID int,
		@SystemCompanyID int,
		@CompanyLevel int
	)

AS
	            
        
        
        if(@CompanyLevel=0) 
				begin
					SELECT  tbl_suppliers.SupplierID,tbl_suppliers.SupplierName,tbl_suppliers.AccountNumber,
			-- concat(tbl_suppliercontacts.Title,' ',tbl_suppliercontacts.FirstName,' ',tbl_suppliercontacts.MiddleName,' ', tbl_suppliercontacts.LastName) as FullName,
							tbl_supplieraddresses.AddressName,tbl_supplieraddresses.City,tbl_state.StateName,tbl_country.CountryName,
							tbl_supplieraddresses.Tel1,tbl_company_sites.SiteName,FlagID
							FROM tbl_suppliers INNER JOIN tbl_supplieraddresses ON (tbl_suppliers.SupplierID = tbl_supplieraddresses.SupplierID AND tbl_supplieraddresses.IsDefaultAddress = 1)
							INNER JOIN tbl_country ON (tbl_supplieraddresses.CountryID = tbl_country.CountryID)
							INNER JOIN tbl_state ON (tbl_supplieraddresses.StateID = tbl_state.StateID)
							INNER JOIN tbl_suppliercontacts ON (tbl_suppliers.SupplierID = tbl_suppliercontacts.SupplierID AND tbl_suppliercontacts.IsDefaultContact = 1)
							INNER JOIN tbl_company_sites ON (tbl_company_sites.SiteID=tbl_suppliers.SystemSiteID) 
						where tbl_suppliers.SystemSiteID=@SystemSiteID AND tbl_suppliers.IsDisabled=0;
				end
        else
        
				begin
		        
				SELECT  tbl_suppliers.SupplierID,tbl_suppliers.SupplierName,tbl_suppliers.AccountNumber,
			-- concat(tbl_suppliercontacts.Title,' ',tbl_suppliercontacts.FirstName,' ',tbl_suppliercontacts.MiddleName,' ', tbl_suppliercontacts.LastName) as FullName,
				tbl_supplieraddresses.AddressName,tbl_supplieraddresses.City,tbl_state.StateName,tbl_country.CountryName,
				tbl_supplieraddresses.Tel1,tbl_company_sites.SiteName,FlagID
				FROM tbl_suppliers INNER JOIN tbl_supplieraddresses ON (tbl_suppliers.SupplierID = tbl_supplieraddresses.SupplierID AND tbl_supplieraddresses.IsDefaultAddress = 1)
				INNER JOIN tbl_country ON (tbl_supplieraddresses.CountryID = tbl_country.CountryID)
				INNER JOIN tbl_state ON (tbl_supplieraddresses.StateID = tbl_state.StateID)
				INNER JOIN tbl_suppliercontacts ON (tbl_suppliers.SupplierID = tbl_suppliercontacts.SupplierID AND tbl_suppliercontacts.IsDefaultContact = 1)
				INNER JOIN tbl_company_sites ON (tbl_company_sites.SiteID=tbl_suppliers.SystemSiteID) 
			where tbl_company_sites.CompanyID=@SystemCompanyID AND tbl_suppliers.IsDisabled=0;
		    
			end
       
        	RETURN