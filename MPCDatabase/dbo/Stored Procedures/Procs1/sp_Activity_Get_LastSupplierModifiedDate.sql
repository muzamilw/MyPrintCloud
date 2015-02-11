CREATE PROCEDURE dbo.sp_Activity_Get_LastSupplierModifiedDate
	@CustomerContactID int,
	@SystemUserID int
AS
	select max(lastmodifieddate) AS LastModifiedDate from tbl_activity where Suppliercontactid=@CustomerContactID and systemuserid=@SystemUserID
	RETURN