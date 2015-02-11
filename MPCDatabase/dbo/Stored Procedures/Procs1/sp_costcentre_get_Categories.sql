CREATE PROCEDURE dbo.sp_costcentre_get_Categories
(@CompanyID int)
AS
SELECT     TypeID, TypeName, IsSystem, IsExternal, CompanyID
FROM         tbl_costcentretypes where CompanyID=@CompanyID and IsSystem=0 
        
                 RETURN