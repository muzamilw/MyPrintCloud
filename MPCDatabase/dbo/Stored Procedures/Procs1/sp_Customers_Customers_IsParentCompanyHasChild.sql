CREATE PROCEDURE dbo.sp_Customers_Customers_IsParentCompanyHasChild
(
@CustomerID int
)
	

AS
	Select ParaentCompanyID from tbl_customers WHERE ParaentCompanyID=@CustomerID

	RETURN