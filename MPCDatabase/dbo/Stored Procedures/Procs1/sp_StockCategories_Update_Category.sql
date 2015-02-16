CREATE PROCEDURE dbo.sp_StockCategories_Update_Category

	(
		@CategoryID integer,
		@ItemCoated integer,
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
	update tbl_stockcategories set ItemCoated=@ItemCoated,Code=@Code,Name=@Name,
	Description=@Description,ItemWeight=@ItemWeight, ItemColour=@ItemColour,
	ItemSizeCustom=@ItemSizeCustom,ItemPaperSize=@ItemPaperSize,
	ItemCoatedType=@ItemCoatedType,ItemExposure=@ItemExposure, ItemCharge=@ItemCharge,
	TaxID=@TaxID,Flag1=@Flag1,Flag2=@Flag2,Flag3=@Flag3,Flag4=@Flag4 where CategoryID=@CategoryID
	RETURN