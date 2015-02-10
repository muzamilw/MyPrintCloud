CREATE PROCEDURE dbo.sp_Scheduling_update_CostCenterFlags

	(
	
		@SectionCostcentreID int ,
		@IsScheduled int 

	)

AS
update      tbl_section_costcentres  set    tbl_section_costcentres  .  IsScheduled   = @IsScheduled 
  where     tbl_section_costcentres  .  SectionCostcentreID   =@SectionCostcentreID

	RETURN