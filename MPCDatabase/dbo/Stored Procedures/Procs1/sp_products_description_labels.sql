CREATE PROCEDURE dbo.sp_products_description_labels
(@ProductID int)
AS
	SELECT tbl_profile_description_labels.ID,tbl_profile_description_labels.Title,tbl_profile_description_labels.ValueID, 
         tbl_profile_description_labels.ProfileID FROM tbl_profile_description_labels where tbl_profile_description_labels.ProfileID=@ProductID
	RETURN