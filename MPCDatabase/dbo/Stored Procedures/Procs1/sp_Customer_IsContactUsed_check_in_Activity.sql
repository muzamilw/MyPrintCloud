
Create PROCEDURE [dbo].[sp_Customer_IsContactUsed_check_in_Activity]
	(
		@CustomerContactID int
	)

AS
	SELECT tbl_activity.ContactID
         FROM tbl_activity WHERE tbl_activity.ContactID=@CustomerContactID
	RETURN