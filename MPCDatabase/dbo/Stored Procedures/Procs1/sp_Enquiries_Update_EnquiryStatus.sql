CREATE PROCEDURE dbo.sp_Enquiries_Update_EnquiryStatus
@EnquiryID int, @Status int
AS
	update tbl_enquiries set Status=@Status where EnquiryID =@EnquiryID
	RETURN