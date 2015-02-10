CREATE PROCEDURE [dbo].[sp_JournalLadger_Get_VoucherByType]  
	
	@CSType varchar(5),
	@CsID int

	
AS

SELECT     InvoiceType, VoucherDate, Description, Reference, TotalAmount, Balance
FROM         tbl_voucher
WHERE     (CSType like @CSType) AND (CSCode = @CsID) order by VoucherID Desc

RETURN