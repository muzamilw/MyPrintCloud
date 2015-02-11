CREATE PROCEDURE dbo.sp_ItemSectionCostCentre_Get_ItemSectionCostCentresBySectionCostCentreID
(
@SectionCostcentreID int
)
AS
	SELECT * FROM tbl_section_costcentres where SectionCostcentreID=@SectionCostcentreID
	RETURN