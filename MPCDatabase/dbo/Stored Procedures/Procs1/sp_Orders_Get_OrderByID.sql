CREATE PROCEDURE [dbo].[sp_Orders_Get_OrderByID]
(
	@EstimateID int
)
AS
	SELECT tbl_estimates.EstimateID,tbl_estimates.Order_Code,tbl_estimates.Estimate_Name,
        tbl_estimates.sectionflagid,tbl_estimates.ContactCompanyID,tbl_estimates.ContactID,tbl_estimates.Estimate_Total,tbl_estimates.AddressID,tbl_estimates.Greeting,tbl_estimates.AccountNumber,
        tbl_estimates.statusid,tbl_estimates.OrderNo,tbl_estimates.CompanyName,tbl_ContactCompanies.Name,tbl_estimates.HeadNotes,tbl_estimates.FootNotes,
        tbl_estimates.Order_Date,tbl_Addresses.Address1,tbl_Addresses.Address2,tbl_Addresses.Address3,
        tbl_Addresses.City,tbl_Addresses.PostCode,tbl_Addresses.Fax,tbl_Addresses.Tel1,tbl_Addresses.Country,
        tbl_Addresses.State,Isnull(tbl_estimates.LastUpdatedBy,0) as LastUpdatedBy,tbl_estimates.SalesPersonid,tbl_estimates.Order_CreationDateTime,Isnull(tbl_estimates.Order_DeliveryDate,'') as Order_DeliveryDate,
        tbl_estimates.Order_ConfirmationDate,tbl_estimates.Order_Status,tbl_estimates.Order_CompletionDate,tbl_estimates.EnquiryID,
        tbl_estimates.OrderManagerID,
        tbl_estimates.ArtworkByDate,
        tbl_estimates.DataByDate,
        tbl_estimates.TargetPrintDate,
        tbl_estimates.StartDeliveryDate,
        tbl_estimates.PaperByDate,
        tbl_estimates.TargetBindDate,
        tbl_estimates.FinishDeliveryDate,
        tbl_estimates.Classification1ID,
        tbl_estimates.Classification2ID,
        tbl_estimates.IsOfficialOrder,
        tbl_estimates.CustomerPO,
        tbl_estimates.OfficialOrderSetBy,
        tbl_estimates.OfficialOrderSetOnDateTime,
        tbl_estimates.IsCreditApproved,
        tbl_estimates.CreditLimitForJob,
        tbl_estimates.CreditLimitSetBy,
        tbl_estimates.CreditLimitSetOnDateTime,
        tbl_estimates.IsJobAllowedWOCreditCheck,
        tbl_estimates.AllowJobWOCreditCheckSetBy,
        tbl_estimates.AllowJobWOCreditCheckSetOnDateTime,tbl_estimates.EstimateValueChanged,tbl_estimates.NewItemAdded,tbl_estimates.EstimateSentTo, tbl_estimates.OrderSourceID
        ,tbl_estimates.isDirectSale
         FROM tbl_estimates 
        LEFT OUTER JOIN tbl_Addresses ON (tbl_estimates.AddressID = tbl_Addresses.AddressID) 
        
        LEFT OUTER  JOIN tbl_ContactCompanies ON (tbl_estimates.ContactCompanyID = tbl_ContactCompanies.ContactCompanyID) 
        
        where tbl_estimates.EstimateID  =@EstimateID
	RETURN