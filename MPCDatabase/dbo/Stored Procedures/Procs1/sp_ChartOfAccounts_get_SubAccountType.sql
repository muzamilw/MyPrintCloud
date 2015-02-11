Create PROCEDURE dbo.sp_ChartOfAccounts_get_SubAccountType
/*
	(
		@parameter1 datatype = default value,
		@parameter2 datatype OUTPUT
	)
*/


AS

SELECT   TypeID,  Name,  SubTypeID FROM tbl_subaccountype

RETURN