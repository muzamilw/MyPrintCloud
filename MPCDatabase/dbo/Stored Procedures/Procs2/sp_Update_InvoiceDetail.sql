
Create PROCEDURE [dbo].[sp_Update_InvoiceDetail]
		@InvoiceID int , @DetailType int , @ItemID int , @InvoiceTitle varchar(50), @NominalCode int , @ItemCharge float, @Quantity float, @ItemTaxValue float, @FlagID int, @DepartmentID int, @Description varchar, @ItemType int, @TaxID int,@InvoiceDetailID int

AS
  begin
	
	if @ItemID = 0 
	 begin
	     UPDATE    tbl_invoicedetails SET InvoiceID =@InvoiceID, DetailType =@DetailType, ItemID = null, InvoiceTitle =@InvoiceTitle, NominalCode =@NominalCode, ItemCharge =@ItemCharge, Quantity =@Quantity, ItemTaxValue =@ItemTaxValue, FlagID =@FlagID, DepartmentID =@DepartmentID, Description =@Description, ItemType =@ItemType, TaxID =@TaxID  where InvoiceDetailID=@InvoiceDetailID
		-- INSERT INTO tbl_invoicedetails (InvoiceID, DetailType, ItemID, InvoiceTitle, NominalCode, ItemCharge, Quantity, ItemTaxValue, FlagID, DepartmentID, Description, ItemType, TaxID) VALUES  (@InvoiceID, @DetailType, null, @InvoiceTitle, @NominalCode, @ItemCharge, @Quantity, @ItemTaxValue, @FlagID, @DepartmentID, @Description, @ItemType, @TaxID)
	 end
	else
	 begin
		UPDATE    tbl_invoicedetails SET InvoiceID =@InvoiceID, DetailType =@DetailType, ItemID =@ItemID, InvoiceTitle =@InvoiceTitle, NominalCode =@NominalCode, ItemCharge =@ItemCharge, Quantity =@Quantity, ItemTaxValue =@ItemTaxValue, FlagID =@FlagID, DepartmentID =@DepartmentID, Description =@Description, ItemType =@ItemType, TaxID =@TaxID  where InvoiceDetailID=@InvoiceDetailID
		 -- INSERT INTO tbl_invoicedetails (InvoiceID, DetailType, ItemID, InvoiceTitle, NominalCode, ItemCharge, Quantity, ItemTaxValue, FlagID, DepartmentID, Description, ItemType, TaxID) VALUES  (@InvoiceID, @DetailType, @ItemID, @InvoiceTitle, @NominalCode, @ItemCharge, @Quantity, @ItemTaxValue, @FlagID, @DepartmentID, @Description, @ItemType, @TaxID)
	 end
	  
	
	
	RETURN
	
end