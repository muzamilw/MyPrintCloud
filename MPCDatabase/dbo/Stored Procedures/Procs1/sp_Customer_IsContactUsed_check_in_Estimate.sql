
Create PROCEDURE [dbo].[sp_Customer_IsContactUsed_check_in_Estimate]
	(
		@Contact_ID int
	)
AS
	SELECT tbl_estimates.ContactID
         FROM tbl_estimates WHERE tbl_estimates.ContactID=@Contact_ID
         
	RETURN