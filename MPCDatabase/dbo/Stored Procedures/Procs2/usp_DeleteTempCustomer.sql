CREATE PROCEDURE [dbo].[usp_DeleteTempCustomer]
		
		@CustomerID	numeric,
		@EstimateCustomerID	numeric,
		@ContactID	numeric
		

AS
BEGIN
		--delete contact
		delete from tbl_contacts 
		where ContactCompanyID = @CustomerID
		--delete address
		delete from tbl_addresses 
		where ContactCompanyID = @CustomerID
		
		--delete contact company
		delete from tbl_contactcompanies 
		where ContactCompanyID = @CustomerID
		
		--update estimates
		update tbl_estimates
		set ContactCompanyID = @EstimateCustomerID,
			ContactID = @ContactID
		where ContactCompanyID = @CustomerID
		
END


select * from tbl_estimates