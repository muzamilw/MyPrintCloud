CREATE PROCEDURE  dbo.sp_company_copy_getPrefix
(
	@SiteID int,
	@Section int,
	@Code varchar(100) output
	)

AS
	
	BEGIN
		
			if @Section = 1 --FinishGood
				Begin
					select @Code=FinishedGoodsPrefix + '-' + dbo.Format('0',convert(varchar(5),@SiteID),3) + '-' + convert(varchar(50),FinishedGoodsNext)	from tbl_prefixes where SystemSiteID=@SiteID					
					update tbl_prefixes  set FinishedGoodsNext=FinishedGoodsNext+1  where SystemSiteID=@SiteID						
				End
			
			if @Section = 2 -- Inventory
				Begin
					select @Code=StockItemPrefix + '-' + dbo.Format('0',convert(varchar(5),@SiteID),3) + '-' + convert(varchar(50),StockItemNext)	from tbl_prefixes where SystemSiteID=@SiteID						
					update tbl_prefixes  set StockItemNext=StockItemNext+1  where SystemSiteID=@SiteID						
				End
			
			if @Section = 3 -- Items
				Begin
					select @Code=ITEMPrefix + '-' + dbo.Format('0',convert(varchar(5),@SiteID),3)+ '-' + convert(varchar(50),ITEMNext)	from tbl_prefixes where SystemSiteID=@SiteID
					update tbl_prefixes  set ITEMNext=ITEMNext+1  where SystemSiteID=@SiteID						
				End		
			
		
				
	END