CREATE PROCEDURE dbo.sp_costcentregroups_insert
(@SystemSiteID int,
@GroupName varchar(100),
@Sequence int)
                  AS
insert into tbl_costcentre_groups(SystemSiteID,GroupName,Sequence) values(@SystemSiteID,@GroupName,@Sequence);
Select @@Identity as GroupID
return