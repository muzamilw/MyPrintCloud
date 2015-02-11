-- Exec [usp_GetWebcmsKeyWordsByPage] 'default.aspx'
CREATE PROCEDURE [dbo].[usp_GetWebcmsKeyWordsByPage] 
	(
		@PageName varchar(255)
	)
	

AS
BEGIN
		declare @PageID	numeric
		set @PageID = 0
		if(Exists (select PageID from tbl_cmsPages where PageName = @PageName))
			Begin
				Select @PageID = PageID from tbl_cmsPages where PageName = @PageName
			end
		if(@PageID <> 0 AND Exists(select PageTagID from tbl_cmsPageTags where PageID = @PageID))
			Begin
				select	t.TagID, t.TagName, t.TagSlug, t.Description
				from	tbl_cmsTags t 
					inner join tbl_cmsPageTags pt on pt.TagID = t.TagID
					inner join tbl_cmsPages p on p.PageID = pt.PageID
				where p.PageID = @PageID
			End
		else
			Begin
				 select	t.TagID, t.TagName, t.TagSlug, t.Description
				 from	tbl_cmsTags t 
						inner join tbl_cmsPageTags pt on pt.TagID = t.TagID
						inner join tbl_cmsPages p on p.PageID = pt.PageID
				 where p.PageID = (Select PageID from tbl_cmsPages 
										 where PageName = 'Default.aspx')
			End
		
		
END