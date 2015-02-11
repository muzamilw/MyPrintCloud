
--Exec [usp_GetContactAddressByID]157166
Create PROCEDURE [dbo].[usp_GetContactAddressByID]
	@AddressID	int
	
AS
BEGIN
				
		select AddressID, ContactCompanyID, AddressName, Address1, Address2, Address3, 
				City, StateID, CountryID, PostCode, Fax, Email, URL, Tel1, Tel2, 
				Extension1, Extension2, Reference, FAO, IsDefaultAddress, IsDefaultShippingAddress
		from	tbl_addresses 
		where	AddressID = @AddressID
		
		
END