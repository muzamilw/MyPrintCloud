CREATE PROCEDURE dbo.sp_lookupmethod_get_byID
(@MethodID int)
AS
SELECT tbl_lookup_methods.MethodID,
         tbl_lookup_methods.Name,tbl_lookup_methods.Type FROM tbl_lookup_methods 
         WHERE  tbl_lookup_methods.MethodID = @MethodID
                 RETURN