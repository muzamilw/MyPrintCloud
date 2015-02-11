CREATE PROCEDURE dbo.sp_Item_Add_Complete_ItemTitle
(
@Title varchar(50),
@DepartmentID int,
@NominalID int,
@SystemSiteID int
)
AS
	insert into tbl_item_titles (ItemTitle,DepartmentID,NominalID,SystemSiteID) Values (@Title,@DepartmentID,@NominalID,@SystemSiteID)
	
RETURN