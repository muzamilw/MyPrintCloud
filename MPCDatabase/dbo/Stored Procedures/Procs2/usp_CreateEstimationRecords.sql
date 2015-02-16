--Exec [usp_CreateEstimationRecords]'Test Contact Company'
CREATE PROCEDURE [dbo].[usp_CreateEstimationRecords] 
		@CustomerName varchar(100)
		
AS
BEGIN
		declare @ContactCompanyID numeric
		declare @AddressID numeric
		declare @ContactID numeric
		
		insert into tbl_contactcompanies
			(Name, TypeID, Status, IsCustomer, IsEmailSubscription, IsMailSubscription, CreationDate, IsGeneral)
		select @CustomerName, TypeID, 1, 0, 1, 1,  GETDATE(), 1 from tbl_contactcompanytypes
		where TypeName = 'Prospect'
		
		select @ContactCompanyID = SCOPE_IDENTITY()
		
		insert into tbl_addresses
		(ContactCompanyID, CountryID, IsDefaultAddress)
		values(@ContactCompanyID, 213, 1)
		select @AddressID = SCOPE_IDENTITY()
		
		insert into tbl_contacts
		(AddressID, ContactCompanyID)
		values(@AddressID, @ContactCompanyID)
		select @ContactID = SCOPE_IDENTITY()
		
		select @ContactCompanyID As CompanyID, @ContactID As ContactID
END