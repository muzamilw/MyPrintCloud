CREATE PROCEDURE dbo.sp_Item_Update_ItemTitles
(

	@Title varchar(50),
	@DepartmentID int,
	@NominalID int,
	@SystemSiteID int	

)
AS

	update tbl_item_titles set DepartmentID= @DepartmentID,NominalID= @NominalID ,SystemSiteID = @SystemSiteID
	where ItemTitle = @Title

	RETURN