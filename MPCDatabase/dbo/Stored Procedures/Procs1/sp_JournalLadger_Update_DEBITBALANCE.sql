CREATE PROCEDURE dbo.sp_JournalLadger_Update_DEBITBALANCE
		
	@Amount float ,
	@AccountNo int,
	@SystemSiteID int

AS


update tbl_chartofaccount  set Balance =  case nature 
  						when 1 then balance+@Amount
		   	                  else 
						balance-@Amount
	                 		  end  
where AccountNo = @AccountNo and SystemSiteID=@SystemSiteID

RETURN