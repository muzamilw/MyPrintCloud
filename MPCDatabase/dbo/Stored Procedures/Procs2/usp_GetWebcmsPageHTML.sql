--Exec [usp_GetWebcmsPageHTML] 'about.aspx'
CREATE PROCEDURE [dbo].[usp_GetWebcmsPageHTML]
	(
		@PageName varchar(255)
	)
	

AS
BEGIN
		select	PageHTML
		 from	tbl_cmsPages 
		  where PageName = @PageName
END