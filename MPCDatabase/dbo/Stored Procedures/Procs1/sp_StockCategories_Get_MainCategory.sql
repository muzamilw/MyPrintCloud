CREATE PROCEDURE dbo.sp_StockCategories_Get_MainCategory

	(
		@CompanyID integer
		--@parameter2 datatype OUTPUT
	)

AS
	SELECT tbl_stockcategories.CategoryID,tbl_stockcategories.ItemCoated,tbl_stockcategories.Code, tbl_stockcategories.Name, tbl_stockcategories.Description, tbl_stockcategories.fixed,  tbl_stockcategories.ItemWeight,
               tbl_stockcategories.ItemColour, tbl_stockcategories.ItemSizeCustom, tbl_stockcategories.ItemPaperSize,  tbl_stockcategories.ItemCoatedType,
               tbl_stockcategories.ItemExposure, tbl_stockcategories.TaxID,tbl_stockcategories.ItemCharge, tbl_stockcategories.recLock,  tbl_stockcategories.Flag1,  tbl_stockcategories.Flag2,  tbl_stockcategories.Flag3,
               tbl_stockcategories.Flag4 FROM tbl_stockcategories where CompanyID=@CompanyID 
	RETURN