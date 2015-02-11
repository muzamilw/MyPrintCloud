CREATE  PROCEDURE dbo.sp_ServerComponents_Get_SoftwareUpDates AS

select PatchTitle+' - '+PatchVersion as [Software Updates] , InstalledOnDate as [Installed On Date]
, [Description] , NoOfUserUpdated [Client Updated]
 from tbl_SoftwareUpdates  order by 2 desc