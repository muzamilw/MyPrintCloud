CREATE PROCEDURE dbo.sp_products_check_productName
(@ProductID int,
@ProductName varchar(50),
@SiteID int)
AS
	Select ID from tbl_profile where (tbl_profile.Name=@ProductName and ID<>@ProductID) and SystemSiteID=@SiteID
	RETURN