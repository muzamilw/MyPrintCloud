CREATE PROCEDURE dbo.sp_products_get_costcentresgroups
(@ProductID int)
AS
	select * from tbl_profile_costcentre_groups where ProfileID=@ProductID
	RETURN