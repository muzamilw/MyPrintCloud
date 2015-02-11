CREATE PROCEDURE dbo.sp_Estimation_Get_EstimateFullWithOutItems
/*
	(
		Procudure written for Estimate Default layout report if no item exist in the estimate so take the estimate 
		information with null item detail
	)
*/
		@EstimateID int
AS
	SELECT tbl_estimates.Estimate_Code,
       tbl_estimates.Estimate_Name,tbl_estimates.Customer_ID,tbl_estimates.Contact_ID,tbl_estimates.Estimate_ID,tbl_estimates.OrderNo,tbl_estimates.EstimateDate,
       NULL as ItemID,NULL as ItemCode,tbl_estimates.Estimate_ID as EstimateID,tbl_customers.CustomerName,tbl_customercontacts.FirstName AS ContactFirstName,
       tbl_customercontacts.MiddleName AS ContactMiddleName,tbl_customercontacts.LastName AS ContactLastName,
       tbl_systemusers.FullName AS SalesPersonFullName,tbl_systemusers.UserName AS SalesPersonUserName,
       tbl_systemusers.Description AS SalesPersonDescription,tbl_estimates.Estimate_Total,tbl_estimates.Estimate_ValidUpto,
       tbl_estimates.UserNotes,tbl_estimates.HeadNotes,tbl_estimates.FootNotes,tbl_estimates.EstimateDate,
       tbl_estimates.Greeting,tbl_estimates.AccountNumber,tbl_estimates.OrderNo,
       tbl_estimates.Order_Code as OrderCode, tbl_estimates.Order_Date as OrderDate,tbl_estimates.Order_CreationDateTime as OrderCreationDate,tbl_estimates.Order_DeliveryDate as DeliveryDate,tbl_estimates.Order_ConfirmationDate as OrderConfirmationDate,tbl_estimates.Order_Status as OrderStatus,tbl_estimates.Order_CompletionDate as OrderCompletionDate,
       NULL as Title,NULL as Qty1,
       NULL as Qty2,NULL as Qty3,NULL as Qty1Title,NULL as Qty2Title,
       NULL as Qty3Title,NULL as Qty1NetTotal,NULL as Qty2NetTotal,NULL as Qty3NetTotal,
       NULL as EstimateDescriptionTitle1,NULL as EstimateDescriptionTitle2,
       NULL as EstimateDescriptionTitle3,NULL as EstimateDescriptionTitle4,NULL as EstimateDescriptionTitle5,
       NULL as EstimateDescriptionTitle6,NULL as EstimateDescriptionTitle7,NULL as EstimateDescriptionTitle8,
       NULL as EstimateDescription1,
       NULL as EstimateDescription2,NULL as EstimateDescription3,NULL as EstimateDescription4,
       NULL as EstimateDescription5,NULL as EstimateDescription6,NULL as EstimateDescription7,
       NULL as EstimateDescription8,
       NULL as EstimateDescription,NULL as IsParagraphDescription,tbl_customers.Terms AS CustomerTerms,
       tbl_customers.ISBN AS ISDN,tbl_customers.AccountNumber,tbl_customercontacts.Department AS ContactDepartment,
       tbl_customercontacts.DOB AS ContactDOB,tbl_customercontacts.Notes AS ContactNotes,
       tbl_customeraddresses.Address1 as ContactAddress1,tbl_customeraddresses.Address2 as ContactAddress2,tbl_customeraddresses.Address3 as ContactAddress3,tbl_customeraddresses.PostCode as ContactPostCode,
       tbl_state.StateName as ContactStateName,tbl_country.CountryName as ContactCountryName,tbl_customeraddresses.City as ContactCity,
       NULL as RunOnNetTotal,NULL as Qty1MarkUp1Value,NULL as Qty2MarkUp2Value,NULL as Qty3MarkUp3Value,
       NULL as Qty1Tax1Value,NULL as Qty1Tax2Value,
       NULL as Qty1Tax3Value,NULL as Qty1GrossTotal,NULL as Qty2Tax1Value,NULL as Qty2Tax2Value,
       NULL as Qty2Tax3Value,NULL as Qty2grossTotal,NULL as Qty3Tax1Value,NULL as Qty3Tax2Value,
       NULL as Qty3Tax3Value,NULL as Qty3GrossTotal,0 as jobSelectedQty,
       
       NULL as ArtworkByDate, NULL as DataByDate, NULL as TargetPrintDate, NULL as StartDeliveryDate, NULL as PaperByDate, NULL as TargetBindDate, NULL as FinishDeliveryDate, NULL as Classification1ID, 
       NULL as Classification2ID, NULL as IsOfficialOrder, NULL as CustomerPO, NULL as OfficialOrderSetBy, NULL as OfficialOrderSetOnDateTime, NULL as IsCreditApproved, NULL as CreditLimitForJob, NULL as CreditLimitSetBy, 
       NULL as CreditLimitSetOnDateTime, NULL as IsJobAllowedWOCreditCheck, NULL as AllowJobWOCreditCheckSetBy, NULL as AllowJobWOCreditCheckSetOnDateTime
        
       FROM tbl_estimates 
    INNER JOIN tbl_customercontacts ON (tbl_estimates.Contact_ID = tbl_customercontacts.CustomerContactID) 
    INNER JOIN tbl_customers ON (tbl_estimates.Customer_ID = tbl_customers.CustomerID) 
    INNER JOIN tbl_customeraddresses ON (tbl_customercontacts.AddressID = tbl_customeraddresses.AddressID) 
    INNER JOIN tbl_state on(tbl_customeraddresses.StateID =tbl_state.StateID) 
    INNER JOIN tbl_country on(tbl_customeraddresses.CountryID =tbl_country.CountryID) 
    INNER JOIN tbl_systemusers ON (tbl_estimates.SalesPerson = tbl_systemusers.SystemUserID)
    WHERE (tbl_estimates.Estimate_ID=@EstimateID)
	RETURN