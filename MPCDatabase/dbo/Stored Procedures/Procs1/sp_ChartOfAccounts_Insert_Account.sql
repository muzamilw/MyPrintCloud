CREATE PROCEDURE [dbo].[sp_ChartOfAccounts_Insert_Account]

	@AccountNo  int,
	@Name  varchar(100),
	@OpeningBalance  float,
	@OpeningBalanceType smallint,
	@TypeID int,
	@SubTypeID int,
	@Description varchar(255),
	@Nature smallint,
	@IsActive smallint,
	@Balance float,
	@IsForReconciliation smallint,
	@SystemSiteID int

AS

insert into tbl_chartofaccount 
	(AccountNo,Name,OpeningBalance,OpeningBalanceType,TypeID,
	 SubTypeID,Description, Nature,IsActive,Balance ,IsForReconciliation,SystemSiteID )
	 VALUES (@AccountNo,@Name,@OpeningBalance,@OpeningBalanceType,@TypeID,@SubTypeID,@Description,@Nature,@IsActive,@Balance,@IsForReconciliation,@SystemSiteID)


RETURN