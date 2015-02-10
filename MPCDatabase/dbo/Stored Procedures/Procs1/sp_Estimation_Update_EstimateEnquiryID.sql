
Create PROCEDURE [dbo].[sp_Estimation_Update_EstimateEnquiryID]
@EstimateID int,
@EnquiryID int

AS
	update tbl_Estimates set EnquiryID=@EnquiryID where EstimateID=@EstimateID
	RETURN