
create PROCEDURE [dbo].[sp_SystemCostCentres_Get_SystemCostCentreIDByTypeID]
(
	@CompanyID int,
	@SystemTypeID int
)
AS
	SELECT  tbl_costcentres.CostCentreID, tbl_costcentres.Name, tbl_costcentres.CompanyID, tbl_costcentres.SystemTypeID 
	FROM tbl_costcentres 
	where tbl_costcentres.SystemTypeID = @SystemTypeID and   tbl_costcentres.SystemSiteId=@CompanyID 
	RETURN