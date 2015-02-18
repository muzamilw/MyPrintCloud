--exec [usp_GetCustomerInfo] 318672
CREATE PROCEDURE [dbo].[usp_GetCustomerInfo]
	@CustomerID	numeric
AS
BEGIN
		select	cc.ContactCompanyID, Name, cc.URL, ct.TypeName, Status, IsCustomer, 
				IsGeneral, IsEmailSubscription, IsMailSubscription			
		from	tbl_contactcompanies cc
				inner join tbl_contactcompanytypes ct on ct.TypeID = cc.TypeID
		where	cc.ContactCompanyID = @CustomerID
		
		select AddressID, ad.AddressName, Address1, Address2, Address3, City, st.StateName,c.CountryID, c.CountryName, 
				PostCode, Fax, Email, ad.URL, Tel1, Tel2, Extension1, Extension2, 
				Reference, FAO, IsDefaultAddress as BillingAddress, IsDefaultShippingAddress As DeliveryAddress
		from	tbl_addresses ad 
				 left join tbl_state st on st.StateID = ad.StateID
				 left join tbl_country c on c.CountryID = ad.CountryID
		where	ad.ContactCompanyID = @CustomerID
END