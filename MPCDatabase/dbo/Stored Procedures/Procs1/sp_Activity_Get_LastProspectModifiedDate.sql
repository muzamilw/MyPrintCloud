CREATE PROCEDURE dbo.sp_Activity_Get_LastProspectModifiedDate
	@CustomerContactID int,
	@SystemUserID int
AS
	select max(lastmodifieddate) AS LastModifiedDate from tbl_activity where Prospectcontactid=@CustomerContactID and systemuserid=@SystemUserID
	RETURN