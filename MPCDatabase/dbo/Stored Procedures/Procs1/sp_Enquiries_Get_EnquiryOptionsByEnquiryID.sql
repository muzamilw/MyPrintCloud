CREATE PROCEDURE dbo.sp_Enquiries_Get_EnquiryOptionsByEnquiryID
@EnquiryID int
AS
	SELECT tbl_enquiry_options.ID,
    tbl_enquiry_options.OptionNo,
    tbl_enquiry_options.ItemTitle,
    tbl_enquiry_options.CoverPages,
    tbl_enquiry_options.OtherPages,
    tbl_enquiry_options.TextPages,
    tbl_enquiry_options.Height,
    tbl_enquiry_options.Width,
    tbl_enquiry_options.OrientationID,
    tbl_enquiry_options.EnquiryID, tbl_enquiry_options.NominalCode
    FROM tbl_enquiry_options 
    WHERE tbl_enquiry_options.EnquiryID = @EnquiryID
	RETURN