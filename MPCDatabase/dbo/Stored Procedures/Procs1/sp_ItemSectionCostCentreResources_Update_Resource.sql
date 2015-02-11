CREATE PROCEDURE dbo.sp_ItemSectionCostCentreResources_Update_Resource
(
	@SectionCostCentreResourceID int,
	@SectionCostcentreID int,
	@ResourceID int,
	@ResourceTime int
	
)
AS
	Update tbl_section_costcentre_resources set SectionCostcentreID=@SectionCostcentreID,ResourceID=@ResourceID,ResourceTime=@ResourceTime where SectionCostCentreResourceID= @SectionCostCentreResourceID
	RETURN