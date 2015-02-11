CREATE PROCEDURE dbo.sp_costcentre_add_categories
(@TypeName varchar(50),@CompanyID int)
AS
				insert into    tbl_costcentretypes (TypeName, IsSystem, IsExternal, CompanyID) 
									values (@TypeName, 0, 1, @CompanyID)

        
                 RETURN