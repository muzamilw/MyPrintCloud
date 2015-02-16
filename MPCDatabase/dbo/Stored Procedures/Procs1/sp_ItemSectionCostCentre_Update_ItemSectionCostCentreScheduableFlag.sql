CREATE PROCEDURE dbo.sp_ItemSectionCostCentre_Update_ItemSectionCostCentreScheduableFlag
(
	@SectionCostcentreID int,
	@IsScheduleable smallint
)
AS
	Update tbl_section_costcentres set tbl_section_costcentres.IsScheduleable =@IsScheduleable where tbl_section_costcentres.SectionCostcentreID = @SectionCostcentreID 
RETURN