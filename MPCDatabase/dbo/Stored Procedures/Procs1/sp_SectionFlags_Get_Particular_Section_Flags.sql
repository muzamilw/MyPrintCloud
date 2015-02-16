CREATE PROCEDURE dbo.sp_SectionFlags_Get_Particular_Section_Flags

	(
		@SectionID int,
		@FromSection int,
		@CompanyID int
		
		 
		--@parameter2 datatype OUTPUT
	)

AS
	 
	 if(@FromSection=0)
		begin
			SELECT SectionFlagID,SectionID,FlagName,flagDescription,FlagColor from tbl_section_flags where (SectionID = @SectionID and CompanyID=@CompanyID) ORDER BY SectionID
		end 
	 else
		begin
			SELECT SectionFlagID,SectionID,FlagName,flagDescription,FlagColor from tbl_section_flags where (SectionID = @SectionID or SectionID =0) and CompanyID=@CompanyID ORDER BY SectionID
		end 
	  
	RETURN