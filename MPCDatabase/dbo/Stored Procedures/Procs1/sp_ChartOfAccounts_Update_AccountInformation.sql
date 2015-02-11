Create PROCEDURE dbo.sp_ChartOfAccounts_Update_AccountInformation

	@AccountNo  int,
	@Name  varchar(100),
	@TypeID int,
	@SubTypeID int,
	@Description varchar(255),
	@IsActive smallint,
	@IsForReconciliation smallint

AS

Update tbl_chartofaccount set Name=@Name, TypeID=@TypeID, SubTypeID=@SubTypeID,Description=@Description,IsActive=@IsActive ,IsForReconciliation=@IsForReconciliation where AccountNo = @AccountNo

RETURN