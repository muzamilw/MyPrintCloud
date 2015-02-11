CREATE PROCEDURE dbo.sp_StockCategories_Add_Category

	(
		@CompanyID integer,
		@Fixed smallint,
		@ItemCoated smallint,
		@Code varchar(5),
		@Name varchar(50),
		@Description varchar(255),
		@ItemWeight smallint,
		@ItemColour smallint,
		@ItemSizeCustom smallint,
		@ItemPaperSize smallint,
		@ItemCoatedType smallint,
		@ItemExposure smallint,
		@ItemCharge smallint,
		@TaxID integer,
		@Flag1 smallint,
		@Flag2 smallint,
		@Flag3 smallint,
		@Flag4 smallint
		--@parameter2 datatype OUTPUT
	)

AS
	INSERT into tbl_stockcategories (CompanyID,Fixed,ItemCoated,Code,Name,Description,
	ItemWeight,ItemColour,ItemSizeCustom,ItemPaperSize,ItemCoatedType,ItemExposure,
	ItemCharge,TaxID,Flag1,Flag2,Flag3,Flag4) VALUES
    (@CompanyID,@Fixed,@ItemCoated,@Code,@Name,@Description,@ItemWeight,@ItemColour,
    @ItemSizeCustom,@ItemPaperSize,@ItemCoatedType,@ItemExposure,@ItemCharge,@TaxID,
    @Flag1,@Flag2,@Flag3,@Flag4)
	RETURN