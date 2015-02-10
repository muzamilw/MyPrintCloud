CREATE PROCEDURE dbo.sp_SectionFlags_FlagID_Used_By_Inventory_Procudcts

	(
		@flagID int
		--@parameter2 datatype OUTPUT
	)

AS
	SELECT ID from tbl_profile WHERE FlagID=@flagID
	RETURN