
create PROCEDURE [dbo].[sp_CostCentreExecution_get_StockPriceByCalculationType]
(
	@StockID int,
	@CalculationType int,
	@returnPrice float output,
	@PerQtyQty float output
)
AS
if (@CalculationType = 2) 
	Begin
		select top 1 
	@returnPrice =  (dt.packcostprice + dt.processingcharge * tbl_stockitems.PackageQty)
	from tbl_stock_cost_and_price as  dt 
	inner join tbl_stockitems on (dt.itemid = tbl_stockitems.itemid)
	where dt.costorpriceidentifier <> 0 and dt.itemid=  @StockID 
	order by fromdate, todate desc
	
		if (@@rowcount = 0)
		begin
			select top 1 
	@returnPrice = (dt.packcostprice + dt.processingcharge * tbl_stockitems.PackageQty)  
	from tbl_stock_cost_and_price as  dt 
	inner join tbl_stockitems on (dt.itemid = tbl_stockitems.itemid)
	where dt.costorpriceidentifier = 0 and dt.itemid=  @StockID 
	order by fromdate, todate desc
		end
 		select @perQtyQty = tbl_stockitems.PerQtyQty from tbl_stockitems where itemid = @Stockid
		return  
	end
else
	begin
		select top 1 
	@returnPrice = (dt.costprice / tbl_stockitems.perqtyqty + dt.processingcharge ) 
	from tbl_stock_cost_and_price as  dt 
	inner join tbl_stockitems on (dt.itemid = tbl_stockitems.itemid)
	where dt.costorpriceidentifier <> 0 and dt.itemid=  @StockID 
	order by fromdate, todate desc
	
	if (@@rowcount = 0)
		begin
			select top 1 
	@returnPrice = (dt.costprice / tbl_stockitems.perqtyqty + dt.processingcharge )  
	from tbl_stock_cost_and_price as  dt 
	inner join tbl_stockitems on (dt.itemid = tbl_stockitems.itemid)
	where dt.costorpriceidentifier = 0 and dt.itemid=  @StockID 
	order by fromdate, todate desc
		end 
		
		select @perQtyQty = tbl_stockitems.PerQtyQty from tbl_stockitems where itemid = @Stockid
		return 

	end