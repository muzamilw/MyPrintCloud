CREATE PROCEDURE dbo.sp_Grn_Update_Detail
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
			  @DepartmentID int,
			   @GoodsReceivedDetailID int
AS

UPDATE    tbl_goodsreceivednotedetail
SET               ItemID =@ItemID , QtyReceived =@QtyReceived , GoodsreceivedID =@GoodsreceivedID , Price =@Price , PackQty =@PackQty , TotalOrderedqty =@TotalOrderedqty , Details =@Details , ItemCode =@ItemCode , [Name] =@Name , 
                      TotalPrice =@TotalPrice , TaxID =@TaxID , NetTax =@NetTax , Discount =@Discount , FreeItems =@FreeItems , DepartmentID =@DepartmentID   where GoodsReceivedDetailID = @GoodsReceivedDetailID 
	RETURN