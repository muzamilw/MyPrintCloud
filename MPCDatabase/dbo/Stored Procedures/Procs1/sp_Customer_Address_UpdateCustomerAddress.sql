CREATE PROCEDURE [dbo].[sp_Customer_Address_UpdateCustomerAddress]

	(	        
        
        @AddressID int,
        @CustomerID int,
		@AddressName varchar(100),
		@Address1  varchar(255),
		@Address2  varchar(255),
		@Address3  varchar(255),
		@City  varchar(30),
		@StateID varchar(30),
		@CountryID varchar(100),
		@Postcode  varchar(100),
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
	UPDATE tbl_Addresses
        SET ContactCompanyID=@CustomerID,AddressName=@AddressName,Address1=@Address1,Address2=@Address2,Address3=@Address3,
        City=@City,State=@StateID,Country=@CountryID,Postcode=@PostCode,Fax=@Fax,Email=@Email,
         URL=@URL,Tel1=@Tel1,Tel2=@Tel2,Extension1=@Extension1,Extension2=@Extension2,Reference=@Reference,
        FAO=@FAO,IsDefaultAddress=@IsDefaultAddress , IsDefaultShippingAddress = @IsDefaultShippingAddress WHERE tbl_Addresses.AddressID=@AddressID
	RETURN