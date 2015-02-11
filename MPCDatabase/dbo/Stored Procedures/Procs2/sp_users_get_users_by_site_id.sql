
Create PROCEDURE [dbo].[sp_users_get_users_by_site_id]
(@SystemSiteID int)
AS
	SELECT  tbl_systemusers.UserName,  tbl_systemusers.SystemUserID, tbl_systemusers.UserType, tbl_systemusers.FullName, tbl_systemusers.Email, tbl_systemusers.Mobile, tbl_systemusers.CompanySiteID, tbl_systemusers.CostPerHour, tbl_systemusers.Description   FROM tbl_systemusers 
	Where tbl_systemusers.CompanySiteID = @SystemSiteID and tbl_systemusers.IsScheduleable <> 0
	RETURN