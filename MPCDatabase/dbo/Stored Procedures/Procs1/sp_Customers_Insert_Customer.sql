CREATE PROCEDURE [dbo].[sp_Customers_Insert_Customer]
	(
	
    @IsCustomer SmallInt,
    @CustomerName varchar(100),
    @AccountNumber varchar(10) ,
    @AccountStatusID smallint, 
    @CustomerTypeID int ,
    @DefaultTill smallint,
    @URL varchar(30), 
    @ISBN varchar(30),
    @Notes nvarchar(3000),
    @CreditLimit float,
    @DefaultMarkUpID int ,
    @DefaultNominalCode int,
    @AccountManagerID int,
    @AccountOpenDate datetime,
    @CreditReference  varchar(50),
    @Terms nvarchar(3000),
    @AccountOnHandDesc varchar(255),
    @AccountBalance float,
    @CustomerCreationDate datetime,
    @VATRegNumber varchar(50) ,
    @VATRegReference varchar(50),
    @IsParaentCompany SmallInt,
    @ParaentCompanyID int,
    @SystemSiteID int,
    @FlagID int,
    @CustomerImage varchar(200),
    @IsEmailSubscription SmallInt,
    @IsMailSubscription SmallInt,
    @IsEmailFormat SmallInt,
    @IsAllowWebAccess SmallInt,
    @DepartmentID int,
    @CustomerSalesPerson int

	)

    AS
	
	    INSERT INTO tbl_contactCompanies (IsCustomer,Name,AccountNumber, 
        AccountStatusID,TypeID,DefaultTill,URL,ISBN,Notes,
        CreditLimit,DefaultMarkUpID,DefaultNominalCode,AccountManagerID,
        AccountOpenDate,CreditReference,Terms,AccountOnHandDesc,AccountBalance,
        CreationDate,VATRegNumber,VATRegReference,IsParaentCompany,
        ParaentCompanyID,SystemSiteID,FlagID,IsEmailSubscription,
        IsMailSubscription,IsEmailFormat,DepartmentID,SalesPerson,IsArchived,Image)
        
        VALUES (@IsCustomer,@CustomerName,@AccountNumber,@AccountStatusID,
        @CustomerTypeID,@DefaultTill,@URL,@ISBN,@Notes,@CreditLimit,@DefaultMarkUpID,
        @DefaultNominalCode,@AccountManagerID,@AccountOpenDate,@CreditReference,
        @Terms,@AccountOnHandDesc,@AccountBalance,@CustomerCreationDate,@VATRegNumber,
        @VATRegReference,@IsParaentCompany,@ParaentCompanyID,@SystemSiteID, 
        @FlagID,@IsEmailSubscription,@IsMailSubscription,@IsEmailFormat,
        @DepartmentID,@CustomerSalesPerson,0,@CustomerImage)

    
	RETURN