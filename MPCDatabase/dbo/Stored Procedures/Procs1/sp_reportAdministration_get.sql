CREATE PROCEDURE dbo.sp_reportAdministration_get

AS
	SELECT tbl_systemusers.SystemUserID, tbl_systemusers.UserName,  tbl_systemusers.Description,  tbl_systemusers.Password,  tbl_systemusers.FullName,  tbl_systemusers.Email,
       tbl_systemusers.Mobile,  tbl_systemusers.UserType,   tbl_systemusers.RoleID,  tbl_systemusers.IsAccountDisabled,  tbl_systemusers.CostPerHour FROM tbl_systemusers
	RETURN