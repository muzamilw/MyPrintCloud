
create PROCEDURE [dbo].[sp_users_add_USER]
(@IsScheduleable bit,
@OrganizationID int,
@UserName varchar(100),
@Description varchar(100),
@SystemSiteID int,
@DepartmentID int,
@Password text,
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
	insert into tbl_systemusers (IsScheduleable,OrganizationID,UserName,Description,CompanySiteID,DepartmentID,Password,FullName,Email,Mobile,UserType,RoleID,IsAccountDisabled,IsTillSupervisor,CostPerHour,CanSendEmail,ReplyEmail) VALUES (@IsScheduleable,@OrganizationID,@UserName,@Description,@SystemSiteID,@DepartmentID,@Password,@FullName,@Email,@Mobile,@UserType,@RoleID,@IsAccountDisabled,@IsTillSupervisor,@CostPerHour,@CanSendEmail,@ReplyEmail); select @@Identity as UID
	RETURN