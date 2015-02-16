--Exec [sp_ChartOfAccounts_get_AccountListByType] 4, 3, 'systemSiteID in(1)'
CREATE PROCEDURE [dbo].[sp_ChartOfAccounts_get_AccountListByType] 
	
	@TypeID int,
	@SubTypeID int,
	@SystemSiteID varchar(1000)

AS

Declare @String varchar(3000)

if @typeid = 0 and @subtypeid= 0 
 Set @String = 'Select AccountNo, (Convert(varchar(15),AccountNo) + '' '' + Name) as Name , TypeID, SubTypeID,OpeningBalance,Balance,IsRead from tbl_chartofaccount INNER JOIN tbl_company_sites ON (tbl_company_sites.CompanySiteID=tbl_chartofaccount.SystemSiteID) where ' + @SystemSiteID + ' and isActive <> 0 '
else 

if @typeid = 0 or @typeid = -1
  Set @String = 'Select AccountNo, (Convert(varchar(15),AccountNo) + '' '' + Name) as Name , TypeID, SubTypeID,OpeningBalance,Balance,IsRead  from tbl_chartofaccount  
	 INNER JOIN tbl_company_sites ON (tbl_company_sites.CompanySiteID=tbl_chartofaccount.SystemSiteID)
	where  TypeID=' + convert(varchar(150),@typeid) + ' and ' + @SystemSiteID +' and isActive <> 0 '

else if(@SubTypeID = 0 or @SubTypeID = -1)

 Set @String = 'Select AccountNo, (Convert(varchar(15),AccountNo) + '' '' + Name) as Name , TypeID, SubTypeID,OpeningBalance,Balance,IsRead  from tbl_chartofaccount  
	 INNER JOIN tbl_company_sites ON (tbl_company_sites.CompanySiteID=tbl_chartofaccount.SystemSiteID)
	where  SubTypeID =' + convert(varchar(150),@subtypeid) + 'and ' + @SystemSiteID + ' and isActive <> 0 '
	
else
	Set @String = 'Select AccountNo,(Convert(varchar(15),AccountNo) + '' '' + Name) as Name , TypeID, SubTypeID,OpeningBalance,Balance,IsRead  from tbl_chartofaccount  
		 INNER JOIN tbl_company_sites ON (tbl_company_sites.CompanySiteID=tbl_chartofaccount.SystemSiteID)
		where   TypeID=' + convert(varchar(150),@typeid) + ' and  SubTypeID =' + convert(varchar(150),@subtypeid) + 'and ' + @SystemSiteID + ' and isActive <> 0 '

Exec(@String)

RETURN