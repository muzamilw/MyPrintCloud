CREATE PROCEDURE dbo.sp_Group_Get_GroupsByContactID
	@ContactID int,@IsCustomerContact smallint
AS
	SELECT tbl_groups.GroupID,tbl_groups.GroupName, tbl_groups.GroupDescription
FROM tbl_group_detail 
LEFT OUTER JOIN tbl_groups ON tbl_group_detail.GroupID = tbl_groups.GroupID
WHERE (tbl_group_detail.ContactID = @ContactID) AND (tbl_group_detail.IsCustomerContact = @IsCustomerContact)
	
	RETURN