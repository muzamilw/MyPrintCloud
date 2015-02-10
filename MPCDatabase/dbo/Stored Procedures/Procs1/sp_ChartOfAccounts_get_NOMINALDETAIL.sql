




Create PROCEDURE [dbo].[sp_ChartOfAccounts_get_NOMINALDETAIL]

	@AccountNo  int,
	@SystemSiteID int

AS
	SELECT tbl_nominaldetail.AccountNo,tbl_nominaldetail.BankDescription,tbl_nominaldetail.NoReconciliation, tbl_nominaldetail.BankName,tbl_nominaldetail.BankAddress,
		tbl_nominaldetail.BankTown,tbl_nominaldetail.BankCounty,tbl_nominaldetail.BankPostcode,tbl_nominaldetail.AccountName,tbl_nominaldetail.AccountNumber,
		tbl_nominaldetail.SortCode,tbl_nominaldetail.ExpiryDate,tbl_nominaldetail.ContactName,tbl_nominaldetail.ContactTel,tbl_nominaldetail.ContactFax, 
		tbl_nominaldetail.ContactEmail,tbl_nominaldetail.ContactURL,IBN 
		FROM tbl_nominaldetail 
		inner join tbl_chartofaccount on (tbl_chartofaccount.AccountNo = tbl_nominaldetail.AccountNo)
		INNER JOIN tbl_company_sites ON (tbl_company_sites.CompanySiteID=tbl_chartofaccount.SystemSiteID) 
		WHERE (tbl_nominaldetail.AccountNO=@AccountNo  and tbl_chartofaccount.systemSiteID=@SystemSiteID) 



RETURN