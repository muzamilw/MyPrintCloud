CREATE PROCEDURE dbo.sp_costcentre_get_costcentreresourceswithnames
(@CostCentreID int)
AS
SELECT tbl_costcentre_resources.ResourceID,
         tbl_systemusers.UserName FROM tbl_systemusers 
         INNER JOIN tbl_costcentre_resources ON (tbl_systemusers.SystemUserID = tbl_costcentre_resources.ResourceID) 
         where tbl_costcentre_resources.costcentreid=@CostCentreID	
         RETURN