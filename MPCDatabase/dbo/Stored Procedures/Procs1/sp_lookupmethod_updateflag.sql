CREATE PROCEDURE dbo.sp_lookupmethod_updateflag
(@MethodID int,@FlagID int)
AS
update tbl_lookup_methods set FlagID=@FlagID where MethodID=@MethodID
                 RETURN