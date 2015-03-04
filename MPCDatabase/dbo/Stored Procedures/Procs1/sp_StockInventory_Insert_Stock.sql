﻿
CREATE PROCEDURE [dbo].[sp_StockInventory_Insert_Stock]
	(
	
@ItemCode varchar(50),
@ItemName varchar(255),
@categoryID integer,
@SubCategory integer,
@status integer,
@StockLocation varchar(50),
@LastModifiedDateTime Datetime,
@LastModifiedBy integer,
@StockCreated datetime,
@SupplierID integer,
@ItemWeight integer,
@ItemColour varchar(50),
@ItemSizeCustom smallint,
@ItemSizeID integer,
@ItemSizeHeight float,
@ItemSizeWidth float,
@ItemSizeDim integer,
@ItemUnitSize integer,
@CostPrice float,
@StockLevel integer,
@PackageQty float,
@ReOrderLevel float,
@ItemCoatedType smallint,
@ItemCoated smallint,
@ItemExposure smallint,
@ItemExposureTime integer,
@ItemProcessingCharge float, 
@PerQtyRate float,
@PerQtyQty float,
@ItemDescription varchar(255),
@LockedBy integer,
@ReorderQty integer,
@inStock integer,
@UnitRate float,
@Tax integer,
@ItemSizeSelectedUnit smallint,
@ItemWeightSelectedUnit smallint,
@LastOrderDate datetime=null,
@LastOrderQty integer,
@ItemType varchar(50),
@FlagID integer,
@InkAbsorption float,
@WashupCounter integer,
@InkYield float,
@PaperBasicAreaID integer,
@PaperType integer,
@PerQtyType integer,
@RollWidth float,
@RollLength float,
@RollStandards integer,
@DepartmentID integer,
@InkYieldStandards integer,
@PerQtyPrice float,
@PackPrice float,
@Region varchar(10),
@isDisabled smallint,
@AlternateName varchar(255),
@InkStandards integer,
@BarCode varchar(100),
@Image image,
@Thumbnail image
--@ItemID integer,
		
--@Identity integer OUTPUT
	)

AS
	INSERT into tbl_stockitems (ItemCode,ItemName,categoryID,SubCategoryID,
status,StockLocation,LastModifiedDateTime,LastModifiedBy,StockCreated,SupplierID,ItemWeight,ItemColour, 
ItemSizeCustom,ItemSizeID,ItemSizeHeight,ItemSizeWidth,ItemSizeDim,ItemUnitSize,CostPrice,StockLevel,
PackageQty,ReOrderLevel,ItemCoatedType, ItemCoated, ItemExposure,ItemExposureTime,ItemProcessingCharge,
PerQtyRate,PerQtyQty,ItemDescription,LockedBy,ReorderQty,inStock,UnitRate,TaxID,ItemSizeSelectedUnit, 
ItemWeightSelectedUnit,LastOrderDate,LastOrderQty, ItemType, FlagID, InkAbsorption,WashupCounter,InkYield, 
PaperBasicAreaID,PaperType,PerQtyType,RollWidth,RollLength,RollStandards, DepartmentID, InkYieldStandards, PerQtyPrice, PackPrice, Region, isDisabled, AlternateName, InkStandards, BarCode,Image)
--, Image, Thumbnail)
VALUES (@ItemCode,@ItemName,@categoryID,@SubCategory,@status,@StockLocation,@LastModifiedDateTime,
@LastModifiedBy,@StockCreated,@SupplierID,@ItemWeight,@ItemColour,@ItemSizeCustom,@ItemSizeID,
@ItemSizeHeight,@ItemSizeWidth,@ItemSizeDim,@ItemUnitSize,@CostPrice,@StockLevel,
@PackageQty,@ReOrderLevel,@ItemCoatedType, @ItemCoated,@ItemExposure,@ItemExposureTime,@ItemProcessingCharge, 
@PerQtyRate,@PerQtyQty,@ItemDescription, @LockedBy,@ReorderQty,@inStock,@UnitRate,@Tax,@ItemSizeSelectedUnit,
@ItemWeightSelectedUnit,@LastOrderDate,@LastOrderQty, @ItemType, @FlagID, @InkAbsorption,@WashupCounter,@InkYield,
@PaperBasicAreaID,@PaperType,@PerQtyType,@RollWidth,@RollLength,@RollStandards,@DepartmentID, @InkYieldStandards, @PerQtyPrice, @PackPrice, @Region, @isDisabled, @AlternateName, @InkStandards, @BarCode,NULL);
--, @Image, @Thumbnail); 
select @@Identity
	RETURN