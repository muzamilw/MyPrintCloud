Create PROCEDURE [dbo].[sp_Estimation_Update_Estimate]

		@EstimateName varchar (255),@EstimateCode varchar (100), 
        @CustomerID int,@ContactID int,@EstimateTotal float,@ValidUpto int,
        @LastUpdatedBy int,@SalesPerson int, 
        @HeadNotes nvarchar (2000),@FootNotes nvarchar(2000),@EstimateDate datetime, 
        @Greeting varchar (50),@AccountNumber varchar (50),@OrderNo varchar (50),@SuccessChanceID int,@ProjectionDate datetime ,@ParentID int,@Version int,@IsInPipeLine int,@AddressID int,@Status int,@flag int ,@EstimateID int,@CompanyName varchar(255),@ProductID int,@SourceID int
AS
	update tbl_estimates set Estimate_Name=@EstimateName,Estimate_Code=@EstimateCode, 
          ContactCompanyID=@CustomerID,ContactID=@ContactID,Estimate_Total=@EstimateTotal,Estimate_ValidUpto=@ValidUpto,
          LastUpdatedBy=@LastUpdatedBy,SalesPersonID=@SalesPerson, 
          HeadNotes=@HeadNotes,FootNotes=@FootNotes,EstimateDate=@EstimateDate, 
          Greeting=@Greeting,AccountNumber=@AccountNumber,OrderNo=@OrderNo,SuccessChanceID=@SuccessChanceID,ProjectionDate=@ProjectionDate,ParentID=@ParentID,Version=@Version,IsInPipeLine=@IsInPipeLine,AddressID=@AddressID,StatusID=@Status,SectionFlagID=@flag,CompanyName=@CompanyName,SourceID=@SourceID,ProductID=@ProductID where EstimateID=@EstimateID 
	RETURN