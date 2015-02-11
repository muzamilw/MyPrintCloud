Create PROCEDURE dbo.sp_ChartOfAccounts_get_AccountType
/*
	(
		@parameter1 datatype = default value,
		@parameter2 datatype OUTPUT
	)
*/


AS

SELECT TypeID, Name FROM tbl_accounttype


RETURN