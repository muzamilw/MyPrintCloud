CREATE PROCEDURE dbo.sp_Customer_Category_InsertCategory
	(
		@category_name varchar(255),
		@image image,
		@thumbnail image,
		@contenttype varchar(50),
		@description1 varchar(255),
		@description2 varchar(255),
		@CustomerID int,
		@parentid int
    )
as

	insert into tbl_finishgood_categories (ItemLibrarayGroupName,Image,Thumbnail,ContentType,Description1,Description2,CustomerID,ParentID)
	 VALUES(@category_name,@image,@thumbnail,@contenttype,@description1,@description2,@CustomerID,@parentid);Select @@Identity as CatID 
    
   

	RETURN