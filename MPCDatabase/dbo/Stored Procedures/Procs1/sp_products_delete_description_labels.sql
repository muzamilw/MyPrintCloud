CREATE PROCEDURE dbo.sp_products_delete_description_labels
(@ProductID int)
AS
	delete from tbl_profile_description_labels where tbl_profile_description_labels.ProfileID=@ProductID
	RETURN