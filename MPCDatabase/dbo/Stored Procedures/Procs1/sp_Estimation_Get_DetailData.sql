
CREATE PROCEDURE sp_Estimation_Get_DetailData
	@EstimateID int
AS
BEGIN
	exec sp_Estimation_Get_EstimateByID @EstimateID

Declare @customerID int 
Set @customerID = (select ContactCompanyID from tbl_estimates where estimateid = @EstimateID)
exec sp_Customers_Contacts_GetCustomerContactsWithBusinessAddress @CustomerID

exec sp_Estimation_Get_EstimateShortByID @EstimateID

exec sp_Estimation_Get_ChildEstimatesShortByParentEstimateID @EstimateID

Declare @EnquiryID int
Set @EnquiryID = (select EnquiryID from tbl_estimates where estimateid = @EstimateID)
exec sp_Enquiries_Get_EnquiryByID @EnquiryID

exec sp_Enquiries_Get_EnquiryOptionsByEnquiryID @EnquiryID	

exec sp_Estimation_Get_EstimateItemsShort @EstimateID
END