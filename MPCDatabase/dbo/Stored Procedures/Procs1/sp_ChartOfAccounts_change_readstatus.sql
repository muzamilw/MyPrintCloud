CREATE PROCEDURE dbo.sp_ChartOfAccounts_change_readstatus

	(
		@AccountNo int,@SystemSiteID int
		
	)
AS

Declare @IsRead bit
select @IsRead=IsRead from tbl_chartofaccount where AccountNo=@AccountNo and SystemSiteID=@SystemSiteID

if (@IsRead<>0)
	begin
	update tbl_chartofaccount set IsRead=0 where AccountNo=@AccountNo and SystemSiteID=@SystemSiteID
	select 0
	end 

else	
	begin
	update tbl_chartofaccount set IsRead=1 where AccountNo=@AccountNo and SystemSiteID=@SystemSiteID
	select 1 
	end
RETURN