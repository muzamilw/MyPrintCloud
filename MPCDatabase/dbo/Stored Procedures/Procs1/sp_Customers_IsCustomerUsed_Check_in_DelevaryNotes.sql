
Create PROCEDURE [dbo].[sp_Customers_IsCustomerUsed_Check_in_DelevaryNotes]

	(
		@CustomerId int
	)
AS
	SELECT tbl_deliverynotes.contactcompanyid
        FROM tbl_deliverynotes WHERE tbl_deliverynotes.contactcompanyid=@CustomerId
	RETURN