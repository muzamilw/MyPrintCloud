CREATE PROCEDURE [dbo].[usp_GetPageAssignedTags]
(
	@PageID int = null
)

AS
BEGIN
		select	TagID, TagName, 0 As Assigned from webcmsTags
		where	TagID not in(Select TagID from webcmsPageTags where PageID = @PageID)
		union
		select	TagID, TagName, 1 As Assigned from webcmsTags
		where	TagID in(Select TagID from webcmsPageTags where PageID = @PageID)
		
END