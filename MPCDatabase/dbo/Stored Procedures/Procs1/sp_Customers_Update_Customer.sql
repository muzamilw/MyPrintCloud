CREATE PROCEDURE [dbo].[sp_Customers_Update_Customer]

	(
		@CustomerID int,
		@AccountNumber varchar(10),
		@CustomerName varchar(100),
		@URL varchar(30),
		@CreditReference varchar(50),
		@CreditLimit float ,
		@Terms nvarchar(3000) ,
		@CustomerTypeID int,
		@DefaultNominalCode int,
		@DefaultTill smallint,
		@DefaultMarkUpID int,
		@AccountOpenDate datetime,
		@AccountManagerID int,
		@CustomerStatus smallint=1,
		@IsCustomer smallint,
		@Notes nvarchar(3000),
		@ISBN varchar(30) ,
		@NotesLastUpdatedDate datetime,
		@NotesLastUpdatedBy int,
		@AccountOnHandDesc varchar(255),
		@AccountStatusID int,
		@IsDisabled smallint,
		@AccountBalance float,
		@VATRegNumber varchar(50),
		@IsParaentCompany smallint,
		@ParaentCompanyID int,
		@VATRegReference varchar(50),
		@FlagID int,
		@CustomerImage varchar(200),
		@IsMailSubscription smallint,
		@IsEmailFormat smallint,
		@IsAllowWebAccess smallint,
	    @IsEmailSubscription smallint,
		@HomeContact nvarchar(3000)=null ,
		@AbountUs  nvarchar(3000)=null ,
		@ContactUs nvarchar(3000)=null,
		@IsShowFinishedGoodPrices bit,
		@DepartmentID int,
		@CustomerSalesPerson int
	) 
	
AS
	UPDATE tbl_contactCompanies SET AccountNumber=@AccountNumber,
        Name=@CustomerName,URL=@URL,CreditReference=@CreditReference,CreditLimit=@CreditLimit,
        Terms=@Terms,TypeID=@CustomerTypeID,DefaultNominalCode=@DefaultNominalCode,DefaultTill=@DefaultTill,
        DefaultMarkUpID=@DefaultMarkUpID,AccountOpenDate=@AccountOpenDate,AccountManagerID=@AccountManagerID,
        Status=@CustomerStatus,IsCustomer=@IsCustomer,Notes=@Notes,ISBN=@ISBN,NotesLastUpdatedDate=@NotesLastUpdatedDate,
        NotesLastUpdatedBy=@NotesLastUpdatedBy,AccountOnHandDesc=@AccountOnHandDesc,AccountStatusID=@AccountStatusID,
        IsDisabled=@IsDisabled,AccountBalance=@AccountBalance,VATRegNumber=@VATRegNumber,IsParaentCompany=@IsParaentCompany,
        ParaentCompanyID=@ParaentCompanyID,VATRegReference=@VATRegReference,FlagID=@FlagID,
        Image=@CustomerImage,IsMailSubscription=@IsMailSubscription,
        IsEmailFormat=@IsEmailFormat,IsEmailSubscription=@IsEmailSubscription,
        HomeContact=@HomeContact,AbountUs=@AbountUs,ContactUs=@ContactUs,IsShowFinishedGoodPrices=@IsShowFinishedGoodPrices,DepartmentID=@DepartmentID 
        
        --,CustomerSalesPerson=@CustomerSalesPerson
        WHERE ContactCompanyID=@CustomerID

	RETURN