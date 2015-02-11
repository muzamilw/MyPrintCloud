CREATE PROCEDURE dbo.sp_lookupmethod_insert
(@Name varchar(50),
@Type int,
@SystemSiteID int)
AS
insert into tbl_lookup_methods (Name,Type,SystemSiteID) VALUES (@Name,@Type,@SystemSiteID);
Select @@Identity as LookUpID
        
                 RETURN