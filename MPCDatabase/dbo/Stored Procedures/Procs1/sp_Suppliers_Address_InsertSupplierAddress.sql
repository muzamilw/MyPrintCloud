CREATE PROCEDURE [dbo].[sp_Suppliers_Address_InsertSupplierAddress]
	(
	@SupplierID int,
	@AddressName varchar(100)=null,
	@Address1 varchar(255)=null,
	@Address2 varchar(255)=null,
	@Address3 varchar(255)=null,
	@City varchar(30)=null,
	@StateID varchar(100),
	@CountryID varchar(100),
	@Postcode varchar(30)=null,
    @Fax varchar(30)=null,
    @Email varchar(50)=null,
    @URL varchar(100)=null,
    @Tel1 varchar(30)=null,
    @Tel2 varchar(30)=null,
    @Extension1 varchar(7)=null,
    @Extension2 varchar(7)=null,
    @Reference varchar(50)=null,
    @FAO varchar(50)=null,
    @IsDefaultAddress smallint
	)
AS
	INSERT INTO tbl_addresses (ContactCompanyID,
        AddressName,Address1,Address2,Address3,City,State,Country,Postcode,
        Fax,Email,URL,Tel1,Tel2,Extension1,Extension2,Reference,FAO,IsDefaultAddress)
        VALUES (@SupplierID,@AddressName,@Address1,@Address2,@Address3,@City,@StateID,@CountryID,@Postcode,
        @Fax,@Email,@URL,@Tel1,@Tel2,@Extension1,@Extension2,@Reference,@FAO,@IsDefaultAddress)
        
	RETURN