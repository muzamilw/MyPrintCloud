CREATE PROCEDURE dbo.sp_JournalLadger_get_CUSTOMER_PAYMENT_DETAIL 

	@CustomerID int

AS

SELECT VoucherID, InvoiceType,VoucherDate,Reference, case Description
								when '' then 'Sales'
							        else 
									Description
	  						      	end  as Description ,
									TotalAmount, Balance, 0.0 as Payment, 0.0 As Discount, RIPID, RIPType,  CSCode, CSType 
									FROM tbl_Voucher WHERE (Balance >0.099  AND InvoiceType in ('SI', 'SC','SA')) and (CSCODE = @CustomerID 
									AND CSType='C' ) order by Invoicetype , VoucherID,VoucherDate

RETURN