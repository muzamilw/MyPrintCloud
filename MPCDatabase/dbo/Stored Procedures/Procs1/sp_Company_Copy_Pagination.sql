CREATE PROCEDURE dbo.sp_Company_Copy_Pagination

	(
		@OldSiteID int,
		@NewSiteID int
		
	)

AS
		--Copy Finishing Styles
			Declare FinishCursor cursor for Select ID from tbl_pagination_finishstyle where SystemSiteID=@OldSiteID
			Declare @OldID int
			Declare @NewID int
			
			open FinishCursor
			
			FETCH NEXT FROM FinishCursor into @OldID
			
			While @@FETCH_STATUS=0
				BEGIN	
				
					insert into tbl_pagination_finishstyle SELECT     Name, Head, [Trim], Foredge, Spine, @NewSiteID
					FROM         tbl_pagination_finishstyle where ID=@OldID
					
					Select @NewID=@@Identity
					
					insert into #FinishingTempTable values(@OldID,@NewID)
					
					FETCH NEXT FROM FinishCursor into @OldID
				
				END
			close FinishCursor
			Deallocate FinishCursor
			
			
			--Copy Section Break down
			insert into tbl_pagination_combinations SELECT     Pagination, Combination, Sequence, Multiplier, Sections, Description, @NewSiteID
			FROM         tbl_pagination_combinations where SystemSiteID=@OldSiteID
			
			--Copy Pagination
			insert into tbl_pagination_profile SELECT Code, Description, Priority, Pages, PaperSizeID, 
			IsNull((Select NewMethodID from #LookUpMethodsTable Where OldMethodID= tbl_pagination_profile.LookupMethodID),0), Orientation, 
			IsNull((Select New_ID from #FinishingTempTable Where Old_ID= tbl_pagination_profile.FinishStyleID),0), MinHeight, Minwidth, Maxheight, MaxWidth, MinWeight, 
            MaxWeight, MaxNoOfColors, GrainDirection, NumberUp, NoOfDifferentTypes, LockedBy, 0, FlagID, @NewSiteID
			FROM         tbl_pagination_profile where SystemSiteID=@OldSiteID
			
RETURN