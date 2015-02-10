CREATE PROCEDURE [dbo].[sp_Suppliers_Contacts_InsertSupplierContact]


	(
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
        @Notes nvarchar(3000)=null,
        @IsDefaultContact smallint,
        @HomeAddress1 varchar(255)=null,
        @HomeAddress2 varchar(255)=null,
        @HomeCity varchar(30)=null,
        @HomeStateID varchar(100),
        @HomePostCode varchar(30)=null,
        @HomeCountryID varchar(100),
        @HomeTel1 varchar(30)=null,
        @HomeTel2 varchar(30)=null,
        @HomeExtension1 varchar(7)=null,
        @HomeExtension2 varchar(7)=null
	)

AS
	INSERT INTO tbl_Contacts (ContactCompanyID,
        AddressID,FirstName, MiddleName,LastName,Title,Mobile,Pager,Email,JobTitle,DOB,Notes,
        IsDefaultContact,HomeAddress1,HomeAddress2,HomeCity,HomeState,HomePostCode,HomeCountry,
        HomeTel1,HomeTel2,HomeExtension1,HomeExtension2)
         VALUES (@SupplierID ,@AddressID,@FirstName, @MiddleName, @LastName, @Title,
        @Mobile,@Pager,@Email,@JobTitle,@DOB,@Notes,@IsDefaultContact,@HomeAddress1,@HomeAddress2,@HomeCity,
        @HomeStateID,@HomePostCode,@HomeCountryID,@HomeTel1,@HomeTel2,@HomeExtension1,@HomeExtension2);select @@identity as SupplierContactID
	RETURN