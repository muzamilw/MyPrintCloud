
Create PROCEDURE [dbo].[sp_Delivery_Add_DeliveryNote]

	(
		@EstimateId int,
		@JobID int,
		@InvoiceID int,
		@FlagID int,
		@RaisedBy int,
		@Code varchar(20),
		@DeliveryDate datetime,
		@CustomerId int ,
		@OrderReff varchar(50),
		@footnote varchar(255),
		@Comments varchar(255),
		@LockedBy int,
		@IsStatus smallint,
		@ContactId int,
		@ContactCompany varchar(50),
		@CustomerOrderReff varchar(100),
		@AddressID int,
		@CreatedBy int,
		@CreationDateTime datetime,
		@SupplierID int,
		@SupplierTelNo varchar(50),
		@CsNo varchar(50),
		@SupplierURL varchar(50),
		@SystemSiteID int
	)

AS
	/* SET NOCOUNT ON */
	
	insert into tbl_deliverynotes (EstimateID,JobID,InvoiceID,FlagID,RaisedBy,Code,DeliveryDate,ContactCompanyID,OrderReff,footnote,Comments,LockedBy,IsStatus,ContactId,ContactCompany,CustomerOrderReff,AddressID,CreatedBy,CreationDateTime,SupplierID,SupplierTelNo,CsNo,SupplierURL,SystemSiteID)
        VALUES
        (@EstimateId,@JobID,@InvoiceID,@FlagID,@RaisedBy,@Code,@DeliveryDate,@CustomerId,@OrderReff,@footnote,@Comments,@LockedBy,@IsStatus,@ContactId,@ContactCompany,@CustomerOrderReff,@AddressID,@CreatedBy,@CreationDateTime,@SupplierID,@SupplierTelNo,@CsNo,@SupplierURL,@SystemSiteID);select @@Identity as DelID
	
	RETURN