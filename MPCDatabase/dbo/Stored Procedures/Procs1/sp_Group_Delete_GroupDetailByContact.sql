CREATE PROCEDURE [dbo].[sp_Group_Delete_GroupDetailByContact]
	@ContactID int,
	@IsCustomerContact smallint
AS
	DELETE FROM tbl_group_detail where ContactID = @ContactID AND IsCustomerContact = @IsCustomerContact
	RETURN