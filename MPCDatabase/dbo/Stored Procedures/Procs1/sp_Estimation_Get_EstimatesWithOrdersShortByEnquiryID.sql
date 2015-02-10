
Create PROCEDURE [dbo].[sp_Estimation_Get_EstimatesWithOrdersShortByEnquiryID]
(@EnquiryID int)
AS
	select tbl_estimates.EstimateID,tbl_estimates.Estimate_Code,tbl_estimates.Estimate_Name,tbl_estimates.Version from tbl_estimates where EnquiryID=@EnquiryID order by tbl_estimates.Estimate_Code
	RETURN