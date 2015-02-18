CREATE PROCEDURE dbo.sp_Enquiries_Update_EnquiryOption
	@OptionNo int,@ItemTitle varchar(100),@CoverPages int,@TextPages int,@OtherPages int,@Height float ,@Width float,
	@OrientationID int,@EnquiryID int, @ID int,@NominalCode int
AS
	update tbl_enquiry_options set OptionNo=@OptionNo,ItemTitle=@ItemTitle,CoverPages=@CoverPages,TextPages=@TextPages,OtherPages=@OtherPages,Height=@Height,Width=@Width,OrientationID=@OrientationID,EnquiryID=@EnquiryID,NominalCode=@NominalCode where (ID=@ID)
	RETURN