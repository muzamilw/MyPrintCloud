CREATE PROCEDURE dbo.sp_costcentregroups_check_detail
(@GroupID int,@CostCentreID int)
                  AS
select CostCentreID from tbl_costcentre_groupdetails where CostCentreID=@CostCentreID and GroupID=@GroupID
return