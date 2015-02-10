CREATE PROCEDURE [dbo].[usp_GetWebcmsPageDetail]-- 'default.aspx'
	(
		@PageName varchar(255)
	)
	

AS
BEGIN
		select	PageID, PageName, PageTitle, MenuTitle, [description], PageRelativePath, 
				SortOrder, [status], Meta_KeywordContent, Meta_DescriptionContent, 
				Meta_HiddenDescriptionContent, Meta_CategoryContent, Meta_RobotsContent, 
				Meta_AuthorContent, Meta_DateContent, Meta_LanguageContent, 
				Meta_RevisitAfterContent, Meta_Title, PageHTML, PageBanner
		 from	tbl_cmsPages 
		  where PageName = @PageName
END