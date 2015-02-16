
Create PROCEDURE [dbo].[sp_costcentre_get_completecode]
(@CompanyID int)
AS
SELECT tbl_costcentres.CostCentreID,tbl_costcentres.CompleteCode FROM tbl_costcentretypes  
         INNER JOIN tbl_costcentres ON (tbl_costcentretypes.TypeID = tbl_costcentres.Type) 
         INNER JOIN tbl_company_sites ON (tbl_company_sites.CompanySiteID=tbl_costcentres.SystemSiteID) 
         where ((tbl_costcentretypes.IsExternal =  1 and   tbl_costcentretypes.IsSystem =0) 
         and tbl_company_sites.CompanyID=@CompanyID)
        
                 RETURN