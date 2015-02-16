CREATE PROCEDURE dbo.sp_ItemSectionCostCentreResources_Insert_Resource
(
	@SectionCostcentreID int,
	@ResourceID int,
	@ResourceTime int
)
AS
	insert into tbl_section_costcentre_resources (SectionCostcentreID,ResourceID,ResourceTime) VALUES (@SectionCostcentreID,@ResourceID,@ResourceTime)
RETURN