
CREATE PROCEDURE [dbo].[sp_SectionFlags_Get_Section_Flags_Table]
(@CompanyID int)
/*
	(
		@parameter1 datatype = default value,
		@parameter2 datatype OUTPUT
	)
*/
AS
	SELECT SectionFlagID, SectionID, FlagName, FlagColor, flagDescription, CompanyID, FlagColumn, isDefault
	 FROM tbl_section_flags where CompanyID = @CompanyID ORDER BY SectionFlagID
	RETURN