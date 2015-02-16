CREATE PROCEDURE dbo.sp_ChartOfAccounts_get_AccountsList

AS

SELECT  tbl_chartofaccount.AccountNo as [Nominal Code], tbl_chartofaccount.Name  as Name, 
(
case when tbl_chartofaccount.Nature <> 0 then
tbl_chartofaccount.Balance 
else
 0
 end
 ) as Debit, 
(
case when tbl_chartofaccount.Nature = 0 then
tbl_chartofaccount.Balance
else
0
end) as Credit,
tbl_accounttype.Name as Type,tbl_subaccountype.Name as [Sub Type], 
tbl_chartofaccount.LastActivityDate as [Last Activity Date],tbl_chartofaccount.IsRead 
FROM         tbl_chartofaccount INNER JOIN
                      tbl_accounttype ON tbl_chartofaccount.TypeID = tbl_accounttype.TypeID LEFT OUTER JOIN
                      tbl_subaccountype ON tbl_chartofaccount.SubTypeID = tbl_subaccountype.SubTypeID  
where isActive <> 0 
	RETURN