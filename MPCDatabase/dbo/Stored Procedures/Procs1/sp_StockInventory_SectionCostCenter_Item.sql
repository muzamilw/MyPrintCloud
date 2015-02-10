CREATE PROCEDURE dbo.sp_StockInventory_SectionCostCenter_Item

	(
		@ItemID integer
		--@parameter2 datatype OUTPUT
	)

AS
	SELECT tbl_section_costcentre_detail.SectionCostCentreDetailId 
	FROM tbl_section_costcentre_detail
	WHERE (((tbl_section_costcentre_detail.StockID = @ItemID)))
	RETURN