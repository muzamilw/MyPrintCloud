CREATE PROCEDURE [dbo].[sp_Suppliers_Contacts_UpdateSupplierContact]

	(
        @SupplierContactID int ,
        @SupplierID int,
		@AddressID int ,
		@FirstName varchar(50)=null, 
		@MiddleName varchar(50)=null, 
		@LastName varchar(50)=null, 
		@Title varchar(20)=null,
        @Mobile varchar(30)=null,
        @Pager varchar(30)=null,
        @Email varchar(30)=null,
        @JobTitle varchar(50)=null,
        @Department varchar(100)=null,
        @DOB datetime,
        @Notes nvarchar(3000),
        @IsDefaultContact smallint,
        @HomeAddress1 varchar(255)=null,
        @HomeAddress2 varchar(255)=null,
        @HomeCity varchar(30)=null,
        @HomeStateID  varchar(30)=null,
        @HomePostCode varchar(30)=null,
        @HomeCountryID  varchar(30)=null,
        @HomeTel1 varchar(30)=null,
        @HomeTel2 varchar(30)=null,
        @HomeExtension1 varchar(7)=null,
        @HomeExtension2 varchar(7)=null
	)

AS
	UPDATE tbl_Contacts 
       SET AddressID=@AddressID,ContactCompanyID=@SupplierID,FirstName=@FirstName,MiddleName=@MiddleName,LastName=@LastName,
       Title=@Title,HomeTel1=@HomeTel1,HomeTel2=@HomeTel2,HomeExtension1=@HomeExtension1,HomeExtension2=@HomeExtension2,
       Mobile=@Mobile ,Email= @Email, Pager= @Pager,JobTitle=@JobTitle, DOB=@DOB, Notes=@Notes,
       IsDefaultContact=@IsDefaultContact,HomeAddress1= @HomeAddress1,HomeAddress2= @HomeAddress2, HomeCity= @HomeCity,
       HomeState= @HomeStateID, HomePostCode= @HomePostCode,HomeCountry= @HomeCountryID
        WHERE ContactID = @SupplierContactID
	RETURN