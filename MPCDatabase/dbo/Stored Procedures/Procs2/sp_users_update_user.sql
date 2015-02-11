
create PROCEDURE [dbo].[sp_users_update_user]
	(@IsScheduleable bit,
@OrganizationID int,
@UserName varchar(100),
@Description varchar(100),
@SystemSiteID int,
@DepartmentID int,
@SystemUserID int,
@FullName varchar(100),
@Email varchar(100),
@Mobile varchar(100),
@UserType varchar(100),
@RoleID int,
@IsAccountDisabled bit,
@IsTillSupervisor bit,
@CostPerHour float,
@CanSendEmail bit,
@ReplyEmail varchar(50))
AS
	update tbl_systemusers set IsScheduleable=@IsScheduleable,OrganizationID=@OrganizationID,UserName=@UserName,Description=@Description,CompanySiteID=@SystemSiteID,DepartmentID=@DepartmentID, FullName=@FullName,Email=@Email,Mobile=@Mobile,UserType=@UserType,RoleID=@RoleID,IsAccountDisabled=@IsAccountDisabled,IsTillSupervisor=@IsTillSupervisor,CostPerHour=@CostPerHour,CanSendEmail=@CanSendEmail,ReplyEmail=@ReplyEmail where (SystemUserID=@SystemUserID)
	RETURN