
Create PROCEDURE [dbo].[sp_Delivery_Update_DeliveryNote]

	(
		@FlagID int,
		@RaisedBy int,
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
		@SupplierID int,
		@SupplierTelNo varchar(50),
		@CsNo varchar(50),
		@SupplierURL varchar(50),
		@DeliveryID int
	)

AS
	/* SET NOCOUNT ON */
	
	update tbl_deliverynotes set FlagID=@FlagID,RaisedBy=@RaisedBy,DeliveryDate=@DeliveryDate,
	ContactCompanyId=@CustomerId,OrderReff=@OrderReff,footnote=@footnote,
    Comments=@Comments,LockedBy=@LockedBy,IsStatus=@IsStatus,ContactId=@ContactId,
    ContactCompany=@ContactCompany,CustomerOrderReff=@CustomerOrderReff,AddressID=@AddressID,
    SupplierID=@SupplierID,SupplierTelNo=@SupplierTelNo,CsNo=@CsNo,SupplierURL=@SupplierURL where DeliveryNoteID=@DeliveryID
	
	RETURN