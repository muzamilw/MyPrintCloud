
Create PROCEDURE [dbo].[sp_users_get_users_by_site]
(@SystemSiteID int)
AS
	SELECT tbl_systemusers.SystemUserID,tbl_systemusers.FullName,tbl_systemusers.Email,tbl_systemusers.UserType 
	FROM tbl_systemusers WHERE tbl_systemusers.CompanySiteID = @SystemSiteID 
	RETURN