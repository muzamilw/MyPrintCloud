
create PROCEDURE [dbo].[sp_ItemSectionCostCentre_Get_SectionCostCentreIdsBySectionID]
(
	@SectionID int
)
AS
	SELECT tbl_section_costcentres.SectionCostcentreID FROM  tbl_section_costcentres 
	WHERE tbl_section_costcentres.ItemSectionID = @SectionID
	RETURN