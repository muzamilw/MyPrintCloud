CREATE PROCEDURE dbo.sp_Item_Get_Complete_ItemTitles
@SystemSiteID int
AS
	Select ID,ItemTitle,isnull(DepartmentID,0) as DepartmentID,isnull(NominalID,0) as NominalID FROM tbl_item_titles  where SystemSiteID=@SystemSiteID order by ItemTitle
	RETURN