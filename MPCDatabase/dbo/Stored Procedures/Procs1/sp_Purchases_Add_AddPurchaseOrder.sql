CREATE PROCEDURE [dbo].[sp_Purchases_Add_AddPurchaseOrder]

	(
		@Code varchar(50),
		@Date_Purchase datetime,
		@SupplierID int,
		@ContactID int,
		@SupplierContactCompany varchar(50),
		@SupplierContactAddressID int,
		@refNo varchar(50),
		@TotalPrice float,
		@UserID int,
		@isProduct bit,
		@JobID int,
		@comments nvarchar(2000),
		@footnotes nvarchar(2000),
		@usernotes text,
		@Status int,
		@Discount float,
		@DiscountType int,
		@TotalTax float,
		@GrandTotal float,
		@NetTotal float,
		@CreatedBy int,
		@LastChangedBy int,
		@FlagID int,
		@SystemSiteID int
	)

AS
	/* SET NOCOUNT ON */
	
	insert into tbl_purchase (Code,date_Purchase,
        SupplierID,ContactID,SupplierContactCompany,SupplierContactAddressID,RefNo,TotalPrice,UserID,isProduct,
        JobID,comments,FootNote,UserNotes,Status,Discount,DiscountType,TotalTax,GrandTotal,NetTotal,CreatedBy,LastChangedBy,FlagID,SystemSiteID)
        VALUES (@Code,@Date_Purchase,@SupplierID,@ContactID,@SupplierContactCompany,@SupplierContactAddressID,@refNo,
        @TotalPrice,@UserID,@isProduct,@JobID,@comments,@footnotes,@usernotes,@Status,@Discount,@DiscountType,@TotalTax,
        @GrandTotal,@NetTotal,@CreatedBy,@LastChangedBy,@FlagID,@SystemSiteID);Select @@Identity as POID

	
	RETURN