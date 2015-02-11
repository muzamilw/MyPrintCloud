CREATE PROCEDURE dbo.sp_FinishedGoods_Update_FinishedGood

	(
		@ID int,
		@Description1 ntext,
		@Description2 ntext,
		@Description3 ntext,
		@Thumbnail image,
		@Image image,
		@IsForWeb smallint,
		@InStock int,
		@Allocated int,
		@IsShowStockOnWeb smallint,
		@ThresholdLevel int,
		@ThresholdProductionQuantity int,
		@Location varchar(50),
		@SupplierID int,
		@SupplierCode varchar(50),
		@ProductCode varchar(50),
		@NominalCode varchar(50),
		@BarCode varchar(50),
		@FinishedGoodCode varchar(50),
		@TaxID int,
		@RangeThresholdLevel int,
		@FromThresholdDate datetime,
		@ToThresholdDate datetime,
		@Cost float,
		@UnitQuantity int,
		@PackQuantity int,
		@RangeThresholdLevel1 int,
		@FromThresholdDate1 datetime,
		@ToThresholdDate1 datetime,
		@RangeThresholdLevel2 int,
		@FromThresholdDate2 datetime,
		@ToThresholdDate2 datetime,
		@File1 image,
		@File2 image,
		@ContentType varchar(50),
		@IsShowAllocatedStockOnWeb smallint,
		@IsShowPriceOnWeb smallint,
		@IsShowFreeStockOnWeb smallint,
		@IsDisabled smallint,
		@SiteDepartmentID int

				
	)

AS
	/* SET NOCOUNT ON */
	
	UPDATE tbl_finishedgoods set Description1=@Description1,Description2=@Description2,Description3=@Description3, Thumbnail=@Thumbnail, 
	[Image]=@Image,IsForWeb=@IsForWeb,InStock=@InStock,Allocated=@Allocated,IsShowStockOnWeb=@IsShowStockOnWeb,
	ThresholdLevel=@ThresholdLevel,ThresholdProductionQuantity=@ThresholdProductionQuantity,
	Location=@Location,SupplierID=@SupplierID,SupplierCode=@SupplierCode,ProductCode=@ProductCode,NominalCode=@NominalCode,BarCode=@BarCode,
	FinishedGoodCode=@FinishedGoodCode,TaxID=@TaxID,RangeThresholdLevel=@RangeThresholdLevel,FromThresholdDate=@FromThresholdDate,
	ToThresholdDate=@ToThresholdDate,Cost=@Cost,UnitQuantity=@UnitQuantity,PackQuantity=@PackQuantity,RangeThresholdLevel1=@RangeThresholdLevel1,
	FromThresholdDate1=@FromThresholdDate1,ToThresholdDate1=@ToThresholdDate1,RangeThresholdLevel2=@RangeThresholdLevel2,FromThresholdDate2=@FromThresholdDate2,
	ToThresholdDate2=@ToThresholdDate2,File1=@File1,File2=@File2,ContentType=@ContentType,IsShowAllocatedStockOnWeb=@IsShowAllocatedStockOnWeb, 
	IsShowPriceOnWeb=@IsShowPriceOnWeb, IsShowFreeStockOnWeb=@IsShowFreeStockOnWeb,IsDisabled =@IsDisabled,SiteDepartmentID=@SiteDepartmentID  where ID=@ID
	
	RETURN