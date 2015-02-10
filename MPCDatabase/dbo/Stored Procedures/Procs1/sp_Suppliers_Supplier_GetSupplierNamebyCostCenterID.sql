CREATE PROCEDURE [dbo].[sp_Suppliers_Supplier_GetSupplierNamebyCostCenterID]--12
(
@CostCenterID int
)
AS
declare @SupplierID int
	Select @SupplierID = PreferredSupplierID from tbl_costCentres Where CostCentreID = @CostCenterID
SELECT Name   from tbl_contactCompanies where tbl_contactCompanies.ContactCompanyID = @SupplierID 

	RETURN