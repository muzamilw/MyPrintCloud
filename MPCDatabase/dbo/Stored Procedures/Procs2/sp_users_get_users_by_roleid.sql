
create PROCEDURE [dbo].[sp_users_get_users_by_roleid]
(@RoleID  int,@SystemSiteID int)
AS
	select tbl_systemusers.SystemUserID,tbl_systemusers.UserName,tbl_systemusers.Description,
	tbl_systemusers.Password,tbl_systemusers.FullName,tbl_systemusers.Email,tbl_systemusers.Mobile,
	tbl_systemusers.RoleID from tbl_systemusers  
	inner join tbl_roles on tbl_roles.roleid=tbl_systemusers.roleid  
	where tbl_roles.roleid=@RoleID and tbl_systemusers.IsAccountDisabled=0 and tbl_systemusers.CompanySiteID =@SystemSiteID
	RETURN