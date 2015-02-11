CREATE PROCEDURE dbo.sp_SectionFlags_Update_Section_Flags

	(
		@SectionID int,
		@FlagName varchar(50),
		@FlagColor varchar(50),
		@flagDescription varchar(250),
		@SectionFlagID int
		--@parameter2 datatype OUTPUT
	)

AS
	 UPDATE tbl_section_flags SET SectionID=@SectionID, FlagName = @FlagName,
	 FlagColor= @FlagColor, flagDescription = @flagDescription 
	 WHERE SectionFlagID = @SectionFlagID
	RETURN