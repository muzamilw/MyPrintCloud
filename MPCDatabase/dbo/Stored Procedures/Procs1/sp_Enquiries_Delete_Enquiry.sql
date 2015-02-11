CREATE PROCEDURE dbo.sp_Enquiries_Delete_Enquiry
(@EnquiryID int)
AS
	Delete from tbl_enquiries where EnquiryID=@EnquiryID
	RETURN