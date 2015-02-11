
CREATE PROCEDURE [dbo].[sp_Estimation_Insert_Estimate]
@EstimateCode varchar (100),
@EstimateName varchar (255),
@CustomerID int,@ContactID int,@Status int,@EstimateTotal int,@ValidUpto int,@LastUpdatedBy int,@CreationDate datetime,@CreationTime datetime,@CreatedBy int,@SalesPerson int,@EstimateDate datetime,@ProjectionDate datetime,@Greeting varchar (50),@AccountNumber varchar (50),@OrderNo varchar (50),@SuccessChanceID int,@AddressID int,@CompanyName varchar (255),@ParentID int,@Version int,@SystemCompanyID int,@IsEstimate int,@IsInPipeLine int,@UserNotes text,@HeadNotes nvarchar (2000),@FootNotes nvarchar(2000),@SiteID int,@EnquiryID int,@ProductID int,@SourceID int
AS
	select @EstimateCode = EstimateNext from tbl_prefixes where PrefixID = 1
	insert into tbl_estimates (Estimate_Code,Estimate_Name,ContactCompanyID,ContactID,StatusID,Estimate_Total,Estimate_ValidUpto,LastUpdatedBy,CreationDate,CreationTime,Created_by,SalesPersonID,EstimateDate,ProjectionDate,Greeting,AccountNumber,OrderNo,SuccessChanceID,AddressID,CompanyName,ParentID,Version,IsEstimate,IsInPipeLine,UserNotes,HeadNotes,FootNotes,sectionflagID,CompanySiteID,EnquiryID,SourceID,ProductID,IsDirectSale) 
	VALUES 
     (@EstimateCode,@EstimateName,@CustomerID,@ContactID,@Status,@EstimateTotal,@ValidUpto,@LastUpdatedBy,@CreationDate,@CreationTime,@CreatedBy,@SalesPerson,@EstimateDate,@ProjectionDate,@Greeting,@AccountNumber,@OrderNo,@SuccessChanceID,@AddressID,@CompanyName,@ParentID,@Version,@IsEstimate,@IsInPipeLine,@UserNotes,@HeadNotes,@FootNotes,'1',@SiteID,@EnquiryID,@SourceID,@ProductID,1) ;
     select @@Identity as EstimateID
     update tbl_prefixes set EstimateNext = @EstimateCode + 1 where PrefixID = 1
	RETURN