CREATE PROCEDURE [dbo].[sp_Customers_DefaultNominalCode]
	(
      @CustomerId int
	)

AS
	SELECT tbl_contactcompanies.DefaultNominalCode FROM tbl_contactcompanies 
        WHERE tbl_contactcompanies.contactcompanyid =@CustomerId
	RETURN