CREATE PROCEDURE dbo.sp_papersizes_check_pagination
(
@PaperSizeID int)
AS
	SELECT tbl_pagination_profile.ID FROM tbl_pagination_profile WHERE ((tbl_pagination_profile.PaperSizeID = @PaperSizeID));
		RETURN