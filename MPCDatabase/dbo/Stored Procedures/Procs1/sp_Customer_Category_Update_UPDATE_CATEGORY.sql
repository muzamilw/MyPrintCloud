CREATE PROCEDURE dbo.sp_Customer_Category_Update_UPDATE_CATEGORY
	(


		@category_id int,
		@category_name varchar(50),
		@description1 varchar(255),
		@description2 varchar(255)
		
	)
AS
	update tbl_finishgood_categories set ItemLibrarayGroupName=@category_name,description1=@description1,description2=@description2 where ID=@category_id
	RETURN