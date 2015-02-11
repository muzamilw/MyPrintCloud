CREATE PROCEDURE dbo.sp_pagination_get_combination_by_pages
(
@Pages int)
AS
	Select * from tbl_pagination_combinations where pagination=@Pages
                RETURN