CREATE PROCEDURE dbo.sp_Suppliers_IsContactUsed_SQL_CHK_SUPPLIER_CONTACT_FROM_GRN
	(
		@ContactID int
	)
AS
	SELECT tbl_goodsreceivednote.ContactID
       FROM tbl_goodsreceivednote WHERE tbl_goodsreceivednote.ContactID=@ContactID
	RETURN