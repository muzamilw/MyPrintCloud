CREATE PROCEDURE dbo.sp_DataCollection_Insert_ActivityFeedback
	(
		@ActivityID int,
		@dDateTime datetime,
		@Comments ntext,
		@UserID int
	)

AS
	INSERT INTO tbl_section_costcentres_feedback
                      (ItemSectionCostcentreID, dDateTime, Comments, SystemUserID)
VALUES     (@ActivityID,@dDateTime,@Comments,@UserID)
	RETURN