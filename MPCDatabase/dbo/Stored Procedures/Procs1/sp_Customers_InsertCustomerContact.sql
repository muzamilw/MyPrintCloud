CREATE PROCEDURE [dbo].[sp_Customers_InsertCustomerContact]
	(
		@CustomerID int  ,
		@AddressID int ,
		@FirstName varchar(50),
		@MiddleName varchar(50),
		@LastName varchar(50),
		@Title varchar(20),
        @Mobile varchar(30),
        @Pager varchar(30),
        @Email varchar(30),
        @JobTitle varchar(30),
        @Department varchar(100) ,
        @DOB datetime,
        @Notes nvarchar(3000),
        @DefaultContact smallint,
        @HomeAddress1 varchar(255),
        @HomeAddress2 varchar(255), 
        @HomeCity varchar(30),
        @HomeStateID varchar(100) ,
        @HomePostCode varchar(30),
        @HomeCountryID varchar(100) ,
        @HomeTel1 varchar(30),
        @HomeTel2 varchar(30), 
        @HomeExtension1 varchar(7),
        @HomeExtension2 varchar(7)
		
	)

AS
	INSERT INTO tbl_Contacts (ContactCompanyID,
        AddressID,FirstName, MiddleName,LastName,Title,Mobile,Pager,Email,JobTitle,DOB,Notes,
        IsDefaultContact,HomeAddress1,HomeAddress2,HomeCity,HomeState,HomePostCode,HomeCountry,
        HomeTel1,HomeTel2,HomeExtension1,HomeExtension2)
        VALUES (@CustomerID ,@AddressID,@FirstName, @MiddleName, @LastName, @Title,
        @Mobile,@Pager,@Email,@JobTitle,@DOB,@Notes,@DefaultContact,@HomeAddress1,@HomeAddress2,@HomeCity,
        @HomeStateID,@HomePostCode,@HomeCountryID,@HomeTel1,@HomeTel2,@HomeExtension1,@HomeExtension2);select @@identity as ContactID
	RETURN