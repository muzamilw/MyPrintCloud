CREATE PROCEDURE dbo.sp_ItemSectionCostCentreDetail_Delete_DetailsBySectionCostCentreID
(
	@SectionCostCentreID int
)
AS
	delete from tbl_section_costcentre_detail where SectionCostCentreID=@SectionCostCentreID
	RETURN