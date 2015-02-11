
CREATE PROCEDURE [dbo].[usp_DeletePurchaseOrders]
@OrderID as int
AS
BEGIN
	
	SET NOCOUNT OFF
	DECLARE @Err int
	
    Declare 
         @ItemsCount as int,
         @Counter as int,
		 @DeletedPurchaseID as int,
		 @POCounter as int,
		 @POCount as int,
         @ItemID as int
         
    DECLARE @ITEMS AS TABLE(
     ROWID  INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
     ItemID int
    
     )
   
  													
     
 --   INSERT INTO @ITEMS 
	--SELECT ItemID
	--			FROM tbl_items 
	--			WHERE EstimateID = @OrderID and (itemType is null or itemType <> 2)
	
	 INSERT INTO @ITEMS 
	SELECT ItemID
				FROM tbl_items 
				WHERE EstimateID = @OrderID 
			
	--select @ItemsCount = count(*) from tbl_items where EstimateID = @OrderID and (itemType is null or itemType <> 2)
	select @ItemsCount = count(*) from tbl_items where EstimateID = @OrderID							
	        
	SET @Counter = 1

    WHILE (@Counter <= @ItemsCount)
    BEGIN

					Select @ItemID = ItemID	 
					from @ITEMS where ROWID = @Counter
					select @DeletedPurchaseID = purchaseid from tbl_purchasedetail where itemid = @ItemID
					delete from tbl_purchasedetail where purchaseid = @DeletedPurchaseID
					delete from tbl_purchase where purchaseid = @DeletedPurchaseID
					select 'delete successfully' as MSG
							
			   SET @Counter = @Counter + 1
		end
		
		 SET @Err = @@Error	
		RETURN @Err
END