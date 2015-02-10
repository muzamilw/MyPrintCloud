CREATE PROCEDURE dbo.sp_product_get_productsubtypes

AS
	SELECT tbl_profile_type.ID,tbl_profile_type.Name,tbl_profile_type.ParentID FROM tbl_profile_type Where ParentID<>0
	RETURN