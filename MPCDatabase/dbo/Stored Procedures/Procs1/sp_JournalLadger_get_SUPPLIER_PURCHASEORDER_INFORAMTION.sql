CREATE PROCEDURE [dbo].[sp_JournalLadger_get_SUPPLIER_PURCHASEORDER_INFORAMTION]

	@GRNID  int
AS

SELECT     tbl_ContactCompanies.AccountNumber, tbl_ContactCompanies.ContactCompanyID, tbl_ContactCompanies.Name, tbl_ContactCompanies.DefaultNominalCode, 
tbl_goodsreceivednote.GoodsReceivedID, tbl_goodsreceivednote.date_Received, tbl_goodsreceivednote.TotalPrice, 
tbl_goodsreceivednote.Comments, tbl_goodsreceivednote.TotalTax, tbl_goodsreceivednote.Discount, tbl_goodsreceivednote.discountType, 
tbl_goodsreceivednote.grandTotal
FROM         tbl_ContactCompanies 
INNER JOIN   tbl_goodsreceivednote ON tbl_ContactCompanies.ContactCompanyID = tbl_goodsreceivednote.SupplierID
where  tbl_goodsreceivednote.GoodsReceivedID =@GRNID 

RETURN