CREATE PROCEDURE dbo.sp_Customer_CustomizedFields_InsertCustomerField
	(

	
		@FieldID int,
		@CompanyID int,
		@CustomerID int,
		@IsCustomer smallint,
		@Value varchar(255)=null
		
	
		
	)
AS
	INSERT INTO tbl_customizedfieldsdata (CompanyID,FieldID,
     CustomerID,IsCustomer,Value)
       VALUES (@CompanyID,@FieldID ,@CustomerID, @IsCustomer,@Value)
	RETURN