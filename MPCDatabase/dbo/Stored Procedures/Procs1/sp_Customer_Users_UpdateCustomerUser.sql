CREATE PROCEDURE dbo.sp_Customer_Users_UpdateCustomerUser
	(
	@CustomerUserID int,
		@UserName varchar(50),
		@RoleID int, 
		@FirstName varchar(50),
		@MiddleName varchar(50) ,
		@LastName varchar(50),
		@Title varchar(20),
		@Address varchar(255),
		@City varchar(30), 
		@State int,
		@Country int,
		@PostCode varchar(30),
		@Tel1 varchar(30),
		@Url varchar(100),
		@Fax varchar(30),
		@Mobile varchar(30),
		@Email varchar(100),
		@JobTitle varchar(30),
		@Department varchar(30), 
		@DOB datetime,
		@IsApprover smallint,
		@DefaultShippingAddress int,
		@DefaultBillingAddress int,
		@ParentID int
	)
	AS
      Update tbl_customerusers SET UserName=@UserName,CustomerUserRoleId=@RoleID,FirstName=@FirstName,MiddleName=@MiddleName,LastName=@LastName,Title=@Title,Address=@Address,City=@City,State=@State,Country=@Country,PostCode=@PostCode,Tel1=@Tel1,Url=@Url,Fax=@Fax,Mobile=@Mobile,Email=@Email,JobTitle=@JobTitle,Department=@Department,DOB=@DOB,IsApprover=@IsApprover ,
      DefaultShippingAddress=@DefaultShippingAddress , DefaultBillingAddress= @DefaultBillingAddress, ParentID =@ParentID where CustomerUserID=@CustomerUserID

	RETURN