CREATE PROCEDURE dbo.sp_Orders_Get_PrePayments
(
	@EstimateID int
)
AS
	
	SELECT     Description, VoucherDate, TotalAmount
	FROM         tbl_voucher where RIPType = 'O' and RIPID=@EstimateID

	
	RETURN