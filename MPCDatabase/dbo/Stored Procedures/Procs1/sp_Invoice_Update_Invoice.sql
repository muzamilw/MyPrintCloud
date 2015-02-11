
Create PROCEDURE [dbo].[sp_Invoice_Update_Invoice]

	(
	@InvoiceID int,
	@InvoiceName varchar(255),
	@CustomerID int,
	@ContactID int,
	@ContactCompany varchar(50),
	@OrderNo varchar(50),
	@InvoiceStatus int,
	@InvoiceTotal float,
	@InvoiceDate datetime,
	@LastUpdatedBy int,
 @Terms text,
 @InvoicePostingDate datetime,
 @InvoicePostedBy int,
 @AddressID int,
 @TaxValue float,
 @GrandTotal float,
 @FlagID int
 	)

AS
	/* SET NOCOUNT ON */
	
	UPDATE tbl_invoices set tbl_invoices.InvoiceName=@InvoiceName,tbl_invoices.ContactCompanyID=@CustomerID,
	tbl_invoices.ContactID=@ContactID,tbl_invoices.ContactCompany=@ContactCompany,OrderNo=@OrderNo,
	tbl_invoices.InvoiceStatus=@InvoiceStatus,tbl_invoices.InvoiceTotal=@InvoiceTotal,
       tbl_invoices.LastUpdatedBy=@LastUpdatedBy,tbl_invoices.Terms=@Terms,
       tbl_invoices.InvoicePostedBy=@InvoicePostedBy,tbl_invoices.AddressID=@AddressID,
       tbl_invoices.GrandTotal=@GrandTotal,InvoiceDate=@InvoiceDate,
       tbl_invoices.TaxValue=@TaxValue,InvoicePostingDate=@InvoicePostingDate,
       FlagID=@FlagID where tbl_invoices.InvoiceID=@InvoiceID
	
	RETURN