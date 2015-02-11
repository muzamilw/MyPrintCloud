CREATE PROCEDURE dbo.sp_products_delete_productByID
(@ProductID int)
AS
	
	declare @Result int
	Declare @SubResult int
		
		Set @Result = 1
		Set @SubResult = 0
		
		Select @SubResult = ProfileID from tbl_item_sections where ProfileID=@ProductID
			
			if @SubResult > 0 
				set	@Result = 0
		
	if @Result = 1 
		Begin
			delete from tbl_profile where ID=@ProductID
			delete from tbl_profile_description_labels where tbl_profile_description_labels.ProfileID=@ProductID
		End
		
	Select @Result	

RETURN