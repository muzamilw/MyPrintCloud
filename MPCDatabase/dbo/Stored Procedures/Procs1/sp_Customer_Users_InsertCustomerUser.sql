CREATE PROCEDURE dbo.sp_Customer_Users_InsertCustomerUser
	(
		@CustomerID int, 
		@ParentID int ,
		@UserName varchar(50),
		@Password varchar(50) ,
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
		@DefaultBillingAddress int

	)

 AS
	insert into tbl_customerusers (CustomerID,ParentID,UserName,Password,CustomerUserRoleId,FirstName,MiddleName,LastName,Title,Address,City,State,Country,PostCode,Tel1,Url,Fax,Mobile,Email,JobTitle,Department,DOB,IsApprover,DefaultShippingAddress,DefaultBillingAddress)
	 VALUES(@CustomerID,@ParentID,@UserName,@Password,@RoleID,@FirstName,@MiddleName,@LastName,@Title,@Address,@City,@State,@Country,@PostCode,@Tel1,@Url,@Fax,@Mobile,@Email,@JobTitle,@Department,@DOB,@IsApprover,@DefaultShippingAddress,@DefaultBillingAddress)

	RETURN