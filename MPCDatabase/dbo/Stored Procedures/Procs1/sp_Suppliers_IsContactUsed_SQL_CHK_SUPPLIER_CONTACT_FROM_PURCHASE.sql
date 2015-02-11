CREATE PROCEDURE dbo.sp_Suppliers_IsContactUsed_SQL_CHK_SUPPLIER_CONTACT_FROM_PURCHASE
	(
		@ContactID int
	)

AS
SELECT tbl_purchase.ContactID
         FROM tbl_purchase WHERE tbl_purchase.ContactID=@ContactID
	RETURN