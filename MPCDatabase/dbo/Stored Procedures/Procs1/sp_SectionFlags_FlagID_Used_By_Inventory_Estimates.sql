
Create PROCEDURE [dbo].[sp_SectionFlags_FlagID_Used_By_Inventory_Estimates]

	(
		@flagID int
		--@parameter2 datatype OUTPUT
	)

AS
	SELECT EstimateID from tbl_estimates WHERE SectionFlagID=@flagID
	RETURN