CREATE PROCEDURE dbo.sp_pagination_check_pagination
(@ID int,
@Code varchar(50),
@SiteID int)
AS
	select ID from tbl_pagination_profile where (ID<>@ID and Code=@Code) and SystemSiteID=@SiteID
	RETURN