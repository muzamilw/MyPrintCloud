
create PROCEDURE [dbo].[sp_costcentregroups_get_detail]
(@GroupID int)
                  AS
SELECT     tbl_costcentre_groupdetails.CostCentreID, tbl_costcentres.Name, tbl_costcentre_groups.Sequence
FROM         tbl_costcentre_groupdetails INNER JOIN
                      tbl_costcentres ON tbl_costcentres.CostCentreID = tbl_costcentre_groupdetails.CostCentreID INNER JOIN
                      tbl_costcentre_groups ON tbl_costcentre_groupdetails.GroupID = tbl_costcentre_groups.GroupID
WHERE     (tbl_costcentre_groupdetails.GroupID = @GroupID)
return