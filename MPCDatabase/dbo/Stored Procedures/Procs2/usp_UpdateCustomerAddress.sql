--exec [usp_GetUserInfo] 3
CREATE PROCEDURE [dbo].[usp_UpdateCustomerAddress]
		@CustomerID				numeric, 
		@AddressLine1	varchar(255), 
		@AddressLine2	varchar(255),
		@AddressLine3	varchar(255),
		@City			varchar(50), 
		@Zip			varchar(50), 
		@Country		int,		
		@Phone			varchar(50),
		@AddressType	varchar(50),
		@AddressID		int = 0

AS
BEGIN
		if(Exists(Select AddressID from tbl_addresses 
					where AddressID = @AddressID))
			Begin
			--Select top 1 @AddressID = AddressID from tbl_addresses 
			--		where ContactCompanyID = @CustomerID
					
			update tbl_addresses
			Set		Address1 = @AddressLine1,
					Address2 = @AddressLine2,
					Address3 = @AddressLine3,
					City	 = @City,
					PostCode = @Zip,
					CountryID = @Country,
					Tel1	 = @Phone,
					AddressName = @AddressType
			where	ContactCompanyID = @CustomerID
			AND		AddressID = @AddressID
			
		End
		Else
			Begin
				insert into tbl_addresses
					(ContactCompanyID, Address1, Address2, Address3,AddressName,
					 City, PostCode,CountryID, IsDefaultAddress)
				values(@CustomerID, @AddressLine1, @AddressLine2, @AddressLine3,
					 @AddressType, @City, @Zip, @Country, 1)
			End	
		
		
END