CREATE PROCEDURE [dbo].[sp_Customers_UpdateCustomerContact]

	(
        
        @CustomerID int  ,
		@AddressID int ,
		@CustomerContactID int,
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
	UPDATE tbl_Contacts 
        SET AddressID=@AddressID,ContactCompanyID=@CustomerID,FirstName=@FirstName,MiddleName=@MiddleName,LastName=@LastName,
        Title=@Title,HomeTel1=@HomeTel1,HomeTel2=@HomeTel2,HomeExtension1=@HomeExtension1,HomeExtension2=@HomeExtension2,
        Mobile=@Mobile ,Email= @Email, Pager= @Pager,JobTitle=@JobTitle, DOB=@DOB, Notes=@Notes,
        IsDefaultContact=@DefaultContact,HomeAddress1= @HomeAddress1,HomeAddress2= @HomeAddress2, HomeCity= @HomeCity,
        HomeState= @HomeStateID, HomePostCode= @HomePostCode,HomeCountry= @HomeCountryID
        WHERE ContactID = @CustomerContactID
	RETURN