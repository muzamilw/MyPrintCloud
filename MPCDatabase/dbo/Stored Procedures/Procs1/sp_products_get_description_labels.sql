CREATE PROCEDURE dbo.sp_products_get_description_labels
(@ProductID int)
AS
	select * from tbl_profile_description_labels where ProfileID=@ProductID
	RETURN