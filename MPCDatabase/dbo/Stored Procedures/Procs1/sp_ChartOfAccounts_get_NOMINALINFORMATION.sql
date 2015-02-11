CREATE PROCEDURE [dbo].[sp_ChartOfAccounts_get_NOMINALINFORMATION]

	@AccountNo  int,
	@SystemSiteID varchar(500)
AS

Declare @String varchar(3000)
Set @String = 'SELECT AccountNo,Name,OpeningBalance, Balance,IsForReconciliation as Reconcilation,
 OpeningBalanceType,TypeID, SubTypeID,Description, Nature,IsActive ,ISFIXED , LastActivityDate,IsRead 
 FROM tbl_chartofaccount 
 INNER JOIN tbl_company_sites ON (tbl_company_sites.CompanySiteID=tbl_chartofaccount.SystemSiteID) 
 WHERE ACCOUNTNO=' + convert(varchar(150),@ACCOUNTNO ) + ' and ' + @SystemSiteID

Exec (@String)
RETURN