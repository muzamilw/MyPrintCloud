CREATE PROCEDURE dbo.sp_Enquiries_Delete_EnquiryOptions
(@EnquiryID int)
AS
	Delete from tbl_enquiry_options where EnquiryID=@EnquiryID
RETURN