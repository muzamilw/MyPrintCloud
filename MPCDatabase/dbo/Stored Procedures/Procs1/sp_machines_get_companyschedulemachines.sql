CREATE PROCEDURE dbo.sp_machines_get_companyschedulemachines
(@CompanyID int)
AS
	select * from tbl_machines where CompanyID=@CompanyID and IsScheduleable <> 0 
        RETURN