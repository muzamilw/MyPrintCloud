﻿
CREATE PROCEDURE [dbo].[sp_Orders_Update_Estimate]
(
		@flag int,
		@EstimateName varchar (255),
		@CustomerID int,
		@ContactID int,
		@EstimateTotal float,
		@LastUpdatedBy int,
		@HeadNotes nvarchar(2000),
		@FootNotes nvarchar(2000),
		@Greeting varchar (50),
		@OrderNo varchar (50),
		@AddressID int,
		@CompanyName varchar (255),
		@Order_Date datetime,
		@Order_DeliveryDate datetime ,
		@EstimateID int,
		@Order_Status smallint,

		@OrderManagerID int,
		@ArtworkByDate datetime,
		@DataByDate datetime,
		@TargetPrintDate datetime,
		@StartDeliveryDate datetime,
		@PaperByDate datetime,
		@TargetBindDate datetime,
		@FinishDeliveryDate datetime,
		@Classification1ID int,
		@Classification2ID int,
		@IsOfficialOrder int,
		@CustomerPO varchar(100),
		@OfficialOrderSetBy int,
		@OfficialOrderSetOnDateTime datetime,
		@IsCreditApproved int,
		@CreditLimitForJob float,
		@CreditLimitSetBy int,
		@CreditLimitSetOnDateTime datetime,
		@IsJobAllowedWOCreditCheck int,
		@AllowJobWOCreditCheckSetBy int,
		@AllowJobWOCreditCheckSetOnDateTime datetime,
		@OrderSourceID smallint,
		@CustomerOrderStatusID int
		
)
AS
	update tbl_estimates set Sectionflagid=@flag,Estimate_Name=@EstimateName,ContactCompanyID=@CustomerID,ContactID=@ContactID, 
        Estimate_Total=@EstimateTotal,LastUpdatedBy=@LastUpdatedBy,HeadNotes=@HeadNotes,FootNotes=@FootNotes, 
        Greeting=@Greeting,OrderNo=@OrderNo,AddressID=@AddressID,CompanyName=@CompanyName,Order_Date=@Order_Date,Order_DeliveryDate=@Order_DeliveryDate,
        Order_Status=@Order_Status,StatusId = @Order_Status,
        
        OrderManagerID=@OrderManagerID,
		ArtworkByDate=@ArtworkByDate,
		DataByDate=@DataByDate,
		TargetPrintDate=@TargetPrintDate,
		StartDeliveryDate=@StartDeliveryDate,
		PaperByDate=@PaperByDate,
		TargetBindDate=@TargetBindDate,
		FinishDeliveryDate=@FinishDeliveryDate,
		Classification1ID=@Classification1ID,
		Classification2ID=@Classification2ID,
		IsOfficialOrder=@IsOfficialOrder,
		CustomerPO=@CustomerPO,
		OfficialOrderSetBy=@OfficialOrderSetBy,
		OfficialOrderSetOnDateTime=@OfficialOrderSetOnDateTime,
		IsCreditApproved=@IsCreditApproved,
		CreditLimitForJob=@CreditLimitForJob,
		CreditLimitSetBy=@CreditLimitSetBy,
		CreditLimitSetOnDateTime=@CreditLimitSetOnDateTime,
		IsJobAllowedWOCreditCheck=@IsJobAllowedWOCreditCheck,
		AllowJobWOCreditCheckSetBy=@AllowJobWOCreditCheckSetBy,
		AllowJobWOCreditCheckSetOnDateTime=@AllowJobWOCreditCheckSetOnDateTime, 
		OrderSourceID=@OrderSourceID
		--,CustomerOrderStatusID=@CustomerOrderStatusID
        where (EstimateID=@EstimateID)
	RETURN