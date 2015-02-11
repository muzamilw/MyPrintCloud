
Create PROCEDURE [dbo].[sp_Customers_IsCustomerUsed_Checkin_Estimate_Projection]

	(
		@CustomerID int
	)

AS
	SELECT tbl_estimate_projection.contactcompanyid
         FROM tbl_estimate_projection WHERE tbl_estimate_projection.contactcompanyid=@CustomerID
         
	RETURN