CREATE PROCEDURE [dbo].[sp_Invoice_Get_InvoiceByID]

	(
		@InvoiceID int
	)

AS
	/* SET NOCOUNT ON */
	
	SELECT tbl_invoices.InvoiceID,tbl_invoices.EstimateID, tbl_invoices.InvoiceCode,tbl_invoices.InvoiceType,tbl_invoices.InvoiceName,tbl_invoices.ContactcompanyId,tbl_invoices.ContactID,tbl_invoices.ContactCompany,
tbl_invoices.OrderNo,tbl_invoices.InvoiceStatus,tbl_invoices.InvoiceTotal,tbl_invoices.InvoiceDate,tbl_invoices.LastUpdatedBy,tbl_invoices.CreationDate,
tbl_invoices.CreatedBy,tbl_invoices.AccountNumber,tbl_invoices.Terms,tbl_invoices.InvoicePostingDate,tbl_invoices.InvoicePostedBy,tbl_invoices.IsArchive,tbl_invoices.TaxValue,
tbl_invoices.GrandTotal,tbl_invoices.FlagID,tbl_invoices.IsProformaInvoice,tbl_ContactCompanies.Name,tbl_Contacts.FirstName,tbl_Contacts.MiddleName,tbl_Contacts.LastName,
tbl_Addresses.AddressName,tbl_Addresses.Address1,tbl_Addresses.Address2,tbl_Addresses.Address3,tbl_Addresses.City,
tbl_Addresses.PostCode,tbl_Addresses.Fax,tbl_Addresses.Email,
tbl_Addresses.URL,tbl_Addresses.Tel1,tbl_Addresses.Tel2,tbl_ContactCompanies.URL,tbl_addresses.State,
tbl_addresses.Country,tbl_invoices.AddressID FROM tbl_invoices
INNER JOIN tbl_Addresses ON (tbl_invoices.AddressID = tbl_Addresses.AddressID)
INNER JOIN tbl_ContactCompanies ON (tbl_invoices.ContactcompanyId = tbl_ContactCompanies.ContactcompanyId)
INNER JOIN tbl_Contacts ON (tbl_invoices.ContactID = tbl_Contacts.ContactID) where InvoiceID=@InvoiceID
	
	RETURN