CREATE PROCEDURE dbo.sp_pagination_get_paginationByID
(@ID int)
AS
	Select * from tbl_pagination_profile_costcentre_groups where PaginationID=@ID
	RETURN