CREATE PROCEDURE dbo.sp_pagination_get_distinctcombination_by_pages
(
@Pages int)
AS
	Select distinct(combination) from tbl_pagination_combinations where pagination=@Pages
                RETURN