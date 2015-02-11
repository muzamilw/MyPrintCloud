
Create PROCEDURE [dbo].[sp_Invoice_Insert_Invoice]

	(
	@AccountNumber varchar(50),
	@AddressID int,
	@ContactCompany varchar(50),
	@ContactID int,
	@CreationDate datetime,
	@CreatedBy int,
	@CustomerID int,
	@InvoiceCode varchar(100),
	@InvoiceDate datetime,
	@InvoiceName varchar(255),
	@InvoiceTotal float,
	@IsArchive bit,
	@InvoiceType int,
	@LastUpdatedBy int,
	@OrderNo varchar(50),
	@Terms text,
	@TaxValue float,
	@InvoiceStatus int,
	@InvoicePostingDate datetime,
	@InvoicePostedBy int,
	@GrandTotal float,
	@FlagID int,
	@SystemSiteID int,
	@EstimateID int
)
AS
	/* SET NOCOUNT ON */
	
	INSERT into tbl_invoices (InvoiceCode,InvoiceType,InvoiceName,ContactCompanyID,ContactID,ContactCompany,OrderNo,InvoiceStatus,InvoiceTotal,InvoiceDate,LastUpdatedBy,CreationDate, 
CreatedBy,AccountNumber,Terms,InvoicePostingDate,InvoicePostedBy, 
 AddressID,IsArchive,TaxValue,GrandTotal,FlagID,SystemSiteID,EstimateID) VALUES
(@InvoiceCode,@InvoiceType,@InvoiceName,@CustomerID,@ContactID,@ContactCompany,@OrderNo,@InvoiceStatus,@InvoiceTotal,@InvoiceDate,@LastUpdatedBy,@CreationDate,
 @CreatedBy,@AccountNumber,@Terms,@InvoicePostingDate,@InvoicePostedBy,
 @AddressID,@IsArchive,@TaxValue,@GrandTotal,@FlagID,@SystemSiteID,@EstimateID); select @@Identity as IID
	
	RETURN