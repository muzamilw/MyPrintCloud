CREATE PROCEDURE dbo.sp_products_get_producttype

AS
	SELECT tbl_profile_type.ID,tbl_profile_type.Name FROM tbl_profile_type Where ParentID=0
	RETURN