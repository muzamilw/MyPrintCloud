CREATE PROCEDURE dbo.sp_costcentregroups_delete
(
@GroupID int)
                  AS
Delete from tbl_costcentre_groupdetails where GroupID=@GroupID


return