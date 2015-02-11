CREATE PROCEDURE dbo.sp_Customers_IsCustomerUsed_Check_in_JOBS

	(
		@CustomerID int
	)
AS
	SELECT tbl_jobs.CustomerID
         FROM tbl_jobs WHERE tbl_jobs.CustomerID=@CustomerID
	RETURN