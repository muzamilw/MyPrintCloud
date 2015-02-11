CREATE PROCEDURE dbo.sp_costcentregroups_insert_detail
(@GroupID int,
@CostCentreID int)
                  AS
insert into tbl_costcentre_groupdetails(GroupID,CostCentreID) values(@GroupID,@CostCentreID)
return