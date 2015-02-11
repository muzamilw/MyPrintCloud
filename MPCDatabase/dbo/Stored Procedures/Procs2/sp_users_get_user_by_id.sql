
create PROCEDURE [dbo].[sp_users_get_user_by_id]
	(
		@SystemUserID int
	)

AS
	SELECT tbl_systemusers.CompanySiteID,tbl_systemusers.IsScheduleable,tbl_systemusers.SystemUserID,
	tbl_systemusers.OrganizationID,tbl_systemusers.UserName, tbl_systemusers.Description,
	tbl_systemusers.DepartmentID,tbl_systemusers.Password,tbl_systemusers.FullName, 
	tbl_systemusers.CostPerHour,tbl_systemusers.IsTillSupervisor,tbl_systemusers.IsAccountDisabled,
	 tbl_systemusers.RoleID, tbl_systemusers.UserType,tbl_systemusers.Mobile,tbl_systemusers.Email, 
	 tbl_roles.RoleName,tbl_systemusers.ReplyEmail,tbl_systemusers.CanSendEmail 
	 FROM tbl_systemusers inner join tbl_roles on(tbl_systemusers.RoleID = tbl_roles.RoleID)  
	 WHERE tbl_systemusers.SystemUserID = @SystemUserID
	RETURN