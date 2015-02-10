CREATE PROCEDURE dbo.sp_Enquiries_Update_EnquiryFlag
@EnquiryID int , @FlagID int
AS
	update tbl_enquiries set flagid=@FlagID where EnquiryID=@EnquiryID
	RETURN