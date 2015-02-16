CREATE PROCEDURE dbo.sp_costcentregroups_update
(@Sequence int,
@GroupName varchar(100),
@GroupID int)
                  AS
update tbl_costcentre_groups set Sequence=@Sequence, GroupName=@GroupName where GroupID=@GroupID;
return