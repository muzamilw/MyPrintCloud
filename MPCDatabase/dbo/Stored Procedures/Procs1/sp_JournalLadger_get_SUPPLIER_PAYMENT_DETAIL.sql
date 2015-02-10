CREATE PROCEDURE dbo.sp_JournalLadger_get_SUPPLIER_PAYMENT_DETAIL

	@SupplierID int

AS

	SELECT VoucherID, InvoiceType,VoucherDate,Reference, case Description
								when '' then 'Purchases'
							        else 
									Description
	  						      	end  as Description ,
									TotalAmount, Balance, 0.0 as Payment, 0.0 As Discount, RIPID, RIPType,  CSCode, CSType 
									FROM tbl_Voucher WHERE Balance >0.099  AND InvoiceType in ('PI', 'PC','PA') 
									and CSCODE = @SupplierID AND CSType='S' 
									order by Invoicetype , VoucherID,VoucherDate


RETURN