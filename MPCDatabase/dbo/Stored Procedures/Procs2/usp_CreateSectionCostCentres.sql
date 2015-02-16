CREATE PROCEDURE [dbo].[usp_CreateSectionCostCentres]
		@SectionID	numeric,
		@CustomerName varchar(500),
		@QtyCharge numeric(18,2),
		@MarkupID	int = 30,
		@CostCenterID numeric
		
AS
BEGIN
		declare @MarkupValue	float,
				@QtyNetTotal	float
				
		select @MarkupValue = MarkUpRate from tbl_markup where MarkUpID = @MarkupID
		set @QtyNetTotal = @QtyCharge + @MarkupValue
		
		insert into tbl_section_costcentres
			(ItemSectionID, CostCentreID, Qty1Charge, Qty1MarkUpID, Qty1MarkUpValue, 
			Qty1NetTotal, Locked)
			values (@SectionID, @CostCenterID, @QtyCharge, @MarkupID, @MarkupValue, @QtyNetTotal, 0)
		
		select SCOPE_IDENTITY()
		
END