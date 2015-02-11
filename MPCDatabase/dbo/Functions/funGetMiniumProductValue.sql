CREATE FUNCTION [dbo].[funGetMiniumProductValue] 
(
 -- Add the parameters for the function here
 @ItemID int
)
RETURNS float
AS
BEGIN
 -------- Declare the return variable here
DECLARE @Result float
declare @tabletype bit


select @tabletype = isQtyRanged from items where ItemId = @ItemID

    if (@tabletype = 1)
    begin
    
  select @Result = max(P) from 
  (
  select PX.PricePaperType1 as P from  itemPriceMatrix PX
  where itemid = @ItemID and PX.PricePaperType1 <> 0 and Px.SupplierId IS NULL 
  union 
  select PX.PricePaperType2 as P from  itemPriceMatrix PX
  where itemid = @ItemID and PX.PricePaperType2 <> 0 and Px.SupplierId IS NULL 
  union 
  select PX.PricePaperType3 as P from  itemPriceMatrix PX
  where itemid = @ItemID and PX.PricePaperType3 <> 0 and Px.SupplierId IS NULL 
  union 
  select PX.PriceStockType4 as P from  itemPriceMatrix PX
  where itemid = @ItemID and PX.PriceStockType4 <> 0 and Px.SupplierId IS NULL 
  union 
  select PX.PriceStockType5 as P from  itemPriceMatrix PX
  where itemid = @ItemID and PX.PriceStockType5 <> 0 and Px.SupplierId IS NULL 
  union 
  select PX.PriceStockType6 as P from  itemPriceMatrix PX
  where itemid = @ItemID and PX.PriceStockType6 <> 0 and Px.SupplierId IS NULL 
  union 
  select PX.PriceStockType7 as P from  itemPriceMatrix PX
  where itemid = @ItemID and PX.PriceStockType7 <> 0 and Px.SupplierId IS NULL 
  union 
  select PX.PriceStockType8 as P from  itemPriceMatrix PX
  where itemid = @ItemID and PX.PriceStockType8 <> 0 and Px.SupplierId IS NULL 
  union 
  select PX.PriceStockType9 as P from  itemPriceMatrix PX
  where itemid = @ItemID and PX.PriceStockType9 <> 0 and Px.SupplierId IS NULL 
  union 
  select PX.PriceStockType10 as P from  itemPriceMatrix PX
  where itemid = @ItemID and PX.PriceStockType10 <> 0 and Px.SupplierId IS NULL 
  union 
  select PX.PriceStockType11 as P from  itemPriceMatrix PX
  where itemid = @ItemID and PX.PriceStockType11 <> 0 and Px.SupplierId IS NULL 
  ) as T
 end
else
 begin
  select @Result = min(P) from 
  (
  select PX.PricePaperType1 as P from  itemPriceMatrix PX
  where itemid = @ItemID and PX.PricePaperType1 <> 0 and Px.SupplierId IS NULL 
  union 
  select PX.PricePaperType2 as P from  itemPriceMatrix PX
  where itemid = @ItemID and PX.PricePaperType2 <> 0 and Px.SupplierId IS NULL 
  union 
  select PX.PricePaperType3 as P from  itemPriceMatrix PX
  where itemid = @ItemID and PX.PricePaperType3 <> 0 and Px.SupplierId IS NULL 
  union 
  select PX.PriceStockType4 as P from  itemPriceMatrix PX
  where itemid = @ItemID and PX.PriceStockType4 <> 0 and Px.SupplierId IS NULL 
  union 
  select PX.PriceStockType5 as P from  itemPriceMatrix PX
  where itemid = @ItemID and PX.PriceStockType5 <> 0 and Px.SupplierId IS NULL 
  union 
  select PX.PriceStockType6 as P from  itemPriceMatrix PX
  where itemid = @ItemID and PX.PriceStockType6 <> 0 and Px.SupplierId IS NULL 
  union 
  select PX.PriceStockType7 as P from  itemPriceMatrix PX
  where itemid = @ItemID and PX.PriceStockType7 <> 0 and Px.SupplierId IS NULL 
  union 
  select PX.PriceStockType8 as P from  itemPriceMatrix PX
  where itemid = @ItemID and PX.PriceStockType8 <> 0 and Px.SupplierId IS NULL 
  union 
  select PX.PriceStockType9 as P from  itemPriceMatrix PX
  where itemid = @ItemID and PX.PriceStockType9 <> 0 and Px.SupplierId IS NULL 
  union 
  select PX.PriceStockType10 as P from  itemPriceMatrix PX
  where itemid = @ItemID and PX.PriceStockType10 <> 0 and Px.SupplierId IS NULL 
  union 
  select PX.PriceStockType11 as P from  itemPriceMatrix PX
  where itemid = @ItemID and PX.PriceStockType11 <> 0 and Px.SupplierId IS NULL 
  ) as T

 end
 -- Return the result of the function
 RETURN @Result

END