CREATE PROCEDURE [dbo].[sp_StockInventory_GET_STOCK_BY_STOCK_ID]

	(
		@ItemID integer
		--@parameter2 datatype OUTPUT
	)

AS
	 SELECT tbl_stockitems.StockItemID, tbl_stockitems.ItemCode, tbl_stockitems.ItemName,
	 tbl_stockitems.AlternateName, tbl_stockitems.ItemWeight, 
	 tbl_stockitems.ItemColour, tbl_stockitems.ItemSizeCustom,
     tbl_stockitems.ItemSizeID, tbl_stockitems.ItemSizeHeight, 
     tbl_stockitems.ItemSizeWidth,tbl_stockitems.TaxID, 
     tbl_stockitems.ItemSizeDim, tbl_stockitems.ItemUnitSize, 
     tbl_stockitems.SupplierID, tbl_stockitems.CostPrice,
     tbl_stockitems.CategoryID, tbl_stockitems.SubCategoryID, 
     tbl_stockitems.LastModifiedDateTime, tbl_stockitems.LastModifiedBy,
     tbl_stockitems.StockLevel, tbl_stockitems.PackageQty, 
     tbl_stockitems.Status, tbl_stockitems.ReOrderLevel, 
     tbl_stockitems.StockLocation, tbl_stockitems.ItemCoatedType,
     tbl_stockitems.ItemCoated , tbl_stockitems.ItemExposure, 
     tbl_stockitems.ItemExposureTime, tbl_stockitems.ItemProcessingCharge,
     tbl_stockitems.ItemType, tbl_stockitems.StockCreated, 
     tbl_stockitems.PerQtyRate, tbl_stockitems.PerQtyQty, 
     tbl_stockitems.ItemDescription, tbl_stockitems.LockedBy, 
     tbl_stockitems.ReorderQty, tbl_stockitems.LastOrderQty, tbl_stockitems.LastOrderDate,
     tbl_stockitems.inStock,  tbl_stockitems.onOrder, tbl_stockitems.Allocated,
     tbl_stockitems.UnitRate,tbl_stockitems.ItemSizeSelectedUnit, 
     tbl_stockitems.ItemWeightSelectedUnit,tbl_stockitems.flagID,
     tbl_stockitems.InkAbsorption,tbl_stockitems.WashupCounter,
     tbl_stockitems.InkYield ,tbl_stockitems.PaperBasicAreaID,
     tbl_stockitems.PaperType,tbl_stockitems.PerQtyType,tbl_stockitems.RollWidth ,
     tbl_stockitems.RollLength , tbl_stockitems.RollStandards,
     tbl_stockitems.InkYieldStandards, tbl_stockitems.PerQtyPrice,
     tbl_stockitems.PackPrice,tbl_stockitems.Region, tbl_stockitems.isDisabled,
     tbl_stockitems.InkStandards, tbl_stockitems.BarCode,tbl_stockitems.Image,
      tbl_contactcompanies.Name 
     FROM tbl_stockitems left outer JOIN tbl_contactcompanies ON (tbl_stockitems.SupplierID = tbl_contactcompanies.contactcompanyid )
     WHERE tbl_stockitems.StockItemID = @ItemID

	RETURN