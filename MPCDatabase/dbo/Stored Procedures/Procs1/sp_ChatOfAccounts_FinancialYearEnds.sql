CREATE PROCEDURE dbo.sp_ChatOfAccounts_FinancialYearEnds

(@SystemSiteID int)
AS

	Declare @FinancialYearStartDate datetime
	Declare @FinancialYearEndDate datetime
	Declare @FinancialYearID int
	
	
	Declare @ID int
	
	Select @FinancialYearStartDate=StartFinancialYear,@FinancialYearEndDate=EndFinancialYear  from tbl_accountdefault Where SystemSiteID=@SystemSiteID
	
	Insert into tbl_financialyear  values (@FinancialYearStartDate,@FinancialYearEndDate,@SystemSiteID)
	
	Select @FinancialYearID=@@Identity
	
	Insert into tbl_financialyeardetail select @FinancialYearID,AccountNo,OpeningBalance,Balance from tbl_chartofaccount where SystemSiteID=@SystemSiteID
	
	Update tbl_chartofaccount set OpeningBalance=Balance where SystemSiteID=@SystemSiteID
	
	Set @FinancialYearStartDate = DATEADD(Day,1,@FinancialYearEndDate)
	Set @FinancialYearEndDate = DATEADD(Year,1,@FinancialYearStartDate)
	Set @FinancialYearEndDate = DATEADD(Day,-1,@FinancialYearEndDate)
	
	Update tbl_accountdefault set StartFinancialYear=@FinancialYearStartDate,EndFinancialYear=@FinancialYearEndDate where SystemSiteID=@SystemSiteID
	
	RETURN