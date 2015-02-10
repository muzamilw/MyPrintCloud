CREATE PROCEDURE dbo.sp_Enquiries_Insert_EnquiryOption
@OptionNo int ,@ItemTitle varchar (100) ,@CoverPages int ,@TextPages int,@OtherPages int ,@Height float,@Width float,@OrientationID int ,@EnquiryID int,@NominalCode int
AS
	insert into tbl_enquiry_options (OptionNo,
    ItemTitle,CoverPages,TextPages,OtherPages,Height,Width,OrientationID,EnquiryID,NominalCode) VALUES 
    (@OptionNo,@ItemTitle,@CoverPages,@TextPages,@OtherPages,@Height,@Width,@OrientationID,@EnquiryID,@NominalCode);select @@Identity as ID
	RETURN