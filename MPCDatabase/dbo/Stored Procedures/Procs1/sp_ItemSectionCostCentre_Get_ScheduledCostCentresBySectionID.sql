

CREATE PROCEDURE [dbo].[sp_ItemSectionCostCentre_Get_ScheduledCostCentresBySectionID]
(
	@SectionID int
)
AS
SELECT 
tbl_costcentres.Name as CostCenterName
, tbl_section_costcentres.SectionCostcentreID
, tbl_section_costcentres.CostCentreID
, tbl_section_costcentres.SystemCostCentreType
, tbl_section_costcentres.EstimatedStartTime
, tbl_section_costcentres.EstimatedEndTime
FROM 
tbl_section_costcentres 
INNER JOIN tbl_costcentres ON (tbl_section_costcentres.CostCentreID = tbl_costcentres.CostCentreID)
Where   tbl_section_costcentres.ItemSectionID = @SectionID and  IsScheduled <> 0 
ORDER BY  tbl_section_costcentres.SectionCostcentreID 
	RETURN