CREATE PROCEDURE dbo.sp_costcentre_get_costcentredll
(@CompanyID int)
AS
select CostCentreDLL from tbl_company where CompanyID =@CompanyID
        
                 RETURN