CREATE PROCEDURE dbo.sp_Grn_Add_Detail
@ItemID int,
 @QtyReceived float,
  @GoodsreceivedID int,
   @Price float,
    @PackQty int,
     @TotalOrderedqty float,
      @Details text,
       @ItemCode varchar(20),
        @Name varchar (255),
         @TotalPrice float,
          @TaxID int,
           @NetTax float,
            @Discount float, 
			 @FreeItems int, 
			  @DepartmentID int
AS

INSERT INTO tbl_goodsreceivednotedetail
                      (ItemID, QtyReceived, GoodsreceivedID, Price, PackQty, TotalOrderedqty, Details, ItemCode, [Name], TotalPrice, TaxID, NetTax, 
                      Discount, FreeItems, DepartmentID)
VALUES     (@ItemID, @QtyReceived, @GoodsreceivedID, @Price, @PackQty, @TotalOrderedqty, @Details, @ItemCode, @Name, @TotalPrice, @TaxID, @NetTax, 
                      @Discount, @FreeItems, @DepartmentID)
	RETURN