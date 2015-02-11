CREATE PROCEDURE [dbo].[usp_DeleteCartItem]
		
		@ItemID	numeric

AS
BEGIN
		declare @SectionID numeric,
				@EstimateID	numeric
		Select	@SectionID = ItemSectionID
		from	tbl_item_sections 
		where	ItemID = @ItemID
		--delete cost centres
		delete from tbl_section_costcentres 
		where ItemSectionID = @SectionID
		--delete sections
		delete from tbl_item_sections 
		where ItemID = @ItemID
		--delete item
		select @EstimateID = EstimateID from tbl_items
		where ItemID = @ItemID
		delete from tbl_items 
		where ItemID = @ItemID
		--delete estimate
		delete from tbl_estimates
		where EstimateID = @EstimateID
		
END