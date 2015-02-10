CREATE PROCEDURE dbo.sp_costcentregroups_check_name
(@GroupID int,@GroupName varchar(100),@SiteID as int)
                  AS
select GroupID from tbl_costcentre_groups where (GroupName=@GroupName and GroupID<>@GroupID) and SystemSiteID=@SiteID
return