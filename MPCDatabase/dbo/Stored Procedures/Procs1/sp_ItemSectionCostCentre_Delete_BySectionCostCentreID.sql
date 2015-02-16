CREATE PROCEDURE dbo.sp_ItemSectionCostCentre_Delete_BySectionCostCentreID
(
	@SectionCostCentreID  int
)
AS
	Delete from tbl_section_costcentres WHERE tbl_section_costcentres.SectionCostcentreID = @SectionCostCentreID 
RETURN