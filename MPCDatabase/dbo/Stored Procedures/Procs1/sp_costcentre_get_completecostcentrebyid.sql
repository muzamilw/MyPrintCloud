
CREATE PROCEDURE [dbo].[sp_costcentre_get_completecostcentrebyid]
(@CostCentreID int)
AS
SELECT ISNULL(tbl_contactcompanies.name,'') as SupplierName, tbl_costcentres.* from tbl_costcentres 
left outer join tbl_contactcompanies on tbl_contactcompanies.contactcompanyid = tbl_costcentres.preferredsupplierid
where tbl_costcentres.CostCentreID=@CostCentreID
	RETURN