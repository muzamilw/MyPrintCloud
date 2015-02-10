CREATE PROCEDURE dbo.sp_StockInventory_SectionInkCoverage_Item

	(
		@ItemID integer
		--@parameter2 datatype OUTPUT
	)

AS
	SELECT tbl_section_inkcoverage.ID FROM tbl_section_inkcoverage 
    WHERE (((tbl_section_inkcoverage.InkID = @ItemID)))
	RETURN