CREATE PROCEDURE dbo.sp_products_get_descriptionLabelCombo
(@OptionalExtra int)
AS
	 if (@OptionalExtra = 0)
		begin
			select * from tbl_profile_description_labels_values where (ValueID <> 7);
		end
	 else
		begin
			select * from tbl_profile_description_labels_values where ValueID = 7 or valueID = 1;
		end
		
RETURN