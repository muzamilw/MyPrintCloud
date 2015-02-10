CREATE PROCEDURE dbo.sp_SectionFlags_Insert_Section_Flags
	(
		@CompanyID integer,
		@SectionID integer,
		@FlagName varchar(50),
		@FlagColor varchar(50),
		@flagDescription varchar(250)
		--@parameter2 datatype OUTPUT
	)
AS
	INSERT into tbl_section_flags(CompanyID,SectionID,FlagName, FlagColor, flagDescription) 
	VALUES (@CompanyID,@SectionID,@FlagName,@FlagColor,@flagDescription);Select @@Identity as SectionFlagID
	RETURN