CREATE PROCEDURE [dbo].[sp_Customer_Address_InsertCustomerAddress]

	(
		@CustomerID int,
		@AddressName varchar(100),
		@Address1  varchar(255),
		@Address2  varchar(255),
		@Address3  varchar(255),
		@City  varchar(30),
		@StateID varchar(100), 
		@CountryID varchar(100), 
		@Postcode  varchar(30),
        @Fax varchar(30),
        @Email  varchar(50),
        @URL  varchar(100), 
        @Tel1  varchar(30) ,
        @Tel2  varchar(30),
        @Extension1  varchar(7),
        @Extension2  varchar(7),
        @Reference  varchar(50), 
        @FAO  varchar(50),
        @IsDefaultAddress smallint,
        @IsDefaultShippingAddress smallint
	)


AS

	INSERT INTO tbl_Addresses (ContactCompanyID,
        AddressName,Address1,Address2,Address3,City,State,Country,Postcode,
        Fax,Email,URL,Tel1,Tel2,Extension1,Extension2,Reference,FAO,IsDefaultAddress,IsDefaultShippingAddress)
        VALUES (@CustomerID,@AddressName,@Address1,@Address2,@Address3,@City,@StateID,@CountryID,@Postcode,
        @Fax,@Email,@URL,@Tel1,@Tel2,@Extension1,@Extension2,@Reference,@FAO,@IsDefaultAddress,@IsDefaultShippingAddress)
	RETURN