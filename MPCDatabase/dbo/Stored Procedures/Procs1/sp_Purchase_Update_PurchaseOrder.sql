CREATE PROCEDURE [dbo].[sp_Purchase_Update_PurchaseOrder]

	(
	@Date_Purchase datetime,
@PurchaseID int,
@SupplierID int,
@ContactID int,
@refNo varchar(50),
@TotalPrice float,
@comments nvarchar(2000),
@footnotes nvarchar(2000),
@SupplierContactAddressID int,
@Status int,
@LockedBy int,
@Discount float,
@DiscountType int,
@TotalTax float,
@GrandTotal float,
@NetTotal float,
@UserID int,
@LastChangedBy int,
@FlagID int,
@SupplierContactCompany varchar(50)
	)

AS
	/* SET NOCOUNT ON */
	
	update tbl_purchase set date_Purchase=@Date_Purchase,SupplierContactCompany=@SupplierContactCompany,
    SupplierID=@SupplierID,ContactId=@ContactID,refNo=@refNo,TotalPrice=@TotalPrice,
    comments=@comments,FootNote=@footnotes,SupplierContactAddressID=@SupplierContactAddressID,Status=@Status,LockedBy=@LockedBy,
    Discount=@Discount,DiscountType=@DiscountType,TotalTax=@TotalTax,GrandTotal=@GrandTotal,NetTotal=@NetTotal,UserID=@UserID,LastChangedBy=@LastChangedBy,FlagID=@FlagID where PurchaseID=@PurchaseID
	
	RETURN