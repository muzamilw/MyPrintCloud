

CREATE PROCEDURE [dbo].[sp_ItemSectionCostCentre_Get_CostCentresBySectionID]
(
	@SectionID int
)
AS
SELECT tbl_costcentres.Name as CostCenterName
, tbl_section_costcentres.SectionCostcentreID
, tbl_section_costcentres.CostCentreID
, tbl_section_costcentres.SystemCostCentreType
, tbl_section_costcentres.IsScheduleable
, tbl_section_costcentres.SetupTime
, (tbl_section_costcentres.Qty1EstimatedTime - tbl_section_costcentres.SetupTime) as Qty1EstimatedTime
, (tbl_section_costcentres.Qty2EstimatedTime - tbl_section_costcentres.SetupTime) as Qty2EstimatedTime
, (tbl_section_costcentres.Qty3EstimatedTime - tbl_section_costcentres.SetupTime) as Qty3EstimatedTime
, tbl_section_costcentres.Qty1WorkInstructions
, tbl_section_costcentres.Qty2WorkInstructions
, tbl_section_costcentres.Qty3WorkInstructions
, tbl_section_costcentres.EstimatedStartTime
, tbl_section_costcentres.EstimatedEndTime
, IsScheduled 
FROM 
tbl_section_costcentres 
INNER JOIN tbl_costcentres ON (tbl_section_costcentres.CostCentreID = tbl_costcentres.CostCentreID)
Where   tbl_section_costcentres.ItemSectionID = @SectionID and tbl_section_costcentres.SystemCostCentreType <> 6 
and tbl_section_costcentres.SystemCostCentreType <> 8  and IsScheduled = 0  and tbl_costcentres.IsScheduleable <> 0
ORDER BY  tbl_section_costcentres.SectionCostcentreID 
	RETURN