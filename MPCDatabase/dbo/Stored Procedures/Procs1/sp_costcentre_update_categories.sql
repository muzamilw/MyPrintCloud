CREATE PROCEDURE dbo.sp_costcentre_update_categories
(@TypeName varchar(50),@TypeID int)
AS
				update tbl_costcentretypes set TypeName=@TypeName where TypeID=@TypeID
                 RETURN