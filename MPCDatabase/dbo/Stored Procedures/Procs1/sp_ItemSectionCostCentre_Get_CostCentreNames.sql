
Create PROCEDURE [dbo].[sp_ItemSectionCostCentre_Get_CostCentreNames]

AS
	SELECT tbl_costcentres.Name as CostCentreName,tbl_costcentres.CostCentreID FROM  tbl_costcentres 
	RETURN