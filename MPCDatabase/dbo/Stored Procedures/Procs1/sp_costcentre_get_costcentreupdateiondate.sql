CREATE PROCEDURE dbo.sp_costcentre_get_costcentreupdateiondate
(@CompanyID int)
AS
select CostCentreUpdationDate from tbl_company where CompanyID =@CompanyID
        
                 RETURN