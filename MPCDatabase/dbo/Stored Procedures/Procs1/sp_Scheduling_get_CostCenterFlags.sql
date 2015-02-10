CREATE PROCEDURE dbo.sp_Scheduling_get_CostCenterFlags

	(
		@SectionCostcentreID int
		
	)

AS
	Select     tbl_section_costcentres.IsScheduled    from     tbl_section_costcentres      
     where      tbl_section_costcentres.SectionCostcentreID    =@SectionCostcentreID
	RETURN