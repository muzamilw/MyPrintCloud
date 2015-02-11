CREATE PROCEDURE dbo.sp_lookupmethod_check_byName
(@MethodID int,
@Name varchar(50),@SiteID int)
AS
select tbl_lookup_methods.MethodID FROM tbl_lookup_methods 
         WHERE  (tbl_lookup_methods.Name = @Name and  tbl_lookup_methods.MethodID <> @MethodID) and SystemSiteID=@SiteID
                 RETURN