
CREATE PROCEDURE [dbo].[sp_costcentre_get_systemcostcentrebysiteid]
(@SystemTypeID int,@SystemSiteID int)
AS
SELECT tbl_costcentres.IsScheduleable,tbl_costcentres.CostCentreID,tbl_costcentres.Name,tbl_costcentres.Description,
         tbl_costcentres.Type,tbl_costcentres.nominalCode,tbl_costcentres.IsDisabled,tbl_costcentres.IsDirectCost,tbl_costcentres.MinimumCost,
         tbl_costcentres.SetupCost,tbl_costcentres.SetupTime,tbl_costcentres.DefaultVAId,
         tbl_costcentres.SetupSpoilage,tbl_costcentres.RunningSpoilage
          FROM tbl_costcentres where SystemTypeID=@SystemTypeID and SystemSiteID=@SystemSiteID
	RETURN