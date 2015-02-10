CREATE PROCEDURE dbo.sp_journalLadger_AddVoucher 
(	 @VoucherDate DateTime,
	 @Description varchar(250),  
	 @Reference varchar(50), 
	 @VoucherType varchar(5), 
	 @InvoiceType varchar(5), 
	 @TotalAmount float, 
	 @CSType varchar(5), 
	 @CSID int, 
	 @UserID int, 
	 @RIPID int, 
	 @RIPType varchar(5), 
	 @ITEMID int, 
	 @PaymentMethod varchar(15),  
     @Balance float, 
     @Reconciled varchar(1), 
     @reconciledDate datetime,
     @SystemSiteID int)
AS
	INSERT INTO tbl_voucher
                      (VoucherDate, Description, Reference, VoucherType, InvoiceType, TotalAmount, CSType, CSCode, UserID, RIPID, RIPType, ITEMID, PaymentMethod, 
                      Balance, Reconciled, reconciledDate,SystemSiteID)
VALUES     (@VoucherDate, @Description, @Reference, @VoucherType, @InvoiceType, @TotalAmount, @CSType, @CSID, @UserID, @RIPID, @RIPType, @ITEMID, @PaymentMethod, 
                      @Balance, @Reconciled, @reconciledDate,@SystemSiteID);
                     Select @@Identity 

RETURN