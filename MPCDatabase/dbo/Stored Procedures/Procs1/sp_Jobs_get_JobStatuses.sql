
CREATE PROCEDURE [dbo].[sp_Jobs_get_JobStatuses]

AS
	select StatusID, HtmlColorCode from tbl_job_statuses
	RETURN