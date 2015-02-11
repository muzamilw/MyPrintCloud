CREATE PROCEDURE dbo.sp_JournalLedger_Get_VoucherByType  
	
	@CSType varchar(5),
	@CsCode int

	
AS

SELECT     InvoiceType, VoucherDate, Description, Reference, TotalAmount, Balance
FROM         tbl_voucher
WHERE     (CSType like @CSType) AND (CSCode = @CsCode)

RETURN