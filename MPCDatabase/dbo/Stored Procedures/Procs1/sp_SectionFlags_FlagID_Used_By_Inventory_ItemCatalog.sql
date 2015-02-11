CREATE PROCEDURE dbo.sp_SectionFlags_FlagID_Used_By_Inventory_ItemCatalog

	(
		@flagID int
		--@parameter2 datatype OUTPUT
	)

AS
	SELECT ID from tbl_finishedgoods_catalogue WHERE FlagID=@flagID
	RETURN