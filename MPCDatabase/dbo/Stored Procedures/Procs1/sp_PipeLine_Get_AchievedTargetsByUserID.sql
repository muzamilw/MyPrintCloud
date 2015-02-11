CREATE PROCEDURE [dbo].[sp_PipeLine_Get_AchievedTargetsByUserID]
	@SystemUserID int,
	@StartDate datetime,
	@EndDate datetime
AS
	SELECT tbl_invoices.InvoiceID,
            tbl_invoices.InvoiceCode,
            tbl_invoices.InvoiceName,tbl_invoices.InvoicePostingDate,
            tbl_invoicedetails.ItemID, 
            tbl_items.ItemCode AS ItemCode,
            tbl_items.jobSelectedQty,tbl_contactcompanies.Name,
            (CASE WHEN tbl_items.jobselectedQty=1 then tbl_items.Qty1NetTotal WHEN tbl_items.jobselectedQty= 2 THEN tbl_items.Qty2NetTotal WHEN tbl_items.jobselectedQty=3 THEN tbl_items.Qty3NetTotal ELSE 0 END ) as ItemTotal 
            FROM 
            tbl_invoicedetails 
            INNER JOIN tbl_invoices ON (tbl_invoicedetails.InvoiceID = tbl_invoices.InvoiceID) 
            LEFT OUTER JOIN tbl_items ON (tbl_invoicedetails.ItemID = tbl_items.ItemID) 
            INNER JOIN tbl_contactcompanies ON (tbl_contactcompanies.ContactCompanyID = tbl_invoices.ContactCompanyID) 
            WHERE 
            tbl_invoices.CreatedBy = @SystemUserID AND 
            tbl_invoices.InvoiceStatus = '1' AND 
            tbl_invoices.InvoicePostingDate between @StartDate and @EndDate
	RETURN