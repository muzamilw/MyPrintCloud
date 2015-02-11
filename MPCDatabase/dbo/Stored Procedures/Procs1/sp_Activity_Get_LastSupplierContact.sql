CREATE PROCEDURE dbo.sp_Activity_Get_LastSupplierContact
	@CustomerContactID int, 
	@Systemuserid int
AS
	select max(tbl_activity.Completiondate) as ActivityDate from tbl_activity where suppliercontactid=@CustomerContactID and systemuserid=@Systemuserid and iscomplete=0
	RETURN