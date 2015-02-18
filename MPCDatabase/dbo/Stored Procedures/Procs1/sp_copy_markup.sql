CREATE PROCEDURE dbo.sp_copy_markup
(@OldSite int,
@SiteID int)
                  AS
		
		Declare @OldID int
		Declare @NewID int
		
		Declare MarkCursor Cursor for SELECT MarkUpID FROM  tbl_markup where SystemSiteID=@OldSite
		
		Open MarkCursor 
		FETCH NEXT from MarkCursor into @OldID
			WHILE @@FETCH_STATUS=0
			BEGIN
				
				--Adding MarkUp To New Site
				insert into tbl_markup  SELECT MarkUpName, MarkUpRate, IsFixed, @SiteID
				FROM  tbl_markup where MarkUpID=@OldID
				
				Select @NewID = @@Identity
				
				insert into #MarkUpTempTable values (@OldID,@NewID)
				
				FETCH NEXT from MarkCursor into @OldID
			END		
		Close MarkCursor
		Deallocate MarkCursor
		
return