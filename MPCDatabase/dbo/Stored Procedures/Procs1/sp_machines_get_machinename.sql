CREATE PROCEDURE dbo.sp_machines_get_machinename
(@MachineID int,
@MachineName varchar(50),
@SiteID int)
AS
	select MachineID from tbl_machines where (MachineName=@MachineName and MachineID<>@MachineID) and SystemSiteID=@SiteID
        RETURN