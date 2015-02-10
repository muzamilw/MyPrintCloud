CREATE PROCEDURE [dbo].[sp_Grn_Add_Grn]

	(
		@PurchaseID int ,@Code varchar(50) ,@Date_Received datetime ,@SupplierID int ,@ContactID int ,@refNo varchar(30) ,@TotalPrice float ,@UserID int ,@isProduct smallint ,@comments nvarchar(2000) ,@usernotes text ,@Address varchar(30) ,@City varchar(30),@State varchar(30) ,@PostalCode varchar(30),@Country varchar(30) ,@Telephone varchar(30) ,
		@Discount float,@DiscountType int,@TotalTax float ,@GrandTotal float,@NetTotal float ,@Status int,
		@LastChangedBy int,@FlagID int,@SystemSiteID int,@CreatedBy int,@DeliveryDate Datetime,@CarrierID int,@Reference1 varchar(50),@Reference2 varchar(50)
	)

AS
	/* SET NOCOUNT ON */
	
	insert into tbl_goodsreceivednote (PurchaseID,Code,date_Received,
    SupplierID,ContactID,RefNo,TotalPrice,UserID,isProduct,comments,UserNotes,address,city,state,postalCode,Country,Tel1,Discount,DiscountType,TotalTax,GrandTotal,NetTotal,Status,LastChangedBy,FlagID,SystemSiteID,CreatedBy,DeliveryDate ,CarrierID ,Reference1 ,Reference2)
    VALUES (@PurchaseID,@Code,@Date_Received,@SupplierID,@ContactID,@refNo,@TotalPrice,@UserID,@isProduct,@comments,@usernotes,@Address,@City,@State,@PostalCode,@Country,@Telephone,@Discount,@DiscountType,@TotalTax,@GrandTotal,@NetTotal,@Status,@LastChangedBy,@FlagID,@SystemSiteID,@CreatedBy,@DeliveryDate ,@CarrierID ,@Reference1 ,@Reference2);Select @@identity as GRID
	
	RETURN