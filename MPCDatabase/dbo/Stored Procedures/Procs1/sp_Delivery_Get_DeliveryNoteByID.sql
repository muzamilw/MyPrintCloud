CREATE PROCEDURE [dbo].[sp_Delivery_Get_DeliveryNoteByID]

	(
@DeliveryID int
	)

AS
	/* SET NOCOUNT ON */
	
	SELECT tbl_ContactCompanies.Name,tbl_Addresses.Address1,
        tbl_Addresses.Address2,tbl_Addresses.Address3,tbl_Addresses.City,tbl_Addresses.PostCode,
        tbl_Addresses.Tel1,tbl_Addresses.Fax,tbl_addresses.State,tbl_addresses.Country,tbl_deliverynotes.SupplierURL,tbl_deliverynotes.CsNo,
        tbl_deliverynotes.SupplierTelNo,tbl_deliverynotes.SupplierID,tbl_deliverynotes.RaisedBy,tbl_deliverynotes.CreationDateTime,tbl_deliverynotes.CreatedBy,
        tbl_deliverynotes.FlagID as SectionFlagID,tbl_deliverynotes.AddressID,tbl_deliverynotes.CustomerOrderReff,tbl_deliverynotes.ContactCompany,tbl_deliverynotes.ContactId,
        tbl_deliverynotes.IsStatus,tbl_deliverynotes.LockedBy,tbl_deliverynotes.Comments,tbl_deliverynotes.footnote,tbl_deliverynotes.OrderReff, 
        tbl_deliverynotes.ContactCompanyID,tbl_deliverynotes.DeliveryDate,tbl_deliverynotes.Code,tbl_deliverynotes.DeliveryNoteID,tbl_deliverynotes.EstimateID,tbl_deliverynotes.JobID,tbl_deliverynotes.InvoiceID, 
        tblSP.Name as SupplierName 
        FROM tbl_deliverynotes 
        INNER JOIN tbl_ContactCompanies ON (tbl_deliverynotes.ContactCompanyID = tbl_ContactCompanies.ContactCompanyID)
        INNER JOIN tbl_Addresses ON (tbl_deliverynotes.AddressID = tbl_Addresses.AddressID)
        Left Outer JOIN tbl_ContactCompanies tblSP ON (tbl_deliverynotes.ContactCompanyID = tblSP.ContactCompanyID)
        WHERE tbl_deliverynotes.DeliveryNoteID  = @DeliveryID
	
	RETURN