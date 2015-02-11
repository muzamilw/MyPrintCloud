CREATE PROCEDURE dbo.sp_ItemSectionCostCentre_Get_SectionsByStatus
(
	@Status int

)
AS
	SELECT  tbl_items.ItemCode,
tbl_section_costcentres.SystemCostCentreType,
tbl_section_costcentres.EstimatedDuration 
,tbl_section_costcentres.SectionCostcentreID 
,tbl_estimates.Estimate_Name, tbl_estimates.Estimate_Code,tbl_customers.CustomerName,
tbl_items.Title ,tbl_item_sections.SectionName
FROM tbl_customers
INNER JOIN tbl_estimates ON (tbl_customers.CustomerID = tbl_estimates.Customer_ID)
INNER JOIN tbl_items ON (tbl_estimates.Estimate_ID = tbl_items.EstimateID)
INNER JOIN tbl_item_sections ON (tbl_items.ItemID = tbl_item_sections.ItemID)
INNER JOIN tbl_section_costcentres ON (tbl_item_sections.ItemSectionID = tbl_section_costcentres.SectionID)
INNER JOIN tbl_costcentres ON (tbl_section_costcentres.CostCentreID = tbl_costcentres.ID)
where  tbl_items.Status=@Status and tbl_section_costcentres.SystemCostCentreType<>6 and tbl_section_costcentres.SystemCostCentreType<>8 
	RETURN