CREATE PROCEDURE dbo.sp_lookupmethod_update
(@Name varchar (50),
@Type int,
@MethodID int)
AS
update tbl_lookup_methods set Name=@Name,Type=@Type where MethodID=@MethodID
        
                 RETURN