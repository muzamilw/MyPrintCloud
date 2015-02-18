CREATE PROCEDURE [dbo].[sp_companies_site_update_costcentredll]
(@CostCentreUpdationDate datetime,
@CostCentreDLL image,
@CompanyID int)
                  AS
Update tbl_company set CostCentreUpdationDate=@CostCentreUpdationDate,CostCentreDLL=@CostCentreDLL where CompanyID=@CompanyID
return