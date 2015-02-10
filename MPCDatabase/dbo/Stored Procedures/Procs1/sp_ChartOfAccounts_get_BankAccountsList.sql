CREATE PROCEDURE dbo.sp_ChartOfAccounts_get_BankAccountsList

	(
		@SiteString varchar(5000)
	)



AS

Declare @String varchar(5000) 
	Set @String= 'SELECT  tbl_chartofaccount.AccountNo as [Nominal Code],tbl_chartofaccount.Name  as Name, tbl_chartofaccount.Balance as Debit,0 as Credit, tbl_accounttype.Name as Type,tbl_subaccountype.Name as [Sub Type],tbl_chartofaccount.LastActivityDate as [Last Activity Date],tbl_chartofaccount.IsRead FROM  tbl_chartofaccount INNER JOIN tbl_accounttype ON tbl_chartofaccount.TypeID = tbl_accounttype.TypeID LEFT OUTER JOIN tbl_subaccountype ON tbl_chartofaccount.SubTypeID = tbl_subaccountype.SubTypeID INNER JOIN tbl_company_sites ON (tbl_company_sites.SiteID=tbl_chartofaccount.SystemSiteID) WHERE (tbl_chartofaccount.Nature=1 and tbl_chartofaccount.TypeID = 1 and tbl_chartofaccount.SubtypeID = 5) and ' + @SiteString + ' RETURN ' 
Exec(@String)
RETURN