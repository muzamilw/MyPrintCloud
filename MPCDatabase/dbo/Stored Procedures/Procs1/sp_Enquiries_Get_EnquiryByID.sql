CREATE PROCEDURE [dbo].[sp_Enquiries_Get_EnquiryByID]
	@EnquiryID int
AS
SELECT tbl_enquiries.EnquiryID,tbl_enquiries.EnquiryTitle,tbl_enquiries.EnquiryCode,
		tbl_enquiries.ReceivedDate,tbl_enquiries.ContactCompanyID,tbl_enquiries.ContactID,tbl_enquiries.CompanyName,tbl_enquiries.RequiredByDate,
		tbl_enquiries.Status,tbl_enquiries.DeliveryByDate,tbl_enquiries.Origination,tbl_enquiries.PreviousQuoteNO,
		tbl_enquiries.PreviousOrderNO,tbl_enquiries.QuoteDestinition,tbl_enquiries.SendUsing,tbl_enquiries.SalesPersonID,
		tbl_enquiries.ProcessID,tbl_enquiries.ArtworkOriginationTypeID,tbl_enquiries.ProofRequiredID,tbl_enquiries.ProductTypeID,
		tbl_enquiries.ProductCode,tbl_enquiries.FrequencyID,tbl_enquiries.DataFormat,tbl_enquiries.DataAvailable,
		tbl_enquiries.PrintingNotes,tbl_enquiries.EnquiryNotes,tbl_enquiries.ItemNotes,tbl_enquiries.CoverSide1Colors,tbl_enquiries.CoverSide2Colors,
		tbl_enquiries.CoverInkCoveragePercentage,tbl_enquiries.CoverSpecialColorsID,tbl_enquiries.TextSide1Colors,
		tbl_enquiries.TextSide2Colors,tbl_enquiries.TextInkCoveragePercentage,tbl_enquiries.TextSpecialColorsID,
		tbl_enquiries.OtherSide1Colors,tbl_enquiries.OtherSide2Colors,tbl_enquiries.OtherInkCoveragePercentage,
		tbl_enquiries.OtherSpecialColorsID,tbl_enquiries.ISCoverPaperSupplied,tbl_enquiries.ISTextPaperSupplied,
		tbl_enquiries.ISOtherPaperSupplied,tbl_enquiries.CoverPaperTypeID,tbl_enquiries.TextPaperTypeID,
		tbl_enquiries.OtherPaperTypeID,tbl_enquiries.PaperNotes,tbl_enquiries.Quantity1,tbl_enquiries.Quantity2,
		tbl_enquiries.Quantity3,tbl_enquiries.FinishingStyleID,tbl_enquiries.CoverStyleID,tbl_enquiries.CoverFinishingID,
		tbl_enquiries.PackingStyleID,tbl_enquiries.InsertStyleID,tbl_enquiries.NoOfInserts,tbl_enquiries.FinishingNotes,
		tbl_enquiries.DeliveryAddressID,tbl_enquiries.BillingAddressID,tbl_enquiries.SystemSiteID,tbl_enquiries.FlagID,tbl_contactcompanies.Name,
		tbl_contacts.AddressID,
		tbl_enquiries.PaperSizeID,tbl_enquiries.CustomeSize,tbl_enquiries.PaperWeight,tbl_enquiries.CoverSpecialColorSide2ID,tbl_enquiries.TextSpecialColorSide2ID,tbl_enquiries.OtherSpecialColorSide2ID,tbl_enquiries.NCRSet

		FROM tbl_enquiries 
		LEFT OUTER JOIN tbl_contactcompanies ON (tbl_enquiries.contactcompanyid = tbl_contactcompanies.contactcompanyid) 
		LEFT OUTER JOIN tbl_contacts ON (tbl_enquiries.ContactID = tbl_contacts.contactid) 
		WHERE tbl_enquiries.EnquiryID = @EnquiryID
	RETURN