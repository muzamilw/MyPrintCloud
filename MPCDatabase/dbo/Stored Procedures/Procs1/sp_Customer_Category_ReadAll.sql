CREATE PROCEDURE [dbo].[sp_Customer_Category_ReadAll]
	(
	@CustomerID int
	)
	

AS
select * from tbl_productCategory where ContactCompanyID=@CustomerID order by DisplayOrder

	RETURN