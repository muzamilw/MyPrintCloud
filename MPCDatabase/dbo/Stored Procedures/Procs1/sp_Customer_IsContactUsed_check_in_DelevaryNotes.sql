CREATE PROCEDURE dbo.sp_Customer_IsContactUsed_check_in_DelevaryNotes
	(
		@ContactId int
	)
AS
	SELECT tbl_deliverynotes.ContactId
         FROM tbl_deliverynotes WHERE tbl_deliverynotes.ContactId=@ContactId
	RETURN