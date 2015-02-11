CREATE PROCEDURE dbo.sp_copy_inkCoverage
(@OldSite int,
@SiteID int)
                  AS
		
		
		Declare @OldID int
		Declare @NewID int
		
		Declare InkCursor Cursor for SELECT     CoverageGroupID
		FROM         tbl_ink_coverage_groups	where SystemSiteID=@OldSite
		
		
		Open InkCursor
		FETCH NEXT from InkCursor into @OldID
		
			WHILE @@FETCH_STATUS=0
			BEGIN
					--Adding Ink Coverage To New Site
					insert into tbl_ink_coverage_groups SELECT     GroupName, Percentage, IsFixed, @SiteID
					FROM         tbl_ink_coverage_groups	where CoverageGroupID=@OldID
					
					insert into #InkCoverageTempTable values (@OldID,@NewID)
		
					FETCH NEXT from InkCursor into @OldID
			END
		Close InkCursor
		Deallocate InkCursor
		
return