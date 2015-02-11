CREATE PROCEDURE dbo.sp_DataCollection_Get_ActivityFeedback
	(
		@ActivityID int
	)
AS
	SELECT     tbl_section_costcentres_feedback.dDateTime, tbl_section_costcentres_feedback.Comments, tbl_section_costcentres_feedback.SystemUserID, 
	                      tbl_systemusers.FullName
	FROM         tbl_section_costcentres_feedback INNER JOIN
	                      tbl_systemusers ON tbl_section_costcentres_feedback.SystemUserID = tbl_systemusers.SystemUserID
	WHERE     (tbl_section_costcentres_feedback.ItemSectionCostcentreID = @ActivityID)
	
RETURN