CREATE PROCEDURE dbo.sp_products_insert_description_label
(@Title varchar(50),
@ValueID int,
@ProductID int)
AS
	insert into tbl_profile_description_labels (Title,ValueID,ProfileID) VALUES (@Title,@ValueID,@ProductID)
	RETURN