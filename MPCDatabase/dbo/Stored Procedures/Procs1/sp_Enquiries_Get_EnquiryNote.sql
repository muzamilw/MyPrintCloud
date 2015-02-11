CREATE PROCEDURE dbo.sp_Enquiries_Get_EnquiryNote
	@EnquiryID int
AS
	Select EnquiryNotes from tbl_enquiries where EnquiryID=@EnquiryID
	RETURN