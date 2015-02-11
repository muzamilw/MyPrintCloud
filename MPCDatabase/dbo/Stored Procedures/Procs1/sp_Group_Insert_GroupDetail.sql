CREATE PROCEDURE dbo.sp_Group_Insert_GroupDetail
@ContactID int,@IsCustomerContact smallint,@GroupID int
AS
	INSERT INTO tbl_group_detail 
(ContactID,IsCustomerContact,GroupID)
Values
(@ContactID,@IsCustomerContact,@GroupID)
	RETURN