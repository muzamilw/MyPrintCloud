
Create PROCEDURE [dbo].[sp_Activity_Get_LastCustomerModifiedDate]
	@CustomerContactID int,
	@SystemUserID int
AS
	select max(lastmodifieddate) AS LastModifiedDate from tbl_activity where contactid=@CustomerContactID and systemuserid=@SystemUserID
	RETURN