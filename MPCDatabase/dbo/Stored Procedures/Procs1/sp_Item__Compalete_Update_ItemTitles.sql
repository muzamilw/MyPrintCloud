CREATE PROCEDURE dbo.sp_Item__Compalete_Update_ItemTitles
(
@ID int,
	@Title varchar(50),
	@DepartmentID int,
	@NominalID int	,
	@SystemSiteID int

)
AS

	update tbl_item_titles set ItemTitle = @Title,DepartmentID= @DepartmentID,NominalID= @NominalID ,SystemSiteID=@SystemSiteID
	where ID=@ID

	RETURN