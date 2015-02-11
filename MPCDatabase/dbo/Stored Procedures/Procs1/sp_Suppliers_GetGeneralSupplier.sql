CREATE PROCEDURE dbo.sp_Suppliers_GetGeneralSupplier
	(
		@SystemSiteID int,
		@SystemCompanyID int,
		@CompanyLevel smallint
	)
AS
        if(@CompanyLevel=0) 
			BEGIN
					SELECT  tbl_suppliers.SupplierID
							FROM tbl_suppliers 
							INNER JOIN tbl_company_sites ON (tbl_company_sites.SiteID=tbl_suppliers.SystemSiteID) 
						where tbl_suppliers.SystemSiteID=@SystemSiteID AND tbl_suppliers.IsDisabled=0 and tbl_suppliers.IsGeneral<>0 ;
			END
       ELSE
        
			BEGIN
		        
				SELECT  tbl_suppliers.SupplierID
				FROM tbl_suppliers 				
				INNER JOIN tbl_company_sites ON (tbl_company_sites.SiteID=tbl_suppliers.SystemSiteID) 
			where tbl_company_sites.CompanyID=@SystemCompanyID AND tbl_suppliers.IsDisabled=0 and tbl_suppliers.IsGeneral<>0 ;
		    
		  END
       
        	RETURN