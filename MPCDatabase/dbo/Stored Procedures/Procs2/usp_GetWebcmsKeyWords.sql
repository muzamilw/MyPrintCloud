-- Exec [usp_GetWebcmsKeyWords] 'About.aspx'
CREATE PROCEDURE [dbo].[usp_GetWebcmsKeyWords] 
		

AS
BEGIN
		select * from tbl_cmsTags
		where isDisplay = 1
		
		
END