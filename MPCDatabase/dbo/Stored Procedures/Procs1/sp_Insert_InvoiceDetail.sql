
CREATE PROCEDURE [dbo].[sp_Insert_InvoiceDetail]
		@InvoiceID int , @DetailType int , @ItemID int , @InvoiceTitle varchar(50), @NominalCode int , @ItemCharge float, @Quantity float, @ItemTaxValue float, @FlagID int, @DepartmentID int, @Description varchar, @ItemType int, @TaxID int

		--@ActivityTypeID int,@ActivityCode varchar(50) ,@ActivityRef varchar(500),@ActivityStartTime datetime,
  --      @ActivityEndTime datetime ,@ActivityProbability int,@ActivityPrice int,@ActivityUnit int,@ActivityNotes ntext,@IsActivityAlarm int,@AlarmDate datetime,@AlarmTime datetime, 
  --      @ActivityLink int,@IsCustomerActivity int,@CustomerContactID int,@SupplierContactID int,@ProspectContactID int,@SystemUserID int,@IsPrivate int,@IsFollowedUp int,@FollowedActivityID int,@LastModifiedDate datetime,@LastModifiedTime datetime,@LastModifiedBy int,@IsComplete int,@CompletionDate datetime ,@CompletionTime datetime ,@CompletionSuccess int,@CompletionResult varchar(100) ,@CompletedBy int ,@CreatedBy int
		--,@SystemSiteID int,@ContactCompanyID int
AS
  begin
	
	if @ItemID = 0 
	 begin

		 INSERT INTO tbl_invoicedetails (InvoiceID, DetailType, ItemID, InvoiceTitle, NominalCode, ItemCharge, Quantity, ItemTaxValue, FlagID, DepartmentID, Description, ItemType, TaxID) VALUES  (@InvoiceID, @DetailType, null, @InvoiceTitle, @NominalCode, @ItemCharge, @Quantity, @ItemTaxValue, @FlagID, @DepartmentID, @Description, @ItemType, @TaxID)
	 end
	else
	 begin
	 
		 INSERT INTO tbl_invoicedetails (InvoiceID, DetailType, ItemID, InvoiceTitle, NominalCode, ItemCharge, Quantity, ItemTaxValue, FlagID, DepartmentID, Description, ItemType, TaxID) VALUES  (@InvoiceID, @DetailType, @ItemID, @InvoiceTitle, @NominalCode, @ItemCharge, @Quantity, @ItemTaxValue, @FlagID, @DepartmentID, @Description, @ItemType, @TaxID)
	 end
	   
	--INSERT INTO tbl_invoicedetails (InvoiceID, DetailType, ItemID, InvoiceTitle, NominalCode, ItemCharge, Quantity, ItemTaxValue, FlagID, DepartmentID, Description, ItemType, TaxID) VALUES  (@InvoiceID, @DetailType, @ItemID, @InvoiceTitle, @NominalCode, @ItemCharge, @Quantity, @ItemTaxValue, @FlagID, @DepartmentID, @Description, @ItemType, @TaxID)
	
	
	RETURN
	
end
	
insert into tbl_invoicedetails (InvoiceID,DetailType,ItemID,NominalCode,ItemCharge,Quantity,ItemTaxValue,FlagID)
Values (1,1,null,1,2,4,5,6)