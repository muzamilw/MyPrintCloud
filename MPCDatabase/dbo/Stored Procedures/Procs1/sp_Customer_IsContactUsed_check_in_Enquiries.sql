CREATE PROCEDURE dbo.sp_Customer_IsContactUsed_check_in_Enquiries
	(
		@ContactID int
	)

AS
	SELECT tbl_enquiries.ContactID
         FROM tbl_enquiries WHERE tbl_enquiries.ContactID=@ContactID
         
	RETURN