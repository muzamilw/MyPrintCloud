CREATE PROCEDURE dbo.sp_ItemSectionCostCentreResources_Delete_ResourcesbySectioncostCentreID
(
	@SectionCostcentreID int
)
AS
	Delete from tbl_section_costcentre_resources where SectionCostcentreID = @SectionCostcentreID
	RETURN