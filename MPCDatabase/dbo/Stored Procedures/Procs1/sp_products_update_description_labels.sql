CREATE PROCEDURE dbo.sp_products_update_description_labels
(@Title varchar(50),
@ValueID int,
@ProductID int,
@ID int)
AS
	update tbl_profile_description_labels set Title=@Title,ValueID=@ValueID,ProfileID=@ProductID where ID=@ID
	RETURN