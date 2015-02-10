CREATE PROCEDURE dbo.sp_Customer_Category_Update_UPDATE_CATEGORY_Image
	(
@image image,
@thumbnail image,
@contenttype varchar(50),
@category_id int
)
AS
	update tbl_finishgood_categories set image=@image,thumbnail=@thumbnail,ContentType=@contenttype where ID=@category_id

	RETURN