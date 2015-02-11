
CREATE PROCEDURE sp_Read_Invoices_By_OrderID
	@EstimateID int
AS

	select tbl_invoices.InvoiceID,tbl_invoices.InvoiceCode,tbl_invoices.InvoiceType,tbl_invoices.InvoiceName,tbl_invoices.ContactCompanyID,tbl_invoices.ContactID, 
	tbl_invoices.ContactCompany,tbl_invoices.OrderNo,tbl_invoices.InvoiceStatus,tbl_invoices.InvoiceTotal,tbl_invoices.InvoiceDate,tbl_invoices.LastUpdatedBy, 
	tbl_invoices.CreationDate,tbl_invoices.CreatedBy,tbl_invoices.AccountNumber,tbl_invoices.Terms,tbl_invoices.InvoicePostingDate,tbl_invoices.InvoicePostedBy, 
	tbl_invoices.LockedBy,tbl_invoices.AddressID,tbl_invoices.IsArchive,tbl_invoices.TaxValue,tbl_invoices.GrandTotal,tbl_invoices.FlagID,tbl_invoices.UserNotes, 
	tbl_invoices.NotesUpdateDateTime,tbl_invoices.NotesUpdatedByUserID,tbl_invoices.SystemSiteID, 
	tbl_invoices.EstimateID,tbl_invoices.IsRead,tbl_invoices.IsProformaInvoice,tbl_invoices.IsPrinted,tbl_invoices.LastUpdateDate, 
	tbl_invoices.ReportSignedBy,tbl_invoices.ReportLastPrintedDate,tbl_invoices.HeadNotes,tbl_invoices.FootNotes 
	from tbl_invoices
	Where tbl_invoices.EstimateID = @EstimateID --and tbl_invoices.InvoiceStatus = 19
Return