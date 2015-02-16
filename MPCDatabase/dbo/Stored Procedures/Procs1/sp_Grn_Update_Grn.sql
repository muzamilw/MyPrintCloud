CREATE PROCEDURE dbo.sp_Grn_Update_Grn

	(
		@GoodsReceivedID int ,
		@Date_Received datetime ,
		@refNo varchar(30) ,
		@TotalPrice float ,
		@comments nvarchar(2000) ,
		@usernotes text,
		@Address varchar(30) ,
		@City varchar(30),
		@State varchar(30) ,
		@PostalCode varchar(30),
		@Country varchar(30) ,
		@Telephone varchar(30) ,
		@Discount float,
		@DiscountType int,
		@TotalTax float ,
		@GrandTotal float,
		@NetTotal float ,
		@Status smallint ,
		@LastChangedBy int,
		@UserID int,
		@FlagID int,
		@DeliveryDate datetime,
		@CarrierID int,
		@Reference1 varchar(50),
		@Reference2 varchar(50)
	)
	
AS
	/* SET NOCOUNT ON */
	
	update tbl_goodsreceivednote set date_Received=@Date_Received,
    RefNo=@refNo,TotalPrice=@TotalPrice,
    comments=@comments,UserNotes=@usernotes,Address=@Address,City=@City,State=@State,PostalCode=@PostalCode,Country=@Country,Tel1=@Telephone,
    Discount=@Discount,DiscountType=@DiscountType,TotalTax=@TotalTax,GrandTotal=@GrandTotal,NetTotal=@NetTotal,Status=@Status,LastChangedBy=@LastChangedBy,UserID=@UserID,FlagID=@FlagID,DeliveryDate=@DeliveryDate ,CarrierID=@CarrierID  ,Reference1=@Reference1  ,Reference2=@Reference2  where GoodsReceivedID=@GoodsReceivedID
	
	RETURN