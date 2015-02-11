CREATE PROCEDURE dbo.sp_Enquiries_Update_EnquiryNote
	@EnquiryID int, @EnquiryNotes ntext
AS
	Update tbl_enquiries set EnquiryNotes=@EnquiryNotes where EnquiryID=@EnquiryID
	RETURN