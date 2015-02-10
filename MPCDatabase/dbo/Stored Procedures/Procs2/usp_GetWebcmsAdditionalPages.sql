CREATE PROCEDURE [dbo].[usp_GetWebcmsAdditionalPages]
	

AS
BEGIN
		
	select	*,CategoryName 
	from	tbl_cmsPageCategory
	
	select	p.PageTitle, p.PageName, p.CategoryID 
	from	tbl_cmsPages p
	where	p.isUserDefined = 1
	order by p.PageName



END