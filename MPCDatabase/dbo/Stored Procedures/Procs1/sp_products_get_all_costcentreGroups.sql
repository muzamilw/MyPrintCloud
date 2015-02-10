CREATE PROCEDURE dbo.sp_products_get_all_costcentreGroups
/*
	(
		@parameter1 datatype = default value,
		@parameter2 datatype OUTPUT
	)
*/
AS
	select * from tbl_profile_costcentre_groups
	RETURN