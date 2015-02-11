CREATE PROCEDURE dbo.sp_costcentre_delete_categories
(@TypeID int)
AS
				delete from tbl_costcentretypes where TypeID=@TypeID
                 RETURN