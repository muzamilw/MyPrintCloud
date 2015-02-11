
CREATE PROCEDURE [dbo].[sp_users_delete_user]
(@SystemUserID int)
AS
	
	Declare @Result bit
	Declare @PreResult bit
	set @Result = 0
	Select @PreResult = CompletedBy from tbl_activity where CompletedBy=@SystemUserID
	
	if @PreResult > 0 
		Set @Result = 1
	print @Result
	--Select @PreResult = CreatedBy from tbl_costcentres where CreatedBy=@SystemUserID
	--print '2'
	--if @PreResult > 0 
	--	Set @Result = 1
	
	
	Select @PreResult = RaisedBy from tbl_deliverynotes where RaisedBy=@SystemUserID
	
	if @PreResult > 0 
		Set @Result = 1
	print @Result
	Select @PreResult = SalesPersonID from tbl_enquiries where SalesPersonID=@SystemUserID
	
	if @PreResult > 0 
		Set @Result = 1
	print @Result
	Select @PreResult = SalesPersonID from tbl_estimates where SalesPersonID=@SystemUserID
	

	
	if @PreResult > 0 
		Set @Result = 1
	print @Result
	Select @PreResult = CreatedBy from tbl_goodsreceivednote where CreatedBy=@SystemUserID
	
	if @PreResult > 0 
		Set @Result = 1
	
	Select @PreResult = CreatedBy from tbl_invoices where CreatedBy=@SystemUserID
	
	if @PreResult > 0 
		Set @Result = 1
	print @Result
	Select @PreResult = CreatedBy from tbl_items where CreatedBy=@SystemUserID
	
	if @PreResult > 0 
		Set @Result = 1
	print @Result
	Select @PreResult = UserID from tbl_purchase where UserID=@SystemUserID
	
	if @PreResult > 0 
		Set @Result = 1
	print @Result
	Select @PreResult = CreatedBy from tbl_tasks where CreatedBy=@SystemUserID
	
	if @PreResult > 0 
		Set @Result = 1
	print @Result
	Select @PreResult = CreatedBy from tbl_tasks where CreatedBy=@SystemUserID
	
	if @PreResult > 0 
		Set @Result = 1
	print @Result
	Select @PreResult = SystemUserID from tbl_userpipeline where SystemUserID=@SystemUserID
	
	if @PreResult > 0 
		Set @Result = 1
	print @Result
	Select @PreResult = SystemUserID from tbl_userreports where SystemUserID=@SystemUserID
	
	if @PreResult > 0 
		Set @Result = 1
	print @Result
	Select @PreResult = SystemUserID from tbl_usertarget where SystemUserID=@SystemUserID
	
	if @PreResult > 0 
		Set @Result = 1
	print @Result
	Select @PreResult = UserID from tbl_voucher where UserID=@SystemUserID
	
	if @PreResult > 0 
		Set @Result = 1
	
	
	print @Result
	
	
	if @Result = 0
		BEGIN
			delete from tbl_systemusers where (SystemUserID=@SystemUserID)
			delete from tbl_systemuser_preferences where (SystemUserID=@SystemUserID)
			delete from tbl_report_notes where UserID=@SystemUserID
			Select 1
		END
	else
		Select 0	
		
	RETURN